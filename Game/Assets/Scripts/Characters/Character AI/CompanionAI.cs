using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    [SerializeField] protected PartyManager _partyManager;
    
    protected BaseCharacter _attachedCharacter;

    void Start()
    {
        _attachedCharacter = GetComponent<BaseCharacter>();
    }
}
