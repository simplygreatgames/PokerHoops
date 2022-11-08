using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [SerializeField] private ScoringTableScriptable defaultScoringTable;
        public ScoringTableScriptable DefaultScoringTable { get => defaultScoringTable; set => defaultScoringTable = value; }


        #region Unity Methods

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #endregion

        #region Score Manager Methods

        public void ScoreGame(Game gameBeingScored)
        {
            foreach (Coach coach in gameBeingScored.CoachesInGame)
                ScorePlayerHand(coach.Hand);

            DeclareWinners(gameBeingScored);
            RecordData(gameBeingScored);
        }

        private void ScorePlayerHand(Hand hand)
        {
            if (ScoreStraightFlush(hand)) return;
            else if (ScoreFours(hand)) return;
            else if (ScoreFullHouse(hand)) return;
            else if (ScoreFlush(hand)) return;
            else if (ScoreStraight(hand)) return;
            else if (ScoreTrips(hand)) return;
            else if (ScoreTwoPair(hand)) return;
            else if (ScoreOnePair(hand)) return;
            else ScoreHighCard(hand);
        }

        private void DeclareWinners(Game gameBeingScored)
        {
            Coach coachA = gameBeingScored.CoachesInGame[0];
            Coach coachB = gameBeingScored.CoachesInGame[1];

            if (coachA.Hand.BasketballScore == coachB.Hand.BasketballScore) gameBeingScored.IsTiedGame = true;
            else if (coachA.Hand.BasketballScore > coachB.Hand.BasketballScore) gameBeingScored.WinningScore = coachA.Hand.PokerScore;
            else gameBeingScored.WinningScore = coachB.Hand.PokerScore;
        }

        private void RecordData(Game gameBeingScored)
        {
            foreach (Coach coach in gameBeingScored.CoachesInGame)
            {
                if (coach is PlayerCoach playerCoach)
                {
                    RecordData newRecordData = playerCoach.TeamRecord.RecordGameData(gameBeingScored);

                    if (playerCoach.IsLocalPlayer)
                        SetScoreBoard(newRecordData);
                }
            }
        }

        private void SetScoreBoard(RecordData recordData) 
        {
            GameUIPanel.Instance.ScoreBoard.SetScoreBoard(recordData);
        }

        #endregion

        #region Poker Scoring Logic

        private bool ScoreStraightFlush(Hand hand)
        {
            bool isStraightFlush = false;

            if (ScoreStraight(hand) && ScoreFlush(hand))
            {
                Card highestValueCard = hand.GetHighestValueCardAceHigh();

                int scoreValue = highestValueCard.Value;

                if (scoreValue == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.StraightFlush, scoreValue, hand.GetCardsFromHand());
                hand.PokerScore = newScore;
                isStraightFlush = true;
            }

            return isStraightFlush;
        }

        private bool ScoreFours(Hand hand)
        {
            List<Card> cardsInHand = hand.GetCardsFromHand();

            var sameValueCards = cardsInHand.GroupBy(card => card.Value);
            var trips = sameValueCards.Where(group => group.Count() == 4);

            bool isFours = false;

            if (trips.Count() == 1)
            {
                List<Card> cards = trips.SelectMany(cardsInPair => cardsInPair).ToList();

                int scoreValue = cards[0].Value;

                if (scoreValue == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.Fours, scoreValue, cards);
                hand.PokerScore = newScore;
                isFours = true;
            }

            return isFours;
        }

        private bool ScoreFullHouse(Hand hand)
        {
            bool isFullHouse = false;

            if (ScoreTrips(hand) && ScoreOnePair(hand))
            {
                Card highestValueCard = hand.GetHighestValueCardAceHigh();

                int scoreValue = highestValueCard.Value;

                if (scoreValue == 1) 
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.FullHouse, scoreValue, hand.GetCardsFromHand());
                hand.PokerScore = newScore;
                isFullHouse = true;
            }

            return isFullHouse;
        }

        private bool ScoreFlush(Hand hand)
        {
            bool isFlush = false;

            List<Card> cardsInHand = hand.GetCardsFromHand();

            var sameSuitGroups = cardsInHand.GroupBy(x => x.Suit);
            var flushGroup =  sameSuitGroups.Where(x => x.Count() >= 5); 
            
            if (flushGroup.Count() == 1)
            {
                Card highestCard = hand.GetHighestValueCardAceHigh();

                int scoreValue = highestCard.Value;

                if (scoreValue == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.Flush, scoreValue, hand.GetCardsFromHand());
                hand.PokerScore = newScore;
                isFlush = true;
            }

            return isFlush;
        }

        private bool ScoreStraight(Hand hand)
        {
            List<int> cardInHandAceLow = hand.GetCardValuesFromHandAceLow();
            List<int> cardsinHandAceHigh = hand.GetCardValuesFromHandAceHigh();

            bool cardAceHighiIsStraight = (cardsinHandAceHigh.Distinct().Count() == 5) && (cardsinHandAceHigh.Max() - cardsinHandAceHigh.Min() == 4);
            bool cardAceLowIsStraight = (cardInHandAceLow.Distinct().Count() == 5) && (cardInHandAceLow.Max() - cardInHandAceLow.Min() == 4);

            if (cardAceHighiIsStraight)
            {
                Card highestCard = hand.GetHighestValueCardAceHigh();

                int scoreValue = highestCard.Value;

                if (scoreValue == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.Straight, scoreValue, hand.GetCardsFromHand());
                hand.PokerScore = newScore;
            }

            else if (cardAceLowIsStraight)
            {
                Card highestCard = hand.GetHighestValueCardAceLow();

                int scoreValue = highestCard.Value;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.Straight, scoreValue, hand.GetCardsFromHand());
                hand.PokerScore = newScore;
            }

            return cardAceHighiIsStraight;
        }

        private bool ScoreTrips(Hand hand)
        {
            List<Card> cardsInHand = hand.GetCardsFromHand();

            var sameValueCards = cardsInHand.GroupBy(card => card.Value);
            var trips = sameValueCards.Where(group => group.Count() == 3);

            bool isTrips = false;

            if (trips.Count() == 1)
            {
                var cardPairs = trips.SelectMany(cardsInPair => cardsInPair);
                List<Card> cards = cardPairs.ToList();

                int scoreValue = cards[0].Value;

                if (scoreValue == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.Trips, scoreValue, cards);
                hand.PokerScore = newScore;
                isTrips = true;
            }

            return isTrips;
        }

        private bool ScoreTwoPair(Hand hand)
        {
            List<Card> cardsInHand = hand.GetCardsFromHand();

            var sameValueCards = cardsInHand.GroupBy(card => card.Value);
            var pairs = sameValueCards.Where(group => group.Count() == 2);

            bool isTwoPair = false;

            if (pairs.Count() == 2)
            {
                var cardPairs = pairs.SelectMany(cardsInPair => cardsInPair);
                List<Card> cards = cardPairs.OrderByDescending(card => card.Value).ToList();

                int scoreValue = cards[0].Value;

                if (cards[cards.Count - 1].Value == 1)
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.PairTwo, scoreValue, cards);
                hand.PokerScore = newScore;
                isTwoPair = true;
            }

            return isTwoPair;
        }

        private bool ScoreOnePair(Hand hand)
        {
            List<Card> cardsInHand = hand.GetCardsFromHand();

            var sameValueCards = cardsInHand.GroupBy(card => card.Value);
            var pairs = sameValueCards.Where(group => group.Count() == 2);

            bool isOnePair = false;

            if (pairs.Count() == 1)
            {
                List<Card> cards = pairs.FirstOrDefault().ToList();

                int scoreValue = cards[0].Value;

                if (scoreValue == 1) 
                    scoreValue = 14;

                PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.PairOne, scoreValue, cards);
                hand.PokerScore = newScore;
                isOnePair = true;
            }

            return isOnePair;
        }

        private void ScoreHighCard(Hand hand)
        {
            Card card = hand.GetHighestValueCardAceHigh();

            int scoreValue = card.Value;

            if (scoreValue == 1)
                scoreValue = 14;

            PokerScore newScore = new PokerScore(hand, Enums.PokerScoreType.HighCard, scoreValue, new List<Card> { card });
            hand.PokerScore = newScore;
        }

        #endregion
    }
}
