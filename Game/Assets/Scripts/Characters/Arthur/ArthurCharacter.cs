using UnityEngine;

public class ArthurCharacter : BaseCharacter
{
    
    
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        
        _rb.MovePosition(currentPosition + direction * (_stats.Speed * Time.deltaTime));
    }
    public override void BasicAttack() {}
    public override void UseSkill1() {}
    public override void UseUltimate() {}
}
