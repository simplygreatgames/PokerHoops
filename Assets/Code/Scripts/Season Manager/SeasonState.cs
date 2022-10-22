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
            StateMachine.Season.Players = PlayerPoolManager.Instance.PlayerPool;

            if (StateMachine.Season.Players.Count == 0)
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

            DealerManager.Instance.BuildDeck();
            RoundManager.Instance.GenerateRoundOfGames(new DefaultRoundSettings());
        }
    }

    #endregion
}
