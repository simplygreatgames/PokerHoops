using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance = null;

        [Header("Dependencies")]
        [SerializeField] private GameObject GamePrefab = null;

        [Header("Game Data")]
        [SerializeField] private List<Game> currentRoundOfGames;
        public List<Game> CurrentRoundOfGames
        {
            get => currentRoundOfGames;
            set => currentRoundOfGames = value;
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
            CurrentRoundOfGames.Add(loadedGame);
        }

        #endregion
    }
}
