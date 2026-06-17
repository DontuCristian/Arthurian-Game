using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField]
    PartyManager _partyManager;
    
    // Members
    private Vector2 _moveInput;

    void Update()
    {
        _partyManager.ActiveCharacter.Move(_moveInput);
    }

    #region InputForwarding
    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void BasicAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _partyManager.ActiveCharacter.BasicAttack();
        }
    }

    public void UseSkill1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _partyManager.ActiveCharacter.UseSkill1();
        }
    }

    public void UseUltimate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _partyManager.ActiveCharacter.UseUltimate();
        }
    }

    public void ChangeCharacter(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _partyManager.SwitchToNextCharacter();
        }
    }

    #endregion
}
