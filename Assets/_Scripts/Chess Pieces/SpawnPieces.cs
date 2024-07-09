using UnityEngine;

public class SpawnPieces : MonoBehaviour
{
    private ChessPiece[,] chessPieces;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] sideMaterials;


    void Awake()
    {
        SpawnAllPieces();

    }

    private void SpawnAllPieces()
    {
        chessPieces = new ChessPiece[8,8];
        int whiteSide = 0, blackSide = 1;

        chessPieces[0, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteSide);
        chessPieces[1, 0] = SpawnSinglePiece(ChessPieceType.Knight, whiteSide);
        chessPieces[2, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteSide);
        chessPieces[3, 0] = SpawnSinglePiece(ChessPieceType.King, whiteSide);
        chessPieces[4, 0] = SpawnSinglePiece(ChessPieceType.Queen, whiteSide);
        chessPieces[5, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteSide);
        chessPieces[6, 0] = SpawnSinglePiece(ChessPieceType.Knight, whiteSide);
        chessPieces[7, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteSide);
        for(int i = 0; i < 8; i++)
            chessPieces[i, 1] = SpawnSinglePiece(ChessPieceType.Pawn, whiteSide);

    }

    private ChessPiece SpawnSinglePiece(ChessPieceType type, int side)
    {
        ChessPiece _chessPiece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();
        _chessPiece.type = type;
        _chessPiece.side = side;
        _chessPiece.GetComponent<Renderer>().material = sideMaterials[side];
        return _chessPiece;
    }
}
