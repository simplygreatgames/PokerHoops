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

    public class DiscardActiveState : PlayerState
    {
        public DiscardActiveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

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

        public void OnCardClicked(Card cardClicked)
        {
            if (cardClicked.CurrentOwner == StateMachine.PlayerCoach)
            {
                ToggleDiscardStatus(cardClicked);
            }

            else
            {
                ToggleDontOwnCard(cardClicked);
            }
        }

        private void ToggleDiscardStatus(Card cardClicked)
        {
            Debug.Log("I own this card! Setting to Discard State");
            cardClicked.DeclareForDiscard(!cardClicked.IsToBeDiscarded);
        }

        private void ToggleDontOwnCard(Card cardClicked)
        {
            Debug.Log("Silly me, I don't own " + cardClicked.CardScriptable.name);
        }
    }

    #endregion
}
