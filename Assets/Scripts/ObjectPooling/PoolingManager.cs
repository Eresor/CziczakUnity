using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolingManager : UnitySingleton<PoolingManager>
{

    public GameObject[] Pool;

    private Dictionary<GameObject,List<GameObject>> instances;

    public static GameObject Instantiate(GameObject obj)
    {
        return Instance.InstantiateObject(obj);
    }

    public static GameObject Instantiate(GameObject obj,Vector3 position, Quaternion rotation)
    {
        var newObj = Instance.InstantiateObject(obj);
        newObj.transform.position = position;
        newObj.transform.rotation = rotation;
        return newObj;
    }

    public static GameObject Instantiate(GameObject obj, Transform parent)
    {
        var newObj = Instance.InstantiateObject(obj);
        newObj.transform.parent = parent;
        return newObj;
    }

    public static void Destroy(GameObject obj)
    {
        Instance.DestroyObject(obj);
    }

    public GameObject InstantiateObject(GameObject obj)
    {
        if(instances == null)
            InitializeInstances();

        if(instances.ContainsKey(obj))
            instances.Add(obj, new List<GameObject>());

        var list = instances[obj];

        foreach (var gameObject in list)
        {
            if (gameObject.activeSelf)
                continue;

            return gameObject;
        }

        var newInstance = Instantiate(obj);
        list.Add(newInstance);
        return newInstance;
    }

    public void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    void InitializeInstances()
    {
        instances = new Dictionary<GameObject, List<GameObject>>();
        foreach (var originalGameObject in Pool)
        {
            instances.Add(originalGameObject,new List<GameObject>());
        }
    }
}
