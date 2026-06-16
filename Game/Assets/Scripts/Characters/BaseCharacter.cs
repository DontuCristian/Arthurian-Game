using System;
using System.Data;
using UnityEngine;

public class BaseCharacter : MonoBehaviour, IControllableCharacter
{
    
    virtual public void Move(Vector2 direction){}
    virtual public void BasicAttack() {}
    virtual public void UseSkill1() {}
    virtual public void UseUltimate() {}
}
