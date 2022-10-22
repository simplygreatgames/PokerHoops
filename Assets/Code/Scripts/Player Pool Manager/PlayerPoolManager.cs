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

        [SerializeField] private List<PlayerCoach> unmatchedPlayers = new List<PlayerCoach>();
        public List<PlayerCoach> UnmatchedPlayers { get => unmatchedPlayers; private set => unmatchedPlayers = value; }


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

        public void AssignPlayersToGames(List<Game> games, Enums.OpponentType roundOpponentType)
        {
            UnmatchedPlayers = PlayerPool;

            for (int i = 0; i < games.Count; i++)
            {
                switch (roundOpponentType)
                {
                    case Enums.OpponentType.Unranked:
                        Debug.Log("Creating Unranked Game");
                        CreateUnrankedGroup(games[i], PlayerPool[i]);
                        break;

                    case Enums.OpponentType.Ranked:
                        Debug.Log("Creating Ranked Game");
                        CreateRankedGroup(games[i], PlayerPool[i]);
                        break;

                    case Enums.OpponentType.HeadToHead:
                        Debug.Log("Creating Head to Head Game");
                        CreateHeadtoHeadGroup(games[i]);
                        break;

                    default:
                        Debug.LogError("OpponentType not found: " + games[i].RoundSettings.RoundType);
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

            cpuCoach.GetComponent<CPUCoach>().CpuType = Enums.CpuType.Unranked;
        }

        private void CreateRankedGroup(Game game, PlayerCoach player)
        {
            game.PlayersInGame.Add(player);
            player.transform.SetParent(game.transform);

            GameObject cpuCoach = Instantiate(CpuPrefab);
            cpuCoach.transform.SetParent(game.transform);

            cpuCoach.GetComponent<CPUCoach>().CpuType = Enums.CpuType.Ranked;
        }

        private void CreateHeadtoHeadGroup(Game game)
        {
            PlayerCoach playerMatching = UnmatchedPlayers[0];

            if (playerMatching == null)
                return;

            UnmatchedPlayers.RemoveAt(0);

            if (UnmatchedPlayers.Count == 0)
            {
                CreateUnrankedGroup(game, playerMatching);
                return;
            }

            for (int i = UnmatchedPlayers.Count - 1; i >= 0; i--)
            {
                PlayerCoach potentialOpponent = UnmatchedPlayers[i];

                if (playerMatching.TeamRecord.HasPlayedOpponent(potentialOpponent.CoachID))
                {
                    Debug.Log("I have played this opponent before");
                }

                else // Has not played Player Before
                {
                    Debug.Log("Adding Player to Game");
                    game.PlayersInGame.Add(playerMatching);
                    game.PlayersInGame.Add(potentialOpponent);

                    playerMatching.transform.SetParent(game.transform);
                    potentialOpponent.transform.SetParent(game.transform);

                    UnmatchedPlayers.RemoveAt(i);
                    break;
                }
            }
        }
        
        #endregion
    }
}
