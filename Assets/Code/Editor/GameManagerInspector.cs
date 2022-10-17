using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(RoundManager))]
    public class GameManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RoundManager gameManager = (RoundManager) target;

            if (GUILayout.Button("Generate Unranked Round"))
                gameManager.GenerateRoundOfGames(new DefaultGameSettings(), Enums.OpponentType.Unranked);
        }
    }
}
