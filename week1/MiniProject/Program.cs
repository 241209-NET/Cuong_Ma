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
            public void Start()
            {
                System.Console.WriteLine("Game Starts!");

                Deck deck = new Deck();
                deck.Generate();

                List<Card> playerHand = [];
                List<Card> dealerHand = [];

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

            public void Choice() { }
            public bool Check() { }
        }
    }
}
