using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void ScoreGame(Game gameBeingScored)
        {
            foreach (Coach coach in gameBeingScored.CoachesInGame)
                ScorePlayerHand(coach.Hand);
        }

        private void ScorePlayerHand(Hand hand)
        {
            Debug.Log("Scoring the players hand");

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

        #region Poker Score Logic

        private bool ScoreStraightFlush(Hand hand)
        {
            bool isStraightFlush = false;


            return isStraightFlush;
        }

        private bool ScoreFours(Hand hand)
        {
            bool isStraightFours = false;

            return isStraightFours;
        }

        private bool ScoreFullHouse(Hand hand)
        {
            bool isFullHouse = false;


            return isFullHouse;
        }

        private bool ScoreFlush(Hand hand)
        {
            bool isFlush = false;


            return isFlush;
        }

        private bool ScoreStraight(Hand hand)
        {
            bool isStraight = false;


            return isStraight;
        }

        private bool ScoreTrips(Hand hand)
        {
            bool isTrips = false;


            return isTrips;
        }

        private bool ScoreTwoPair(Hand hand)
        {
            bool isTwoPair = false;


            return isTwoPair;
        }

        private bool ScoreOnePair(Hand hand)
        {
            bool isOnePair = false;


            return isOnePair;
        }

        private void ScoreHighCard(Hand hand)
        {
            List<Card> cardsInHand = hand.GetCardsFromHand();

            int highestValue = 0;

            foreach (Card card in cardsInHand)
            {
                if (card.Value > highestValue)
                    highestValue = card.Value;
            }

            PokerScore newScore = new PokerScore(Enums.PokerScoreType.HighCard, highestValue);
            hand.PokerScore = newScore;
        }

        #endregion
    }
}
