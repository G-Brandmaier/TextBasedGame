﻿using Inlämningsuppgift3.Classes;

namespace Inlämningsuppgift3
{
    internal class Program
    {
        static void Main(string[] args)
        {          
            Game newGame = new Game();
            newGame.StartGame();
            Console.WriteLine("Press < ENTER > to exit the game");
            Console.ReadKey();
        }
    }
}