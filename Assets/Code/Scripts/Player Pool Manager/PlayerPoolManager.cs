using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class PlayerPoolManager : MonoBehaviour
    {
        public static PlayerPoolManager Instance;

        [Header("Dependencies")]
        [SerializeField] private Transform playerPoolSpawnPoint = null;

        [SerializeField] private GameObject playerPrefab = null;
        public GameObject PlayerPrefab { get => playerPrefab; set => playerPrefab = value; }

        [SerializeField] private GameObject cpuPrefab = null;
        public GameObject CpuPrefab { get => cpuPrefab; set => cpuPrefab = value; }

        [Header("Info")]
        [SerializeField] private List<PlayerCoach> playerPool = new List<PlayerCoach>();
        public List<PlayerCoach> PlayerPool { get => playerPool; private set => playerPool = value; }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #region Adding / Removing Players To Pool

        public void InstantiateNewPlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                GameObject playerObj = Instantiate(PlayerPrefab, playerPoolSpawnPoint);
                AddPlayerToPool(playerObj.GetComponent<PlayerCoach>());
            }
        }

        public void AddPlayerToPool(PlayerCoach player)
        {
            if (PlayerPool.Contains(player))
            {
                Debug.Log("Tried to add player that was already in the player pool");
                return;
            }

            PlayerPool.Add(player);
        }

        public void RemovePlayerFromPool(PlayerCoach player)
        {
            if (PlayerPool.Contains(player))
                PlayerPool.Remove(player);

            else Debug.Log("Tried to remove player that was not in the pool");
        }

        #endregion

        #region Assigning Players to Game

        public void AssignPlayersToGames(List<Game> games)
        {
            for (int i = 0; i < games.Count; i++)
            {
                switch (games[i].GameSettings.OpponentType)
                {
                    case Enums.OpponentType.Unranked:
                        CreateUnrankedGroup(games[i], PlayerPool[i]);
                        break;

                    case Enums.OpponentType.Ranked:
                        CreateRankedGroup(games[i], PlayerPool[i]);
                        break;

                    case Enums.OpponentType.HeadToHead:
                        CreateHeadtoHeadGroup(games[i], new List<PlayerCoach> { PlayerPool[i], PlayerPool[i + 1] } );
                        break;

                    default:
                        Debug.LogError("OpponentType not found: " + games[i].GameSettings.OpponentType);
                        break;
                }
            }
        }

        private void CreateUnrankedGroup(Game game, PlayerCoach player)
        {
            game.PlayersInGame.Add(player);
            player.transform.SetParent(game.transform);

            GameObject cpuCoach = Instantiate(CpuPrefab);
            cpuCoach.transform.SetParent(game.transform);
        }

        private void CreateRankedGroup(Game game, PlayerCoach player)
        {
            game.PlayersInGame.Add(player);
            // Generate Ranked Opponent
        }

        private void CreateHeadtoHeadGroup(Game game, List<PlayerCoach> players)
        {
            foreach (PlayerCoach player in players)
                game.PlayersInGame.Add(player);
        }
        
        #endregion
    }
}
