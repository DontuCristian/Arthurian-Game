using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseCharacter))]
public sealed class NetworkCharacterController : NetworkBehaviour
{
    private BaseCharacter _character;
    private Vector2 _lastInput;

    private void Awake()
    {
        _character = GetComponent<BaseCharacter>();
    }

    public override void OnStartClient()
    {
        DisableCompanionAi();
    }

    public override void OnStartServer()
    {
        DisableCompanionAi();
    }

    private void DisableCompanionAi()
    {
        CompanionAI companionAI = GetComponent<CompanionAI>();
        if (companionAI != null)
            companionAI.enabled = false;
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        Vector2 movement = ReadMovementInput();
        if (movement == _lastInput)
            return;

        _lastInput = movement;
        SetMovementServerRpc(movement);
    }

    [ServerRpc]
    private void SetMovementServerRpc(Vector2 movement)
    {
        _character.Move(Vector2.ClampMagnitude(movement, 1f));
    }

    private static Vector2 ReadMovementInput()
    {
        Vector2 movement = Gamepad.current?.leftStick.ReadValue() ?? Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return movement;

        if (keyboard.wKey.isPressed)
            movement.y += 1f;
        if (keyboard.sKey.isPressed)
            movement.y -= 1f;
        if (keyboard.aKey.isPressed)
            movement.x -= 1f;
        if (keyboard.dKey.isPressed)
            movement.x += 1f;

        return Vector2.ClampMagnitude(movement, 1f);
    }
}
