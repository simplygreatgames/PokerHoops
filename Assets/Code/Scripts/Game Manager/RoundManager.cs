using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance = null;

        [Header("Dependencies")]
        [SerializeField] private GameObject GamePrefab = null;

        [Header("Round Info")]
        [SerializeField] private int currentRound;
        public int CurrentRound { get => currentRound; set => currentRound = value; }

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

        public void GenerateRoundOfGames(GameSettings gameSettings, Enums.OpponentType opponentType)
        {
            if (GamePrefab == null)
            {
                Debug.LogError("Must have Game Prefab Set");
                return;
            }

            CurrentRoundOfGames.Clear();
            CurrentRound = SeasonManager.Instance.CurrentSeason.RoundNumber;

            switch (opponentType)
            {
                case Enums.OpponentType.Unranked:

                    for (int i = 0; i < SeasonManager.Instance.CurrentSeason.Players.Count; i++)
                    {
                        GameObject loadedGameObj = Instantiate(GamePrefab, this.transform);
                        Game loadedGame = loadedGameObj.GetComponent<Game>();
                        
                        loadedGame.GameSettings = gameSettings;
                        loadedGame.GameType = opponentType;
                        CurrentRoundOfGames.Add(loadedGame);
                    }

                    break;

                case Enums.OpponentType.Ranked:
                    break;

                case Enums.OpponentType.HeadToHead:
                    break;

                default:
                    break;
            }

            PlayerPoolManager.Instance.AssignPlayersToGames(CurrentRoundOfGames);
        }

        #endregion
    }
}
