using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace War
{
    /// <summary>
    /// The Player class is used to hold the information for each individual participating in a game of War.
    /// </summary>
    class Player
    {
        /// <summary>
        /// Gets or sets a unique numerical identifier for this player.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Name of this player.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a list of Card objects that represents the cards that will be played during a round.
        /// </summary>
        public List<Card> PlayDeck { get; set; } = new List<Card>();

        /// <summary>
        /// Gets or sets a list of Card objects that represents the cards that were won in previous rounds. This list will be shuffled into the PlayDeck when it is empty.
        /// </summary>
        public List<Card> WinPile { get; set; } = new List<Card>();

        /// <summary>
        /// Gets the total number of cards this player has remaining in both the PlayDeck and WinPile.
        /// </summary>
        public int NumberOfCardsRemaining
        {
            get
            {
                return PlayDeck.Count + WinPile.Count;
            }
        }

        /// <summary>
        /// When the PlayDeck is empty ShuffleInWinPile will be used to build a new PlayDeck by shuffling the Cards in the WinPile.
        /// This will result in an empty WinPile.
        /// </summary>
        public void ShuffleInWinPile()
        {
            Random random = new Random();

            //Assign a random number to each card in the win pile.
            WinPile.ForEach(card => card.ShuffleValue = random.Next());

            //Order the WinPile by the new random number to "shuffle" the cards
            WinPile = WinPile.OrderBy(card => card.ShuffleValue).ToList();

            //Add the cards in the WinPile into the PlayDeck
            PlayDeck.AddRange(WinPile);

            //Empty the WinPile
            WinPile = new List<Card>();
        }
    }
}
