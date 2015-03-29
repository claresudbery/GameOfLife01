using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.Classes
{
    public class GamePosition
    {
        public Point Position { get; set; }
        public int NumNeighbours {
            get { return Neighbours.Count; }
        }
        public IList<Point> Neighbours { get; set; }
        private int _nextNeighbourIndex = 0;

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
    }
}