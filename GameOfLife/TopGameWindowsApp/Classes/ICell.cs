namespace GameOfLife.Classes
{
    public interface ICell
    {
        void Die();
        void Live();
        bool IsAlive();
        int XCoordinate();
        int YCoordinate();
    }
}
