using TMPro;
using UnityEngine;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>
    /// Tile is just a visual class of grid objects,
    /// all tiles know which grid object they belong,
    /// we should not use this class for logical operations,
    /// just use for visual operations
    /// </para>
    /// </summary>
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private float _tileGap;
        [SerializeField] private float _thickness;
        
        private GridObject m_gridObject;
        
        /// <summary>
        ///   <para> It adjust it's size via thickness and tile gap in TilePrefab /// </para>
        /// </summary>
        private void AdjustSize()
        {
            var size = m_gridObject.GetGridSystem().GetSize();
            transform.localScale = new Vector3(size-_tileGap, _thickness, size-_tileGap);
        }
        
        /// <summary>
        ///   <para> Just a visual helper /// </para>
        /// </summary>
        private void AdjustPositionText(GridPosition gridPosition)
        {
            _positionText.text = gridPosition._x + "," + gridPosition._z;
        }
        
        public void SetGridObject(GridObject gridObject)
        {
            m_gridObject = gridObject;
            
            AdjustPositionText(m_gridObject.GetGridPosition());
            AdjustSize();
        }

        public GridObject GetGridObject()
        {
            return m_gridObject;
        }

        public float GetThickness()
        {
            return _thickness;
        }
    }
}