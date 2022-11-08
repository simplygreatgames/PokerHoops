using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [System.Serializable]
    public class RecordData
    {
        public bool PlayerWon;

        [Header("Player")]
        public int PlayerID = -1;
        public string PlayerName = string.Empty;
        public int PlayerBasketballScore = 0;
        public Enums.PokerScoreType PlayerHandType;
        public int PlayerHandValue = 0;

        [Header("Player")]
        public int OpponentsID = -1;
        public string OpponentsName = string.Empty;
        public int OpponentBasketballScore = 0;
        public Enums.PokerScoreType OpponentHandType;
        public int OpponentHandValue = 0;
    }
}
