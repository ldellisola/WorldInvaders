using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T,K> where T:MonoBehaviour,IPooledObject<K>
{
    private List<T> objects = new List<T>();

    public GameObject gameObject;
    public Transform container;



   public ObjectPool(Transform trans, GameObject obj)
    {
        container = trans;
        gameObject = obj;
    }





    public T Add(K objectData)
    {
        var index = objects.FindIndex(t => !t.gameObject.activeSelf);
        
        if (index >= 0)
        {
            objects[index].gameObject.SetActive(true);
            objects[index].Initialize(objectData);

            return objects[index];
        }

        return CreateNew(objectData);
    }


    private T CreateNew(K objectData)
    {
        GameObject obj = Object.Instantiate(gameObject);
        obj.transform.localScale = Vector3.one;

        obj.SetActive(true);
        T item = obj.GetComponent<T>();
        item.Initialize(objectData);
        
        objects.Add(item);

        return item;
    }
}
