namespace SimplyGreatGames.PokerHoops
{
    public class GameStateMachine : StateMachineOperator
    {
        public GameState CurrentState;
        public Game Game;

        public void RegisterStateMachine(Game game) => Game = game;

        public void SetGameState(GameState nextState)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit();

            CurrentState = nextState;

            if (CurrentState != null)
                CurrentState.OnStateEnter();
        }
    }
}
