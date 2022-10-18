using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class PlayerState : State
    {
        protected PlayerStateMachine StateMachine;
        public PlayerState(PlayerStateMachine stateMachine) => StateMachine = stateMachine;

        #region State Methods

        public override void OnStateEnter()
        {
            StateMachine.AnimateState(this);
        }

        public override void OnStateExit()
        {
        }

        public override void Tick()
        {
        }

        #endregion
    }

    #region Player States

    public class InitializePlayerState : PlayerState
    {
        public InitializePlayerState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            DrawStartingHand();
        }

        private void DrawStartingHand() => DealerManager.Instance.Deck.DrawFromDeck(5, StateMachine.PlayerCoach);
    }

    #endregion
}
