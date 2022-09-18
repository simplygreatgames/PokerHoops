using UnityEditor;
using UnityEngine;

namespace SimplyGreatGames.UI
{
    [CustomEditor(typeof(ButtonManager))]
    public class ButtonManagerInspector : Editor
    {
        private ButtonManager buttonManager = null;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            buttonManager = (ButtonManager)target;

            if (GUILayout.Button("Set Button Group"))
                EditorSetButtonGroup(buttonManager.DebugButtonGroupName, buttonManager.DebugEndXPosition);

            if (GUILayout.Button("Reset Buttons"))
                EditorResetButtons();

            if (GUILayout.Button("Animate Buttons"))
                EditorAnimateButtons(buttonManager.DebugButtonGroupName, buttonManager.DebugIsToggledOn);
        }

        private void EditorSetButtonGroup(string buttonGroupName, float endXPosition)
        {
            EditorResetButtons();

            ButtonGroup buttonGroupLoading = buttonManager.GetButtonGroup(buttonGroupName);

            for (int i = 0; i < buttonGroupLoading.Buttons.Count; i++)
            {
                Animator buttonAnimator = buttonGroupLoading.Buttons[i];

                if (buttonAnimator != null)
                    buttonManager.SetButtonPosition((RectTransform)buttonAnimator.transform, endXPosition, i);

                else Debug.Log("Animator was null in Toggle Group " + buttonManager.gameObject.name);
            }
        }

        private void EditorResetButtons()
        {
            if (buttonManager.ButtonArea == null)
            {
                Debug.Log("Warning! Must set ButtonArea!");
                return;
            }

            foreach (RectTransform rectTransform in buttonManager.ButtonArea)
                rectTransform.anchoredPosition = Vector2.zero;
        }

        private void EditorAnimateButtons(string buttonGroup, bool toggleValue)
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Must be in Play Mode");
                return;
            }

            buttonManager.ToggleButtonGroup(buttonGroup, toggleValue);
        }
    }
}
