using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(ScoringTableScriptable))]
    public class ScoringTableScriptableInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ScoringTableScriptable scoringTable = (ScoringTableScriptable)target;

            if (GUILayout.Button("Build Base Table"))
                scoringTable.BuildBaseTable();
        }
    }
}