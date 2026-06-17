using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public float MaxHealth { get; private set; }

    [field: SerializeField] public float Power { get; private set; }

    [field: SerializeField] public float Defence { get; private set; }

}
