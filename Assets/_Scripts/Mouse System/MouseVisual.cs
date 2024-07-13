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
                    hoverTile.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.1f);
                    hoverTile = mouseInfo.HitObject;
                }
                hoverTile.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.6f);

                return;
            }

            _renderer.enabled = false;
        }
    }
}