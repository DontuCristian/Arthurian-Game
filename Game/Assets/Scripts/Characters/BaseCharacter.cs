using System;
using System.Data;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected CharacterStats _stats;
    
    public CompanionAI CompanionAI { get; private set; }
    public HumanController Controller { get; private set; }

    public bool IsPlayerControlled => Controller != null;
    public Vector2 MovementInput { get; private set; }
    public Vector2 CharacterDirection { get; private set; }
    
    public bool IsMoving => MovementInput.magnitude > 0.01f;
    
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        CompanionAI = GetComponent<CompanionAI>();
        _rb = GetComponent<Rigidbody2D>();
        
        CharacterDirection = Vector2.right;
    }

    internal void SetController(HumanController controller)
    {
        Controller = controller;

        if (CompanionAI == null)
            return;

        CompanionAI.enabled = controller == null;

        if (controller != null)
            CompanionAI.SetFollowTarget(null);
    }

    protected virtual void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + MovementInput * _stats.Speed * Time.fixedDeltaTime);
    }

    // Don't call in Update every frame
    virtual public void Move(Vector2 direction)
    {
        MovementInput = direction; 
        if(direction.magnitude > 0.01f)
            CharacterDirection = direction.normalized;
    }
    
    virtual public void BasicAttack(){}
    virtual public void UseSkill1(){}
    virtual public void UseUltimate(){}
}
