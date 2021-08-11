using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Game : MonoBehaviour
{

    

    public RocketFactory rocketFactory;
    public KeyCode redKeyCode = KeyCode.Alpha1;
    public KeyCode greenKeyCode = KeyCode.Alpha2;
    public KeyCode blueKeyCode = KeyCode.Alpha3;
    [SerializeField]
    Transform[] rocketSpawns;
    public bool AutoFire{get; set;}
    TextMeshProUGUI redValue, greenValue, blueValue;
    GameObject autoFirePanel, firePanel;
    public bool FireRedRocket{get; set;}
    public bool FireGreenRocket{get; set;}
    public bool FireBlueRocket{get; set;}

    Slider redSlider, greenSlider, blueSlider;
    [SerializeField] float fireRateMultiplier = 0.3f;
    [SerializeField]float redAutoFireWaitTime, greenAutoFireWaitTime, blueAutoFireWaitTime;

    void Start(){
        
        autoFirePanel = GameObject.Find("Canvas").transform.Find("AutoFirePanel").gameObject;
        firePanel = GameObject.Find("Canvas").transform.Find("FirePanel").gameObject;

        redSlider = autoFirePanel.transform.Find("RedSlider").GetComponent<Slider>();
        greenSlider = autoFirePanel.transform.Find("GreenSlider").GetComponent<Slider>();
        blueSlider = autoFirePanel.transform.Find("BlueSlider").GetComponent<Slider>();

        redValue = autoFirePanel.transform.Find("RedValue").GetComponent<TextMeshProUGUI>();
        greenValue = autoFirePanel.transform.Find("GreenValue").GetComponent<TextMeshProUGUI>();
        blueValue = autoFirePanel.transform.Find("BlueValue").GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if(!AutoFire){
            firePanel.SetActive(true);
            autoFirePanel.SetActive(false);

            if(Input.GetKeyDown(redKeyCode)){
                ShootProjectile(RocketType.Red);
            }else if (Input.GetKeyDown(greenKeyCode)){
                ShootProjectile(RocketType.Green);
            }else if(Input.GetKeyDown(blueKeyCode)){
                ShootProjectile(RocketType.Blue);
            }
        }else if(AutoFire){
            firePanel.SetActive(false);
            autoFirePanel.SetActive(true);

            if(FireRedRocket){                
                if(redAutoFireWaitTime <= 0){
                    redAutoFireWaitTime = fireRateMultiplier;
                    ShootProjectile(RocketType.Red);
                }
                redAutoFireWaitTime -= Time.deltaTime * redSlider.value;
            }
            if(FireGreenRocket){
                if(greenAutoFireWaitTime <= 0){
                    greenAutoFireWaitTime = fireRateMultiplier;
                    ShootProjectile(RocketType.Green);
                }
                greenAutoFireWaitTime -= Time.deltaTime * greenSlider.value;                
            }
            if(FireBlueRocket){
                if(blueAutoFireWaitTime <= 0){
                    blueAutoFireWaitTime = fireRateMultiplier;
                    ShootProjectile(RocketType.Blue);
                }
                blueAutoFireWaitTime -= Time.deltaTime * blueSlider.value;                
            }
            AssignValues();
        }
        
    }

    public void AssignValues(){
        redValue.text = redSlider.value.ToString();
        blueValue.text = blueSlider.value.ToString();
        greenValue.text = greenSlider.value.ToString();
    }

    void ShootProjectile(RocketType projectile){
        Rocket instance = rocketFactory.Get(projectile);                
        Transform t = instance.transform;
        t.localPosition = rocketSpawns[((int)projectile)-1].position;
        instance.OnHit += HitProjectile;
        instance.Shoot();
    }

    void HitProjectile(Rocket rocketThatHit, HitReceiver hitReceiver){
        rocketThatHit.Die();        
        rocketFactory.Reclaim(rocketThatHit);
        if(hitReceiver != null)
            hitReceiver.GotHit(rocketThatHit);
    }
}

public enum RocketType{
    None,
    Red,
    Green,
    Blue
}

