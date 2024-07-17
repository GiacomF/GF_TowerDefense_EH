using UnityEngine;

public class ModifiedPool : ObjectPool
{
    public Transform target;

    public void CallPooledObject(GameObject obj)
    {
        base.GetPooledObject(obj);
    }
}
