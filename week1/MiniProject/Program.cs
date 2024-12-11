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
    }
}
