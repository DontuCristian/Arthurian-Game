using UnityEngine;

public class MerlinAI : CompanionAI
{
    [SerializeField] protected float _minFollowDist = 1.5f;
    [SerializeField] protected float _maxFollowDist = 6f;
    [SerializeField] protected float _idealFollowDist = 3f;
    
    
    private float DistanceToTarget => Vector2.Distance( transform.position, FollowTarget.transform.position);
    
    // Update is called once per frame
    void Update()
    {
        if (FollowTarget == null)
        {
            _attachedCharacter.Move(Vector2.zero);
            return;
        }


        float distance = DistanceToTarget;

        if (distance > _maxFollowDist)
        {
            Follow(1.5f);
        }
        else if (distance > _idealFollowDist)
        {
            float speedPercentage =
                Mathf.Lerp(
                    0f,
                    1.5f,
                    Mathf.InverseLerp(
                        _idealFollowDist,
                        _maxFollowDist,
                        distance));

            Follow(speedPercentage);
        }
        else if (distance < _minFollowDist)
        {
            Follow(-0.3f);
        }
        else
        {
            _attachedCharacter.Move(Vector2.zero);
        }
    }

    void Follow(float speedPercentage)
    {
        var direction = GetDirectionToTarget();

        _attachedCharacter.Move(direction * speedPercentage);
    }

    Vector2 GetDirectionToTarget()
    {
        Vector2 target =  FollowTarget.transform.position;
        Vector2 direction = target - (Vector2)transform.position;
        
        direction.Normalize();
        
        return direction;
    }
}
