using System;

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
            Console.WriteLine("Battle Ended, wait for player to return to the lobby");
        }

        public override void OnExit()
        {
            base.OnExit();
            Console.WriteLine("Battle ended, battle result state exit");
        }
    }
}