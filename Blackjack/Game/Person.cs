using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    /// <summary>
    /// Person class 
    /// </summary>
    abstract class Person : IPerson
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public bool Lost { get; set; }

        /// <summary>
        /// Person constructor
        /// </summary>
        /// <param name="name">Person's name</param>
        protected Person(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Lost = false;
        }

        /// <summary>
        /// Draw the top card from the deck
        /// </summary>
        /// <param name="deck">Deck to draw from</param>
        /// <returns>The card drawn</returns>
        public Card Draw(Deck deck)
        {
            Card card = deck.Draw();
            Hand.Add(card);
            Console.WriteLine($"{Name} receives {card} : Total {SumHand()}");
            return card;
        }

        /// <summary>
        /// Return the value of all card values in the persons hand
        /// </summary>
        /// <returns>Sum of card values in hand as integer</returns>
        public int SumHand()
        {
            // Old way that's faster
            /*
            int sum = 0;
            foreach (Card card in Hand)
                sum += card.Value;
            */
            // But wanted to make use of LINQ
            return (from card in Hand select card.Value).Sum();
        }

        /// <summary>
        /// Declare person the winner!
        /// </summary>
        public void Winner()
        {
            Console.WriteLine($"{Name} wins!");
        }

        /// <summary>
        /// Check if person has busted but has an ace
        /// </summary>
        public void CheckForAce()
        {
            // Check if person busted but has an ace
            if (SumHand() > 21)
                foreach (Card card in Hand)
                    if (card.Type == Card.CardType.Ace && card.Value != 1)
                    {
                        card.Value = 1;
                        Console.WriteLine($"{Name} swapped {card} value to 1");
                        Console.WriteLine($"{Name}'s new total {SumHand()}");
                    }
        }

        /// <summary>
        /// Bust!
        /// </summary>
        public void Bust()
        {
            Console.WriteLine($"{Name} bust!");
            Lost = true;
        }

        /// <summary>
        /// Overridden ToString
        /// </summary>
        /// <returns>Player name and sum of hand</returns>
        public override string ToString()
        {
            return ($"{Name} hand total - {SumHand()}");
        }
    }
}
