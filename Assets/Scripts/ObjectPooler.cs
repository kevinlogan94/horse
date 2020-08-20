using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

//https://www.youtube.com/watch?v=tdSmKaJvCoA&t=815s
public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public GameObject Parent;
        public int Size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            var objectPool = GeneratePool(pool);
            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string poolTag, Vector3? position = null)
    {
        if (!PoolDictionary.ContainsKey(poolTag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesnt exist.");
            return null;
        }
        var objectToSpawn = PoolDictionary[poolTag].Dequeue();
        objectToSpawn.SetActive(true);

        if (position != null)
        {
            objectToSpawn.transform.position = (Vector3) position;
            objectToSpawn.transform.SetSiblingIndex(1); // 0 is the background
        }

        PoolDictionary[poolTag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private Queue<GameObject> GeneratePool(Pool pool)
    {
        var objectPool = new Queue<GameObject>();
        for (var i = 0; i < pool.Size; i++)
        {
            var obj = InstantiatePoolPrefab(pool);
            objectPool.Enqueue(obj);
        }

        return objectPool;
    }

    private GameObject InstantiatePoolPrefab(Pool pool)
    {
        var obj = Instantiate(pool.Prefab);
        obj.SetActive(false);
        // obj.GetComponent<Renderer>().enabled = true;
        if (pool.Parent != null)
        {
            //https://answers.unity.com/questions/572176/how-can-i-instantiate-a-gameobject-directly-into-a-1.html
            obj.transform.SetParent(pool.Parent.transform, false);
        }

        return obj;
    }

    private Queue<GameObject> ManipulatePoolSize(Pool pool, int newSize)
    {
        var objectPool = PoolDictionary[pool.Tag];
        while (objectPool.Count != newSize)
        {
            if (objectPool.Count < newSize)
            {
                var obj = InstantiatePoolPrefab(pool);
                objectPool.Enqueue(obj);
            }
            else
            {
                var objectToDestroy = PoolDictionary[pool.Tag].Dequeue();
                Destroy(objectToDestroy);
            }
        }
        return objectPool;
    }

    public void ReOptimizeHorsePools(string coreHorseBreedTag, string secondaryHorseBreedTag)
    {
        if (Pools.All(x => x.Tag != coreHorseBreedTag) || Pools.All(x => x.Tag != secondaryHorseBreedTag))
        {
            Debug.LogWarning("We couldn't find the pools for " + coreHorseBreedTag + " and " + secondaryHorseBreedTag + ".");
            return;
        }
        
        const int topSize = 30;
        const int midSize = 10;
        const int bottomSize = 3;
        foreach (var pool in Pools.Where(pool => pool.Tag != "IncrementText"))
        {
            if (pool.Tag == coreHorseBreedTag)
            {
                ManipulatePoolSize(pool, topSize);
            } else if (pool.Tag == secondaryHorseBreedTag)
            {
                ManipulatePoolSize(pool, midSize);
            }
            else if (PoolDictionary[pool.Tag].Count > bottomSize)
            {
                ManipulatePoolSize(pool, bottomSize);
            }
        }
    }
}
