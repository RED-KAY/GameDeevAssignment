using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitReceiver : MonoBehaviour
{
    [HideInInspector]    
    public Collider collider;

    protected virtual void Awake(){
        collider = this.GetComponent<Collider>();        
    }

    public virtual void GotHit(Projectile projectile){
        // Debug.Log("HitReceiver's called");
    }
}
