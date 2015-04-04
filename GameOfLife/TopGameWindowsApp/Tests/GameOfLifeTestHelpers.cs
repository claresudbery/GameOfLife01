using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfLife.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Tests
{
    /// <summary>
    /// Note that this is a partial class, containing helper code and nothing else. 
    /// The actual tests are in a separate file (GameOfLifeTests.cs)
    /// </summary>
    public partial class GameOfLifeTests : ICell
    {
        bool _dieHasBeenCalled = false;
        bool _liveHasBeenCalled = false;
        private Survival _survivalState;
        private int _xCoordinate;
        private int _yCoordinate;
        private IList<ICell> _cells;
        private int _gridWidth = 8;
        private int _gridHeight = 4;
        private const int NumCornerNeighbours = 3;
        private const int NumEdgeNeighbours = 5;
        private const int NumCentreNeighbours = 8;

        public void Die()
        {
            _dieHasBeenCalled = true;
        }

        public void Live()
        {
            _liveHasBeenCalled = true;
        }

        public bool IsAlive()
        {
            return Survival.Alive == _survivalState;
        }

        public int XCoordinate()
        {
            return _xCoordinate;
        }

        public int YCoordinate()
        {
            return _yCoordinate;
        }

        [TestInitialize]
        public void Initialize()
        {
            _cells = new List<ICell>();
        }

        private void CellShouldDie()
        {
            Assert.IsTrue(_dieHasBeenCalled, "Die() was not called, but should have been.");
        }

        private void CellShouldSurvive()
        {
            Assert.IsFalse(_dieHasBeenCalled, "Die() was called when it shouldn't have been.");
        }

        private void CellShouldStayDead()
        {
            Assert.IsFalse(_liveHasBeenCalled, "Live() was called, but shouldn't have been.");
        }

        private void CellShouldRegenerate()
        {
            Assert.IsTrue(_liveHasBeenCalled, "Live() was not called, but should have been.");
        }

        private void AddDeadNeighbours(int numNeighbours, GamePosition gamePosition)
        {
            AddNeighbours(numNeighbours, Survival.Dead, gamePosition);
        }

        private void AddLiveNeighbours(int numNeighbours, GamePosition gamePosition)
        {
            AddNeighbours(numNeighbours, Survival.Alive, gamePosition);
        }

        private void AddNeighbours(
            int numNeighbours,
            Survival survivalState,
            GamePosition gamePosition)
        {
            for (int count = 1; count <= numNeighbours; count++)
            {
                Point neighbourPosition = gamePosition.NextNeighbour();
                ICell neighbour = new Cell(survivalState, neighbourPosition.X, neighbourPosition.Y);
                _cells.Add(neighbour);
            }
        }

        private void TestAllTypesOfCellPosition(int numLiveNeighbours, Action successJudger)
        {
            GamePosition cornerCell = GetCornerCell();
            if (cornerCell.NumNeighbours >= numLiveNeighbours)
            {
                TestParticularCellPosition(
                    numLiveNeighbours,
                    cornerCell,
                    successJudger);
            }

            GamePosition edgeCell = GetEdgeCell();
            if (edgeCell.NumNeighbours >= numLiveNeighbours)
            {
                TestParticularCellPosition(
                    numLiveNeighbours,
                    edgeCell,
                    successJudger);
            }

            TestParticularCellPosition(
                numLiveNeighbours,
                GetCentreCell(),
                successJudger);
        }

        private void TestParticularCellPosition(
            int numLiveNeighbours,
            GamePosition gamePosition,
            Action successJudger)
        {
            _xCoordinate = gamePosition.Position.X;
            _yCoordinate = gamePosition.Position.Y;
            _cells.Add(this);

            if (numLiveNeighbours > 0)
            {
                AddLiveNeighbours(numLiveNeighbours, gamePosition);
            }
            int numDeadNeighbours = gamePosition.NumNeighbours - numLiveNeighbours;
            if (numDeadNeighbours > 0)
            {
                AddDeadNeighbours(numDeadNeighbours, gamePosition);
            }
            Grid grid = new Grid(_cells);

            grid.Evolve();

            successJudger();
            _cells.Clear();
        }

        private GamePosition GetCornerCell()
        {
            int xCoordinate = _gridWidth - 1;
            int yCoordinate = _gridHeight - 1;

            return new GamePosition
            {
                Position = new Point(xCoordinate,
                                    yCoordinate),
                Neighbours = new List<Point>
                {
                    AboveAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyAbove(xCoordinate, yCoordinate),
                    DirectlyToTheLeft(xCoordinate, yCoordinate)
                }
            };
        }

        private GamePosition GetEdgeCell()
        {
            if (_gridWidth < 3 && _gridHeight < 3)
            {
                throw new Exception("Grid is not big enough to allow edge pieces");
            }

            return _gridWidth >= 3 ? GetEdgeCellOnBottomEdge() : GetEdgeCellOnRightHandEdge();
        }

        private GamePosition GetEdgeCellOnRightHandEdge()
        {
            int xCoordinate = _gridWidth - 1;
            int yCoordinate = _gridHeight >= 4 ? _gridHeight - 3 : _gridHeight - 2;

            return new GamePosition
            {
                Position = new Point(xCoordinate,
                                    yCoordinate),
                Neighbours = new List<Point>
                {
                    AboveAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyAbove(xCoordinate, yCoordinate),
                    DirectlyToTheLeft(xCoordinate, yCoordinate),
                    BelowAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyBelow(xCoordinate, yCoordinate)
                }
            };
        }

        private GamePosition GetEdgeCellOnBottomEdge()
        {
            int xCoordinate = _gridWidth >= 4 ? _gridWidth - 3 : _gridWidth - 2; ;
            int yCoordinate = _gridHeight - 1;

            return new GamePosition
            {
                Position = new Point(xCoordinate,
                                    yCoordinate),
                Neighbours = new List<Point>
                {
                    AboveAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyAbove(xCoordinate, yCoordinate),
                    AboveAndToTheRight(xCoordinate, yCoordinate),
                    DirectlyToTheLeft(xCoordinate, yCoordinate),
                    DirectlyToTheRight(xCoordinate, yCoordinate)
                }
            };
        }

        private GamePosition GetCentreCell()
        {
            if (_gridWidth < 3 || _gridHeight < 3)
            {
                throw new Exception("Grid is not big enough to allow centre pieces");
            }

            int xCoordinate = _gridWidth >= 4 ? _gridWidth - 3 : _gridWidth - 2;
            int yCoordinate = _gridHeight >= 4 ? _gridHeight - 3 : _gridHeight - 2;

            return new GamePosition
            {
                Position = new Point(_gridWidth - 2, _gridHeight - 2),
                Neighbours = new List<Point>
                {
                    AboveAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyAbove(xCoordinate, yCoordinate),
                    AboveAndToTheRight(xCoordinate, yCoordinate),
                    DirectlyToTheLeft(xCoordinate, yCoordinate),
                    DirectlyToTheRight(xCoordinate, yCoordinate),
                    BelowAndToTheLeft(xCoordinate, yCoordinate),
                    DirectlyBelow(xCoordinate, yCoordinate),
                    BelowAndToTheRight(xCoordinate, yCoordinate)
                }
            };
        }

        private Point BelowAndToTheRight(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate + 1, yCoordinate + 1);
        }

        private Point DirectlyBelow(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate, yCoordinate + 1);
        }

        private Point BelowAndToTheLeft(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate - 1, yCoordinate + 1);
        }

        private Point DirectlyToTheRight(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate + 1, yCoordinate);
        }

        private Point DirectlyToTheLeft(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate - 1, yCoordinate);
        }

        private Point AboveAndToTheRight(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate + 1, yCoordinate - 1);
        }

        private Point DirectlyAbove(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate, yCoordinate - 1);
        }

        private Point AboveAndToTheLeft(int xCoordinate, int yCoordinate)
        {
            return new Point(xCoordinate - 1, yCoordinate - 1);
        }
    }
}
