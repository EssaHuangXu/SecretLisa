using UnityEngine;

namespace SecretLisa
{
    public class GameStateLobby : GameState
    {
        public GameStateLobby()
        {
            AddTransition(typeof(GameStateEventBattleStart), typeof(GameStateBattle));    
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Wait for battle start!");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Lobby state exit");
        }
    }
}