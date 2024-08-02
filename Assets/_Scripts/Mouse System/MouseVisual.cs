using UnityEngine;

namespace _Scripts.Mouse_System
{
    /// <summary>
    ///   <para> Just a visual class that helps us to test MouseWorld Class /// </para>
    /// </summary>
    public class MouseVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        GameObject hoverTile = null;

        private void FixedUpdate()
        {
            AdjustPosition();
        }
    
        private void AdjustPosition()
        {
            var mouseInfo = MouseWorld.GetMouseMovementInfo();
            
            if (mouseInfo.IsHit)
            {
                _renderer.enabled = true;
                transform.position = mouseInfo.HitPoint;
                
                if(hoverTile == null)
                    hoverTile = mouseInfo.HitObject;
                if(hoverTile != mouseInfo.HitObject)
                {
                    hoverTile = mouseInfo.HitObject;
                }
                

                return;
            }

            _renderer.enabled = false;
        }
    }
}