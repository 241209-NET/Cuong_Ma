using System;
using System.Collections.Generic;

namespace MiniProject
{
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

            System.Console.WriteLine($"You drew: {playerHand[0].Value} and {playerHand[1].Value}");
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
                    System.Console.WriteLine(HandCheck("You", playerHand));
                    System.Console.WriteLine("\nYou lose.\n");
                    Restart();
                }
                else
                {
                    System.Console.WriteLine(HandCheck("You", playerHand));
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
            int aceCount = 0;

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
                    aceCount++;
                }
            }

            for (int i = 0; i < aceCount; i++)
            {
                if (handValue + 11 > 21)
                {
                    handValue += 1;
                }
                else
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

        public string HandCheck(string person, List<Card> hand)
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
            return start + cards.Trim() + $" ({HandValueCheck(hand)})";
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
                System.Console.WriteLine(HandCheck("The Dealer", dealerHand));
                if (DealerLoseCheck())
                {
                    System.Console.WriteLine("\nYou Win!\n");
                    Restart();
                }
            }
            else
            {
                System.Console.WriteLine("\nThe Dealer chooses to Stand");
                System.Console.WriteLine(HandCheck("The Dealer", dealerHand));
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
                Program.GameStart();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
