using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    RocketType rocketId = RocketType.None;
    public delegate void RocketHit(Rocket rocketThatHit, HitReceiver hitReceiver);
    public event RocketHit OnHit;

    public RocketType RocketId {
        get{
            return rocketId;
        }
        set{
            if(rocketId == RocketType.None && value != RocketType.None){
                rocketId = value;
            }else{
                Debug.LogError("Not allowed to change rocketId.");
            }
        }
    }
    protected override void Awake(){
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        HitReceiver hitReceiver = null;
        hitReceiver = other.GetComponent<HitReceiver>();    
        OnHit(this, hitReceiver);
    }

    public void Die(){
        OnHit = null;
    }
}
