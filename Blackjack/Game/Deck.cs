using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    /// <summary>
    /// Class for a 52 card deck 
    /// </summary>
    public class Deck
    {
        private List<Card> _deck = new List<Card>();
        private List<Card> _discard = new List<Card>();
        private readonly Random _rng = new Random();

        /// <summary>
        /// Deck constructor, add each card to the deck property
        /// </summary>
        public Deck()
        {
            var typeList = Enum.GetValues(typeof(Card.CardType));
            var suitList = Enum.GetValues(typeof(Card.SuitType));

            foreach (Card.CardType type in typeList)
                foreach (Card.SuitType suit in suitList)
                    _deck.Add(new Card(type, suit, GetValue(type.ToString())));

            Shuffle();

            //foreach (Card card in deck)
                //Console.WriteLine(card.ToString());
            //Console.WriteLine(deck.Count);
        }

        /// <summary>
        /// Draw the top card from the deck
        /// Handles the deck aspect where Person.Draw() is updating the Person's hand
        /// </summary>
        /// <returns>The drawn card</returns>
        public Card Draw()
        {
            // If the deck is out of cards, move the discard to the deck
            // Shuffle the deck, and reset the discard pile
            if (_deck.Count == 0)
            {
                _deck = _discard;
                Shuffle();
                _discard = new List<Card>();
                Console.WriteLine($"Shuffled deck");
            }
            // Get the top card of the deck
            Card card = _deck[0];
            // Remove it from the deck
            _deck.Remove(card);
            return card;
        }

        /// <summary>
        /// Discard the card to the discard pile
        /// </summary>
        /// <param name="card">Card to discard</param>
        public void Discard(Card card)
        {
            // Return the value of an ace to 11 before discarding
            if (card.Type == Card.CardType.Ace)
            {
                card.Value = 11;
            }
            _discard.Add(card);
        }

        /// <summary>
        /// Shuffle the deck 
        /// </summary>
        public void Shuffle()
        {
            _deck = _deck.OrderBy(guid => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Set the value of the card
        /// </summary>
        /// <param name="type">Card type</param>
        /// <returns>Card value</returns>
        private static int GetValue(string type)
        {
            int value = 0;
            if (type == "Two")
                value = 2;
            else if (type == "Three")
                value = 3;
            else if (type == "Four")
                value = 4;
            else if (type == "Five")
                value = 5;
            else if (type == "Six")
                value = 6;
            else if (type == "Seven")
                value = 7;
            else if (type == "Eight")
                value = 8;
            else if (type == "Nine")
                value = 9;
            else if (type == "Ten" || type == "Jack" || type == "Queen" || type == "King")
                value = 10;
            else if (type == "Ace") value = 11;

            return value;
        }
    }
}
