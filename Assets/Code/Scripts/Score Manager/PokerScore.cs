using System.Collections.Generic;

namespace SimplyGreatGames.PokerHoops
{
    public class PokerScore
    {
        public Coach ScoreOwner;
        public Enums.PokerScoreType PokerScoreType = Enums.PokerScoreType.HighCard;
        public int ScoreValue;
        public List<Card> Cards;

        public PokerScore(Hand pokerHand, Enums.PokerScoreType pokerScoreType, int scoreValue, List<Card> cards)
        {
            ScoreOwner = pokerHand.Owner;
            PokerScoreType = pokerScoreType;
            Cards = cards;
            ScoreValue = scoreValue;
        }
    }
}
