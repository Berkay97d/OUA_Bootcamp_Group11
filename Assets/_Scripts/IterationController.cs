using System;
using UnityEngine;

namespace _Scripts
{
    public class IterationController : MonoBehaviour
    {
        public static event Action OnIterationCompleted;
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnIterationCompleted?.Invoke();
            }
        }
    }
}