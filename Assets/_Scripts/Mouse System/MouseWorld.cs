using UnityEngine;

namespace _Scripts.Mouse_System
{
    /// <summary>
    ///   <para>
    /// Logical operations about our mouse actions,
    /// Don't reference this class, just use with static functions
    /// /// </para>
    /// </summary>
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private Camera _mainCamera;
        
        
        private static MouseWorld Instance;

    
        private void Awake()
        {
            Instance = this;
        }
    
        /// <summary>
        ///   <para>
        /// Sends a ray from camera to mouse position and fill the MouseFloorInfo struct
        /// with information of the ray
        /// /// </para>
        /// </summary>
        public static MouseFloorInfo GetMouseMovementInfo()
        {
            var mouseInfo = new MouseFloorInfo();
        
            var ray = Instance._mainCamera.ScreenPointToRay(Input.mousePosition);
            
            mouseInfo.HitRay = ray;
            mouseInfo.IsHit = Physics.Raycast(ray, out RaycastHit hit,float.MaxValue ,Instance._floorLayerMask);
            mouseInfo.HitPoint = hit.point;
            if(mouseInfo.IsHit)
                mouseInfo.HitObject = hit.transform.gameObject;

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