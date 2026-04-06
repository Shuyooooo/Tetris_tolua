using Tetris.Piece;
using UnityEngine;

public class PieceDebugView : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    private CurrentPiece piece;

    public void Bind(CurrentPiece currentPiece)
    {
        piece = currentPiece;
    }


    private void Update()
    {
        if (piece == null)
        {
            return;
        }

        x = piece.Position.x;
        y = piece.Position.y;
    }
}