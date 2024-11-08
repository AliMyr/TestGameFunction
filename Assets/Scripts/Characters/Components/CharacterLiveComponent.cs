using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    public float MaxHealth { get; set; } = 50;
    public float Health { get; set; }

    public event System.Action<Character> OnCharacterDeath;

    public CharacterLiveComponent()
    {
        Health = MaxHealth;
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            SetDeath();
        }
    }

    private void SetDeath()
    {
        Debug.Log("Character is dead");

        OnCharacterDeath?.Invoke(null);
    }
}
