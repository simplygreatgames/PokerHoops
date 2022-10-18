using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(RoundManager))]
    public class RoundManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RoundManager roundManager = (RoundManager) target;

            if (GUILayout.Button("Generate Unranked Round"))
                roundManager.GenerateRoundOfGames(new DefaultGameSettings());
        }
    }
}
