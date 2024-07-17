using UnityEngine;

public class ObjectPooler<T> : MonoBehaviour
{
    [SerializeField] protected ObjectPool pool;
    [SerializeField] private GameObject[] spawnablePrefabs;
    [SerializeField] private int prefabIndex;

    public virtual void RequestPooledObject(T parameter)
    {
        pool.GetPooledObject(pool.objectsPrefab[prefabIndex]);
    }
}
