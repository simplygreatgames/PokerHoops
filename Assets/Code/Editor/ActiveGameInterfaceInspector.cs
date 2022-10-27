using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(ActiveGameInterface))]
    public class ActiveGameInterfaceInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ActiveGameInterface activeGameInterface = (ActiveGameInterface)target;

            if (GUILayout.Button("Activate Game Interface"))
            {
                if (!Application.isPlaying)
                {
                    Debug.LogError("Must be In Play Mode to activate the interface");
                    return;
                }

                activeGameInterface.InitializeInterface();
            }
        }

    }
}