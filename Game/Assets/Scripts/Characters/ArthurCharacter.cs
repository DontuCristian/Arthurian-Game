using UnityEngine;

public class ArthurCharacter : BaseCharacter
{
    
    
    public override void Move(Vector2 direction)
    {
        transform.Translate(direction * (5f * Time.deltaTime));
    }
    public void BasicAttack() {}
    public void UseSkill1() {}
    public void UseUltimate() {}
}
