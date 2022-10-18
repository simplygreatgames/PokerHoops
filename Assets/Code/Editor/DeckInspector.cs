using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(Deck))]
    public class DeckInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Deck deck = (Deck)target;

            if (GUILayout.Button("Build Deck"))
                deck.BuildDeck();

            if (GUILayout.Button("Shuffle Deck"))
                deck.ShuffleDeck();
        }
    }
}