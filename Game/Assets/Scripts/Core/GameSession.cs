using UnityEngine;
using FishNet.Managing;

namespace Arthur.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PartyManager _partyManager;
        [SerializeField] private HumanController _initialController;
        [SerializeField] private BaseCharacter _initialCharacter;
        [SerializeField] private bool _startOnStart = true;
        [SerializeField] private GameMode _startupMode = GameMode.Singleplayer;
        [SerializeField] private NetworkingRole _startupRole = NetworkingRole.None;

        public static GameSession Instance { get; private set; }

        public GameContext Context { get; private set; }

        private IGameMode _gameMode;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (_startOnStart)
                StartSession(_startupMode, _startupRole);
        }

        public void StartSession(GameMode mode, NetworkingRole role)
        {
            StopSession();

            if (_partyManager == null || _initialController == null || _initialCharacter == null)
            {
                Debug.LogError("GameSession is missing its party, initial controller, or initial character reference.");
                return;
            }

            if (mode == GameMode.OnlineMultiplayer)
                EnsureNetworkManager();

            Context = new GameContext(
                mode,
                role,
                _partyManager,
                _initialController,
                _initialCharacter);

            _gameMode = CreateMode(mode);
            _gameMode.Initialize(Context);
        }

        public void StartHostSession() =>
            StartSession(GameMode.OnlineMultiplayer, NetworkingRole.Host);

        public void StartClientSession() =>
            StartSession(GameMode.OnlineMultiplayer, NetworkingRole.Client);

        public void StopSession()
        {
            _gameMode?.Shutdown();
            _gameMode = null;
            Context = null;
        }

        private static IGameMode CreateMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Singleplayer => new SingleplayerMode(),
                GameMode.LocalMultiplayer => new LocalMultiplayerMode(),
                GameMode.OnlineMultiplayer => new OnlineMultiplayerMode(),
                _ => throw new System.ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        private static void EnsureNetworkManager()
        {
            if (Object.FindAnyObjectByType<NetworkManager>() != null)
                return;

            GameObject networkManagerObject = new GameObject("NetworkManager");
            networkManagerObject.AddComponent<NetworkManager>();
        }
    }
}
