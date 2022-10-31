using UnityEngine;
using UnityEngine.UI;

namespace SimplyGreatGames.PokerHoops
{
    public class GameUIPanel : MonoBehaviour
    {
        public static GameUIPanel Instance;

        [Header("Dependencies")]
        [SerializeField] private Button[] buttons;
        public Button[] Buttons { get => buttons; private set => buttons = value; }

        [Header("Runtime Data")]
        [SerializeField] private PlayerCoach owner = null;
        public PlayerCoach Owner { get => owner; private set => owner = value; }

        #region Unity Methods & Initialization

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            Buttons = GetComponentsInChildren<Button>();
        }

        public void RegisterOwner(PlayerCoach newOwner)
        {
            if (!newOwner.IsLocalPlayer)
            {
                Debug.LogError("Trying to Control Game UI Panel When Not the Local Player");
                return;
            }

            Owner = newOwner;
        }

        #endregion 

        #region Buttons

        public void ToggleButtonEnabled(PlayerCoach requestingCoach, string buttonName, bool toggleValue)
        {
            if (!requestingCoach.IsLocalPlayer)
                return;

            Button buttonFound = null;

            foreach (Button button in Buttons)
            {
                if (button.gameObject.name == buttonName)
                    buttonFound = button;
            }

            if (buttonFound == null)
            {
                Debug.Log("Button " + buttonName + " can't be found on " + gameObject.name);
                return;
            }

            buttonFound.gameObject.SetActive(toggleValue);
        }

        public void ToggleButtonInteractable(PlayerCoach requestingCoach, string buttonName, bool toggleValue)
        {
            if (!requestingCoach.IsLocalPlayer)
                return;

            Button buttonFound = null;

            foreach (Button button in Buttons)
            {
                if (button.gameObject.name == buttonName)
                    buttonFound = button;
            }

            if (buttonFound == null)
            {
                Debug.Log("Button " + buttonName + " can't be found on " + gameObject.name);
                return;
            }

            buttonFound.interactable = toggleValue;
        }

        #endregion

        #region On Button Methods

        public void OnDiscardButton()
        {
            Owner.Hand.DiscardMarkedCards();
            RoundManager.Instance.MarkGameComplete(Owner.CurrentGame);
        }

        #endregion
    }
}
