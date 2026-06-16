using UnityEngine;
using UnityEngine.TextCore.Text;

public class MerlinCharacter : BaseCharacter
{
    [SerializeField] private Projectile _projectile;
    
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        
        _rb.MovePosition( currentPosition + direction * (_stats.Speed * Time.deltaTime));
    }
    
    public override void BasicAttack()
    {
        var bullet = Instantiate(_projectile, transform.position, Quaternion.identity);
        
        var randomDirection = Random.insideUnitCircle.normalized;
        
        bullet.MovementDirection = randomDirection;
    }
    public override void UseSkill1() {}
    public override void UseUltimate() {}
}
