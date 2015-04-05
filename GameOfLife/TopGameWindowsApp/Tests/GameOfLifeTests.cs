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
        public void GivenALiveCellWithAllDeadNeighboursAndThreeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 0,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: 3);
        }

        [TestMethod]
        public void GivenALiveCellWithOneLiveNeighbourAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 1;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 1,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithTwoLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellSurvives()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 2;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 2,
                successJudger: CellShouldSurvive,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithThreeLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellSurvives()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 3;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 3,
                successJudger: CellShouldSurvive,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithFourLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 4;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 4,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithFiveLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 5;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 5,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithSixLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 6;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 6,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithSevenLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 7;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 7,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenALiveCellWithEightLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies()
        {
            _survivalState = Survival.Alive;
            const int numLiveNeighbours = 8;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 8,
                successJudger: CellShouldDie,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithAllDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 0;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 0,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithOneLiveNeighbourAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 1;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 1,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithTwoLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 2;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 2,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithThreeLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellRegenerates()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 3;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 3,
                successJudger: CellShouldRegenerate,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithFourLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 4;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 4,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithFiveLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 5;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 5,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithSixLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 6;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 6,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithSevenLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 7;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 7,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }

        [TestMethod]
        public void GivenADeadCellWithEightLiveNeighboursAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellIsStillDead()
        {
            _survivalState = Survival.Dead;
            const int numLiveNeighbours = 8;

            TestAllTypesOfCellPosition(
                numLiveNeighbours: 8,
                successJudger: CellShouldStayDead,
                numLiveNonNeighbours: CalculateNumberOfNonLiveNonNeighboursMostLikelyToBreakTest(numLiveNeighbours));
        }
    }
}
