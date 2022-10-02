using UnityEngine;
using UnityEngine.EventSystems;

namespace SimplyGreatGames.PokerHoops
{
    public class MouseInput : InputController
    {
        [SerializeField] private bool isToggledOn;
        public bool IsToggledOn
        {
            get => isToggledOn;
            set => isToggledOn = value;
        }

        [SerializeField] private bool debugMode;
        public bool DebugMode
        {
            get => debugMode;
            set
            {
                debugMode = value;
                Debug_ToggleInputListening(debugMode);
            }
        }

        #region Unity Methods

        public void Start()
        {
            InitializeInput(true);
        }

        public void Update()
        {
            if (Input.GetMouseButtonUp(0))
                LeftClick();

            if (Input.GetMouseButtonUp(1))
                RightClick();
        }

        #endregion

        #region Input Controller

        public override void InitializeInput(bool value)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                enabled = value;
        }

        #endregion

        #region Mouse Clicking 

        private void LeftClick()
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit objectHit))
            {
                if (objectHit.transform.TryGetComponent(out Interfaces.IInteractable _interactable))
                {
                    Event_Invoke_OnLeftClickedObject(objectHit.transform.gameObject);
                    _interactable.OnLeftClick();
                }
            }
        }

        private void RightClick()
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit objectHit))
            {
                if (objectHit.transform.TryGetComponent(out Interfaces.IInteractable _interactable))
                {
                    Event_Invoke_OnLeftClickedObject(objectHit.transform.gameObject);
                    _interactable.OnRightClick();
                }
            }
        }

        #endregion
    }
}
