using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent
{
    float MaxHealth { get; set; }

    float Health { get; set; }

    public void SetDamage(float damage);
}
