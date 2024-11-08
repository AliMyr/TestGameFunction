using UnityEngine;

public interface ILiveComponent
{
    float MaxHealth { get; set; }
    float Health { get; set; }
    void SetDamage(float damage);

    event System.Action<Character> OnCharacterDeath;
}
