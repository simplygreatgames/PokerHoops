namespace SimplyGreatGames.PokerHoops
{
    public class PlayerState : State
    {
        public PlayerState(PlayerStateMachine stateMachine) => StateMachine = stateMachine;
        protected PlayerStateMachine StateMachine;

        #region State Methods

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

        #endregion

    }

    #region Player States

    public class InitializeState : PlayerState
    {
        public InitializeState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            BuildDeck();
            DrawStartingHand();
        }

        private void BuildDeck() => StateMachine.Player.Deck.BuildDeck();
        private void DrawStartingHand() => StateMachine.Player.Deck.DrawFromDeck(5);
    }

    #endregion
}
