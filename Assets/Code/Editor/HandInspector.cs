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

            if (GUILayout.Button("Add Cards To Hand"))
                hand.Debug_AddCardsToHand();

            if (GUILayout.Button("Remove Cards From Hand"))
                hand.Debug_RemoveCardsFromHand();
        }
    }

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