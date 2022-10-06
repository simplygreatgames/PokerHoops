using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CustomEditor(typeof(UIShaderController))]
    public class UIShaderControllerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UIShaderController uiShaderController = (UIShaderController)target;

            if (GUILayout.Button("Animate Shine"))
                uiShaderController.TweenShine();
        }
    }
}