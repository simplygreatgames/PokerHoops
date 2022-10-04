using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public abstract class StateMachineOperator : MonoBehaviour
    {
        public Animator StateAnimator;

        public void AnimateState(State curretState)
        {
            string[] stateName = curretState.ToString().Split('.');

            if (stateName.Length < 3)
            {
                Debug.LogWarning("Game State Name Does Not Have Required Format");
                return;
            }

            if (StateAnimator == null)
            {
                Debug.LogWarning("State Animator Not Found on " + gameObject.name);
                return;
            }

            StateAnimator.Play(stateName[2], 0);
        }
    }
}
