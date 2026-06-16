using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    protected PartyManager _partyManager;
    protected BaseCharacter _attachedCharacter;

    void Awake()
    {
        _attachedCharacter = GetComponent<BaseCharacter>();
        
        _partyManager = FindObjectOfType<PartyManager>();

        if (_partyManager == null)
        {
            Debug.LogError("No party manager found");
        }
    }
}
