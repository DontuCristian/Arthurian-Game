using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private List<BaseCharacter> _party = new();

    private readonly Dictionary<HumanController, BaseCharacter> _controlledCharacters = new();

    public IReadOnlyList<BaseCharacter> Party => _party;

    public event Action<HumanController, BaseCharacter, BaseCharacter> ControlChanged;

    public bool TryGetControlledCharacter(HumanController controller, out BaseCharacter character)
    {
        return _controlledCharacters.TryGetValue(controller, out character);
    }

    public bool AssignController(HumanController controller, BaseCharacter character)
    {
        if (controller == null || character == null || !_party.Contains(character))
            return false;

        if (IsControlledByAnotherController(controller, character))
            return false;

        _controlledCharacters.TryGetValue(controller, out BaseCharacter previousCharacter);

        if (previousCharacter == character)
            return true;

        if (previousCharacter != null)
            previousCharacter.SetController(null);

        _controlledCharacters[controller] = character;
        character.SetController(controller);

        ControlChanged?.Invoke(controller, previousCharacter, character);
        return true;
    }

    public bool SwitchToNextCharacter(HumanController controller)
    {
        if (!TryGetControlledCharacter(controller, out BaseCharacter currentCharacter))
            return false;

        int currentIndex = _party.IndexOf(currentCharacter);

        for (int offset = 1; offset < _party.Count; offset++)
        {
            BaseCharacter candidate =
                _party[(currentIndex + offset) % _party.Count];

            if (!IsControlled(candidate))
                return AssignController(controller, candidate);
        }

        return false;
    }

    public bool ReleaseController(HumanController controller)
    {
        if (!_controlledCharacters.TryGetValue(controller, out BaseCharacter character))
            return false;

        _controlledCharacters.Remove(controller);
        character.SetController(null);

        ControlChanged?.Invoke(controller, character, null);
        return true;
    }

    public void AddCharacterToParty(BaseCharacter character)
    {
        if (character != null && !_party.Contains(character))
            _party.Add(character);
    }

    public void RemoveCharacterFromParty(BaseCharacter character)
    {
        if (character == null || !_party.Contains(character))
            return;

        HumanController controller = GetControllerOf(character);

        if (controller != null)
            ReleaseController(controller);

        _party.Remove(character);
    }

    public List<BaseCharacter> GetPlayerControlledCharacters()
    {
        List<BaseCharacter> result = new();

        foreach (BaseCharacter character in _party)
        {
            if (character != null && character.IsPlayerControlled)
                result.Add(character);
        }

        return result;
    }

    private bool IsControlled(BaseCharacter character)
    {
        return GetControllerOf(character) != null;
    }

    private bool IsControlledByAnotherController(HumanController controller, BaseCharacter character)
    {
        HumanController currentController = GetControllerOf(character);
        return currentController != null && currentController != controller;
    }

    private HumanController GetControllerOf(BaseCharacter character)
    {
        foreach (var pair in _controlledCharacters)
        {
            if (pair.Value == character)
                return pair.Key;
        }

        return null;
    }
}