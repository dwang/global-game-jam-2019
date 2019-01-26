using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public virtual void Damage(PlayerController player)
    {
        Destroy(player.gameObject);
    }
}
