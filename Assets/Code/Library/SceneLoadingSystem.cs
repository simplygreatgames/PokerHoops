using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimplyGreatGames.Systems
{
    public class SceneLoadingSystem : MonoBehaviour
    {
        public static SceneLoadingSystem Instance = null;

        public delegate void LevelLoaded(int sceneIndex);
        public event LevelLoaded OnLevelLoaded;

        private Animator animator;
        private bool isLoading = false;
        private int nextLevelIndex = 0;

        #region Animations

        [HideInInspector] public readonly string LoadLevelIn = "LoadIn";
        [HideInInspector] public readonly string LoadLevelOut = "LoadOut";

        #endregion

        #region Unity Methods & Events

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                animator = GetComponent<Animator>();
            }

            else Destroy(gameObject);
        }

        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnLevelLoadedInEvent() => OnLevelLoaded?.Invoke(SceneManager.GetActiveScene().buildIndex);
        public void OnLevelLoadedOutEvent() => SceneManager.LoadScene(nextLevelIndex);

        #endregion

        #region Loading Methods

        public void LoadLevel(int level)
        {
            if (isLoading) 
                return;

            isLoading = true;
            nextLevelIndex = level;
            animator.SetTrigger(LoadLevelOut);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GetComponent<Animator>().SetTrigger(LoadLevelIn);
            isLoading = false;
        }

        #endregion
    }
}

