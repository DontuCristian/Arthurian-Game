using UnityEngine;

public class MerlinAI : CompanionAI
{
    [SerializeField] protected float _minFollowDist = 1.5f;
    [SerializeField] protected float _maxFollowDist = 6f;
    [SerializeField] protected float _idealFollowDist = 3f;
    
    
    private float DistanceToTarget => Vector2.Distance( transform.position, _partyManager.ActiveCharacter.transform.position);
    
    // Update is called once per frame
    void Update()
    {
        if (_partyManager == null)
        {
            return;
        }
        
        if(DistanceToTarget > _maxFollowDist)
        {
            Follow(1f);
        }
        else if (DistanceToTarget < _idealFollowDist && DistanceToTarget > _minFollowDist)
        {
            //Do nothing
            _attachedCharacter.Move(Vector2.zero);
        }
        else if(DistanceToTarget > _idealFollowDist &&
                !_partyManager.ActiveCharacter.IsMoving)
        {
            Follow(0.5f);
        }
        else if (DistanceToTarget > _idealFollowDist &&
                 _partyManager.ActiveCharacter.IsMoving)
        {
            Follow(1f);
        }
        else if (DistanceToTarget < _minFollowDist)
        {
            Follow(-0.3f);
        }
    }

    void Follow(float speedPercentage)
    {
        var direction = GetDirectionToTarget();

        _attachedCharacter.Move(direction * speedPercentage);
    }

    Vector2 GetDirectionToTarget()
    {
        Vector2 target =  _partyManager.ActiveCharacter.transform.position;
        Vector2 direction = target - (Vector2)transform.position;
        
        direction.Normalize();
        
        return direction;
    }
}
