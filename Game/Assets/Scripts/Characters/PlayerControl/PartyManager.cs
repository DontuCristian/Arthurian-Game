using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacter> _party;

    private int _activeCharacterIndex;

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
        _activeCharacterIndex++;

        if (_activeCharacterIndex >= _party.Count)
        {
            _activeCharacterIndex = 0;
        }

        Debug.Log($"Now controlling {ActiveCharacter.name}");
    }
}
