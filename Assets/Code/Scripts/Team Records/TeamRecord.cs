using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class TeamRecord : MonoBehaviour
    {
        [SerializeField] private int teamRank;
        public int TeamRank { get => teamRank; set => teamRank = value; }

        [SerializeField] private List<RecordData> recordData = new List<RecordData>();
        public List<RecordData> RecordDataHistory { get => recordData; private set => recordData = value; }

        public RecordData RecordGameData(Game game)
        {
            Coach player = game.CoachesInGame[0];
            Coach opponent = game.CoachesInGame[1];

            RecordData recordData = new RecordData();
            
            recordData.PlayerID = player.CoachID;
            recordData.OpponentsID = opponent.CoachID;

            recordData.PlayerName = player.CoachName;
            recordData.OpponentsName = opponent.CoachName;

            recordData.PlayerBasketballScore = player.Hand.BasketballScore;
            recordData.OpponentBasketballScore = opponent.Hand.BasketballScore;

            recordData.PlayerHandType = player.Hand.PokerScore.PokerScoreType;
            recordData.OpponentHandType = opponent.Hand.PokerScore.PokerScoreType;

            recordData.PlayerHandValue = player.Hand.PokerScore.ScoreValue;
            recordData.OpponentHandValue = opponent.Hand.PokerScore.ScoreValue;

            if (!game.IsTiedGame)
                recordData.PlayerWon = game.WinningScore.ScoreOwner.IsHomePlayer;

            RecordDataHistory.Add(recordData);
            return recordData;
        }

        public bool HasPlayedOpponent(int coachId)
        {
            bool hasPlayed = false;

            foreach (RecordData recordData in RecordDataHistory)
            {
                if (recordData.OpponentsID == coachId)
                    hasPlayed = true;
            }

            return hasPlayed;
        }

        public RecordData GetLatestRecord()
        {
            if (RecordDataHistory.Count == 0) return null;
            else return RecordDataHistory[^1];
        }
    }
}
