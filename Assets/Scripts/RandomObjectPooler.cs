using UnityEngine;

public class RandomObjectPooler : ObjectPooler
{
    [SerializeField] private GameObject[] prefabs;

    protected override GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(SelectObjectToCreate(), transform);
        Pool.Add(obj);
        obj.SetActive(false);
        return obj;
    }

    GameObject SelectObjectToCreate()
    { 
        int index = Random.Range(0, prefabs.Length);

        return prefabs[index];
    }
}
