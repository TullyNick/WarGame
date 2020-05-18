using System;
using System.Collections.Generic;
using System.Text;

namespace War
{
    /// <summary>
    /// The RoundInfo class is used to hold all of the information needed to process the results of a single round of War.
    /// </summary>
    class RoundInfo
    {
        /// <summary>
        /// Gets or sets a list of key value pairs that represents the cards that will be taken by the winner of the round and the Id of the player that currently owns the card.
        /// </summary>
        public List<KeyValuePair<int, Card>> CardsToWin { get; set; } = new List<KeyValuePair<int, Card>>();

        /// <summary>
        /// Gets or sets a list of key value pairs that represents the cards to be compared to check for the round winner and the player that "played" the card.
        /// </summary>
        public List<KeyValuePair<int, Card>> CardsPlayed { get; set; } = new List<KeyValuePair<int, Card>>();

        /// <summary>
        /// Gets or sets a list of players that are currently participating in a War.
        /// </summary>
        public List<Player> WarParticipants { get; set; } = new List<Player>();

        /// <summary>
        /// Gets or sets the player that won the round and will be taking the cards from the CardsToWin list.
        /// </summary>
        public Player WinningPlayer { get; set; }

        /// <summary>
        /// Gets or sets the number of times a War has occurred this round.
        /// </summary>
        public int NumberOfWars { get; set; } = 0;
    }
}
