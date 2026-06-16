using UnityEngine;

public class ArthurCharacter : BaseCharacter
{
    
    
    public override void Move(Vector2 direction)
    {
        transform.Translate(direction * (5f * Time.deltaTime));
    }
    public override void BasicAttack() {}
    public override void UseSkill1() {}
    public override void UseUltimate() {}
}
