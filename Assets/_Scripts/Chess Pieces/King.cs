using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class King : ChessPiece
    {
        bool canSpecialMove = true;
        public override List<GridObject> GetAttackPattern()
        {
            List<GridObject> attackTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            attackTiles =  _gridObject.GetNeighboorGrids();
            
            return attackTiles;
        }

        public List<GridObject> GetBishopPattern()
        {
            List<GridObject> bishopMoveTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            // Up-left
            for(int xLimit = piecePos._x - 1, zLimit = piecePos._z + 1; xLimit >= 0 && zLimit < 8; xLimit--, zLimit++)
            {
                GridObject gridObjectToAdd = m_gridSystem.GetGridObject(new GridPosition(xLimit, zLimit));
                if(!gridObjectToAdd.GetIsOccupied() && !gridObjectToAdd.GetIsBroken())
                {
                    bishopMoveTiles.Add(gridObjectToAdd);
                }
                else
                {
                    continue;
                }
            }

            // Up-right
            for(int xLimit = piecePos._x + 1, zLimit = piecePos._z + 1; xLimit < 8 && zLimit < 8; xLimit++, zLimit++)
            {
                GridObject gridObjectToAdd = m_gridSystem.GetGridObject(new GridPosition(xLimit, zLimit));
                if(!gridObjectToAdd.GetIsOccupied() && !gridObjectToAdd.GetIsBroken())
                {
                    bishopMoveTiles.Add(gridObjectToAdd);
                }
                else
                {
                    continue;
                }
            }

            // Down-left
            for(int xLimit = piecePos._x - 1, zLimit = piecePos._z - 1; xLimit >= 0 && zLimit >= 0; xLimit--, zLimit--)
            {
                GridObject gridObjectToAdd = m_gridSystem.GetGridObject(new GridPosition(xLimit, zLimit));
                if(!gridObjectToAdd.GetIsOccupied() && !gridObjectToAdd.GetIsBroken())
                {
                    bishopMoveTiles.Add(gridObjectToAdd);
                }
                else
                {
                    continue;
                }
            }

            // Down-right
            for(int xLimit = piecePos._x + 1, zLimit = piecePos._z - 1; xLimit < 8 && zLimit >= 0; xLimit++, zLimit--)
            {
                GridObject gridObjectToAdd = m_gridSystem.GetGridObject(new GridPosition(xLimit, zLimit));
                if(!gridObjectToAdd.GetIsOccupied() && !gridObjectToAdd.GetIsBroken())
                {
                    bishopMoveTiles.Add(gridObjectToAdd);
                }
                else
                {
                    continue;
                }
            }

            return bishopMoveTiles;
        }

        public List<GridObject> GetKnightPattern()
        {
            List<GridObject> knightMoveTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            int[,] moveOffsets = new int[,]
            {
                { -1,  2 }, { 1,  2 },  // 2-up 1-left, 2-up 1-right
                { -2,  1 }, { 2,  1 },  // 1-up 2-left, 1-up 2-right
                { -2, -1 }, { 2, -1 },  // 1-down 2-left, 1-down 2-right
                { -1, -2 }, { 1, -2 }   // 2-down 1-left, 2-down 1-right
            };

            for (int i = 0; i < moveOffsets.GetLength(0); i++)
            {
                int newX = piecePos._x + moveOffsets[i, 0];
                int newZ = piecePos._z + moveOffsets[i, 1];
                if(newX >= 0 && newX < 8 && newZ >= 0 && newZ < 8)
                knightMoveTiles.Add(m_gridSystem.GetGridObject(new GridPosition(newX, newZ)));
            }

            foreach (var gridObject in knightMoveTiles.ToList())
            {
                if(gridObject.GetIsBroken() || gridObject.GetIsOccupied())
                {
                    knightMoveTiles.Remove(gridObject);
                }
            }

            return knightMoveTiles;
        }

        public bool GetCanSpecialMove()
        {
            return canSpecialMove;
        }

        public void SetCanSpecialMove(bool value)
        {
            canSpecialMove = value;
        }
    }
}