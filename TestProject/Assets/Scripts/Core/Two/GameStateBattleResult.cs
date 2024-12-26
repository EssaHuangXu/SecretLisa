using UnityEngine;

namespace SecretLisa
{
    public class GameStateBattleResult : GameState
    {
        public GameStateBattleResult()
        {
            AddTransition(typeof(GameStateEventExitBattle), typeof(GameStateLobby));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Battle ended, wait for player to return to the lobby");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Battle ended, battle result state exit");
        }
    }
}