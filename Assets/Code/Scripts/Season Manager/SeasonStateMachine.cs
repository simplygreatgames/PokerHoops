namespace SimplyGreatGames.PokerHoops
{
    public class SeasonStateMachine : StateMachineOperator
    {
        public SeasonState CurrentState;
        public Season Season { get; private set; }

        public void RegisterStateMachine(Season season) => Season = season;

        public void SetSeasonState(SeasonState nextState)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit();

            CurrentState = nextState;

            if (CurrentState != null)
                CurrentState.OnStateEnter();
        }
    }
}
