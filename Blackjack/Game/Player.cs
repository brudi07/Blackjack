using System;

namespace Blackjack
{
    /// <inheritdoc />
    /// <summary>
    /// Player class
    /// </summary>
    internal class Player : Person
    {
        public int GamesPlayed { get; set; } = 0;
        public int GamesWon { get; set; } = 0;

        /// <inheritdoc />
        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="name">Player name</param>
        public Player(string name) : base(name)
        {
        }

        /// <summary>
        /// Console output for player wins, increment games won count
        /// </summary>
        public new void Winner()
        {
            base.Winner();
            GamesWon++;
            Console.WriteLine($"{Name} has {GamesWon} win(s) out of {GamesPlayed} games played. ({(100 * GamesWon / GamesPlayed)}%)");
        }
    }
}
