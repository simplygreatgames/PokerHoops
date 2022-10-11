using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class PlayerPoolManager : MonoBehaviour
    {
        public static PlayerPoolManager Instance;

        [SerializeField] List<Player> playerPool = new List<Player>();
        public List<Player> PlayerPool { get => playerPool; private set => playerPool = value; }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #region Adding / Removing Players

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
    }
}
