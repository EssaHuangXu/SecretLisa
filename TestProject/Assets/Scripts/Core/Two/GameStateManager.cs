using System;

namespace SecretLisa
{
    public class GameStateManager
    {
        public IGameState CurrentGameState { get; private set; } = new GameStateLobby();

        public void ChangeGameState(IGameStateEvent @event)
        {
            var result = CurrentGameState.HandleEvent(@event);
            if (result != null)
            {
                CurrentGameState = result;
            }
        }
    }
}