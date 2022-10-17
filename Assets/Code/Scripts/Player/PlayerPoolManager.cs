using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class PlayerPoolManager : MonoBehaviour
    {
        public static PlayerPoolManager Instance;

        [SerializeField] List<Player> playerPool = new List<Player>();
        public List<Player> PlayerPool { get => playerPool; private set => playerPool = value; }

        [SerializeField] List<Player> playersNeedingGames = new List<Player>();
        public List<Player> PlayersNeedingGames { get => playersNeedingGames; private set => playersNeedingGames = value; }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #region Adding / Removing Players To Pool

        public void AddPlayerToPool(Player player)
        {
            if (PlayerPool.Contains(player))
            {
                Debug.Log("Tried to add player that was already in the player pool");
                return;
            }

            playerPool.Add(player);
        }

        public void RemovePlayerFromPool(Player player)
        {
            if (PlayerPool.Contains(player))
                playerPool.Remove(player);

            else Debug.Log("Tried to remove player that was not in the pool");
        }

        #endregion

        #region Assigning Players to Game

        public void AssignPlayersToGames(List<Game> games)
        {
            PlayersNeedingGames = PlayerPool;

            for (int i = 0; i < games.Count; i++)
            {
                switch (games[i].GameType)
                {
                    case Enums.OpponentType.Unranked:
                        CreateUnrankedGroup(games[i], playersNeedingGames[i]);
                        break;

                    case Enums.OpponentType.Ranked:
                        CreateRankedGroup(games[i], PlayersNeedingGames[i]);
                        break;

                    case Enums.OpponentType.HeadToHead:
                        CreateHeadtoHeadGroup(games[i], new List<Player> { PlayersNeedingGames[i], PlayersNeedingGames[i + 1] } );
                        break;

                    default:
                        Debug.LogError("OpponentType not found: " + games[i].GameType);
                        break;
                }
            }
        }

        private void CreateUnrankedGroup(Game game, Player player)
        {
            game.PlayersInGame.Add(player);
            player.transform.SetParent(game.transform);
            // Generate Unranked Opponent
        }

        private void CreateRankedGroup(Game game, Player player)
        {
            game.PlayersInGame.Add(player);
            // Generate Ranked Opponent
        }

        private void CreateHeadtoHeadGroup(Game game, List<Player> players)
        {
            foreach (Player player in players)
                game.PlayersInGame.Add(player);
        }
        
        #endregion
    }
}
