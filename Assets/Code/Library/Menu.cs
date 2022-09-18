using UnityEngine;

namespace SimplyGreatGames.UI
{
    [RequireComponent(typeof(Animator), typeof(RectTransform))]
    public abstract class MenuBase : MonoBehaviour
    {
        [Header("Menu Settings")]
        public string MenuID;
        public bool IsStartingOnScreen;

        public Animator Animator { get; private set; }
        public RectTransform RectTransform { get; private set; }

        #region Unity Methods

        public void Awake()
        {
            Animator = GetComponent<Animator>();
            RectTransform = GetComponent<RectTransform>();
        }

        public void Start()
        {
            Initialize();
        }

        public void OnValidate()
        {
            if (string.IsNullOrEmpty(MenuID))
                Debug.LogWarning("Warning! Must set Menu Name In " + gameObject.name);
        }

        protected virtual void Initialize()
        {
            if (IsStartingOnScreen) AnimateOpen();
            else SetClose();
        }

        #endregion

        #region Base Menu Methods

        public void AnimateOpen() => Animator.SetBool("IsOpen", true);
        public void AnimateClose() => Animator.SetBool("IsOpen", false);
        public void SetClose() => RectTransform.anchoredPosition = Vector2.zero;

        #endregion
    }

    public class Menu : MenuBase
    {

    }
}
