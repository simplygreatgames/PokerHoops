using System.Collections.Generic;

namespace SimplyGreatGames.PokerHoops
{
    public class PokerScore
    {
        public Enums.PokerScoreType PokerScoreType = Enums.PokerScoreType.HighCard;
        public int ScoreValue;
        public List<Card> Cards;

        public PokerScore(Enums.PokerScoreType pokerScoreType, int scoreValue, List<Card> cards)
        {
            PokerScoreType = pokerScoreType;
            Cards = cards;
            ScoreValue = scoreValue;
        }
    }
}
