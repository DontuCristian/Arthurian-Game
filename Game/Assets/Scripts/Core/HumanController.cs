using UnityEngine;
using UnityEngine.InputSystem;

public class HumanController : MonoBehaviour
{
    [SerializeField] private PartyManager _partyManager;

    private Vector2 _moveInput;

    private void Update()
    {
        if (TryGetCharacter(out BaseCharacter character))
            character.Move(_moveInput);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void BasicAttack(InputAction.CallbackContext context)
    {
        if (context.started && TryGetCharacter(out BaseCharacter character))
            character.BasicAttack();
    }

    public void UseSkill1(InputAction.CallbackContext context)
    {
        if (context.started && TryGetCharacter(out BaseCharacter character))
            character.UseSkill1();
    }

    public void UseUltimate(InputAction.CallbackContext context)
    {
        if (context.started && TryGetCharacter(out BaseCharacter character))
            character.UseUltimate();
    }

    public void ChangeCharacter(InputAction.CallbackContext context)
    {
        if (context.started)
            _partyManager.SwitchToNextCharacter(this);
    }

    private bool TryGetCharacter(out BaseCharacter character)
    {
        character = null;

        return _partyManager != null &&
               _partyManager.TryGetControlledCharacter(this, out character);
    }
}