using UnityEngine;

public class SpawnPieces : MonoBehaviour
{
    private ChessPiece[,] chessPieces;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] sideMaterials;

    private void Awake()
    {
        SpawnAllPieces();
        PositionAllPieces();   
    }

    // Spawning all pieces based on default chess layout
    private void SpawnAllPieces()
    {
        chessPieces = new ChessPiece[8,8];
        int whiteSide = 0, blackSide = 1;

        // White side
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

        // Black side
        chessPieces[0, 7] = SpawnSinglePiece(ChessPieceType.Rook, blackSide);
        chessPieces[1, 7] = SpawnSinglePiece(ChessPieceType.Knight, blackSide);
        chessPieces[2, 7] = SpawnSinglePiece(ChessPieceType.Bishop, blackSide);
        chessPieces[3, 7] = SpawnSinglePiece(ChessPieceType.King, blackSide);
        chessPieces[4, 7] = SpawnSinglePiece(ChessPieceType.Queen, blackSide);
        chessPieces[5, 7] = SpawnSinglePiece(ChessPieceType.Bishop, blackSide);
        chessPieces[6, 7] = SpawnSinglePiece(ChessPieceType.Knight, blackSide);
        chessPieces[7, 7] = SpawnSinglePiece(ChessPieceType.Rook, blackSide);
        for(int i = 0; i < 8; i++)
            chessPieces[i, 6] = SpawnSinglePiece(ChessPieceType.Pawn, blackSide);
    }

    // Spawning single chess piece with specified side
    private ChessPiece SpawnSinglePiece(ChessPieceType type, int side)
    {
        ChessPiece _chessPiece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();
        _chessPiece.type = type;
        _chessPiece.side = side;
        _chessPiece.GetComponent<Renderer>().material = sideMaterials[side];
        return _chessPiece;
    }

    // Position all pieces traversing through chessPieces array then calling PositionSinglePiece by index
    private void PositionAllPieces()
    {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                if(chessPieces[x, y] != null)
                    PositionSinglePiece(x, y, true);
            }
        }
    }

    // Position single piece with given positions, decide to make it instant or smooth (force)
    // Vector3(x * tileSize, yOffset, y * tileSize)
    private void PositionSinglePiece(int x, int y, bool force = false)
    {
        chessPieces[x, y].currentX = x;
        chessPieces[x, y].currentY = y;
        chessPieces[x, y].transform.position = new Vector3(x, 0.5f, y);
    }
}
