namespace Tetris.ChessBoardSet
{
    using Tetris.Piece;
    using UnityEngine;

    /// <summary>
    /// 棋盘类包含棋盘检测
    /// </summary>
    public class ChessBoard
    {
        public int boardWidth;
        public int boardHeight;

        private int[,] cells;

        public ChessBoard(int width, int height)
        {
            this.boardWidth = width;
            this.boardHeight = height;
            InitBoard(width, height);
        }

        /// <summary>
        /// 棋盘初始化
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void InitBoard(int width, int height)
        {
            cells = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = 0;
                }
            }
        }

        /// <summary>
        /// 检测位置合法性
        /// </summary>
        /// <param name="piecePosition"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool IsValidPosition(Vector2Int piecePosition, Vector2Int[] points)
        {
            foreach (var point in points)
            {
                //预算下一次坐标位置
                int x = piecePosition.x + point.x;
                int y = piecePosition.y + point.y;

                //如果超出边界则坐标不可用
                if (x < 0 || x >= boardWidth || y < 0 || y >= boardHeight)
                {
                    return false;
                }
                //如果已被占用则不可用
                if (cells[x, y] == 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 写入棋盘占用
        /// </summary>
        /// <param name="currentPiece"></param>
        public void SetBoardStatus(CurrentPiece currentPiece)
        {
            foreach (var point in currentPiece.Points)
            {
                Vector2Int index = currentPiece.Position + point;
                cells[index.x, index.y] = 1;
            }
        }

        /// <summary>
        /// 判定棋盘行是否可消
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsRowFull(int y)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                if (cells[x, y] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 消除行
        /// </summary>
        /// <param name="y"></param>
        public void ClearRow(int y)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                cells[x, y] = 0;
            }
        }

        /// <summary>
        /// 重置棋盘（清空所有格子，用于重开）
        /// </summary>
        public void Reset()
        {
            InitBoard(boardWidth, boardHeight);
        }

        /// <summary>
        /// 行下降
        /// </summary>
        /// <param name="startY"></param>
        public void ShiftRowsDown(int startY)
        {
            for (int y = startY; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    cells[x, y - 1] = cells[x, y];
                }
            }

            for (int x = 0; x < boardWidth; x++)
            {
                cells[x, boardHeight - 1] = 0;
            }
        }
    }
}