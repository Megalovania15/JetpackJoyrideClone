using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    public List<GameObject> Pool { get; private set; } = new();

    void Awake()
    {
        InitialisePool();

        /*if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Pool.Add(transform.GetChild(i).gameObject);
            }
        }*/
    }

    void InitialisePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    //need to be able to retrieve an item from the pool and enable it

    public GameObject RetrieveObject()
    {
        //take the first item off the list that is not activated in the hierarchy

        foreach (GameObject obj in Pool)
        {
            if (!obj.activeInHierarchy)
            {
                //obj.SetActive(true);
                return obj;
            }
        }

        //create a new object if there are no available objects in the pool
        return CreateNewObject();
    }


    //as well as disable an item from the pool
    public void DisableObject(GameObject objectToDisable)
    { 
        objectToDisable.SetActive(false);
    }

    protected virtual GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        Pool.Add(obj);
        obj.SetActive(false);
        return obj;
    }

    void ShufflePool()
    { 
        
    }
}
