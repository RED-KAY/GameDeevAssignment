using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class RocketFactory : ScriptableObject
{
    [SerializeField]
    Rocket[] prefabs;
    [SerializeField]
    bool recycle;
    List<Rocket>[] pools;

    public Rocket Get(RocketType rocket){
        Rocket instance = null;
        if(recycle){
            if(pools == null){
                CreatePools();
            }
            List<Rocket> pool = pools[((int)rocket)-1];
            int lastIndex = pool.Count - 1;

            if(lastIndex >= 0){ 
                instance = GetFromPool(pool);
                if(instance){
                    instance.gameObject.SetActive(true);
                }else{
                    instance = CreateRocket(rocket);
                }             
            }else{
                instance = CreateRocket(rocket);
            }
        }else{
            instance = CreateRocket(rocket);
        }
        return instance;
    }

    public Rocket GetRandom (){
        return Get((RocketType)Random.Range(0, 4));
    }

    public void Reclaim (Rocket rocketToRecycle){
        if(recycle){
            if(pools==null){
                CreatePools();                
            }
            pools[((int)rocketToRecycle.RocketId)-1].Add(rocketToRecycle);
            rocketToRecycle.gameObject.SetActive(false);
        }else{
            Destroy(rocketToRecycle.gameObject);
        }
    }

    void CreatePools () {
		pools = new List<Rocket>[prefabs.Length];
		for (int i = 0; i < pools.Length; i++) {
			pools[i] = new List<Rocket>();
		}
	}


    Rocket CreateRocket(RocketType rocketType){
        Rocket instance=null;
        if(rocketType != RocketType.None){
            instance = Instantiate(prefabs[((int)rocketType)-1]);
            instance.RocketId = rocketType; 
        }else{
            Debug.LogError("Invalid Rocket Type: " + rocketType.ToString());
        }
        return instance;
    }

    Rocket GetFromPool(List<Rocket> pool){
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].gameObject.activeSelf){
                Rocket rocketToBe = pool[i];
                pool.RemoveAt(i);
                return rocketToBe;
            }
        }
        return null;
    }
}
