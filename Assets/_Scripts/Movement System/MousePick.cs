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
            if(currentlyDragging && Input.GetMouseButtonUp(0))
            {
                currentlyDragging.SetPosition(PositionPieces.GetTileCenter(currentlyDragging.currentX, currentlyDragging.currentY));
                currentlyDragging = null;
            }
        }
    }

}
