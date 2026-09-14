using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    protected BaseCharacter _attachedCharacter;

    public BaseCharacter FollowTarget { get; private set; }

    protected virtual void Awake()
    {
        _attachedCharacter = GetComponent<BaseCharacter>();
    }

    public void SetFollowTarget(BaseCharacter target)
    {
        FollowTarget = target;
    }
}