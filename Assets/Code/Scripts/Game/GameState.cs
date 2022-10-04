namespace SimplyGreatGames.PokerHoops
{
    public class GameState : State
    {
        public GameState(GameStateMachine stateMachine) => StateMachine = stateMachine;
        protected GameStateMachine StateMachine;

        public override void OnStateEnter()
        {
            StateMachine.AnimateState(this);
        }

        public override void Tick()
        {
        }

        public override void OnStateExit()
        {
        }
    }
}
