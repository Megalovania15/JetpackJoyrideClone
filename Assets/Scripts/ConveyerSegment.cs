using UnityEngine;
using UnityEngine.Events;

public class ConveyerSegment : MonoBehaviour
{
    // Defines the speed and direction we will move over our lifetime.
    [SerializeField]
    private Vector2 velocity = new Vector2(-1f, 0);

    // Point after which we should be warped back to the right-hand side.
    [field: SerializeField]
    public Transform LHS { set;  get; }

    // Event emitted when we pass the left-hand side.
    [SerializeField]
    private UnityEvent onDone = new();

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    void Update()
    {
        // Notify our creator that we are past the left-hand side:
        if (transform.position.x < LHS.position.x)
        {
            onDone.Invoke();
        }
    }

    public void OnDone(UnityAction listener)
    {
        onDone.AddListener(listener);
    }
}
