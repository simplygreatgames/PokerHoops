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
            BuildDeck();
            DrawStartingHand();
        }

        private void BuildDeck() 
        { 
            StateMachine.Player.Deck.BuildDeck(); 
            Debug.Log("Should be refactored! Players will not have individual decks. Instead dealer will own the deck"); 
        }
        private void DrawStartingHand() => StateMachine.Player.Deck.DrawFromDeck(5);
    }

    #endregion
}
