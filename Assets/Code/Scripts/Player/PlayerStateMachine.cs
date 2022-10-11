﻿namespace SimplyGreatGames.PokerHoops
{
    public class PlayerStateMachine : StateMachineOperator
    {
        public PlayerState CurrentState;
        public Player Player { get; private set; }

        public void RegisterStateMachine(Player player) => Player = player;

        public void SetPlayerState(PlayerState nextState)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit();

            CurrentState = nextState;

            if (CurrentState != null)
                CurrentState.OnStateEnter();
        }
    }
}
