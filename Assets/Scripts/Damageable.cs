using UnityEngine;
public class Damageable : HitReceiver{

    [SerializeField]
    private int health = 100;

    public int Health{
        get{
            return health;
        }
        set{            
            if(health >= value){
                health = value; 
                           
            }if(health<=0){
                health = 0;
                Die();
            }
        }
    }

    public delegate void TookDamage(int damageAmount);
    public event TookDamage OnDamageTaken;

    public override void GotHit(Projectile projectile)
    {
        base.GotHit(projectile);
        //Debug.Log("Damageable's called");
        TakeDamage(Random.Range(projectile.minDamage, projectile.maxDamage+1));
    }

    public void TakeDamage(int damageAmount){
        OnDamageTaken(damageAmount);
        Health = Health - damageAmount;
        ShowPopUp(damageAmount); 
    }

    private void ShowPopUp(int damageAmount){

    }

    private void Die(){
        Debug.Log(name + " Dead! Reassigning health...");
        Health = 100;
    }
}