using UnityEngine;

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
            Debug.Log("Player in battle");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Game over, battle state exit");
        }
    }
}