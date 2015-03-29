using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Classes
{
    public class Grid
    {
        private IList<ICell> cells;

        public Grid(IList<ICell> cells)
        {
            this.cells = cells;
        }

        public void Evolve()
        {
            foreach(var cell in cells)
            {
                int numLiveNeighbours = NumLiveNeighbours(cell);
                if (3 == numLiveNeighbours)
                {
                    cell.Live();
                }
                else
                {
                    if (2 == numLiveNeighbours && cell.IsAlive())
                    {
                        cell.Live();
                    }
                    else
                    {
                        cell.Die();
                    }
                }
            }
        }

        private bool CellsAreTheSame (ICell firstCell, ICell secondCell)
        {
            return (firstCell.XCoordinate() == secondCell.XCoordinate()
                    && firstCell.YCoordinate() == secondCell.YCoordinate());
        }

        private int NumLiveNeighbours(ICell candidateCell)
        {
            return cells.Count(cell => !CellsAreTheSame(cell, candidateCell) && cell.IsAlive());
        }
    }
}