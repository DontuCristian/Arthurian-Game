using System;
using System.Data;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected CharacterStats _stats;
    
    public CompanionAI CompanionAI { get; private set; }
    public Vector2 MovementInput { get; private set; }
    
    public bool IsMoving => MovementInput.magnitude > 0.01f;
    
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        CompanionAI = GetComponent<CompanionAI>();
        _rb = GetComponent<Rigidbody2D>();
    }

    virtual public void Move(Vector2 direction)
    {
       MovementInput = direction; 
    }
    
    virtual public void BasicAttack(){}
    virtual public void UseSkill1(){}
    virtual public void UseUltimate(){}
}
