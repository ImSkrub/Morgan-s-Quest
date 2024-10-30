using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float CurrentHealth { get; }

    void GetDamage(int value);
    void Die();
}
