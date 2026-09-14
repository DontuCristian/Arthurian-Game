namespace Arthur.Core
{
    public enum GameMode
    {
        Singleplayer,
        LocalMultiplayer,
        OnlineMultiplayer
    }

    public enum NetworkingRole
    {
        None,
        Host,
        Client,
        DedicatedServer
    }

    public sealed class GameContext
    {
        public GameMode Mode { get; }
        public NetworkingRole Role { get; }
        public global::PartyManager PartyManager { get; }
        public global::HumanController InitialController { get; }
        public global::BaseCharacter InitialCharacter { get; }

        public bool HasNetworking =>
            Role == NetworkingRole.Host ||
            Role == NetworkingRole.DedicatedServer ||
            Role == NetworkingRole.Client;

        public bool IsServer =>
            Role == NetworkingRole.Host ||
            Role == NetworkingRole.DedicatedServer;

        public bool IsHost => Role == NetworkingRole.Host;
        public bool IsClient => Role == NetworkingRole.Client;

        public bool IsLocal =>
            Mode == GameMode.Singleplayer ||
            Mode == GameMode.LocalMultiplayer;

        public bool IsDedicated =>
            Role == NetworkingRole.DedicatedServer;

        public GameContext(
            GameMode mode,
            NetworkingRole role,
            global::PartyManager partyManager,
            global::HumanController initialController,
            global::BaseCharacter initialCharacter)
        {
            Mode = mode;
            Role = role;
            PartyManager = partyManager;
            InitialController = initialController;
            InitialCharacter = initialCharacter;
        }
    }

}
