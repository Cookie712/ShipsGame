using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsGame
{
    public class Board
    {
        public readonly int size;
        public readonly char[,] grid;

        public Board(int size)
        {
            this.size = size;
            this.grid = new char[size, size];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }

        public void DisplayBoard()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < size; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void PlaceShip(int row, int col)
        {
            grid[row, col] = 'S';
        }

        public void PlaceHit(int row, int col)
        {
            grid[row, col] = 'X';
        }

        public void PlaceMiss(int row, int col)
        {
            grid[row, col] = 'O';
        }

        public bool AllShipsSunk()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (grid[row, col] == 'S')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsValidPlacement(int row, int col)
        {
            return row >= 0 && row < size && col >= 0 && col < size && grid[row, col] == ' ';
        }
    }
}