namespace Tetris.Manager
{
    using UnityEngine;

    public class GameTimeManager : MonoBehaviour
    {
        private float elapseTime;

        public float ElapseTime => elapseTime;
        public static GameTimeManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            elapseTime += Time.deltaTime;
        }
    }
}
