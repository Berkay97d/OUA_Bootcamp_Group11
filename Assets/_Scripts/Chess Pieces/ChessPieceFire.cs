using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceFire : MonoBehaviour
    {
        private ChessPiece _chessPiece;


        private void Awake()
        {
            _chessPiece = GetComponent<ChessPiece>();
        }

        private void FixedUpdate()
        {
           // transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        }

    }
}