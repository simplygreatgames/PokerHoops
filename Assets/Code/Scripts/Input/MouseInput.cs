using UnityEngine;
using UnityEngine.EventSystems;

namespace SimplyGreatGames.PokerHoops
{
    public class MouseInput : InputController
    {
        public override PlayerCoach InputOwner { get; set; }

        [SerializeField] private bool isReadingInput;
        public bool IsReadingInput
        {
            get => isReadingInput;
            set => isReadingInput = value;
        }

        [SerializeField] private bool isReadingCards;
        public bool IsReadingCards
        {
            get => isReadingCards;
            set => isReadingCards = value;
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
            if (isReadingInput == false || IsReadingCards == false)
                return;

            if (Input.GetMouseButtonUp(0))
                LeftClick();

            if (Input.GetMouseButtonUp(1))
                RightClick();
        }

        #endregion

        #region Input Controller

        public override void InitializeInput(bool value)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop) // if is desktop, turn on the mouse input controls
                isReadingInput = value;
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
                    _interactable.OnLeftClick(InputOwner);
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
                    _interactable.OnRightClick(InputOwner);
                }
            }
        }

        #endregion
    }
}
