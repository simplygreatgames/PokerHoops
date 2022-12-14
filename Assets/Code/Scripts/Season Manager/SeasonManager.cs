using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class SeasonManager : MonoBehaviour
    {
        public static SeasonManager Instance;

        [Header("Settings")]
        [SerializeField] private GameObject seasonPrefab;

        [SerializeField] private int numberOfPlayers = 0;
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }

        public Season CurrentSeason { get; private set; }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #region Season Management

        public void LoadNewSeason(SeasonSettings seasonSettings)
        {
            GameObject seasonObj = Instantiate(seasonPrefab, transform);
            CurrentSeason = seasonObj.GetComponent<Season>();

            CurrentSeason.Settings = seasonSettings;
        }

        public void StartNewSeason()
        {
            SeasonSettings seasonSettings = new SeasonSettings()
            {
                NumberOfPlayers = this.NumberOfPlayers
            };

            LoadNewSeason(seasonSettings);
            CurrentSeason.StateMachine.SetSeasonState(new StartSeasonState(CurrentSeason.StateMachine));
        }
        public void StartSeasonLoaded()
        {
            if (CurrentSeason == null)
            {
                Debug.LogError("Must Load season first or use the startt new season method");
                return;
            }
                
            CurrentSeason.StateMachine.SetSeasonState(new StartSeasonState(CurrentSeason.StateMachine));
        }

        #endregion

        #region Round Management

        public void MarkRoundComplete()
        {
            CurrentSeason.RoundNumber++;

            RunSeasonState runSeasonState = (RunSeasonState) CurrentSeason.StateMachine.CurrentState;
            runSeasonState.GenerateNextRoundOfGames();
        }

        #endregion
    }
}
