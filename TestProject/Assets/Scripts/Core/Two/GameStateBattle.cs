using System;

namespace SecretLisa
{
    public class GameStateBattle : GameState
    {
        public GameStateBattle()
        {
            AddTransition(typeof(GameStateEventBattleEnd), typeof(GameStateBattleResult));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Console.WriteLine("Wait for player to click battle start");
        }

        public override void OnExit()
        {
            base.OnExit();
            Console.WriteLine("Game over, battle state exit");
        }
    }
}