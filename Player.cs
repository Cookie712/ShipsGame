using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsGame
{
    public class Player
    {
        protected Board board;
        protected Board opponentBoard;
        protected const int safeDistance = 2;
        public Player(Board board, Board opponentBoard)
        {
            this.board = board;
            this.opponentBoard = opponentBoard;
        }

        public virtual void PlaceShips()
        {
            Console.WriteLine("Dostępne statki: 4 statki jednomasztowe, 3 dwumasztowe, 2 trójmasztowe, 1 czteromasztowy.");

            PlaceShipOfSize(4, "one-masted", 1);
            PlaceShipOfSize(3, "two-masted", 2);
            PlaceShipOfSize(2, "three-masted", 3);
            PlaceShipOfSize(1, "four-masted", 4);
        }

        protected virtual void PlaceShipOfSize(int number, string shipType, int size)
        {
            for (int i = 0; i < number; i++)
            {
                bool isValidPlacement = false;
                int row = 0;
                int col = 0;

                while (!isValidPlacement)
                {
                    Console.WriteLine($"Ustaw swój {size}-masztowy statek. Wpisz początkowy wiersz i kolumnę:");
                    string input = Console.ReadLine();

                    if (input.Length < 2)
                    {
                        Console.WriteLine("Nieprawidłowe dane wejściowe. Spróbuj ponownie.");
                        continue;
                    }

                    char rowChar = Char.ToUpper(input[0]);
                    char colChar = input[1];

                    if (!Char.IsLetter(rowChar) || !Char.IsDigit(colChar))
                    {
                        Console.WriteLine("Nieprawidłowe dane wejściowe. Spróbuj ponownie.");
                        continue;
                    }

                    row = rowChar - 'A';
                    col = colChar - '1';

                    if (!board.IsValidPlacement(row, col))
                    {
                        Console.WriteLine("Nieprawidłowe miejsce docelowe. Spróbuj ponownie.");
                        continue;
                    }

                    isValidPlacement = true;
                }

                for (int j = 0; j < size; j++)
                {
                    switch (shipType)
                    {
                        case "four-masted":
                            board.PlaceShip(row, col + j);
                            break;
                        case "three-masted":
                            board.PlaceShip(row, col + j);
                            break;
                        case "two-masted":
                            board.PlaceShip(row, col + j);
                            break;
                        case "one-masted":
                            board.PlaceShip(row, col + j);
                            break;
                    }
                }

                board.DisplayBoard();
            }
        }

        public virtual void Shoot()
        {
            Console.WriteLine("Wprowadź współrzędne do strzału");
            int row = 0;
            int col = 0;
            bool validShot = false;

            while (!validShot)
            {
                string input = Console.ReadLine().ToUpper();
                if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
                {
                    row = input[0] - 'A';
                    col = input[1] - '1';

                    if (opponentBoard.IsValidPlacement(row, col))
                    {
                        validShot = true;

                        if (opponentBoard.grid[row, col] == 'S')
                        {
                            Console.WriteLine("Trafiony!");
                            opponentBoard.PlaceHit(row, col);

                            bool shipDestroyed = true;
                            for (int i = 0; i < board.size; i++)
                            {
                                for (int j = 0; j < board.size; j++)
                                {
                                    if (opponentBoard.grid[i, j] == 'S')
                                    {
                                        shipDestroyed = false;
                                        break;
                                    }
                                }
                                if (!shipDestroyed) break;
                            }

                            if (shipDestroyed)
                            {
                                Console.WriteLine("Zatopiles statek!");
                                validShot = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pudło.");
                            opponentBoard.PlaceMiss(row, col);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowe współrzędne.Spróbuj ponownie:");
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowe dane wejściowe. Wprowadź współrzędne w prawidłowym formacie:");
                }
            }
        }
    }
}