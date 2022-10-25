using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class GameState : State
    {
        public GameState(GameStateMachine stateMachine) => StateMachine = stateMachine;
        protected GameStateMachine StateMachine;

        public override void OnStateEnter()
        {
            StateMachine.AnimateState(this);
        }

        public override void Tick()
        {
        }

        public override void OnStateExit()
        {
        }
    }

    public class InitializeGameState : GameState
    {
        public InitializeGameState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            DrawPlayerCoachStartingHand();
            SetToDiscardState();
        }

        private void DrawPlayerCoachStartingHand()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach.GetType() == typeof(PlayerCoach))
                {
                    DealerManager.Instance.Deck.DrawFromDeck(StateMachine.Game.RoundSettings.StartingHandNumber, coach);
                    Debug.Log("Drawing Starting Hand For Coach: " + coach.gameObject.name);
                }
            }
        }

        private void SetToDiscardState() => StateMachine.SetGameState(new DiscardState(StateMachine));
    }

    public class DiscardState : GameState
    {
        public DiscardState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Debug.Log("Ready for Players to Discard");
        }
    }

    public class TransferState : GameState
    {
        public TransferState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }

    public class DrawState : GameState
    {
        public DrawState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }

    public class ScoreState : GameState
    {
        public ScoreState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }

    public class OvertimeState : GameState
    {
        public OvertimeState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }
    public class ClosedState : GameState
    {
        public ClosedState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }
    }
}
