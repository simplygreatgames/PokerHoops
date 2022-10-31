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
        }
    }

    public class StartSeasonPlayerState : PlayerState
    {
        public StartSeasonPlayerState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            ToggleOffStartSeasonButton();
        }

        private void ToggleOffStartSeasonButton()
        {
            GameUIPanel.Instance.ToggleButtonEnabled(StateMachine.PlayerCoach, "StartSeasonButton", false);
        }
    }

    public class InGameInactiveState : PlayerState
    {
        public InGameInactiveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            ToggleOffReadingCardInput();
            ToggleOffDiscardButton();
        }

        private void ToggleOffReadingCardInput()
        {
            StateMachine.PlayerCoach.MouseInput.IsReadingCards = false;
        }

        private void ToggleOffDiscardButton()
        {
            GameUIPanel.Instance.ToggleButtonInteractable(StateMachine.PlayerCoach, "DiscardButton", false);
        }
    }

    public class InGameActiveDiscardState : PlayerState, Interfaces.IHandleCards
    {
        public InGameActiveDiscardState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            ToggleOnReadingCardInput();
            ToggleOnDiscardButton();
        }

        private void ToggleOnReadingCardInput()
        {
            StateMachine.PlayerCoach.MouseInput.IsReadingCards = true;
        }

        private void ToggleOnDiscardButton()
        {
            GameUIPanel.Instance.ToggleButtonEnabled(StateMachine.PlayerCoach, "DiscardButton", true);
            GameUIPanel.Instance.ToggleButtonInteractable(StateMachine.PlayerCoach, "DiscardButton", true);
        }

        private void DeclareCardForDiscard(Card cardClicked)
        {
            cardClicked.DeclareForDiscard(!cardClicked.IsToBeDiscarded);
        }

        private void PlayRejectDiscardAnimation(Card cardClicked)
        {
            Debug.Log("Playing Reject Discard Animation for " + cardClicked.CardScriptable.name);
        }

        #region IHandleCards Interface

        public void OnCardClicked(Card cardClicked, Enums.MouseInputType mouseInputType)
        {
            Debug.Log("Card Clicked: " + cardClicked.name);

            switch (mouseInputType)
            {
                case Enums.MouseInputType.LeftClicked:
                    if (cardClicked.CurrentOwner == StateMachine.PlayerCoach) DeclareCardForDiscard(cardClicked);
                    else PlayRejectDiscardAnimation(cardClicked);

                    break;

                case Enums.MouseInputType.RightClicked:
                    break;

                default:
                    break;
            }
        }

        #endregion
    }

    #endregion
}
