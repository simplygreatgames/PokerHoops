using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class SeasonState : State
    {
        protected SeasonStateMachine StateMachine;
        public SeasonState(SeasonStateMachine stateMachine) => StateMachine = stateMachine;

        public override void OnStateEnter()
        {
            StateMachine.AnimateState(this);
        }

        public override void OnStateExit()
        {
        }

        public override void Tick()
        {
        }
    }

    #region Season States

    public class InitializeSeasonState : SeasonState
    {
        private bool initializeWasSuccess = true;

        public InitializeSeasonState(SeasonStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            InstantiateNewPlayers();
            PullPlayersFromPool();
            InitializeStats();
            CompleteInitialization();
        }

        private void InstantiateNewPlayers()
        {
            PlayerPoolManager.Instance.InstantiateNewPlayers(StateMachine.Season.Settings.NumberOfPlayers);
        }

        private void PullPlayersFromPool()
        {
            StateMachine.Season.PlayerCoaches = PlayerPoolManager.Instance.PlayerPool;

            if (StateMachine.Season.PlayerCoaches.Count == 0)
            {
                Debug.LogWarning("Did not find any players in the pull");
                initializeWasSuccess = false;
            }
        }

        private void InitializeStats()
        {
            StateMachine.Season.Stats = new SeasonStats();
        }

        private void CompleteInitialization()
        {
            if (initializeWasSuccess) StateMachine.SetSeasonState(new WaitingForStartSeasonState(StateMachine));
            else
            {
                Debug.LogError("Could not initialize season");
                Object.Destroy(StateMachine.Season.gameObject);
            }
        }
    }

    public class WaitingForStartSeasonState : SeasonState
    {
        public WaitingForStartSeasonState(SeasonStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }

    public class StartSeasonState : SeasonState
    {
        public StartSeasonState(SeasonStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            StartSeason();
        }

        private void StartSeason() => StateMachine.SetSeasonState(new RunSeasonState(StateMachine));
    }

    public class RunSeasonState : SeasonState
    {
        public RunSeasonState(SeasonStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            SetPlayersToStartSeasonState();
            GenerateNextRoundOfGames();
        }

        private void SetPlayersToStartSeasonState()
        {
            foreach (PlayerCoach playerCoach in StateMachine.Season.PlayerCoaches)
                playerCoach.StateMachine.SetPlayerState(new StartSeasonPlayerState(playerCoach.StateMachine));
        }

        public void GenerateNextRoundOfGames()
        {
            if (StateMachine.Season.RoundNumber < ScheduleManager.Instance.ScheduleScriptable.Opponents.Length) 
            {
                DealerManager.Instance.BuildDeck();
                RoundManager.Instance.GenerateRoundOfGames(); 
                Debug.Log("Generating Round " + StateMachine.Season.RoundNumber); 
            }

            else Debug.Log("Season over!");
        }
    }
    #endregion
}
