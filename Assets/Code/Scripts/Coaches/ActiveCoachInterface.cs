using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class ActiveCoachInterface : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] public Transform[] activeCardSlots;
        public Transform[] ActiveCardSlots { get => activeCardSlots; set => activeCardSlots = value; }

        [Header("Debug Information")]
        [SerializeField] private Coach activeCoach = null;
        public Coach ActiveCoach 
        { 
            get => activeCoach;
            set
            {
                activeCoach = value;
                SetCardsToSlots();
            }
        }


        #region Update Interface 

        private void SetCardsToSlots()
        {
            for (int i = 0; i < ActiveCoach.Hand.HandSlots.Length; i++)
            {
                ActiveCoach.Hand.HandSlots[i].transform.SetParent(ActiveCardSlots[i]);
                ActiveCoach.Hand.HandSlots[i].transform.localPosition = Vector3.zero;
            }
        }

        #endregion
    }
}
