using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolPreparer : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs;

    [SerializeField]
    private int initialPoolSize = 100;

    private void Awake()
    {
        //Prewarm each prefab
        foreach (var prefab in prefabs)
        {
            if (prefab == null)
            {
                Debug.LogError("Null prefab in PoolPreparer");
            }
            else
            {
                IPoolable poolablePrefab = prefab.GetComponent<IPoolable>();
                if (poolablePrefab == null)
                {
                    Debug.LogError("Prefab does not contain an IPoolable and can't be pooled");
                }
                else
                {
                    Pool.Prewarm(poolablePrefab, initialPoolSize);
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        List<GameObject> prefabsToRemove = new List<GameObject>();
        foreach (var prefab in prefabs.Where(t => t != null))
        {
            
            //Make sure the object is a user created prefab
            if (PrefabUtility.GetPrefabType(prefab) != PrefabType.Prefab)
            {
                Debug.LogError(string.Format("{0} is not a prefab.  It has been removed.", prefab.gameObject.name));
                prefabsToRemove.Add(prefab);
            }

            //Make sure the Object implements IPoolable
            IPoolable poolablePrefab = prefab.GetComponent<IPoolable>();
            if (poolablePrefab == null)
            {
                Debug.LogError("Prefab does not contain an IPoolable and can't be pooled.  It has been removed.");
                prefabsToRemove.Add(prefab);
            }
        }

        //recreate the prefabs list with only the prefabs that pass validation
        prefabs = prefabs
            .Where(t => t != null && prefabsToRemove.Contains(t) == false)
            .ToArray();
    }

#endif

}