
using UnityEngine;

namespace Arthur.Core
{
    public class SingleplayerMode : IGameMode
    {
        private GameContext _context;

        public void Initialize(GameContext context)
        {
            _context = context;
            _context.PartyManager.ControlChanged += OnControlChanged;

            if (!_context.PartyManager.AssignController(
                    _context.InitialController,
                    _context.InitialCharacter))
            {
                Debug.LogError("SingleplayerMode could not assign the initial controller to its character.");
            }
        }

        public void Shutdown()
        {
            if (_context == null)
                return;

            _context.PartyManager.ControlChanged -= OnControlChanged;
            _context.PartyManager.ReleaseController(_context.InitialController);
            _context = null;
        }

        private void OnControlChanged( global::HumanController controller, global::BaseCharacter previousCharacter, global::BaseCharacter currentCharacter)
        {
            if (controller != _context.InitialController)
                return;

            foreach (global::BaseCharacter character in _context.PartyManager.Party)
            {
                if (character != null && !character.IsPlayerControlled && character.CompanionAI != null)
                    character.CompanionAI.SetFollowTarget(currentCharacter);
            }
        }
    }
}