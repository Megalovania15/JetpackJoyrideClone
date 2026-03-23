using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> pool = new();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pool.Add(transform.GetChild(i).gameObject);
        }
        InitialisePool();
        
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

        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
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

    GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        pool.Add(obj);
        obj.SetActive(false);
        return obj;
    }
}
