namespace SimplyGreatGames.PokerHoops
{
    public class PlayerStateMachine : StateMachineOperator
    {
        public PlayerState CurrentState;
        public PlayerCoach PlayerCoach { get; private set; }

        public void RegisterStateMachine(PlayerCoach player) => PlayerCoach = player;

        public void SetPlayerState(PlayerState nextState)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit();

            CurrentState = nextState;

            if (CurrentState != null)
                CurrentState.OnStateEnter();
        }
    }
}
