using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public abstract class InputController : MonoBehaviour
    {
        public abstract void InitializeInput(bool value);

        public delegate void LeftClickedObject(GameObject gameObject);
        public event LeftClickedObject OnLeftClickedObject;
        public void Event_AddListener_OnLeftClickedObject(LeftClickedObject listener) => OnLeftClickedObject += listener;
        public void Event_RemoveListener_OnLeftClickedObject(LeftClickedObject listener) => OnLeftClickedObject -= listener;
        public void Event_Invoke_OnLeftClickedObject(GameObject gameObject) => OnLeftClickedObject?.Invoke(gameObject);

        public delegate void RightClickedObject(GameObject gameObject);
        public event RightClickedObject OnRightClickedObject;
        public void Event_AddListener_OnRightClickedObject(RightClickedObject listener) => OnRightClickedObject += listener;
        public void Event_RemoveListener_OnRightClickedObject(RightClickedObject listener) => OnRightClickedObject -= listener;
        public void Event_Invoke_OnRightClickedObject(GameObject gameObject) => OnRightClickedObject?.Invoke(gameObject);

        #region Debug Methods

        public void Debug_ToggleInputListening(bool isToggledOn)
        {
            if (isToggledOn)
            {
                Event_AddListener_OnLeftClickedObject(Debug_LeftClick);
                Event_AddListener_OnRightClickedObject(Debug_RightClick);
            }

            else
            {
                Event_RemoveListener_OnLeftClickedObject(Debug_LeftClick);
                Event_RemoveListener_OnRightClickedObject(Debug_RightClick);
            }
        }

        public void Debug_LeftClick(GameObject gameObject)
        {
            Debug.Log("Debug: Left Clicked: " + gameObject.name);
        }

        public void Debug_RightClick(GameObject gameObject)
        {
            Debug.Log("Debug: Right Clicked: " + gameObject.name);
        }

        #endregion
    }
}
