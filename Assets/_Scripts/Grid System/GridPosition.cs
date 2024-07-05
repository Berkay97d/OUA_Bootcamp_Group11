namespace _Scripts.Grid_System
{
    public struct GridPosition
    {
        public int _x;
        public int _z;

        public GridPosition(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public override string ToString()
        {
            return $"x: {_x}; z: {_z}";
        }
    }
}