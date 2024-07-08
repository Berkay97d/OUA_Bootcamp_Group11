using UnityEngine;

namespace _Scripts.Mouse_System
{
    /// <summary>
    ///   <para> Basic Struct holds info about mouse like isHitting an object and if it then where /// </para>
    /// </summary>
    public struct MouseFloorInfo
    {
        public bool IsHit { get; set; }
        public Vector3 HitPoint { get; set; }
        public GameObject HitObject { get; set; }
    }
}