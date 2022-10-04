using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameManager gameManager = (GameManager) target;

            if (GUILayout.Button("Load New Game"))
                gameManager.LoadNewGame(new DefaultGameSettings());
        }
    }
}
