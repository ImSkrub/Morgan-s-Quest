using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    int Speed { get; }
    Rigidbody2D Rigidbody { get; }

}
