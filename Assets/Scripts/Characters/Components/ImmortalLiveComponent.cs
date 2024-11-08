using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    float ILiveComponent.MaxHealth { get => 1; set { } }
    float ILiveComponent.Health { get => 1; set { } }

    public event System.Action<Character> OnCharacterDeath;

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal");
    }
}
