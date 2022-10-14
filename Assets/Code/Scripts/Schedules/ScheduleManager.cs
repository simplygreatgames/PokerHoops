using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ScheduleManager : MonoBehaviour
    {
        public static ScheduleManager Instance;

        [SerializeField] private ScheduleScriptable scheduleScriptable;

        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Schedule Methods

        public void CreateSchedule(Player player)
        {
            player.TeamSchedule.CreateSchedule(scheduleScriptable);
        }

        #endregion
    }
}
