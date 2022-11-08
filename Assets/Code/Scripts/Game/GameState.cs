using System.Collections;
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

    #region Unranked Game States

    public class InitializeGameState : GameState
    {
        public InitializeGameState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            SetPlayersToInGameState();
            DrawPlayerCoachStartingHand();
            SetToDiscardState();
        }

        private void SetPlayersToInGameState()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach is PlayerCoach)
                {
                    PlayerCoach playerCoach = (PlayerCoach) coach;
                    playerCoach.StateMachine.SetPlayerState(new InGameInactiveState(playerCoach.StateMachine));

                    if (playerCoach.IsLocalPlayer)
                        GameUIPanel.Instance.ScoreBoard.SetScoreBoard(null);
                }
            }
        }

        private void DrawPlayerCoachStartingHand()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach.GetType() == typeof(PlayerCoach))
                    DealerManager.Instance.Deck.DrawFromDeck(StateMachine.Game.RoundSettings.StartingHandNumber, coach);
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
            ToggleLocalPlayerInputState();
        }

        public override void OnStateExit()
        {
            base.OnStateEnter();
        }

        private void ToggleLocalPlayerInputState()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach is PlayerCoach)
                {
                    PlayerCoach playerCoach = (PlayerCoach)coach;

                    if (playerCoach.IsLocalPlayer) 
                        playerCoach.StateMachine.SetPlayerState(new InGameActiveDiscardState(playerCoach.StateMachine));
                }
            }
        }
    }

    public class TransferState : GameState
    {
        public TransferState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            TransferPlayerDiscardToCPU();
            GoToNextState();
        }

        private void TransferPlayerDiscardToCPU()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach is PlayerCoach)
                {
                    PlayerCoach playerCoach = (PlayerCoach)coach;
                    playerCoach.Hand.DiscardMarkedCards();
                }
            }
        }
        private void GoToNextState()
        {
            StateMachine.SetGameState(new WaitForDrawState(StateMachine));
        }
    }

    public class WaitForDrawState : GameState
    {
        public WaitForDrawState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetPlayerCoachesToInactiveGameState();
            AlertRoundManagerReadyToDraw();
        }

        private void SetPlayerCoachesToInactiveGameState()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
            {
                if (coach is PlayerCoach playerCoach)
                {
                    playerCoach.StateMachine.SetPlayerState(new InGameInactiveState(playerCoach.StateMachine));
                }
            }
        }

        private void AlertRoundManagerReadyToDraw()
        {
            RoundManager.Instance.MarkGameWaitingToDraw(StateMachine.Game);
        }
    }

    public class DrawState : GameState
    {
        public DrawState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            DrawCardsForBothPlayers();
            StateMachine.StartCoroutine(DelayForAnimationEnd());
        }

        private void DrawCardsForBothPlayers()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
                coach.Hand.DrawToFill();
        }

        public IEnumerator DelayForAnimationEnd()
        {
            yield return new WaitForSeconds(2);
            GoToNextState();
        }

        private void GoToNextState()
        {
            StateMachine.SetGameState(new ScoreState(StateMachine));
        }
    }

    public class ScoreState : GameState
    {
        public ScoreState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            SendHandsToBeScored();
            GoToNextState();
        }

        private void SendHandsToBeScored()
        {
            ScoreManager.Instance.ScoreGame(StateMachine.Game);
        }

        private void GoToNextState()
        {
            if (StateMachine.Game.IsTiedGame) StateMachine.SetGameState(new OvertimeState(StateMachine));
            else StateMachine.SetGameState(new ClosedState(StateMachine));
        }
    }

    public class OvertimeState : GameState
    {
        public OvertimeState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Tied Game! Going to overtime! Flipping for winner");
            RandomlySelectWinner();
        }

        private void RandomlySelectWinner()
        {
            int winner = Random.Range(1, 3);

            Coach winningCoach = StateMachine.Game.CoachesInGame[winner];

            StateMachine.Game.WinningScore = winningCoach.Hand.PokerScore;

            if (StateMachine.Game.CoachesInGame[0] is PlayerCoach playerCoach)
            {
                RecordData recordData = playerCoach.TeamRecord.GetLatestRecord();

                if (recordData == null)
                {
                    Debug.Log("No Record Available");
                    return;
                }

                recordData.PlayerWon = winningCoach.CoachID == playerCoach.CoachID;
            }

            StateMachine.SetGameState(new ClosedState(StateMachine));
        }
    }

    public class ClosedState : GameState
    {
        public ClosedState(GameStateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Game Over! " + StateMachine.Game.WinningScore.ScoreOwner.name + " won with a " + StateMachine.Game.WinningScore.PokerScoreType);

            StateMachine.StartCoroutine(CloseGame());
        }

        private IEnumerator CloseGame()
        {
            yield return new WaitForSeconds(5);

            RegisterClosedGame();
            CleanupGame();
        }

        private void CleanupGame()
        {
            foreach (Coach coach in StateMachine.Game.CoachesInGame)
                coach.Hand.CleanHand();
        }

        private void RegisterClosedGame()
        {
            RoundManager.Instance.MarkGameCompleted(StateMachine.Game);
        }
    }

    #endregion
}
