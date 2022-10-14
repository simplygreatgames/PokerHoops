using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CreateAssetMenu(fileName = "Schedule", menuName = "Scriptables/Schedules/Schedule")]
    public class ScheduleScriptable : ScriptableObject
    {
        public Enums.OponentType[] Opponents;
    }
}
