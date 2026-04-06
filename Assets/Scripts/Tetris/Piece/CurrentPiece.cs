namespace Tetris.Piece
{
    using Tetris.Enums;
    using UnityEngine;

    /// <summary>
    /// 当前方块的坐标点
    /// </summary>
    public class CurrentPiece
    {
        public PieceType Type;
        public int Rotation;
        public Vector2Int[] Points;
        public Vector2Int Position;
    }
}
