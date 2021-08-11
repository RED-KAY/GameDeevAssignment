using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Damageable))]
public class DamagePopUp : MonoBehaviour
{
    //TextMeshProUGUI damageText;

    Damageable damageable;

    public DamagePopUpText damagePrefab;

    List<DamagePopUpText> pool;
    Canvas canvas;

    void Awake(){
        damageable = GetComponent<Damageable>();
        pool = new List<DamagePopUpText>();  
        //damageText = transform.Find("Canvas").Find("Damage").GetComponent<TextMeshProUGUI>();
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        damageable.OnDamageTaken += ShowPopUp;
    }

    private void ShowPopUp(int damageAmount){
        DamagePopUpText instance = GetPopUp();
        instance.damagePopUp = this;
        instance.transform.SetParent(canvas.transform);
        instance.transform.localPosition = new Vector3(0,0,0);
        instance.damageText.text = damageAmount.ToString();
        Animator a = instance.GetComponent<Animator>();
        a.SetTrigger("TookDamage");
        
    }

    private DamagePopUpText CreatePopUp(){
        DamagePopUpText instance = null;
        instance = Instantiate(damagePrefab);
        return instance;        
    }

    public void Reclaim(DamagePopUpText damageText){
        if(pool == null){
            pool = new List<DamagePopUpText>();  
        }
        pool.Add(damageText);
        damageText.gameObject.SetActive(false);
    }

    private DamagePopUpText GetPopUp(){
        DamagePopUpText instance = null;
        if(pool.Count - 1 >= 0){
            instance = GetFromPool();
            if(instance){
                instance.gameObject.SetActive(true);
            }else{
                instance = CreatePopUp();
            }
        }else{
            instance = CreatePopUp();
        }
        return instance;
    }

    private DamagePopUpText GetFromPool(){
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].gameObject.activeSelf){
                DamagePopUpText popUp = pool[i];
                pool.RemoveAt(i);
                return popUp;
            }
        }
        return null;
    }


}
