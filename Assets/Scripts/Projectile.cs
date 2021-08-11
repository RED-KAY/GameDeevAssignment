using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public int minDamage = 5, maxDamage = 15;
    protected Rigidbody _RB;

    protected virtual void Awake()
    {
        _RB = GetComponent<Rigidbody>();        
        
    }

    public virtual void Shoot(){
        _RB.velocity = new Vector3(0, 0, -speed);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
