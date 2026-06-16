using UnityEngine;

public interface IControllableCharacter
{
    public void Move(Vector2 direction);

    public void BasicAttack();

    public void UseSkill1();

    public void UseUltimate();
}
