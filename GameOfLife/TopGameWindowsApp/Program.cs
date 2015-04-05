using System;
using System.Collections.Generic;
using System.Threading;
using GameOfLife.Classes;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            string newInput = "E";
            int gridWidth = 8;
            int gridHeight = 4;
            IList<ICell> cells = CreateCells(gridWidth, gridHeight);
            var grid = new Grid(cells);

            ShowContentsOfGrid(grid); 
            Console.WriteLine("Above is a brand new grid, which has not yet evolved.");

            while ("E" == newInput.ToUpper())
            {
                grid.Evolve();
                ShowContentsOfGrid(grid);
                Console.WriteLine("This grid has now evolved. Enter 'E' to evolve again, or enter any other key to quit.");
                newInput = Console.ReadLine();
            }

            Console.WriteLine("OK, then, if I really must. Goodbye [sob].");

            Thread.Sleep(System.TimeSpan.FromSeconds(2));
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

        private static void ShowContentsOfGrid(Grid grid)
        {
            var gridContents = grid.GetCellsInOrder();

            for (int rowIndex = 0; rowIndex < gridContents.Length; rowIndex++)
            {
                var row = new string(gridContents[rowIndex]);
                Console.WriteLine(row);
            }
        }
    }
}
