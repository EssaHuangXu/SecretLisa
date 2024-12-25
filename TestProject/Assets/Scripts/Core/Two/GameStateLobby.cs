using System;

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
            Console.Write("Wait for battle start!");
        }

        public override void OnExit()
        {
            base.OnExit();
            Console.Write("Lobby state exit");
        }
    }
}