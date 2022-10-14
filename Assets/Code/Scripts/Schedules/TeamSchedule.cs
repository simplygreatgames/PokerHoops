using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class TeamSchedule : MonoBehaviour
    {
        [SerializeField] private int teamRank;
        public int TeamRank { get => teamRank; set => teamRank = value; }

        [SerializeField] private ScheduleLine[] scheduleLines;
        public ScheduleLine[] ScheduleLines { get => scheduleLines; set => scheduleLines = value; }

        public void CreateSchedule(ScheduleScriptable scheduleScriptable)
        {
            ScheduleLines = new ScheduleLine[scheduleScriptable.Opponents.Length];

            for (int i = 0; i < scheduleScriptable.Opponents.Length; i++)
            {
                ScheduleLines[i] = new ScheduleLine()
                {
                    LineNumber = i,
                    OpponentType = scheduleScriptable.Opponents[i],
                    ScheduleLineData = new ScheduleLineData()
                };
            }
        }

    }
}
