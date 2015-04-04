using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.Classes
{
    public class GamePosition
    {
        public Point Position { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        public GamePosition(
            Point position,
            int gridWidth,
            int gridHeight)
        {
            Position = position;
            GridWidth = gridWidth;
            GridHeight = gridHeight;
        }

        private int _nextNeighbourIndex = 0;
        private Point _previousNonNeighbour = new Point(-1, 0);

        public IList<Point> Neighbours { get; set; }

        public int NumNeighbours {
            get { return Neighbours.Count; }
        }

        public int NumNonNeighbours
        {
            get { return (GridWidth * GridHeight) - NumNeighbours - 1; }
        }

        public Point NextNeighbour()
        {
            if (_nextNeighbourIndex >= Neighbours.Count)
            {
                throw new Exception("No neighbours left!");
            }

            Point nextNeighbour = Neighbours[_nextNeighbourIndex];
            _nextNeighbourIndex++;

            return nextNeighbour;
        }

        public Point NextNonNeighbour()
        {
            return RecursiveNextNonNeighbour();
        }

        private Point RecursiveNextNonNeighbour()
        {
            int xCoordinate = _previousNonNeighbour.X + 1;
            int yCoordinate = _previousNonNeighbour.Y;

            if (xCoordinate >= GridWidth)
            {
                xCoordinate = 0;
                yCoordinate = yCoordinate + 1;
            }

            if (yCoordinate >= GridHeight)
            {
                throw new Exception("No non-neighbours left!");
            }

            var nextNonNeighbour = new Point(xCoordinate, yCoordinate);

            if (ThisIsNotActuallyANonNeighbour(nextNonNeighbour))
            {
                _previousNonNeighbour = nextNonNeighbour;
                nextNonNeighbour = RecursiveNextNonNeighbour();
            }

            _previousNonNeighbour = nextNonNeighbour;
            return nextNonNeighbour;
        }

        private bool ThisIsNotActuallyANonNeighbour(Point potentialNonNeighbour)
        {
            return Position == potentialNonNeighbour || Neighbours.Contains(potentialNonNeighbour);
        }
    }
}