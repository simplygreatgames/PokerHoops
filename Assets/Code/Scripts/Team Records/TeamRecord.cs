using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class TeamRecord : MonoBehaviour
    {
        [SerializeField] private int teamRank;
        public int TeamRank { get => teamRank; set => teamRank = value; }

        public List<RecordData> RecordData = new List<RecordData>();

        public bool HasPlayedOpponent(int coachId)
        {
            bool hasPlayed = false;

            foreach (RecordData recordData in RecordData)
            {
                if (recordData.CoachID == coachId)
                    hasPlayed = true;
            }

            return hasPlayed;
        }
    }
}
