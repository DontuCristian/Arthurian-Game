using UnityEngine;

public class BasicCompanionAI : MonoBehaviour
{
    [SerializeField] 
    PartyManager _partyManager;
    
    BaseCharacter _attachedCharacter;

    void Start()
    {
        _attachedCharacter = GetComponent<BaseCharacter>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (_attachedCharacter == null || _attachedCharacter == _partyManager.ActiveCharacter)
        {
            return;
        }
        
        Vector2 target = new Vector2(_partyManager.ActiveCharacter.transform.position.x, _partyManager.ActiveCharacter.transform.position.y);

        Vector2 direction = target - new Vector2(transform.position.x, transform.position.y);
        
        direction.Normalize();
        transform.Translate(direction * (2f * Time.deltaTime));
    }
}
