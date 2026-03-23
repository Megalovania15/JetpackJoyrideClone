using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private float baseMovementSpeed = 5f;

    public float CurrentMovementSpeed { get; }

    private List<IMoveable> moveableObjects = new();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<IMoveable>(out IMoveable moveableObject))
            {
                moveableObjects.Add(moveableObject);
                //moveableObject.Move();
            }
        }

        StartCoroutine(CheckNewObjectsAndMove());
    }

    IEnumerator CheckNewObjectsAndMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<IMoveable>(out IMoveable moveableObject))
                {
                    if (!moveableObjects.Contains(moveableObject))
                    {
                        moveableObjects.Add(moveableObject);
                        //moveableObject.Move();
                    }
                }
            }
        }
    }
}
