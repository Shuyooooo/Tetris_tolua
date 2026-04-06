namespace Tetris.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>
    /// 积分界面。必须和 UIDocument 挂在同一个物体上，并在 UIDocument 里指定 Source 和 Panel Settings。
    /// 提供开始、重开按钮及最高分显示。
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        private Label scoreLabel;
        private Label highScoreLabel;
        private Button startButton;
        private Button restartButton;

        /// <summary> 点击「开始」时触发，由 Game 订阅。 </summary>
        public event Action OnStartClicked;

        /// <summary> 点击「重开」时触发，由 Game 订阅。 </summary>
        public event Action OnRestartClicked;

        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                Debug.LogWarning("ScoreUI: 当前物体上没有 UIDocument，请把 ScoreUI 和 UIDocument 挂在同一物体（如 bg）上。");
                return;
            }
            // 让 UI Toolkit 面板画在 Canvas 上方，否则按钮会被 Canvas 挡住点不到
            uiDocument.sortingOrder = 15;

            VisualElement root = uiDocument.rootVisualElement;
            scoreLabel = root.Q<Label>("scoreLabel");
            highScoreLabel = root.Q<Label>("highScoreLabel");
            startButton = root.Q<Button>("startButton");
            restartButton = root.Q<Button>("restartButton");

            if (startButton != null)
            {
                startButton.clicked += () => OnStartClicked?.Invoke();
            }
            if (restartButton != null)
            {
                restartButton.clicked += () => OnRestartClicked?.Invoke();
                restartButton.style.display = DisplayStyle.None;
            }
        }

        /// <summary> 更新当前分数显示 </summary>
        public void SetScore(int score)
        {
            if (scoreLabel != null)
            {
                scoreLabel.text = score.ToString();
            }
        }

        /// <summary> 更新最高分显示 </summary>
        public void SetHighScore(int score)
        {
            highScoreLabel.text = score.ToString();
        }

        /// <summary> 显示/隐藏「开始」按钮 </summary>
        public void ShowStartButton(bool show)
        {
            startButton.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }

        /// <summary> 显示/隐藏「重开」按钮 </summary>
        public void ShowRestartButton(bool show)
        {
            restartButton.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
