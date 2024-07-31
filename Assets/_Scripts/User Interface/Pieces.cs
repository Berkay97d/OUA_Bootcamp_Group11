using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    [SerializeField] private List<GameObject> pieces; // Ta�lar�n referanslar�
    private List<Vector3> startPositions; // Ta�lar�n ba�lang�� konumlar�

    void Start()
    {
        // Ba�lang�� konumlar�n� kaydet
        startPositions = new List<Vector3>();
        foreach (GameObject piece in pieces)
        {
            startPositions.Add(piece.transform.position);
        }
    }

    public void ResetPieces()
    {
        // Ta�lar� ba�lang�� konumlar�na geri d�nd�r
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].transform.position = startPositions[i];
        }
    }
}
