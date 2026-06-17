using UnityEngine;

public class ArthurCharacter : BaseCharacter
{
    
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackHalfAngle = 30f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _didLastAttackHit;

    public override void BasicAttack()
    {
        _didLastAttackHit  = false;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll( transform.position, _attackRange, _enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Vector2 toTarget = ((Vector2)hit.transform.position - (Vector2)transform.position).normalized;

            float angle = Vector2.Angle(CharacterDirection, toTarget);

            if (angle <= _attackHalfAngle)
            {
                _didLastAttackHit = true;
                
                var sprite = hit.GetComponent<SpriteRenderer>();
                
                sprite.color = Color.lightSeaGreen;
            }
        }
    }
    
    public override void UseSkill1() {}
    public override void UseUltimate() {}

    private void OnDrawGizmosSelected()
    {
        Vector2 direction = CharacterDirection.normalized;

        Gizmos.color =
            _didLastAttackHit
                ? Color.red
                : Color.yellow;

        Vector3 origin = transform.position;

        Vector2 left =
            Quaternion.Euler(0, 0, -_attackHalfAngle) *
            direction;

        Vector2 right =
            Quaternion.Euler(0, 0, _attackHalfAngle) *
            direction;

        Gizmos.DrawLine(
            origin,
            origin + (Vector3)(left * _attackRange));

        Gizmos.DrawLine(
            origin,
            origin + (Vector3)(right * _attackRange));

        const int segments = 20;

        Vector3 previous =
            origin + (Vector3)(left * _attackRange);

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;

            float angle =
                Mathf.Lerp(
                    -_attackHalfAngle,
                    _attackHalfAngle,
                    t);

            Vector2 point =
                (Quaternion.Euler(0, 0, angle) * direction)
                * _attackRange;

            Vector3 current =
                origin + (Vector3)point;

            Gizmos.DrawLine(previous, current);

            previous = current;
        }
    }
}
