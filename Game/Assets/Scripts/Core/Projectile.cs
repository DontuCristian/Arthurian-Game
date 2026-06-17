using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public Vector2 MovementDirection
    {
        get => _movementDirection;
        set => _movementDirection = value;
    }

    private Vector2 _movementDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movementDirection * _speed * Time.deltaTime);
    }
}
