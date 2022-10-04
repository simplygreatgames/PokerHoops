using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        [Header("Dependencies")]
        [SerializeField] private GameObject GamePrefab = null;

        [Header("Game Data")]
        [SerializeField] private Game currentGame;
        public Game CurrentGame
        {
            get => currentGame;
            set => currentGame = value;
        }

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Saving / Loading Game

        public void LoadNewGame(GameSettings gameSettings)
        {
            if (GamePrefab == null)
            {
                Debug.LogError("Must have Game Prefab Set");
                return;
            }

            GameObject loadedGameObj = Instantiate(GamePrefab, this.transform);
            Game loadedGame = loadedGameObj.GetComponent<Game>();

            loadedGame.GameSettings = gameSettings;
            CurrentGame = loadedGame;
        }

        #endregion
    }
}
