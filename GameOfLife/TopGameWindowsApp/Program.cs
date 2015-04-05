using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading;
using GameOfLife.Classes;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the game of life. Enter the number of rows in your grid, or any other key to quit.");
            try
            {
                int numRows = int.Parse(Console.ReadLine() ?? "");
                PlayGame(numRows);
            }
            catch (Exception)
            {
                Console.WriteLine("OK, then, if you really must. Goodbye [sob].");
                Thread.Sleep(System.TimeSpan.FromSeconds(2));
            }
        }

        private static void PlayGame(int numRows)
        {
            var grid = new GameOfLifeGrid(GetCells(numRows));
            Console.WriteLine("Thank you. Above is the grid you have entered, which has not yet evolved.");

            string newInput = "E";
            while ("E" == newInput.ToUpper())
            {
                grid.Evolve();
                ShowContentsOfGrid(grid);
                Console.WriteLine("This grid has now evolved. Enter 'E' to evolve again, or enter any other key to quit.");
                newInput = Console.ReadLine();
            }

            throw new Exception("Time to stop");
        }

        private static IList<ICell> GetCells(int numRows)
        {
            IList<ICell> cells = new List<ICell>();

            Console.WriteLine("Now enter {0} rows. Press Enter after each row.\n{1}\n{2}",
                numRows,
                "Each row must consist of 'X' for live or 'o' for dead (eg 'XoooooXXX').",
                "Enter any other key to quit.");

            int rowIndex = 0;
            while (rowIndex < numRows)
            {
                GetNextRow(cells, rowIndex);
                rowIndex++;
            }

            return cells;
        }

        private static void GetNextRow(IList<ICell> cells, int rowIndex)
        {
            string newInput = Console.ReadLine().ToUpper();

            if (!StringContainsOnlyXAndO(newInput))
            {
                throw new Exception("Time to stop");
            }

            for (int charIndex = 0; charIndex < newInput.Length; charIndex++)
            {
                char nextCell = newInput[charIndex];
                cells.Add(new Cell(
                    'X' == nextCell ? Survival.Alive : Survival.Dead,
                    charIndex,
                    rowIndex));
            }
        }

        private static bool StringContainsOnlyXAndO(string newInput)
        {
            return Regex.IsMatch(newInput.ToUpper(), "^[XO]*$");
        }

        private static IList<ICell> CreateCells(int gridWidth, int gridHeight)
        {
            var cells = new List<ICell>();

            for (int xCoordinate = 0; xCoordinate < gridWidth; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < gridHeight; yCoordinate++)
                {
                    var newCell = new Cell(Survival.Alive, xCoordinate, yCoordinate);
                    cells.Add(newCell);
                }
            }

            return cells;
        }

        private static void ShowContentsOfGrid(GameOfLifeGrid gameOfLifeGrid)
        {
            var gridContents = gameOfLifeGrid.GetCellsInOrder();

            for (int rowIndex = 0; rowIndex < gridContents.Length; rowIndex++)
            {
                var row = new string(gridContents[rowIndex]);
                Console.WriteLine(row);
            }
        }
    }
}
