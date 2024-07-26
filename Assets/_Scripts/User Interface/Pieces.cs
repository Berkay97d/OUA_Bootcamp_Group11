using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    [SerializeField] private List<GameObject> pieces; // Taþlarýn referanslarý
    private List<Vector3> startPositions; // Taþlarýn baþlangýç konumlarý

    void Start()
    {
        // Baþlangýç konumlarýný kaydet
        startPositions = new List<Vector3>();
        foreach (GameObject piece in pieces)
        {
            startPositions.Add(piece.transform.position);
        }
    }

    public void ResetPieces()
    {
        // Taþlarý baþlangýç konumlarýna geri döndür
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].transform.position = startPositions[i];
        }
    }
}
