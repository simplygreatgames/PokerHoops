namespace SimplyGreatGames.PokerHoops
{
    public class PokerScore
    {
        public Enums.PokerScoreType PokerScoreType = Enums.PokerScoreType.HighCard;
        public int ScoreValue;

        public PokerScore(Enums.PokerScoreType pokerScoreType, int score)
        {
            PokerScoreType = pokerScoreType;
            ScoreValue = score;
        }
    }
}
