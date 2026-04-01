using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    // Left-hand side before warping objects back to the right-hand side.
    [SerializeField]
    private Transform lhs;

    // Right-hand side position to which objects get warped.
    [SerializeField]
    private Transform rhs;

    // Prefab for conveyer objects.
    [SerializeField]
    public GameObject prefab;

    // 1:1 statekeeping of what the conveyer belt looks like. Front = leftmost segment, Back = rightmost segment.
    private LinkedList<GameObject> queue = new();

    void Start()
    {
        var obj = Instantiate(prefab, lhs.position, Quaternion.identity);
        InitialiseSegment(obj);
        while (queue.Last.Value.transform.position.x < rhs.position.x)
        {
            AddSegment();
        }
    }

    private void InitialiseSegment(GameObject obj)
    {
        var segment = obj.GetComponent<ConveyerSegment>();
        segment.LHS = lhs;
        segment.OnDone(WarpSegments);
        queue.AddLast(obj);
    }

    private void AddSegment()
    {
        var previousObject = queue.Last.Value;
        var nextPosition = previousObject.transform.position;
        nextPosition.x += previousObject.transform.localScale.x;
        var obj = Instantiate(prefab, nextPosition, Quaternion.identity);
        InitialiseSegment(obj);
    }

    // For every segment that has passed the LHS, warp it to the RHS.
    private void WarpSegments()
    {
        while (queue.First.Value.transform.position.x < lhs.position.x)
        {
            var front = queue.First.Value;
            queue.RemoveFirst();
            var back = queue.Last.Value;
            var nextPosition = back.transform.position;
            nextPosition.x += back.transform.localScale.x;
            front.transform.position = nextPosition;
            queue.AddLast(front);
        }
    }
}
