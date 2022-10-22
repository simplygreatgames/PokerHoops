using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class CPUCoach : Coach
    {
        [SerializeField] private Enums.CpuType cpuType;
        public Enums.CpuType CpuType 
        { 
            get => cpuType;
            set
            {
                cpuType = value;

                switch (CpuType)
                {
                    case Enums.CpuType.Unranked:
                        CoachName = "CPU (Unranked)";
                        break;

                    case Enums.CpuType.Ranked:
                        CoachName = "CPU (Ranked)";
                        break;

                    default:
                        Debug.LogWarning("Unrecongnized CPU Type");
                        break;
                }
            }
        }

        #region Unity Methods & Initialization
        
        public void Awake()
        {
            CoachID = gameObject.GetInstanceID();

            GetComponents();
            RegisterComponents();
        }

        private void GetComponents()
        {
            if (Hand == null)
                Hand = GetComponentInChildren<Hand>();
        }

        private void RegisterComponents()
        {
            Hand.RegisterHand(this);
        }

        #endregion
    }
}
