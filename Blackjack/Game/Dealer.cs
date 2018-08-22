namespace Blackjack
{
    /// <inheritdoc />
    /// <summary>
    /// Dealer class
    /// </summary>
    internal class Dealer : Person
    {
        /// <inheritdoc />
        /// <summary>
        /// Dealer constructor
        /// </summary>
        /// <param name="name">Name of the dealer</param>
        public Dealer(string name = "Dealer") : base(name)
        {
        }

        // Moved to the Person class as Draw method
        // Easier to implement by doing person.Draw(deck);
        // Instead of dealer.Deal(deck, person);
        /*
        public Card Deal(Deck deck, Person person)
        {
            Card card = deck.Draw();
            person.Hand.Add(card);
            Console.WriteLine($"{person.Name} recieves {card.ToString()} : {person.SumHand()}");
            return card;
        }
        */
    }
}
