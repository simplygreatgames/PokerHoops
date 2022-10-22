using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ScheduleManager : MonoBehaviour
    {
        public static ScheduleManager Instance;

        [SerializeField] private ScheduleScriptable scheduleScriptable;
        public ScheduleScriptable ScheduleScriptable { get => scheduleScriptable; set => scheduleScriptable = value; }

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Schedule Methods

        public void CreateSchedule(PlayerCoach player)
        {
            player.TeamSchedule.CreateSchedule(scheduleScriptable);
        }

        #endregion
    }
}
