namespace Tetris.Utilities
{
    using Tetris.Enums;
    using UnityEngine;

    /// <summary>
    /// 方块类型数据定义
    /// </summary>
    public static class PieceData
    {
        // I
        public static readonly Vector2Int[][] I = new Vector2Int[][]
        {
            new[] { new Vector2Int(-2, 0), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(0, 2), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) },
            new[] { new Vector2Int(-2, 0), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(0, 2), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) },
        };

        // O
        public static readonly Vector2Int[][] O = new Vector2Int[][]
        {
            new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
            new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
            new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
            new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
        };

        // T
        public static readonly Vector2Int[][] T = new Vector2Int[][]
        {
            new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) },
            new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) },
            new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, -1) },
            new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) },
        };

        // S
        public static readonly Vector2Int[][] S = new Vector2Int[][]
        {
            new[] { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0) },
            new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1) },
            new[] { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0) },
            new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1) },
        };

        // Z
        public static readonly Vector2Int[][] Z = new Vector2Int[][]
        {
            new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, -1) },
            new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, -1) },
        };

        // J
        public static readonly Vector2Int[][] J = new Vector2Int[][]
        {
            new[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) },
            new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1) },
            new[] { new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },
        };

        // L
        public static readonly Vector2Int[][] L = new Vector2Int[][]
        {
            new[] { new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
            new[] { new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) },
            new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-1, -1) },
            new[] { new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1) },
        };

        /// <summary>
        /// 通过枚举返回类型基准坐标点
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Vector2Int[] GetCells(PieceType type, int rotation)
        {
            rotation = ((rotation % 4) + 4) % 4;

            return type switch
            {
                PieceType.I => I[rotation],
                PieceType.O => O[rotation],
                PieceType.T => T[rotation],
                PieceType.S => S[rotation],
                PieceType.Z => Z[rotation],
                PieceType.J => J[rotation],
                PieceType.L => L[rotation],
                _ => T[rotation],
            };
        }
    }
}