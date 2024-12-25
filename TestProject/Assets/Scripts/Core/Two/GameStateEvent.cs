namespace SecretLisa
{
    public interface IGameStateEvent
    {
    }

    public struct GameStateEventBattleStart : IGameStateEvent
    {
    }

    public struct GameStateEventBattleEnd : IGameStateEvent
    {
    }
    
    public struct GameStateEventExitBattle : IGameStateEvent
    {
    }
}