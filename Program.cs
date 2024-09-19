using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Board playerBoard = new Board(10);
            Board enemyBoard = new Board(10);

            Player player1 = new Player(playerBoard, enemyBoard);
            player1.PlaceShips();
            playerBoard.DisplayBoard();

            ComputerPlayer player2 = new ComputerPlayer(enemyBoard, playerBoard);
            player2.PlaceShips();

            while (!playerBoard.AllShipsSunk() && !enemyBoard.AllShipsSunk())
            {
                Console.WriteLine("Twoja kolej:");
                player1.Shoot();
                if (playerBoard.AllShipsSunk())
                {
                    Console.WriteLine("Wygrywasz!");
                    break;
                }

                Console.WriteLine("Kolej komputera:");
                player2.Shoot();
                if (enemyBoard.AllShipsSunk())
                {
                    Console.WriteLine("Przegrywasz!");
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
