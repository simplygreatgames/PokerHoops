namespace SimplyGreatGames.PokerHoops
{
    public abstract class State
    {
        public abstract void OnStateEnter();
        public abstract void Tick();
        public abstract void OnStateExit();
    }
}
