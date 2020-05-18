using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace War
{
    /// <summary>
    /// The GameInfo class is used to hold all of the information to play a single game of War.
    /// </summary>
    class GameInfo
    {
        /// <summary>
        /// Gets or sets the list of players participating in this game of War.
        /// </summary>
        public List<Player> Players { get; set; } = new List<Player>();

        /// <summary>
        /// Gets the number of players participating in this game of War.
        /// </summary>
        public int NumberOfPlayers
        {
            get
            {
                return Players.Count;
            }
        }

        /// <summary>
        /// Gets the participating players that have not been eliminated yet.
        /// </summary>
        public List<Player> RemainingPlayers
        {
            get
            {
                return Players.Where(player => player.NumberOfCardsRemaining > 0).ToList();
            }
        }

        /// <summary>
        /// Gets the number of participating players that have not been eliminated yet.
        /// </summary>
        public int NumberOfPlayersRemaining
        {
            get
            {
                return RemainingPlayers.Count;
            }
        }

        /// <summary>
        /// Gets or sets the number of rounds that have been played in this game of War.
        /// </summary>
        public int NumberOfRounds { get; set; }

        /// <summary>
        /// Gets or sets the player that won this game of War.
        /// </summary>
        public Player Winner { get; set; }

        /// <summary>
        /// Gets a value indicating if more rounds need to be played by checking if there is only one player that has not been eliminated.
        /// </summary>
        public bool GameComplete
        {
            get
            {
                return NumberOfPlayersRemaining == 1;
            }
        }

        /// <summary>
        /// Gets or sets the information needed for the current round of the Game.
        /// </summary>
        public RoundInfo Round { get; set; }
    }
}
