using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class AnimatorController : MonoBehaviour
    {
        public Animator Animator { get; private set; }

        public bool IsToggled;

        public void Awake()
        {
            Animator = GetComponent<Animator>();
            SetToggle(IsToggled);
        }

        public void SetToggle(bool toggleValue)
        {
            Animator.SetBool("Toggle", toggleValue);
        }
    }
}
