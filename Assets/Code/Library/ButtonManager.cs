using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimplyGreatGames.UI
{
    public class ButtonManager : MonoBehaviour
    {
        [Header("Requirements")]
        public RectTransform ButtonArea;

        [Header("Settings")]
        public bool StartToggledOn;
        public float StartDelay;
        [Range(0,100)] public float GroupSpacing;
        public ButtonManagerAnimationStyle AnimationStyle;
        [Range(0, 1)] public float CascadeDelay;
        public ButtonGroup[] ButtonGroups;

        [Header("Debug Settings")]
        public string DebugButtonGroupName;
        public float DebugEndXPosition;
        public bool DebugIsToggledOn;

        private ButtonGroup buttonGroupCurrent = null;
        private List<Animator> buttonGroupsAppended = new List<Animator>();

        #region Unity Methods

        public void Start()
        {
            if (StartToggledOn && ButtonGroups.Length > 0)
                StartCoroutine(ToggleButtonGroupOnDelay(ButtonGroups[0].Name, true));
        }

        #endregion

        #region Button Groups

        public void ToggleInteractiveGroup(string name, bool isInteractive)
        {
            ButtonGroup buttonGroup = GetButtonGroup(name);

            if (buttonGroup == null)
                return;

            foreach (Animator animator in buttonGroup.Buttons)
            {
                if (animator)
                {
                    Button button = animator.GetComponent<Button>();

                    if (button)
                        button.interactable = isInteractive;
                }
            }
        }

        public IEnumerator ToggleButtonGroupOnDelay(string name, bool isLoadingIn)
        {
            yield return new WaitForSeconds(StartDelay);
            ToggleButtonGroup(name, isLoadingIn);
        }

        public void ToggleButtonGroup(string name, bool isLoadingIn)
        {
            ButtonGroup buttonGroupLoading = GetButtonGroup(name);

            if (buttonGroupLoading == null)
                return;

            if (isLoadingIn == true)
            {
                if (buttonGroupLoading.IsAppendend)
                {
                    List<Animator> appendedButtonsToLoad = new List<Animator>();

                    foreach (Animator buttonGroup in buttonGroupLoading.Buttons)
                    {
                        if (buttonGroupCurrent == null || !buttonGroupCurrent.Buttons.Contains(buttonGroup))
                            appendedButtonsToLoad.Add(buttonGroup);
                    }

                    if (appendedButtonsToLoad.Count > 0)
                    {
                        foreach (Animator buttonAnimator in appendedButtonsToLoad)
                            buttonGroupsAppended.Add(buttonAnimator);
                    }

                    else if (appendedButtonsToLoad.Count == 0)
                        return; // Didn't find a unique button to add to the list
                }

                else if (!buttonGroupLoading.IsAppendend && buttonGroupCurrent != null)
                {
                    ToggleButtonGroup(buttonGroupCurrent.Name, false);
                    buttonGroupCurrent = new ButtonGroup(buttonGroupLoading);
                }

                else if (!buttonGroupLoading.IsAppendend && buttonGroupCurrent == null)
                    buttonGroupCurrent = new ButtonGroup(buttonGroupLoading);

                buttonGroupLoading = AddAppendedButtonGroups(buttonGroupLoading);
            }

            else if (isLoadingIn == false)
            {
                if (buttonGroupLoading.IsAppendend)
                {
                    foreach (var buttonGroup in buttonGroupLoading.Buttons)
                        buttonGroupsAppended.Remove(buttonGroup);
                }

                else if (buttonGroupCurrent != null && buttonGroupLoading.Name == buttonGroupCurrent.Name)
                    buttonGroupCurrent = null;
            }

            ExecuteButtonGroupLoad(isLoadingIn, buttonGroupLoading);
        }

        private void ExecuteButtonGroupLoad(bool isLoadingIn, ButtonGroup buttonGroupLoading)
        {
            for (int i = 0; i < buttonGroupLoading.Buttons.Count; i++)
            {
                Animator buttonAnimator = buttonGroupLoading.Buttons[i];

                if (buttonAnimator == null)
                {
                    Debug.Log("Animator was null in Toggle Group " + gameObject.name);
                    return;
                }

                switch (AnimationStyle)
                {
                    case ButtonManagerAnimationStyle.Synchronized:
                        StartCoroutine(AnimateButtonGroupLoad(isLoadingIn, buttonAnimator, i, 0));
                        break;

                    case ButtonManagerAnimationStyle.Cascade:
                        StartCoroutine(AnimateButtonGroupLoad(isLoadingIn, buttonAnimator, i, CascadeDelay));
                        break;

                    default:
                        Debug.Log("Animation Style Not Set for Button Controller on " + gameObject.name);
                        StartCoroutine(AnimateButtonGroupLoad(isLoadingIn, buttonAnimator, i, 0));
                        break;
                }
            }
        }

        private IEnumerator AnimateButtonGroupLoad(bool toggleValue, Animator animatedObject, int numberInGroup, float delay)
        {
            yield return new WaitForSeconds(numberInGroup * delay);

            if (toggleValue == true)
                SetButtonPosition((RectTransform) animatedObject.transform, numberInGroup);

            animatedObject.SetBool("Toggle", toggleValue);
        }

        #endregion

        #region Button Positions

        public void SetButtonPosition(RectTransform rectTransform, int numberInGroup)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, ((rectTransform.rect.height * rectTransform.localScale.y) * -numberInGroup) + (-numberInGroup * GroupSpacing));
        }

        public void SetButtonPosition(RectTransform rectTransform, float xPosition, int numberInGroup)
        {
            rectTransform.anchoredPosition = new Vector2(xPosition, ((rectTransform.rect.height * rectTransform.localScale.y) * -numberInGroup) + ( -numberInGroup * GroupSpacing));
        }

        #endregion

        #region Signals

        public void OnSignalToggleOn(string menuName) => ToggleButtonGroup(menuName, true);
        public void OnSignalToggleOff(string menuName) => ToggleButtonGroup(menuName, false);

        #endregion

        #region Helpers

        public ButtonGroup GetButtonGroup(string name)
        {
            ButtonGroup buttonGroupLoading = null;

            foreach (ButtonGroup buttonGroupTemplate in ButtonGroups)
            {
                if (buttonGroupTemplate.Name == name)
                    buttonGroupLoading = new ButtonGroup(buttonGroupTemplate);
            }

            if (buttonGroupLoading == null)
                Debug.Log("Warning!!! Could not find toggle group by name " + name + " On game object " + gameObject.name);

            return buttonGroupLoading;
        }

        public ButtonGroup AddAppendedButtonGroups(ButtonGroup buttonGroupLoading)
        {
            if (buttonGroupsAppended.Count > 0)
            {
                ButtonGroup appendedButtonGroup = new ButtonGroup();

                if (buttonGroupCurrent != null)
                {
                    foreach (Animator buttonAnimator in buttonGroupCurrent.Buttons)
                        appendedButtonGroup.Buttons.Add(buttonAnimator);
                }

                foreach (Animator buttonAnimator in buttonGroupsAppended)
                    appendedButtonGroup.Buttons.Add(buttonAnimator);

                buttonGroupLoading = new ButtonGroup(appendedButtonGroup);
            }

            return buttonGroupLoading;
        }

        #endregion
    }

    [System.Serializable]
    public class ButtonGroup
    {
        public string Name;
        public bool IsAppendend;
        public List<Animator> Buttons;

        public ButtonGroup()
        {
            Name = string.Empty;
            IsAppendend = false;
            Buttons = new List<Animator>();
        }

        public ButtonGroup(ButtonGroup copiedGroup)
        {
            Name = copiedGroup.Name;
            IsAppendend = copiedGroup.IsAppendend;
            Buttons = copiedGroup.Buttons;
        }
    }

    public enum ButtonManagerAnimationStyle
    {
        Synchronized,
        Cascade
    }
}
