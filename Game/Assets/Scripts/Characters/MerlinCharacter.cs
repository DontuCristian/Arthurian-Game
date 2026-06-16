using UnityEngine;

public class MerlinCharacter : BaseCharacter
{
    public override void Move(Vector2 direction)
    {
        transform.Translate(direction * (4f * Time.deltaTime));
    }
    public override void BasicAttack() {}
    public override void UseSkill1() {}
    public override void UseUltimate() {}
}
