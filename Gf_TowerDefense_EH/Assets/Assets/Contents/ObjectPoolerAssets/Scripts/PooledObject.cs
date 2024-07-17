using System.Collections;
using UnityEngine;

public interface IPoolableItem
{
    ObjectPool Pool { get; set; }
    GameObject Prefab { get; set; }
    public void GetParameter(){}
    public void OnUse(){}
    public void OnRelease(){}
}

public class PooledObject<T> : MonoBehaviour, IPoolableItem
{
    public ObjectPool Pool { get; set; }
    
    public GameObject Prefab { get; set; }

    public void OnUse()
    {
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
        Pool.ReturnToPool(this);
    }

    public virtual void GetParameter(T parameter){}

    private IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(5f);
        OnRelease();
    }
}
