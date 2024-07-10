using _Scripts.Grid_System;
using _Scripts.Mouse_System;
using UnityEngine;

public class MousePick : MonoBehaviour
{
    private ChessPiece currentlyDragging;
    
    // Significant placement errors because of the tilted camera angle !!!
    private void FixedUpdate()
    {
        var mousePos = MouseWorld.GetMouseMovementInfo().HitPoint;

        // Mouse left clicked
        if(Input.GetMouseButtonDown(0))
        {
            currentlyDragging = MoveOps.GetPiece(mousePos);
        }

        if(currentlyDragging)
        {
            Plane  horizontalPlane = new Plane(Vector3.up, Vector3.up * 1);
            float distance = 0.0f;
            Ray currentRay = MouseWorld.GetMouseMovementInfo().HitRay;
            if(horizontalPlane.Raycast(currentRay, out distance))
            {
                currentlyDragging.SetPosition(currentRay.GetPoint(distance) + Vector3.up * 1.5f);  
            }
        }

        // Mouse left click released
        if(currentlyDragging != null && Input.GetMouseButtonUp(0))
        {
            Vector2Int previousPosition = new Vector2Int(currentlyDragging.currentX, currentlyDragging.currentY);

            Vector3 droppedWorldPos = MoveOps.GetPiecePos(mousePos);
            bool validMove = MoveOps.MoveTo(currentlyDragging, (int) droppedWorldPos.x, (int) droppedWorldPos.z);
            if(!validMove)
                currentlyDragging.SetPosition(PositionPieces.GetTileCenter(previousPosition.x, previousPosition.y));
            currentlyDragging = null;
        }
        else
        {
            // Code for when piece is dragged and dropped outside of the board does not work !!!
            if(currentlyDragging && Input.GetMouseButtonUp(0))
            {
                currentlyDragging.SetPosition(PositionPieces.GetTileCenter(currentlyDragging.currentX, currentlyDragging.currentY));
                currentlyDragging = null;
            }
        }
    }

}
