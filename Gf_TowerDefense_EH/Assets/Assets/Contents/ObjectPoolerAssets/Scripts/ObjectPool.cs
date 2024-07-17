using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] public List<GameObject> objectsPrefab;

    private Dictionary<GameObject, Stack<IPoolableItem>> poolDictionary;

    private void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        poolDictionary = new Dictionary<GameObject, Stack<IPoolableItem>>();

        foreach (GameObject prefab in objectsPrefab)
        {
            Stack<IPoolableItem> stack = new Stack<IPoolableItem>();

            for (int i = 0; i < initPoolSize; i++)
            {
                IPoolableItem obj = Instantiate(prefab, gameObject.transform).GetComponent<IPoolableItem>();
                obj.Pool = this;
                obj.Prefab = prefab;
                stack.Push(obj);
                ((MonoBehaviour)obj).gameObject.SetActive(false);
            }
            
            poolDictionary.Add(prefab, stack);
        }
    }

    public IPoolableItem GetPooledObject(GameObject prefab)
    {
        var type = prefab.GetComponent<IPoolableItem>().GetType();

        if(!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Prefab not handled by the pool!");
            return null;
        }

        var stack = poolDictionary[prefab];

        if(stack.Count == 0)
        {
            IPoolableItem newInstance = Instantiate(prefab).GetComponent<IPoolableItem>();
            newInstance.Pool = this;
            newInstance.Prefab = prefab;
            newInstance.OnUse();
            return newInstance;
        }

        IPoolableItem nextInstance = stack.Pop();
        nextInstance.OnUse();
        return nextInstance;
    }

    public void ReturnToPool(IPoolableItem objectToReturn)
    {
        GameObject prefab = objectToReturn.Prefab;

        if (poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab].Push(objectToReturn);
        }
        else
        {
            Debug.LogWarning("Object doesn't belong to any known pool!");
        }

        Debug.Log("Method Completed");
    }
}
