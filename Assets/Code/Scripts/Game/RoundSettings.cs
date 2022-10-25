namespace SimplyGreatGames.PokerHoops
{
    public abstract class RoundSettings
    {
        public abstract Enums.RoundType RoundType { get; set; }
        public abstract int StartingHandNumber { get; set; }
    }

    [System.Serializable]
    public class DefaultRoundSettings : RoundSettings
    {
        public override Enums.RoundType RoundType { get; set; }
        public override int StartingHandNumber { get; set; }

        public DefaultRoundSettings()
        {
            RoundType = Enums.RoundType.DefaultSchedule;
            StartingHandNumber = 5;
        }
    }
}
