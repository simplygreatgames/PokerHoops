using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(Card))]
    public class CardInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Card card = (Card)target;

            if (GUILayout.Button("Initialize Card"))
                card.InitializeCard();

            if (GUILayout.Button("Flip Card"))
                card.FlipCard();
        }
    }
}