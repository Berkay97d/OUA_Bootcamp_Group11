using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

public class FireVisual : MonoBehaviour
{
    [SerializeField] private GameObject _visual;


    private void Start()
    {
        ChessPieceFire.OnChessPieceShot += ChessPieceFireOnOnChessPieceShot;
        UnitReplayManager.OnReplayShot += UnitReplayManagerOnOnReplayShot;
    }

    private void OnDestroy()
    {
        ChessPieceFire.OnChessPieceShot += ChessPieceFireOnOnChessPieceShot;
        UnitReplayManager.OnReplayShot += UnitReplayManagerOnOnReplayShot;
    }

    private void UnitReplayManagerOnOnReplayShot(GridObject obj)
    {
        StartCoroutine(InnerRoutine());
        

        IEnumerator InnerRoutine()
        {
            var worldPosition = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(obj.GetGridPosition());
            worldPosition.y += 0.5f;
            transform.position = worldPosition;

            yield return new WaitForSeconds(0.1f);
            _visual.gameObject.SetActive(true);   
            yield return new WaitForSeconds(2f);
            _visual.gameObject.SetActive(false);
        }
    }

    private void ChessPieceFireOnOnChessPieceShot(UnitTurnData obj)
    {
        StartCoroutine(InnerRoutine());
        

        IEnumerator InnerRoutine()
        {
            var gridPosition = obj.shotGrid.GetGridPosition();
            var worldPosition = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(gridPosition);
            worldPosition.y += 0.5f;
            transform.position = worldPosition;

            yield return new WaitForSeconds(0.1f);
            _visual.gameObject.SetActive(true);   
            yield return new WaitForSeconds(2f);
            _visual.gameObject.SetActive(false);
        }
        
    }
}
