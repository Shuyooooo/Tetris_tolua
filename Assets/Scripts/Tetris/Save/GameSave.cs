namespace Tetris.Save
{
    using UnityEngine;

    /// <summary>
    /// 游戏数据持久化。WebGL下 PlayerPrefs会存到浏览器Local Storage,刷新/关闭后仍保留。
    /// </summary>
    public static class GameSave
    {
        private const string KeyHighScore = "Tetris_HighScore";

        public static int GetHighScore()
        {
            return PlayerPrefs.GetInt(KeyHighScore, 0);
        }

        public static void SetHighScore(int score)
        {
            PlayerPrefs.SetInt(KeyHighScore, score);
            PlayerPrefs.Save();
        }
    }
}
