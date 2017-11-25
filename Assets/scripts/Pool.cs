using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    /// <summary>
    /// The size of a pool if one isn't specified
    /// </summary>
    private const int DEFUALT_POOL_SIZE = 100;

    /// <summary>
    /// Dictionary containing all the pools
    /// </summary>
    private static Dictionary<IPoolable, Pool> pools = new Dictionary<IPoolable, Pool>();

    /// <summary>
    /// Returns the requested pool for the given prefab, if no pool exists it creates one using default size
    /// </summary>
    /// <param name="prefab">the type of pool you want</param>
    /// <returns>pool for prefab</returns>
    public static Pool GetPool(IPoolable prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            if (pools[prefab] != null)
                return pools[prefab];
            else
                pools.Remove(prefab);
        }

        var pool = new GameObject("Pool-" + (prefab as Component).name).AddComponent<Pool>();
        pool.Initialize(prefab);
        pools.Add(prefab, pool);
        return pool;
    }

    /// <summary>
    /// Returns the requested pool for the given prefab, if no pool exists it creates one using initialsize
    /// </summary>
    /// <param name="prefab">prefab you want a pool of</param>
    /// <param name="initialSize">The Initial size of the pool</param>
    /// <returns>pool for given prefab</returns>
    public static Pool Prewarm(IPoolable prefab, int initialSize)
    {
        if (pools.ContainsKey(prefab))
        {
            if (pools[prefab] != null)
            {
                Debug.LogError("Pool already created, can't prewarm");
                return pools[prefab];
            }
            else
                pools.Remove(prefab);
        }

        //creates the new pool
        var pool = new GameObject("Pool-" + (prefab as Component).name).AddComponent<Pool>();
        pool.Initialize(prefab, initialSize);                  //creates all the objects in the pool
        pools.Add(prefab, pool);                             //puts new pool in dicationary
        return pool;
    }

    /// <summary>
    /// Prefab to for this pool
    /// </summary>
    private GameObject prefab;

    private Queue<IPoolable> objects = new Queue<IPoolable>();
    private List<IPoolable> disabledObjects = new List<IPoolable>();

    private void Initialize(IPoolable poolablePrefab, int initialSize = DEFUALT_POOL_SIZE)
    {
        this.prefab = (poolablePrefab as Component).gameObject;
        for (int i = 0; i < initialSize; i++)
        {
            var pooledObject = (Instantiate(this.prefab) as GameObject).GetComponent<IPoolable>();
            (pooledObject as Component).gameObject.name += " " + i;

            pooledObject.OnDestroyEvent += () => AddObjectToAvailable(pooledObject);

            //initializes the object to disabled, this puts it in the disableObject list
            (pooledObject as Component).gameObject.SetActive(false);
        }
        MakeDisabledObjectsChildren();
    }

    /// <summary>
    /// Puts object back in the queue after being disabled
    /// </summary>
    /// <param name="pooledObject"></param>
    private void AddObjectToAvailable(IPoolable pooledObject)
    {
        disabledObjects.Add(pooledObject);
        objects.Enqueue(pooledObject);
    }


    /// <summary>
    /// Expands the pool if nessasary and returns next object
    /// </summary>
    /// <returns>The next object in the pool</returns>
    public IPoolable Get()
    {
        lock (this)
        {
            if (objects.Count == 0)
            {
                int amountToGrowPool = Mathf.Max((disabledObjects.Count / 10), 1);
                Initialize(this.prefab.GetComponent<IPoolable>(), amountToGrowPool);
            }

            var pooledObject = objects.Dequeue();

            return pooledObject;
        }
    }

    /// <summary>
    /// expands pool if nessasary and set the position and rotation of object before activiating and returning it
    /// </summary>
    /// <param name="position">objects new position</param>
    /// <param name="rotation">objects new rotation</param>
    /// <returns>next object, positioned, rotated, and activated</returns>
    public IPoolable Get(Vector3 position, Quaternion rotation)
    {
        var pooledObject = Get();

        (pooledObject as Component).transform.position = position;
        (pooledObject as Component).transform.rotation = rotation;
        (pooledObject as Component).gameObject.SetActive(true);

        return pooledObject;
    }

    /// <summary>
    /// expands pool if nessasary and set the position and rotation and parent of object before activiating and returning it
    /// </summary>
    /// <param name="position">objects new position</param>
    /// <param name="rotation">objects new rotation</param>
    /// <param name="parent">Objects new parent</param>
    /// <returns>next object, positioned, rotated, and activated</returns>
    public IPoolable Get(Vector3 position, Quaternion rotation, Transform parent)
    {
        var pooledObject = Get();

        (pooledObject as Component).transform.SetParent(parent);
        (pooledObject as Component).transform.position = position;
        (pooledObject as Component).transform.rotation = rotation;
        (pooledObject as Component).gameObject.SetActive(true);

        return pooledObject;
    }

    private void Update()
    {
        MakeDisabledObjectsChildren();
    }

    /// <summary>
    /// Brings newly disabled objects back to being children of the pool
    /// </summary>
    private void MakeDisabledObjectsChildren()
    {
        if (disabledObjects.Count > 0)
        {
            foreach (var pooledObject in disabledObjects)
            {
                (pooledObject as Component).transform.SetParent(transform);
            }

            disabledObjects.Clear();
        }
    }
}