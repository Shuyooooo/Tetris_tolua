namespace Tetris.Piece
{
    using Tetris.ChessBoardSet;
    using Tetris.Utilities;
    using UnityEngine;

    /// <summary>
    /// 当前方块的移动控制与检测
    /// </summary>
    public class PieceController
    {
        private PieceSpawner pieceSpawner;
        private ChessBoard chessBoard;

        public PieceController(PieceSpawner pieceSpawner, ChessBoard chessBoard)
        {
            this.pieceSpawner = pieceSpawner;
            this.chessBoard = chessBoard;
        }

        /// <summary>
        /// 处理输入
        /// </summary>
        public bool HandleInput(CurrentPiece currentPiece, RectTransform baseSquareTransform)
        {
            if (baseSquareTransform != null && currentPiece != null)
            {
                //向左移动
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    TryMoveHorizontal(currentPiece, baseSquareTransform, -1);
                }
                //向右移动
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TryMoveHorizontal(currentPiece, baseSquareTransform, 1);
                }
                //顺时针旋转
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RotateClockwise(currentPiece);
                    pieceSpawner.RefreshPiecePoints(currentPiece.Points, baseSquareTransform);
                }

                return Input.GetKey(KeyCode.DownArrow);
            }

            return false;
        }

        /// <summary>
        /// 水平移动
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="baseSquareTransform"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool TryMoveHorizontal(CurrentPiece currentPiece, RectTransform baseSquareTransform, int direction)
        {
            Vector2Int nextPosition = currentPiece.Position + new Vector2Int(direction, 0);
            if (!chessBoard.IsValidPosition(nextPosition, currentPiece.Points))
            {
                return false;
            }//如果能够继续移动就更新坐标
            else
            {
                currentPiece.Position = nextPosition;
                pieceSpawner.IndexToUIPosition(currentPiece, baseSquareTransform);
                return true;
            }
        }

        /// <summary>
        /// 向下速移
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="baseSquareTransform"></param>
        /// <returns></returns>
        public bool TryMoveDown(CurrentPiece currentPiece, RectTransform baseSquareTransform)
        {
            Vector2Int nextPosition = currentPiece.Position + new Vector2Int(0, -1);
            if (!chessBoard.IsValidPosition(nextPosition, currentPiece.Points))
            {
                return false;
            }//如果能够继续移动就更新坐标
            else
            {
                //更新方块的逻辑位置
                currentPiece.Position = nextPosition;
                //用逻辑坐标映射UI坐标
                pieceSpawner.IndexToUIPosition(currentPiece, baseSquareTransform);
                return true;
            }
        }

        public bool CanMoveDown(CurrentPiece currentPiece)
        {
            Vector2Int nextPosition = currentPiece.Position + new Vector2Int(0, -1);
            return chessBoard.IsValidPosition(nextPosition, currentPiece.Points);
        }

        /// <summary>
        /// 顺时针旋转
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="currentTypeTransform"></param>
        private void RotateClockwise(CurrentPiece currentPiece)
        {
            //+1就是旋转90度，%4是为了保证旋转后仍然在0-3的范围内
            int nextRotation = (currentPiece.Rotation + 1) % 4;
            Vector2Int[] nextPoints = PieceData.GetCells(currentPiece.Type, nextRotation);
            if (!chessBoard.IsValidPosition(currentPiece.Position, nextPoints))
            {
                return;
            }

            currentPiece.Rotation = nextRotation;
            currentPiece.Points = nextPoints;
        }
    }
}
