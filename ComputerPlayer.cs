using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsGame
{
    public class ComputerPlayer : Player
    {
        private Random random;

        public ComputerPlayer(Board board, Board opponentBoard) : base(board, opponentBoard)
        {
            this.board = board;
            this.opponentBoard = opponentBoard;
            random = new Random();
        }

        public override void PlaceShips()
        {
            PlaceShipOfSize(4, "one-masted", 1);
            PlaceShipOfSize(3, "two-masted", 2);
            PlaceShipOfSize(2, "three-masted", 3);
            PlaceShipOfSize(1, "four-masted", 4);
        }

        protected override void PlaceShipOfSize(int number, string shipType, int size)
        {
            for (int i = 0; i < number; i++)
            {
                bool isValidPlacement = false;
                int row = 0;
                int col = 0;

                while (!isValidPlacement)
                {
                    row = random.Next(0, board.size);
                    col = random.Next(0, board.size - safeDistance);

                    if (board.IsValidPlacement(row, col) && IsStraightLine(row, col, size))
                    {
                        isValidPlacement = true;
                    }
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
            }
        }

        private bool IsStraightLine(int row, int col, int size)
        {
            int countHorizontal = 0;
            for (int i = 0; i < size; i++)
            {
                if (col + i < board.size && board.grid[row, col + i] == ' ')
                {
                    countHorizontal++;
                }
            }

            return countHorizontal == size;
        }

        public override void Shoot()
        {
            bool isValidShot = false;
            int row = 0;
            int col = 0;

            while (!isValidShot)
            {
                row = random.Next(0, opponentBoard.size);
                col = random.Next(0, opponentBoard.size);

                if (opponentBoard.IsValidPlacement(row, col))
                {
                    isValidShot = true;
                }
            }

            if (opponentBoard.grid[row, col] == 'S')
            {
                opponentBoard.PlaceHit(row, col);
                Console.WriteLine("Komputer uderzył w twój statek!");
                Shoot();
            }
            else if (opponentBoard.grid[row, col] == ' ')
            {
                opponentBoard.PlaceMiss(row, col);
                Console.WriteLine("Komputer chybił.");
            }
        }
    }
}