using UnityEngine;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>In GridSystem includes logical operations about creation of grid system</para>
    /// </summary>
    public class GridSystem
    {
        private int m_width;
        private int m_height;
        private float m_size;

        private GridObject[,] m_gridObjects;
        
        /// <summary>
        ///   <para>
        /// In constructor, we create grid objects and put them in a 2D array via information with where object is created,
        /// width and height refers the horizontal and forward size of the grid system,
        /// size refers 3D size of a single gridObject, tiles adjust their size via size
        /// </para>
        /// </summary>
        public GridSystem(int width, int height, float size)
        {
            m_width = width;
            m_height = height;
            m_size = size;

            m_gridObjects = new GridObject[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    var gridPosition = new GridPosition(x, z);
                    m_gridObjects[x,z] = new GridObject(this, gridPosition);
                }
            }
        }

        /// <summary>
        ///   <para> Returns grid objects 3D World position via grid position/// </para>
        /// </summary>
        public Vector3 GetWorldPositionFromGridPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition._x, 0, gridPosition._z) * m_size;
        }

        /// <summary>
        ///   <para> Returns grid objects grid position x and z via 3D World position/// </para>
        /// </summary>
        public GridPosition GetGridPositionFromWorldPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / m_size),
                Mathf.RoundToInt(worldPosition.z / m_size)
            );
        }
        
        /// <summary>
        ///   <para> Returns the reference of a single grid object via it's grid position/// </para>
        /// </summary>
        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return m_gridObjects[gridPosition._x, gridPosition._z];
        }

        /// <summary>
        ///   <para> Create tile visuals and pass the value of grid object of tiles/// </para>
        /// </summary>
        public void CreateTiles(Transform tilePrefab)
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int z = 0; z < m_height; z++)
                {
                    var gridPosition = new GridPosition(x, z);
                    var tileTransform =GameObject.Instantiate(tilePrefab, GetWorldPositionFromGridPosition(gridPosition), Quaternion.identity);
                    var tile = tileTransform.GetComponent<Tile>();
                    var gridObject = GetGridObject(gridPosition);
                    tile.SetGridObject(gridObject);
                }
            }
        }

        public float GetSize()
        {
            return m_size;
        }
    }
}