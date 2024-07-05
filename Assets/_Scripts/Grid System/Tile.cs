using TMPro;
using UnityEngine;

namespace _Scripts.Grid_System
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private float _tileGap;
        [SerializeField] private float _thickness;
        
        private GridObject m_gridObject;
        
        
        private void AdjustSize()
        {
            var size = m_gridObject.GetGridSystem().GetSize();
            transform.localScale = new Vector3(size-_tileGap, _thickness, size-_tileGap);
        }

        private void AdjustPositionText(GridPosition gridPosition)
        {
            _positionText.text = gridPosition.ToString();
        }
        
        public void SetGridObject(GridObject gridObject)
        {
            m_gridObject = gridObject;
            
            AdjustPositionText(m_gridObject.GetGridPosition());
            AdjustSize();
        }
    }
}