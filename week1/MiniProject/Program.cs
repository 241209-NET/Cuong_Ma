using System;
using System.Collections.Generic;

namespace MiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CSharp BlackJack!");
            BlackJackGame game = new BlackJackGame();
            game.Start();
            game.Choice();
        }

        public class Card
        {
            public string Value { get; set; }

            public Card(string value)
            {
                Value = value;
            }
        }

        public class Deck
        {
            private List<Card> cards = [];

            public void Generate()
            {
                string[] values =
                {
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
                };

                foreach (string value in values)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cards.Add(new Card(value));
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
                return null;
            }
        }

        public class BlackJackGame
        {
            List<Card> playerHand = [];
            List<Card> dealerHand = [];
            Deck deck = new Deck();

            public void Start()
            {
                System.Console.WriteLine("Game Starts!");

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
                System.Console.WriteLine("Please Choose: 'Hit' or 'Stand'");

                string? playerChoice = Console.ReadLine()?.ToLower();
                if (playerChoice != "hit" && playerChoice != "stand")
                {
                    System.Console.WriteLine("That is not a valid input, please choose again.");
                    Choice();
                }
                if (playerChoice == "hit")
                {
                    playerHand.Add(deck.Draw());
                    System.Console.WriteLine($"You drew: {playerHand[playerHand.Count - 1].Value}");
                    if (LoseCheck())
                    {
                        System.Console.WriteLine(PlayerHandCheck());
                        System.Console.WriteLine("You lose.");
                        return;
                    }
                    else
                    {
                        System.Console.WriteLine(PlayerHandCheck());
                        Choice();
                    }
                }
                if (playerChoice == "stand")
                {
                    if (WinCheck())
                    {
                        System.Console.WriteLine("You Win!");
                    }
                    else
                    {
                        System.Console.WriteLine("You Lose.");
                    }
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
                return false;
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

            public string PlayerHandCheck()
            {
                string start = "You currently have: ";
                string cards = "";
                foreach (Card card in playerHand)
                {
                    cards += card.Value + " ";
                }
                return start + cards.Trim();
            }
        }
    }
}
