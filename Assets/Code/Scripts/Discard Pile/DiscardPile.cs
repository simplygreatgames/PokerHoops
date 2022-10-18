using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class DiscardPile : MonoBehaviour
    {
        [SerializeField] private List<Card> currentPile;
        public List<Card> CurrentPile
        {
            get { return currentPile; }
            private set { currentPile = value; }
        }

        private PlayerCoach playerOwner;

        public void RegisterDiscardPile(PlayerCoach player) => playerOwner = player;

        public void AddToDiscardPile(Card card)
        {
            currentPile.Add(card);

            card.transform.SetParent(this.transform);
            card.transform.DOLocalMove(Vector3.zero, 1);
        }
    }
}
