using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Animator))]
public class DamagePopUpText : MonoBehaviour
{
    public Animator animator;
    [HideInInspector]
    public DamagePopUp damagePopUp;
    public TextMeshProUGUI damageText;
    // Start is called before the first frame update
    void Awake()
    {
        if(damageText == null){
            damageText = GetComponent<TextMeshProUGUI>();
            damagePopUp = transform.parent.parent.GetComponent<DamagePopUp>();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
            damagePopUp.Reclaim(this);
        }
    }
}
