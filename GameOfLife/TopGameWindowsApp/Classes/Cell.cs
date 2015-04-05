namespace GameOfLife.Classes
{
    public class Cell : ICell
    {
        private Survival _survivalState;
        private readonly int _xCoordinate;
        private readonly int _yCoordinate;

        public Cell(
            Survival survivalState,
            int xCoordinate, 
            int yCoordinate)
        {
            _survivalState = survivalState;
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
        }

        public void Die()
        {
            _survivalState = Survival.Alive;
        }

        public void Live()
        {
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
    }
}