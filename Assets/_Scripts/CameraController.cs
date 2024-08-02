using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float ms;
   
    [SerializeField] private float _zoom;
    
    
    private void Update()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = +-1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        
        Vector3 zoomVec = new Vector3(0,0,0);
        zoomVec.z = Input.mouseScrollDelta.y * _zoom;

        Vector3 moveVector = transform.forward * zoomVec.z + Vector3.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        
        
        transform.position += moveVector * ms * Time.deltaTime;

     
        
    }
}
