using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Classes
{
    public class GameOfLifeGrid
    {
        private IList<ICell> cells;

        public GameOfLifeGrid(IList<ICell> cells)
        {
            this.cells = cells;
        }

        public char[][] GetCellsInOrder()
        {
            return cells
                .GroupBy(cell => cell.YCoordinate())
                .Select(row => row
                    .OrderBy(cell => cell.XCoordinate())
                    .Select(x => x.IsAlive() ? 'X' : 'o')
                    .ToArray())
                .ToArray();
        }

        public void Evolve()
        {
            var newCells = new List<ICell>();
            ICell tempCell;

            foreach(var cell in cells)
            {
                tempCell = new Cell(cell);

                int numLiveNeighbours = NumLiveNeighbours(cell);
                if (3 == numLiveNeighbours)
                {
                    tempCell.Live();
                }
                else
                {
                    if (2 == numLiveNeighbours && cell.IsAlive())
                    {
                        tempCell.Live();
                    }
                    else
                    {
                        tempCell.Die();
                    }
                }

                newCells.Add(tempCell);
            }

            cells = newCells;
        }

        private bool CellsAreTheSame (ICell firstCell, ICell secondCell)
        {
            return (firstCell.XCoordinate() == secondCell.XCoordinate()
                    && firstCell.YCoordinate() == secondCell.YCoordinate());
        }

        private bool CellsAreNeighbours(ICell firstCell, ICell secondCell)
        {
            return (CoordinatesAreEqualOrAdjacent(firstCell.XCoordinate(), secondCell.XCoordinate())
                    && CoordinatesAreEqualOrAdjacent(firstCell.YCoordinate(), secondCell.YCoordinate()));
        }

        private bool CoordinatesAreEqualOrAdjacent(int firstCoordinate, int secondCoordinate)
        {
            return firstCoordinate <= secondCoordinate + 1
                    && firstCoordinate >= secondCoordinate - 1;
        }

        private int NumLiveNeighbours(ICell candidateCell)
        {
            return cells.Count(cell =>  
                cell.IsAlive()
                && CellsAreNeighbours(cell, candidateCell)
                && !CellsAreTheSame(cell, candidateCell));
        }

        public ICell GetCell(int xCoordinate, int yCoordinate)
        {
            return cells.First(
                cell => cell.XCoordinate() == xCoordinate
                        && cell.YCoordinate() == yCoordinate);
        }
    }
}