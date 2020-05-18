using System;
using System.Collections.Generic;
using System.Linq;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To War!");

            bool playGame = true;

            while (playGame)
            {
                //Set up the GameInfo object for this game.
                GameInfo gameInfo = SetUpGame();

                Console.WriteLine("*******");
                Console.WriteLine("*Start*");
                Console.WriteLine("*******");

                //Check if the game is complete to see if we need to have another round.
                while (!gameInfo.GameComplete)
                {
                    //Prompt user for key press to trigger next round.
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    PlayRound(gameInfo);

                    //Increment the number of rounds played
                    gameInfo.NumberOfRounds++;
                    Console.WriteLine();
                }

                //Display the winner of the game and how many rounds it took for the game to end.
                Console.WriteLine($"{gameInfo.RemainingPlayers.FirstOrDefault().Name} Wins!");
                Console.WriteLine($"Number of rounds played: {gameInfo.NumberOfRounds}");

                //Ask the user if they would like to play again.
                Console.WriteLine("Would you like to start another game? Enter Y to play another game, enter any other value to exit");
                string playAgain = Console.ReadLine();
                playGame = playAgain.Equals("Y", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Prompts the user for the number of players and the name of those players names. It will then deal cards to each of those players.
        /// </summary>
        /// <returns>A GameInfo object that will be used to play a new game of War.</returns>
        public static GameInfo SetUpGame()
        {
            //Create a new GameInfo object to be populated with the required set up information.
            GameInfo gameInfo = new GameInfo();
            int numberOfPlayers = 0;

            //Prompt user for the number of players
            Console.WriteLine("Please Enter how many players will be playing. There must be at least 2 players and at most 4 players.");
            while (numberOfPlayers == 0)
            {
                string playersInput = Console.ReadLine();

                bool validInput = int.TryParse(playersInput, out numberOfPlayers);

                //Limit range of number of players to 2-4.
                validInput = numberOfPlayers >= 2 && numberOfPlayers < 5;

                if (!validInput)
                {
                    //The value entered by the user was either not an integer or was not in the proper range prompt for the user to correct.
                    numberOfPlayers = 0;
                    Console.WriteLine($"{playersInput} is not a number between 2 and 4 please enter a number between 2 and 4");
                }
            }

            //Loop to create Player objects until we create the number of players specified by the user
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Player playerToAdd = new Player();

                //Set unique Player ID.
                playerToAdd.ID = i;

                //Prompt user to enter a name until they enter at least one character.
                while (playerToAdd.Name.Length == 0)
                {
                    Console.WriteLine($"Please Enter the name of Player number {i}:");
                    playerToAdd.Name = Console.ReadLine();
                }

                //Add the new player to the GameInfo object.
                gameInfo.Players.Add(playerToAdd);
            }

            if (gameInfo.NumberOfPlayers == 3)
            {
                //In a game with 3 players the cards cannot be divided evenly inform the user that one player will have an extra card.
                Console.WriteLine($"Since the deck cannot be divided evenly into 3 piles, {gameInfo.Players.LastOrDefault().Name} will have 1 extra card at the start.");
            }

            bool sufficientShuffle = false;

            while (!sufficientShuffle)
            {
                //Deal cards to the created players.
                ShuffleAndDeal(gameInfo);

                //Check if the cards are shuffled in a way the would cause the game to never end.
                int[] cardValues = gameInfo.Players.FirstOrDefault().PlayDeck.Select(card => card.CardValue).ToArray();
                List<List<Card>> playerDecks = gameInfo.Players.Select(player => player.PlayDeck).ToList();
                for (int i = 0; i < cardValues.Count(); i++)
                {
                    int value = cardValues[i];

                    //If all players decks have the same card value at every index then the game will never end and we will need to shuffle again.
                    sufficientShuffle = !playerDecks.All(deck => deck.ElementAt(i).CardValue == value);
                }
            }

            return gameInfo;
        }

        /// <summary>
        /// Create a standard 52 card deck, shuffle that deck and deal the cards to players in the passed in GameInfo object.
        /// </summary>
        /// <param name="gameInfo"></param>
        public static void ShuffleAndDeal(GameInfo gameInfo)
        {
            List<Card> deck = new List<Card>();
            Random random = new Random();

            //Create the 52 card deck.
            for (int i = 0; i < 4; i++)
            {
                //Loop through the 4 suits and create the 13 cards in each suit.
                string suit = string.Empty;

                //Determin the suit name using the index of the loop
                switch (i)
                {
                    case 0:
                        suit = "Hearts";
                        break;
                    case 1:
                        suit = "Diamonds";
                        break;
                    case 2:
                        suit = "Spades";
                        break;
                    case 3:
                        suit = "Clubs";
                        break;
                }

                for (int j = 0; j < 13; j++)
                {
                    Card card = new Card();

                    //Assign the index of the inner loop as this cards value
                    card.CardValue = j;

                    //Get a random number to be used to shuffle this card into the deck
                    card.ShuffleValue = random.Next();

                    // Determine the "face" name of the card using the index of the inner loop
                    switch (j)
                    {
                        case 0:
                            card.CardName = $"2 of {suit}";
                            break;
                        case 1:
                            card.CardName = $"3 of {suit}";
                            break;
                        case 2:
                            card.CardName = $"4 of {suit}";
                            break;
                        case 3:
                            card.CardName = $"5 of {suit}";
                            break;
                        case 4:
                            card.CardName = $"6 of {suit}";
                            break;
                        case 5:
                            card.CardName = $"7 of {suit}";
                            break;
                        case 6:
                            card.CardName = $"8 of {suit}";
                            break;
                        case 7:
                            card.CardName = $"9 of {suit}";
                            break;
                        case 8:
                            card.CardName = $"10 of {suit}";
                            break;
                        case 9:
                            card.CardName = $"Jack of {suit}";
                            break;
                        case 10:
                            card.CardName = $"Queen of {suit}";
                            break;
                        case 11:
                            card.CardName = $"King of {suit}";
                            break;
                        case 12:
                            card.CardName = $"Ace of {suit}";
                            break;
                    }
                    deck.Add(card);
                }
            }

            //Shuffle the deck by ordering the cards by the randomly generated numbers.
            deck = deck.OrderBy(card => card.ShuffleValue).ToList();

            //Determine how many cards need to be in each deck
            int cardsPerDeck = 52 / gameInfo.NumberOfPlayers;

            //loop through the players in the passed in GameInfo object.
            for (int i = 0; i < gameInfo.NumberOfPlayers; i++)
            {
                //Determine how many cards from the deck each player needs to take. This will only be different than cardsPerDeck if it is the last player in a 3 player game.
                int cardsToTake = i == 2 && gameInfo.NumberOfPlayers == 3 ? cardsPerDeck + 1 : cardsPerDeck;

                //Assign cards to this players PlayDeck by taking the correct number of cards from where the last player stopped taking cards.
                gameInfo.Players[i].PlayDeck = deck.Skip(cardsPerDeck * i).Take(cardsToTake).ToList();
            }
        }

        /// <summary>
        /// Evaluate the results of a single round of a game and update the player objects in the passed in GameInfo object.
        /// </summary>
        /// <param name="gameInfo"></param>
        public static void PlayRound(GameInfo gameInfo)
        {
            Console.WriteLine("************************************");

            //Empty round info from previous rounds and create new Round.
            gameInfo.Round = new RoundInfo();

            //Get the next card from each player that still has cards remaining.
            foreach (Player player in gameInfo.RemainingPlayers)
            {
                if (player.PlayDeck.Count == 0)
                {
                    //If there are no more cards in the players PlayDeck the WinPile needs to create the new PlayDeck before we can determine the card they play.
                    player.ShuffleInWinPile();
                }

                //Represents the top card of this players PlayDeck
                KeyValuePair<int, Card> playerCard = new KeyValuePair<int, Card>(player.ID, player.PlayDeck.FirstOrDefault());

                //Add this card to the CardsPlayed and CardsToWin lists.
                gameInfo.Round.CardsToWin.Add(playerCard);
                gameInfo.Round.CardsPlayed.Add(playerCard);
            }

            foreach (KeyValuePair<int, Card> cardPlayed in gameInfo.Round.CardsPlayed)
            {
                //Show the user what card each player played.
                Console.WriteLine($"{gameInfo.Players.FirstOrDefault(player => player.ID == cardPlayed.Key).Name}'s Card: {cardPlayed.Value.CardName}");
            }

            //Check for a winner of the round.
            CheckForWinner(gameInfo);

            //Update Player decks base on round results.
            CollectWinnings(gameInfo);
            Console.WriteLine("************************************");
        }

        /// <summary>
        /// Determine the winner of the round or if a War needs to occur.
        /// </summary>
        /// <param name="gameInfo"></param>
        public static void CheckForWinner(GameInfo gameInfo)
        {
            //The highest card value amoung the cards played this round.
            int winningValue = gameInfo.Round.CardsPlayed.Max(card => card.Value.CardValue);

            //If there is more than one card with the highest value in cards played than a War needs to occur.
            bool triggerWar = gameInfo.Round.CardsPlayed.Where(card => card.Value.CardValue == winningValue).Count() > 1;

            if (triggerWar)
            {
                //Determine what Players need to participate in the War.
                gameInfo.Round.WarParticipants = gameInfo.Players.Where(player => gameInfo.Round.CardsPlayed.Any(card => card.Value.CardValue == winningValue && card.Key == player.ID)).ToList();

                //Execute the War.
                War(gameInfo);
            }
            else
            {
                //No War is required set the WinningPlayer for the round.
                gameInfo.Round.WinningPlayer = gameInfo.Players.FirstOrDefault(player => player.ID == gameInfo.Round.CardsPlayed.FirstOrDefault(card => card.Value.CardValue == winningValue).Key);
            }
        }

        /// <summary>
        /// Execute a War and recursively check for a winner until a war no longer needs to occur.
        /// </summary>
        /// <param name="gameInfo"></param>
        public static void War(GameInfo gameInfo)
        {
            Console.WriteLine("******");
            Console.WriteLine("*WAR!*");
            Console.WriteLine("******");

            //Empty out the cards played, the cards played previously do not effect the outcome of the War.
            gameInfo.Round.CardsPlayed = new List<KeyValuePair<int, Card>>();

            //For each player participating in the war add the proper amount of cards to the win pile and determine their new played card.
            foreach (Player participant in gameInfo.Round.WarParticipants)
            {
                /*Determine the index of the PlayDeck that this instance of War starts at.
                *The starting index is equal to:
                * (The number of wars that have previously occured this round) X (The number of cards needed for each war) + (The card played at the beginning of the round)
                */
                int startingCardIndex = gameInfo.Round.NumberOfWars * 4 + 1;

                //Increment the number of wars that have occured this round.
                gameInfo.Round.NumberOfWars++;

                //Each player needs to add 4 cards to the win pile (if they can), the last card will be added as the new Card Played for the round.
                for (int i = 0; i < 4; i++)
                {
                    if (gameInfo.Round.CardsPlayed.Any(card => card.Key == participant.ID))
                    {
                        //This player is out of cards and already played their card for the war they cannot add any more cards.
                        continue;
                    }

                    //Find the card at the current index of the players PlayDeck
                    Card warCard = participant.PlayDeck.ElementAtOrDefault(startingCardIndex + i);

                    if (warCard == null)
                    {
                        //If the card at the proper index does not exist shuffle in the win pile and check again.
                        participant.ShuffleInWinPile();
                        warCard = participant.PlayDeck.ElementAtOrDefault(startingCardIndex + i);
                    }

                    if (warCard == null)
                    {
                        //If we still cannot find the card after shuffling the win pile, this player does not have another card so the last card in thier deck will be used as their play card in the war
                        gameInfo.Round.CardsPlayed.Add(new KeyValuePair<int, Card>(participant.ID, participant.PlayDeck.LastOrDefault()));
                    }
                    else if (i == 3)
                    {
                        //This is the 4th card of the war it needs to be played face up.
                        gameInfo.Round.CardsPlayed.Add(new KeyValuePair<int, Card>(participant.ID, participant.PlayDeck.ElementAt(startingCardIndex + i)));
                        gameInfo.Round.CardsToWin.Add(new KeyValuePair<int, Card>(participant.ID, participant.PlayDeck.ElementAt(startingCardIndex + i)));
                    }
                    else
                    {
                        //This is a card placed face down add it to the cards to be taken by the winner.
                        gameInfo.Round.CardsToWin.Add(new KeyValuePair<int, Card>(participant.ID, participant.PlayDeck.ElementAt(startingCardIndex + i)));
                    }
                }
            }

            foreach (KeyValuePair<int, Card> cardPlayed in gameInfo.Round.CardsPlayed)
            {
                //Display the card played by each war participant.
                Console.WriteLine($"{gameInfo.Players.FirstOrDefault(player => player.ID == cardPlayed.Key).Name}'s Card: {cardPlayed.Value.CardName}");
            }

            //Recursivly check for a winner of the round.
            CheckForWinner(gameInfo);
        }

        /// <summary>
        /// Remove played cards from players PlayDecks and add the cards to the WinPile of the round winner.
        /// </summary>
        /// <param name="gameInfo"></param>
        public static void CollectWinnings(GameInfo gameInfo)
        {
            //Show the user who won the round and what cards they recieve.
            Console.WriteLine("************************************");
            Console.WriteLine($"{gameInfo.Round.WinningPlayer.Name} won the round!");
            Console.WriteLine($"{gameInfo.Round.WinningPlayer.Name} gets the following cards:");

            foreach (KeyValuePair<int, Card> winnings in gameInfo.Round.CardsToWin)
            {
                Console.WriteLine($"   {winnings.Value.CardName}");
            }

            //Add the Card objects from the CardsToWin of the round to the winning players WinPile.
            gameInfo.Round.WinningPlayer.WinPile.AddRange(gameInfo.Round.CardsToWin.Select(card => card.Value));

            //Detremine what players participated in this round.
            int[] playerIdsInRound = gameInfo.Round.CardsToWin.Select(card => card.Key).Distinct().ToArray();

            //Foreach player that participated in this round remove the proper cards from thier PlayDeck.
            foreach (int playerID in playerIdsInRound)
            {
                //Get the player associated with this id.
                Player player = gameInfo.Players.FirstOrDefault(player => player.ID == playerID);

                //Get the cards in the CardsToWin that are currently in this players PlayDeck.
                List<Card> cardsToRemove = gameInfo.Round.CardsToWin.Where(card => card.Key == playerID).Select(card => card.Value).ToList();

                //Remove the cards from the players PlayDeck.
                foreach (Card card in cardsToRemove)
                {
                    player.PlayDeck.Remove(card);
                }

                if (player.NumberOfCardsRemaining == 0)
                {
                    //If this player has no cards remaining show the user that they have been eliminated.
                    Console.WriteLine();
                    Console.WriteLine("************************************");
                    Console.WriteLine($"{player.Name} has been eliminated!");
                    Console.WriteLine("************************************");
                    Console.WriteLine();
                }
            }
        }
    }
}
