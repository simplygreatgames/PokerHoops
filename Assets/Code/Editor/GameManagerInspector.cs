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

            if (GUILayout.Button("Load New Game"))
                gameManager.LoadNewGame(new DefaultGameSettings());
        }
    }
}
