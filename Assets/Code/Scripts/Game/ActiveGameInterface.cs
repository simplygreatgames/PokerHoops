using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ActiveGameInterface : MonoBehaviour
    {
        public static ActiveGameInterface Instance = null;

        [Header("Dependencies")]
        [SerializeField] private ActiveCoachInterface coachActiveLeft = null;
        public ActiveCoachInterface CoachActiveLeft
        {
            get => coachActiveLeft;
        }

        [SerializeField] private ActiveCoachInterface coachActiveRight = null;
        public ActiveCoachInterface CoachActiveRight
        {
            get => coachActiveRight;
        }

        [Header("Debug Information")]
        public bool SpoofPlayerOwnerhip = true;

        [SerializeField] private Game activeGame = null;
        public Game ActiveGame
        {
            get => activeGame;
            set
            {
                activeGame = value;
                InitializeInterface();
            }
        }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void InitializeInterface()
        {
            if (ActiveGame == null)
            {
                Debug.LogError("Could not find Active Game");
                return;
            }

            else if(ActiveGame.CoachesInGame[0] == null || ActiveGame.CoachesInGame[1] == null)
            {
                Debug.LogError("Missing Active Coach");
                return;
            }

            CoachActiveLeft.ActiveCoach = ActiveGame.CoachesInGame[0];
            CoachActiveRight.ActiveCoach = ActiveGame.CoachesInGame[1];

            if (CoachActiveLeft.ActiveCoach is PlayerCoach)
            {
                PlayerCoach activePlayerCoach = CoachActiveLeft.ActiveCoach as PlayerCoach;
                activePlayerCoach.IsLocalPlayer = true;
                activePlayerCoach.MouseInput.IsReadingCards = true;
            }
        }
    }
}
