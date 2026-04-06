namespace Tetris.ChessBoardSet
{
    using Tetris.Const;
    using Tetris.Piece;
    using Tetris.Save;
    using Tetris.UI;
    using UnityEngine;
    using Tetris.Enums;
    using UnityEngine.UI;

    /// <summary>
    /// 游戏棋盘类
    /// </summary>
    public class Game : MonoBehaviour
    {
        public GameObject baseSquare;
        public RectTransform settledPiecesRoot;
        public ScoreUI scoreUI;
        public Image GameOverImage;
        public int Width;
        public int Height;

        private ChessBoard chessBoard;
        private int currentScore;
        private int highScore;
        private CurrentPiece currentPiece;
        private PieceSpawner pieceSpawner;
        private PieceController pieceController;
        private RectTransform baseSquareTransform;
        private RectTransform[,] settledViews;
        private float deltaTime;
        //存储当前棋盘的状态，来判断方块是否碰撞了
        private GameState gameState = GameState.Init;

        private void Awake()
        {
            //初始化棋盘
            chessBoard = new ChessBoard(Width, Height);
            //初始化当前的方块类
            currentPiece = new CurrentPiece();
            pieceSpawner = new PieceSpawner();
            pieceController = new PieceController(pieceSpawner, chessBoard);
            settledViews = new RectTransform[Width, Height];
            highScore = GameSave.GetHighScore();
        }

        private void Start()
        {
            baseSquareTransform = (RectTransform)baseSquare.transform;
            //挂载调试脚本.TODO 后续可以删除
            var debug = baseSquare.GetComponent<PieceDebugView>();
            debug.Bind(currentPiece);

            //设置默认场景表现层UI
            if (scoreUI != null)
            {
                scoreUI.SetScore(0);
                scoreUI.SetHighScore(highScore);
                scoreUI.ShowStartButton(true);
                scoreUI.ShowRestartButton(false);
                scoreUI.OnStartClicked += StartGame;
                scoreUI.OnRestartClicked += RestartGame;
            }

            //初始化基准方块，后续根据这个基准方块来生成不同类型的方块
            baseSquare.SetActive(false);
            // 开局时隐藏 GameOver 图
            if (GameOverImage != null)
                GameOverImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 点击「开始」：从 Init 进入 Running，生成第一个方块并开始下落。
        /// </summary>
        public void StartGame()
        {
            gameState = GameState.Running;
            currentScore = 0;
            deltaTime = 0f;
            scoreUI.SetScore(0);
            scoreUI.ShowStartButton(false);
            scoreUI.ShowRestartButton(true);
            GameOverImage.gameObject.SetActive(false);

            baseSquare.SetActive(true);
            pieceSpawner.SpawnPiece(pieceSpawner.GetRandomPieceType(), 0, currentPiece, baseSquareTransform, Width, Height);
            if (!chessBoard.IsValidPosition(currentPiece.Position, currentPiece.Points))
            {
                OnGameOver();
                return;
            }
        }

        /// <summary>
        /// 点击「重开」：清空棋盘与落地方块，重置分数，进入 Running。
        /// </summary>
        public void RestartGame()
        {
            gameState = GameState.Running;
            chessBoard.Reset();
            ClearAllSettledPieces();
            currentScore = 0;
            deltaTime = 0f;
            if (scoreUI != null)
            {
                scoreUI.SetScore(0);
                scoreUI.SetHighScore(highScore);
                scoreUI.ShowRestartButton(false);
                scoreUI.ShowRestartButton(true);
            }

            GameOverImage.gameObject.SetActive(false);
            baseSquare.SetActive(true);
            pieceSpawner.SpawnPiece(pieceSpawner.GetRandomPieceType(), 0, currentPiece, baseSquareTransform, Width, Height);
            if (!chessBoard.IsValidPosition(currentPiece.Position, currentPiece.Points))
            {
                OnGameOver();
            }
        }

        /// <summary>
        /// 清空棋盘
        /// </summary>
        private void ClearAllSettledPieces()
        {
            for (int y = 0; y < Height; y++)
            {

                for (int x = 0; x < Width; x++)
                {
                    if (settledViews[x, y] != null)
                    {
                        //清除表现
                        Destroy(settledViews[x, y].gameObject);
                        settledViews[x, y] = null;
                    }
                }
            }

            if (settledPiecesRoot != null)
            {
                for (int i = settledPiecesRoot.childCount - 1; i >= 0; i--)
                {
                    Destroy(settledPiecesRoot.GetChild(i).gameObject);
                }
            }
        }

        /// <summary>
        /// 结束游戏
        /// </summary>
        private void OnGameOver()
        {
            gameState = GameState.GameOver;
            //更新最高分
            if (currentScore > highScore)
            {
                highScore = currentScore;
                GameSave.SetHighScore(highScore);
            }
            if (scoreUI != null)
            {
                scoreUI.SetHighScore(highScore);
                scoreUI.ShowRestartButton(true);
            }
            // 弹出 GameOver 图
            if (GameOverImage != null)
            {
                GameOverImage.gameObject.SetActive(true);
            }

            baseSquare.SetActive(false);
        }

        private void Update()
        {
            if (gameState == GameState.Running)
            {
                bool isFastDrop = pieceController.HandleInput(currentPiece, baseSquareTransform);
                DropDown(isFastDrop);
            }
        }

        /// <summary>
        /// 方块下降方法
        /// </summary>
        private void DropDown(bool isFastDrop)
        {
            float dropInterval = isFastDrop ? GameConst.DropInterval * 0.1f : GameConst.DropInterval;
            //一秒下降一格
            if (deltaTime >= dropInterval)
            {
                bool moved = pieceController.TryMoveDown(currentPiece, baseSquareTransform);
                if (!moved)
                {
                    OnPieceLanded();
                }

                deltaTime = 0f;
            }
            else
            {
                deltaTime += Time.deltaTime;
            }
        }

        private void OnPieceLanded()
        {
            // 1) 固化当前块到数据和显示
            chessBoard.SetBoardStatus(currentPiece);
            RenderSettledPiece();
            // 2) 清行并同步下移
            ClearLines();
            // 3) 生成下一块
            SpawnActivePiece();
        }

        private void SpawnActivePiece()
        {
            pieceSpawner.SpawnPiece(pieceSpawner.GetRandomPieceType(), 0, currentPiece, baseSquareTransform, Width, Height);
            if (!chessBoard.IsValidPosition(currentPiece.Position, currentPiece.Points))
            {
                OnGameOver();
            }
        }

        /// <summary>
        /// 复写方块方法
        /// </summary>
        private void RenderSettledPiece()
        {
            for (int i = 0; i < currentPiece.Points.Length; i++)
            {
                Vector2Int index = currentPiece.Position + currentPiece.Points[i];
                RectTransform sourceCell = (RectTransform)baseSquareTransform.GetChild(i);
                GameObject settledCell = Instantiate(sourceCell.gameObject, settledPiecesRoot);
                var settledCellTransform = (RectTransform)settledCell.transform;
                settledCellTransform.anchoredPosition = new Vector2(index.x * GameConst.CellSize, index.y * GameConst.CellSize);
                //更新视觉集合
                settledViews[index.x, index.y] = settledCellTransform;
            }
        }

        /// <summary>
        /// 消除指定行方法(表现 + 数据)
        /// </summary>
        private void ClearLines()
        {
            int linesCleared = 0;
            for (int y = 0; y < Height; y++)
            {
                if (!chessBoard.IsRowFull(y))
                {
                    continue;
                }
                linesCleared++;
                ClearVisualRow(y);
                chessBoard.ClearRow(y);
                ShiftVisualRowsDown(y + 1);
                chessBoard.ShiftRowsDown(y + 1);
                y--;
            }

            if (linesCleared > 0 && scoreUI != null)
            {
                currentScore += linesCleared * GameConst.ScorePerLine;
                scoreUI.SetScore(currentScore);
            }
        }

        /// <summary>
        /// 表现层消除行
        /// </summary>
        /// <param name="y"></param>
        private void ClearVisualRow(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                RectTransform cell = settledViews[x, y];
                if (cell != null)
                {
                    Destroy(cell.gameObject);
                    settledViews[x, y] = null;
                }
            }
        }

        /// <summary>
        /// 其他行表现下降
        /// </summary>
        /// <param name="startY"></param>
        private void ShiftVisualRowsDown(int startY)
        {
            for (int y = startY; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    RectTransform cell = settledViews[x, y];
                    settledViews[x, y - 1] = cell;
                    if (cell != null)
                    {
                        cell.anchoredPosition = new Vector2(x * GameConst.CellSize, (y - 1) * GameConst.CellSize);
                    }

                    settledViews[x, y] = null;
                }
            }
        }
    }
}

