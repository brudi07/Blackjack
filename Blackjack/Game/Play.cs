using System;
using System.Collections.Generic;

namespace Blackjack
{
    internal class Play
    {

        // Create a delegate to write messages
        private delegate void Delegate(string message);
        // Use lambda expression to create an anonymous function to instantiate Message
        private static readonly Delegate Message = (m => Console.WriteLine((m)));

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
                player.Lost = false;
                dealer.Lost = false;

                // Deal initial cards
                player.Draw(deck);
                dealer.Draw(deck);
                player.Draw(deck);
                dealer.Draw(deck);

                Message(player.ToString());
                Message(dealer.ToString());

                // Allow player the choice to hit or stay
                bool playerChoice = true;
                while (playerChoice)
                {
                    Message("1: Hit \n2: Stay");
                    // Console.ReadKey(true) hides the key press in console
                    key = Console.ReadKey(true).Key;
                    // Player choice
                    if (key == ConsoleKey.D1)
                        player.Draw(deck);
                    else if (key == ConsoleKey.D2)
                        playerChoice = false;
                    // Check if player busted but has an ace
                    player.CheckForAce();
                    // Check if player busts, inverted to reduce nesting
                    if (player.SumHand() <= 21) continue;
                    player.Bust();
                    playerChoice = false;
                }

                // Dealer draws until hand value < 17
                // No need to do this is player busts
                if (!player.Lost)
                {
                    while (dealer.SumHand() < 17)
                    {
                        dealer.Draw(deck);
                        // Check if dealer busted but has an ace
                        dealer.CheckForAce();
                        // Check if dealer busts
                        if (dealer.SumHand() <= 21) continue;
                        dealer.Bust();
                    }
                }

                Message(player.ToString());
                Message(dealer.ToString());

                // Win/Lose Conditions
                if (player.Lost)
                    dealer.Winner();
                else if (dealer.Lost)
                    player.Winner();
                else if (player.SumHand() == dealer.SumHand())
                    Message("Tie.");
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
                Message("Press any key to play again or press 0 to exit. \n");
                key = Console.ReadKey(true).Key;
                Console.Clear();
                if (key == ConsoleKey.D0)
                    playing = false;
            }
        }

    }
}
