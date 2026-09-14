namespace Arthur.Core
{
    public interface IGameMode
    {
        void Initialize(GameContext context);

        void Shutdown();
    }
}
