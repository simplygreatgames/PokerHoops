using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(PlayerStateMachineOperator stateMachineOperator) => StateMachineOperator = stateMachineOperator;
        protected PlayerStateMachineOperator StateMachineOperator;



        #region State Methods

        public override void OnStateEnter()
        {
            StateMachineOperator.AnimateState(this);
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

    public class InitializeState : PlayerStateMachine
    {
        public InitializeState(PlayerStateMachineOperator stateMachineOperator) : base(stateMachineOperator) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            BuildDeck();
            DrawStartingHand();
        }

        private void BuildDeck() => StateMachineOperator.Player.Deck.BuildDeck();
        private void DrawStartingHand() => StateMachineOperator.Player.Deck.DrawFromDeck(5);
    }

    #endregion
}
