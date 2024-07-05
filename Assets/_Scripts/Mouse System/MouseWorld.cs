using UnityEngine;

namespace _Scripts.Mouse_System
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private Camera _mainCamera;
        
        
        private static MouseWorld Instance;

    
        private void Awake()
        {
            Instance = this;
        }
    
        public static MouseFloorClickedInfo GetMouseMovementInfo()
        {
            var mouseInfo = new MouseFloorClickedInfo();
        
            var ray = Instance._mainCamera.ScreenPointToRay(Input.mousePosition);
            
            mouseInfo.IsHit = Physics.Raycast(ray, out RaycastHit hit,float.MaxValue ,Instance._floorLayerMask);
            mouseInfo.HitPoint = hit.point;

            return mouseInfo;
        }
        
        public static Vector3 GetMousePosition()
        {
            var ray = Instance._mainCamera.ScreenPointToRay(Input.mousePosition);
            
            Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Instance._floorLayerMask);
            
            return hit.point;
        }
    }
}