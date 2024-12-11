using System;
using System.Collections.Generic;

namespace MiniProject
{
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
}
