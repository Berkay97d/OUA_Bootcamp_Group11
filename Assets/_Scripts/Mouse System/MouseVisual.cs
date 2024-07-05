using UnityEngine;

namespace _Scripts.Mouse_System
{
    public class MouseVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        

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
                return;
            }

            _renderer.enabled = false;
        }
    }
}