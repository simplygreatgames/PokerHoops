using UnityEngine;

namespace SimplyGreatGames.UI
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance = null;

        public MenuBase[] Menus;

        #region Unity

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            Initialize();
        }

        private void Initialize()
        {
            foreach (MenuBase menu in Menus)
                menu.gameObject.SetActive(true);
        }

        #endregion

        #region Menu Animation

        public void ToggleOffMenus()
        {
            foreach (MenuBase menu in Menus)
                ToggleMenu(menu.MenuID, animatedOn: false);
        }   

        public void ToggleMenu(string menuID, bool animatedOn = true)
        {
            MenuBase menuFound = null;

            foreach (MenuBase menu in Menus)
            {
                if (menuID == menu.MenuID)
                    menuFound = menu;
            }

            if (menuFound == null)
            {
                Debug.Log("Warning! Unable to find menu by name of " + menuID);
                return;
            }

            if (animatedOn) menuFound.AnimateOpen();
            else menuFound.AnimateClose();
        }

        public void PlayOverride(string menuID, string animationName)
        {
            MenuBase menuFound = null;

            foreach (MenuBase menu in Menus)
            {
                if (menuID == menu.MenuID)
                    menuFound = menu;
            }

            if (menuFound == null)
            {
                Debug.Log("Warning! Unable to find menu by name of " + menuID);
                return;
            }

            menuFound.Animator.Play(animationName);
        }

        #endregion
    }
}
