namespace Tetris.Piece
{
    using Tetris.Const;
    using Tetris.Utilities;
    using UnityEngine;
    using Tetris.Enums;

    /// <summary>
    /// 当前方块的生成与应用
    /// </summary>
    public class PieceSpawner
    {
        /// <summary>
        /// 获取随机的方块类型
        /// </summary>
        /// <returns></returns>
        public PieceType GetRandomPieceType()
        {
            return (PieceType)Random.Range(0, 7);
        }

        /// <summary>
        /// 生成方块
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rotation"></param>
        /// <param name="currentPiece"></param>
        /// <param name="currentTypeTransform"></param>
        /// <param name="boardWidth"></param>
        /// <param name="boardHeight"></param>
        public void SpawnPiece(PieceType type, int rotation, CurrentPiece currentPiece, RectTransform currentTypeTransform, int boardWidth, int boardHeight)
        {
            currentPiece.Type = type;
            currentPiece.Rotation = ((rotation % 4) + 4) % 4;
            currentPiece.Position = new Vector2Int(boardWidth / 2, boardHeight - 2);
            currentPiece.Points = PieceData.GetCells(type, rotation);
            //刷新方块
            RefreshPiecePoints(currentPiece.Points, currentTypeTransform);
            IndexToUIPosition(currentPiece, currentTypeTransform);
        }

        /// <summary>
        /// 刷新子块位置的逻辑坐标数据
        /// </summary>
        /// <param name="points"></param>
        /// <param name="baseSquareTransform"></param>
        public void RefreshPiecePoints(Vector2Int[] points, RectTransform baseSquareTransform)
        {
            for (int i = 0; i < points.Length; i++)
            {
                RectTransform cell = baseSquareTransform.GetChild(i) as RectTransform;
                cell.anchoredPosition = new Vector2(points[i].x * GameConst.CellSize, points[i].y * GameConst.CellSize);
            }
        }

        /// <summary>
        /// 刷新方块根节点的UI坐标
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="baseSquareTransform"></param>
        public void IndexToUIPosition(CurrentPiece currentPiece, RectTransform baseSquareTransform)
        {
            baseSquareTransform.anchoredPosition = new Vector2(currentPiece.Position.x * GameConst.CellSize, currentPiece.Position.y * GameConst.CellSize);
        }
    }
}
