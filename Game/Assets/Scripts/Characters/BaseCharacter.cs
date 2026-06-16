using System;
using System.Data;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IControllableCharacter
{
    
    public CompanionAI CompanionAI { get; private set; }
    public Vector2 MovementInput { get; private set; } = new();
    public bool IsMoving => MovementInput.magnitude > 0.01f;

    protected virtual void Awake()
    {
        CompanionAI = GetComponent<CompanionAI>();
    }

    virtual public void Move(Vector2 direction)
    {
       MovementInput = direction; 
    }

    virtual public void BasicAttack(){}
    virtual public void UseSkill1(){}
    virtual public void UseUltimate(){}
}
