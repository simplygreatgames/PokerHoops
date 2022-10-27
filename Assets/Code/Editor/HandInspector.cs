using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(Hand))]
    public class HandInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Hand hand = (Hand)target;

            if (GUILayout.Button("Draw Card"))
                hand.Debug_DrawCard();

            if (GUILayout.Button("Discard To Dealer"))
                hand.Debug_DiscardToDealer();

            if (GUILayout.Button("Discard To Opponent"))
                hand.Debug_DiscardToOpponent();
        }
    }
}