using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(PlayerStateMachine))]
    public class PlayerStateMachineInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerStateMachine playerStateMachineOperator = (PlayerStateMachine)target;

            if (GUILayout.Button("SetInitialState"))
                playerStateMachineOperator.SetPlayerState(new InitializeState(playerStateMachineOperator));
        }
    }
}