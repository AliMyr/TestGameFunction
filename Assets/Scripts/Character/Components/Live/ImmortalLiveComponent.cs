using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    float ILiveComponent.MaxHealth { get => 1; set { } }
    float ILiveComponent.Health { get => 1; set { } }

    public void SetDamage(float damage)
    {
        Debug.Log("I am Aboba");
    }
}
