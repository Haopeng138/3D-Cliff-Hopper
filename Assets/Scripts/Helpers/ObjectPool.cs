using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<GameObject> PrefabsForPool;

    private List<GameObject> _pooledObjects = new List<GameObject>();

    public GameObject GetObjectFromPool(string objectName){

        Debug.Log("GetObjectFromPool " + PrefabsForPool.Count);
         var instance = _pooledObjects.FirstOrDefault(obj => obj.name == objectName);
      
        if (instance != null){
         
            _pooledObjects.Remove(instance);
            instance.SetActive(true);
            return instance;
        }

        var preFab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);

        if (preFab != null){
         
            instance = Instantiate(preFab,Vector3.zero,Quaternion.identity,transform);
            instance.name = objectName;
            return instance;
        }

        Debug.LogWarning("Prefab with name " + objectName + " not found");
        return null;
    }

    public void PoolObject(GameObject obj){
        obj.SetActive(false);
        _pooledObjects.Add(obj);
    }
}
