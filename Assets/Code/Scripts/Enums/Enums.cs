namespace SimplyGreatGames.PokerHoops
{
    public class Enums
    {
        public enum CardSuits
        {
            Club,
            Diamond,
            Heart,
            Spade
        }

        public enum RoundType
        {
            DefaultSchedule = 0,
            OverrideSchedule = 1
        }

        public enum OpponentType
        {
            Unranked,
            Ranked,
            HeadToHead
        }

        public enum CpuType
        {
            Unranked,
            Ranked
        }

        public enum DiscardType
        {
            ToDealer = 0,
            ToOpponent = 1
        }

        public enum MouseInputType
        {
            LeftClicked,
            RightClicked
        }

        public enum PokerScoreType
        {
            HighCard = 0,
            PairOne = 1,
            PairTwo = 2,
            Trips = 3,
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            Fours = 7,
            StraightFlush = 8
        }
    }
}
