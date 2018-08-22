namespace Blackjack
{
    /// <summary>
    /// Card Class
    /// Was using a struct to reduce heap memory usage for cards
    /// But cannot reassign the value of aces then
    /// </summary>
    public class Card
    {
        public enum CardType
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        };

        public enum SuitType
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        };

        public CardType Type { get; set; }
        public SuitType Suit { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// Card constructor
        /// </summary>
        /// <param name="ct">Type</param>
        /// <param name="st">Suit</param>
        /// <param name="value">Value</param>
        public Card(CardType ct, SuitType st, int value)
        {
            Type = ct;
            Suit = st;
            Value = value;
        }

        /// <summary>
        /// Override + operator to add card values
        /// </summary>
        /// <param name="c1">Card 1</param>
        /// <param name="c2">Card 2</param>
        /// <returns>Sum of cards value</returns>
        public static int operator +(Card c1, Card c2)
        {
            return c1.Value + c2.Value;
        }

        /// <summary>
        /// Overridden ToString
        /// </summary>
        /// <returns>Type and Suit of card</returns>
        public override string ToString()
        {
            return $"{Type} of {Suit}";
        }

    }
}
