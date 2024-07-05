namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>A basic struct that holds x and z positions of our Grid Object</para>
    /// </summary>
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