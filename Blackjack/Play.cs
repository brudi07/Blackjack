using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Blackjack
{
    internal class Play
    {

        public static void PlayGame()
        {
            // Instantiate using optional argument constructor
            Dealer dealer = new Dealer();
            // Instantiate with named argument for player
            Player player = new Player(name : "Player");
            // Instantiate the deck
            Deck deck = new Deck();

            // Get player name
            Console.Write("Enter your name: ");
            player.Name = Console.ReadLine();

            bool playing = true;
            while (playing)
            {
                // Update games played and reset lost booleans
                ConsoleKey key;
                player.GamesPlayed++;
                bool playerLost = false;
                bool dealerLost = false;

                // Deal initial cards
                player.Draw(deck);
                dealer.Draw(deck);
                player.Draw(deck);
                dealer.Draw(deck);

                Console.WriteLine(player.ToString());
                Console.WriteLine(dealer.ToString());

                // Allow player the choice to hit or stay
                bool playerChoice = true;
                while (playerChoice)
                {
                    Console.WriteLine("1: Hit \n2: Stay");
                    // Console.ReadKey(true) hides the key press in console
                    key = Console.ReadKey(true).Key;
                    // Player choice
                    if (key == ConsoleKey.D1)
                        player.Draw(deck);
                    else if (key == ConsoleKey.D2)
                        playerChoice = false;
                    // Check if player busted but has an ace
                    if (player.SumHand() > 21)
                        foreach (Card card in player.Hand)
                            if (card.Type == Card.CardType.Ace)
                            {
                                card.Value = 1;
                                Console.WriteLine($"{player.Name} swapped {card} value to 1");
                                Console.WriteLine($"{player.Name}'s new total {player.SumHand()}");
                            }
                    // Check if player busts, inverted to reduce nesting
                    if (player.SumHand() <= 21) continue;
                    player.Bust();
                    playerLost = true;
                    playerChoice = false;
                }

                // Dealer draws until hand value < 17
                if (!playerLost)
                {
                    while (dealer.SumHand() < 17)
                    {
                        dealer.Draw(deck);
                        // Check if dealer busted but has an ace
                        if (dealer.SumHand() > 21)
                            foreach (Card card in dealer.Hand)
                                if (card.Type == Card.CardType.Ace)
                                {
                                    card.Value = 1;
                                    Console.WriteLine($"Dealer swapped {card} value to 1");
                                    Console.WriteLine($"Dealer's new total {dealer.SumHand()}");
                                }
                        // Check if dealer busts
                        if (dealer.SumHand() <= 21) continue;
                        dealer.Bust();
                        dealerLost = true;
                    }
                }

                Console.WriteLine(player.ToString());
                Console.WriteLine(dealer.ToString());

                // Win/Lose Conditions
                if (playerLost)
                    dealer.Winner();
                else if (dealerLost)
                    player.Winner();
                else if (player.SumHand() == dealer.SumHand())
                    Console.WriteLine("Tie.");
                else if (player.SumHand() > dealer.SumHand())
                    player.Winner();
                else
                    dealer.Winner();

                // Discard hands
                foreach (Card card in player.Hand)
                    deck.Discard(card);
                foreach (Card card in dealer.Hand)
                    deck.Discard(card);
                player.Hand = new List<Card>();
                dealer.Hand = new List<Card>();

                // Play again or exit
                Console.WriteLine("Press any key to play again or press 0 to exit. \n");
                key = Console.ReadKey(true).Key;
                Console.Clear();
                if (key == ConsoleKey.D0)
                    playing = false;
            }
        }

    }
}
