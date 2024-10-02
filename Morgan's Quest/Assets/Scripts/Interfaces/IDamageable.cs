using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float currentHealth { get; }

    void GetDamage(int value);
    void Die();
}
