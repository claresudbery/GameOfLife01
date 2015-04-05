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

        public Cell(ICell cellToCopy)
        {
            _survivalState = cellToCopy.IsAlive() ? Survival.Alive : Survival.Dead;
            _xCoordinate = cellToCopy.XCoordinate();
            _yCoordinate = cellToCopy.YCoordinate();
        }

        public void Die()
        {
            _survivalState = Survival.Dead;
        }

        public void Live()
        {
            _survivalState = Survival.Alive;
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