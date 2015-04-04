using System;
using System.Collections.Generic;
using System.Drawing;
using GameOfLife.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Tests
{
    /// <summary>
    /// Note that this is a partial class, containing actual tests and nothing else.
    /// All of the associated helper code is in a separate file (GameOfLifeTestHelpers.cs)
    /// </summary>
    [TestClass]
    public partial class GameOfLifeTests : ICell
    {
        [TestMethod]
        public void GivenALiveCellWithAllDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 0,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithOneLiveNeighbourAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 1,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithTwoLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellSurvives()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 2,
                successJudger: CellShouldSurvive);
        }

        [TestMethod]
        public void GivenALiveCellWithThreeLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellSurvives()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 3,
                successJudger: CellShouldSurvive);
        }

        [TestMethod]
        public void GivenALiveCellWithFourLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 4,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithFiveLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 5,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithSixLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 6,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithSevenLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 7,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenALiveCellWithEightLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 8,
                successJudger: CellShouldDie);
        }

        [TestMethod]
        public void GivenADeadCellWithAllDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 0,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithOneLiveNeighbourAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 1,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithTwoLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 2,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithThreeLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellRegenerates()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 3,
                successJudger: CellShouldRegenerate);
        }

        [TestMethod]
        public void GivenADeadCellWithFourLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 4,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithFiveLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 5,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithSixLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 6,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithSevenLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 7,
                successJudger: CellShouldStayDead);
        }

        [TestMethod]
        public void GivenADeadCellWithEightLiveNeighboursAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            TestAllTypesOfCellPosition(
                numLiveNeighbours: 8,
                successJudger: CellShouldStayDead);
        }
    }
}
