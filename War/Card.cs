using System;
using System.Collections.Generic;
using System.Text;

namespace War
{
    /// <summary>
    /// The Card class is used to represent each individual card in a deck of cards.
    /// </summary>
    class Card
    {
        /// <summary>
        /// Gets or sets the name of the card (Ace of Spades, 2 of Clubs, etc...).
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets a numerical value that will be used to compare the "strength" of the card against the other cards.
        /// </summary>
        public int CardValue { get; set; }

        /// <summary>
        /// Gets or sets a numerical value that will be used to order cards in a list. This will be a randomly generated number to shuffle a list of Cards.
        /// </summary>
        public int ShuffleValue { get; set; }
    }
}
