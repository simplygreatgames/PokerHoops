using UnityEngine;
using System.Collections;

namespace SimplyGreatGames.PokerHoops
{
    public class AnimatorController : MonoBehaviour
    {
        public Animator Animator { get; private set; }

        [Header("Start Values")]
        public bool IsSetOnStart;
        public bool StartValue;
        public float StartDelay;

        public void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        public void Start()
        {
            if (IsSetOnStart)
                StartCoroutine(SetToggleOnDelay());
        }

        private IEnumerator SetToggleOnDelay()
        {
            yield return new WaitForSeconds(StartDelay);
            SetToggle(StartValue);
        }

        public void SetToggle(bool toggleValue)
        {
            Animator.SetBool("Toggle", toggleValue);
        }
    }
}
