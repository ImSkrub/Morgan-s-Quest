using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    BulletPool BulletPool { get; }
    float BulletSpeed { get; }
    float FireRate { get; }
    float NextFire { get; }

    void Shoot(float x, float y);
}
