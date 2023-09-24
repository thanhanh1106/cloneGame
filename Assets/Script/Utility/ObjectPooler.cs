using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefabs;
        public int InitialSize;
    }

    public List<Pool> Pools;
    Dictionary<string, Queue<GameObject>> poolDictionary;

    protected override void Awake()
    {
        MakeSingleton(true);
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int index = 0; index < pool.InitialSize; index++)
            {
                GameObject obj = Instantiate(pool.Prefabs);
                obj.SetActive(false);
                obj.transform.parent = transform;
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject GetGameObjectFormPool(string tag, Vector3 postion, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new MissingMemberException($"Pool with tag {tag} doesn't exit!");
        }

        GameObject objectToGet;
        // nếu còn game object trong pool thì lấy nó ra dùng bình thường 
        if (poolDictionary[tag].Count > 0)
        {
            objectToGet = poolDictionary[tag].Dequeue();
        }
        // nếu hết game object trong pool thì tạo nó ra
        else
        {
            objectToGet = Instantiate(GetPrefabsWithTag(tag));
        }

        objectToGet.SetActive(true);
        objectToGet.transform.position = postion;
        objectToGet.transform.rotation = rotation;
        objectToGet.transform.parent = null;
        return objectToGet;
    }

    public void ReturnGameObjectToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
            throw new MissingMemberException($"Pool with tag {tag} doesn't exit!");
        obj.transform.parent = this.transform;
        obj.SetActive(false);

        poolDictionary[tag].Enqueue(obj);
    }

    GameObject GetPrefabsWithTag(string tag)
    {
        foreach (Pool pool in Pools)
        {
            if (pool.Tag.Equals(tag))
                return pool.Prefabs;
        }
        return null;
    }
}
