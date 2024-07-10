using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPieces : MonoBehaviour
{
    // Position all pieces traversing through chessPieces array then calling PositionSinglePiece by index
    public static void PositionAllPieces(ChessPiece[,] _chessPieces)
    {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                if(_chessPieces[x, y] != null)
                    PositionSinglePiece(_chessPieces[x,y] ,x, y, true);
            }
        }
    }

    // Position single piece with given positions, decide to make it instant or smooth (force)
    public static void PositionSinglePiece(ChessPiece _chessPiece, int x, int y, bool force = false)
    {
        _chessPiece.currentX = x;
        _chessPiece.currentY = y;
        _chessPiece.transform.position = GetTileCenter(x, y);
    }

    public static Vector3 GetTileCenter(int x, int y)
    {
        // Vector3(x * tileSize, yOffset, y * tileSize) + Vector3(tileSize / 2, 0, tileSize / 2)
        return new Vector3(x * 1, 0.5f, y * 1) + new Vector3(1 / 2, 0, 1 / 2);
    }
}
