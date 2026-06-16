using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacter> _party;

    private int _activeCharacterIndex = 0;

    public BaseCharacter ActiveCharacter
    {
        get
        {
            return _party[_activeCharacterIndex];
        }
    }

    public IReadOnlyList<BaseCharacter> Party => _party;

    public void SwitchToNextCharacter()
    {
        _party[_activeCharacterIndex].CompanionAI.enabled = true;
        
        _activeCharacterIndex++;

        if (_activeCharacterIndex >= _party.Count)
        {
            _activeCharacterIndex = 0;
        }
        
        _party[_activeCharacterIndex].CompanionAI.enabled = false;
        
        Debug.Log($"Now controlling {ActiveCharacter.name}");
    }
}
