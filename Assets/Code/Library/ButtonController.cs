using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SimplyGreatGames.UI 
{
    public class ButtonController : MonoBehaviour
    {
        public bool InactivateOnClick = false;

        [Range(0f,3f)] public float Cooldown = 0;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        public void OnButtonEvent()
        {
            if (InactivateOnClick)
                button.interactable = false;

            else
            {
                StartCoroutine(DelayButton());

                IEnumerator DelayButton()
                {
                    button.interactable = false;
                    yield return new WaitForSeconds(Cooldown);
                    button.interactable = true;
                }
            }
        }
    }
}
