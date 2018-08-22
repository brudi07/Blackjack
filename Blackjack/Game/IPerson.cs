using System.Collections.Generic;

namespace Blackjack
{
    /// <summary>
    /// IPerson interface
    /// </summary>
    public interface IPerson
    {
        // Properties
        string Name { get; set; }
        List<Card> Hand { get; set; }

        // Methods
        Card Draw(Deck deck);
        int SumHand();
    }
}