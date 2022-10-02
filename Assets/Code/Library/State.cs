using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public abstract class State
    {
        public abstract void OnStateEnter();
        public abstract void Tick();
        public abstract void OnStateExit();
    }

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

            StateAnimator.Play(stateName[2], 0);
        }
    }
}
