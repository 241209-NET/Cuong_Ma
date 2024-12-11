using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to CSharp BlackJack!");
            GameStart();
        }

        public static void GameStart()
        {
            BlackJackGame game = new BlackJackGame();
            game.Start();
            game.Choice();
        }

        public class Card
        {
            public required string Value { get; set; }
        }

        public class Deck
        {
            private List<Card> cards = [];

            public void Generate()
            {
                string[] values =
                [
                    "Ace",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9",
                    "10",
                    "Jack",
                    "Queen",
                    "King",
                ];

                foreach (string value in values)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cards.Add(new Card { Value = value });
                    }
                }
            }

            public Card Draw()
            {
                Random rng = new Random();
                if (cards.Count > 0)
                {
                    int randomIndex = rng.Next(cards.Count);
                    Card card = cards[randomIndex];
                    cards.RemoveAt(randomIndex);
                    return card;
                }
                else
                {
                    return null!;
                }
            }
        }

        public class BlackJackGame
        {
            List<Card> playerHand = [];
            List<Card> dealerHand = [];
            Deck deck = new();

            public void Start()
            {
                System.Console.WriteLine("\nGame Starts!\n");

                deck.Generate();

                playerHand.Add(deck.Draw());
                playerHand.Add(deck.Draw());
                dealerHand.Add(deck.Draw());
                dealerHand.Add(deck.Draw());

                System.Console.WriteLine(
                    $"You drew: {playerHand[0].Value} and {playerHand[1].Value}"
                );
                System.Console.WriteLine(
                    $"Dealer drew: {dealerHand[0].Value} and {dealerHand[1].Value}"
                );
            }

            public void Choice()
            {
                System.Console.WriteLine("\nPlease Choose: 'Hit', 'Stand', or 'Quit'");

                string? playerChoice = Console.ReadLine()?.ToLower();
                if (playerChoice != "hit" && playerChoice != "stand" && playerChoice != "quit")
                {
                    System.Console.WriteLine("That is not a valid input, please choose again.");
                    Choice();
                }
                else if (playerChoice == "hit")
                {
                    playerHand.Add(deck.Draw());
                    System.Console.WriteLine(
                        $"{Environment.NewLine}You drew: {playerHand[playerHand.Count - 1].Value}"
                    );
                    if (LoseCheck())
                    {
                        System.Console.WriteLine(HandCheck("You"));
                        System.Console.WriteLine("\nYou lose.\n");
                        Restart();
                    }
                    else
                    {
                        System.Console.WriteLine(HandCheck("You"));
                        DealerTurn();
                        Choice();
                    }
                }
                else if (playerChoice == "stand")
                {
                    DealerTurn();
                    if (TieCheck())
                    {
                        System.Console.WriteLine("\nYou Tie.\n");
                        Restart();
                    }
                    else if (WinCheck())
                    {
                        System.Console.WriteLine("\nYou Win!\n");
                        Restart();
                    }
                    else
                    {
                        System.Console.WriteLine("\nYou Lose.\n");
                        Restart();
                    }
                }
                else if (playerChoice == "quit")
                {
                    System.Console.WriteLine("\nGame Over.\n");
                    Environment.Exit(0);
                }
            }

            public int HandValueCheck(List<Card> hand)
            {
                int handValue = 0;
                foreach (Card card in hand)
                {
                    if (int.TryParse(card.Value, out int numericValue))
                    {
                        handValue += numericValue;
                    }
                    else if (card.Value == "Jack" || card.Value == "Queen" || card.Value == "King")
                    {
                        handValue += 10;
                    }
                    else if (card.Value == "Ace")
                    {
                        handValue += 11;
                    }
                }
                return handValue;
            }

            public bool LoseCheck()
            {
                int playerHandValue = HandValueCheck(playerHand);
                if (playerHandValue > 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool TieCheck()
            {
                int playerHandValue = HandValueCheck(playerHand);
                int dealerHandValue = HandValueCheck(dealerHand);
                if (playerHandValue == dealerHandValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool WinCheck()
            {
                if (
                    (
                        HandValueCheck(playerHand) <= 21
                        && HandValueCheck(playerHand) > HandValueCheck(dealerHand)
                    )
                    || HandValueCheck(dealerHand) > 21
                )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool DealerLoseCheck()
            {
                if (HandValueCheck(dealerHand) > 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public string HandCheck(string person)
            {
                string start = person + " currently hold: ";
                string cards = "";
                if (person == "You")
                {
                    foreach (Card card in playerHand)
                    {
                        cards += card.Value + " ";
                    }
                }
                else
                {
                    foreach (Card card in dealerHand)
                    {
                        cards += card.Value + " ";
                    }
                }
                return start + cards.Trim();
            }

            public void DealerTurn()
            {
                if (
                    HandValueCheck(dealerHand) < 17
                    || HandValueCheck(dealerHand) < HandValueCheck(playerHand)
                )
                {
                    dealerHand.Add(deck.Draw());
                    System.Console.WriteLine("\nThe Dealer chooses to Hit");
                    System.Console.WriteLine(HandCheck("The Dealer"));
                    if (DealerLoseCheck())
                    {
                        System.Console.WriteLine("\nYou Win!\n");
                        Restart();
                    }
                }
                else
                {
                    System.Console.WriteLine("\nThe Dealer chooses to Stand");
                    System.Console.WriteLine(HandCheck("The Dealer"));
                }
            }

            public bool StartAgain()
            {
                System.Console.WriteLine(
                    "\nWould you like to play again? Please Choose: 'Yes' or 'Quit'"
                );

                string? playerChoice = Console.ReadLine()?.ToLower();

                if (playerChoice != "yes" && playerChoice != "quit")
                {
                    System.Console.WriteLine("That is not a valid input, please choose again.");
                    StartAgain();
                }
                else if (playerChoice == "yes")
                {
                    return true;
                }
                else if (playerChoice == "quit")
                {
                    return false;
                }
                return false;
            }

            public void Restart()
            {
                if (StartAgain())
                {
                    GameStart();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
