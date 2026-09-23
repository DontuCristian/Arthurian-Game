
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;

namespace Arthur.Core
{
    public class OnlineMultiplayerMode : IGameMode
    {
        private NetworkManager _networkManager;
        private GameContext _context;
        private GameObject _characterTemplate;

        public void Initialize(GameContext context)
        {
            _context = context;
            _networkManager = Object.FindAnyObjectByType<NetworkManager>();

            if (_networkManager == null)
            {
                Debug.LogError("OnlineMultiplayerMode requires a FishNet NetworkManager in the scene.");
                return;
            }

            _characterTemplate = context.InitialCharacter.gameObject;
            DisableOfflineCharacters();
            _networkManager.ServerManager.OnAuthenticationResult += OnAuthenticationResult;

            switch (context.Role)
            {
                case NetworkingRole.Host:
                    _networkManager.ServerManager.StartConnection();
                    _networkManager.ClientManager.StartConnection();
                    break;
                case NetworkingRole.Client:
                    _networkManager.ClientManager.StartConnection();
                    break;
                case NetworkingRole.DedicatedServer:
                    _networkManager.ServerManager.StartConnection();
                    break;
                default:
                    Debug.LogError("OnlineMultiplayerMode requires Host, Client, or DedicatedServer as its networking role.");
                    break;
            }
        }

        public void Shutdown()
        {
            if (_networkManager == null)
                return;

            _networkManager.ServerManager.OnAuthenticationResult -= OnAuthenticationResult;

            if (_networkManager.ClientManager.Started)
                _networkManager.ClientManager.StopConnection();

            if (_networkManager.ServerManager.Started)
                _networkManager.ServerManager.StopConnection(true);

            _networkManager = null;
            RestoreOfflineCharacters();
            _characterTemplate = null;
            _context = null;
        }

        private void OnAuthenticationResult(NetworkConnection connection, bool authenticated)
        {
            if (authenticated && _networkManager.ServerManager.Started)
                SpawnPlayer(connection);
        }

        private void SpawnPlayer(NetworkConnection connection)
        {
            Vector3 spawnPosition = new(connection.ClientId * 2f, 0f, 0f);
            GameObject player = Object.Instantiate(_characterTemplate, spawnPosition, Quaternion.identity);
            player.SetActive(true);

            NetworkObject networkObject = player.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                Debug.LogError("The online player prefab requires a NetworkObject component.");
                Object.Destroy(player);
                return;
            }

            _networkManager.ServerManager.Spawn(networkObject, connection);
        }

        private void DisableOfflineCharacters()
        {
            _context.InitialController.enabled = false;

            foreach (BaseCharacter character in _context.PartyManager.Party)
                character.gameObject.SetActive(false);
        }

        private void RestoreOfflineCharacters()
        {
            if (_context == null)
                return;

            _context.InitialController.enabled = true;

            foreach (BaseCharacter character in _context.PartyManager.Party)
                character.gameObject.SetActive(true);
        }
    }
}
