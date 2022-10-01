namespace SimplyGreatGames.PokerHoops
{
    public class PlayerStateMachineOperator : StateMachineOperator
    {
        public PlayerStateMachine CurrentState;
        public Player Player;

        public void RegisterStateMachine(Player player) => Player = player;

        public void SetPlayerState(PlayerStateMachine nextState)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit();

            CurrentState = nextState;

            if (CurrentState != null)
                CurrentState.OnStateEnter();
        }
    }
}
