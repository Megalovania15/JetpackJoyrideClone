using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class LevelElement : MonoBehaviour, IMoveable
{
    public UnityEvent onBoundaryPointPassed = new();
    public Transform BoundaryPos { get; set; }

    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;

    void OnEnable()
    {
        if (rb is null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        //isTail = false;
        Move();
    }

    void OnDisable()
    {
        ZeroOutVelocity();
    }

    void Update()
    {
        if (transform.position.x <= BoundaryPos.position.x)
        {
            //Debug.Log("Did this ever get called...?");
            onBoundaryPointPassed.Invoke();
            //Destroy(gameObject);
        }
    }

    public void Move()
    {
        rb.linearVelocity = movementSpeed * Vector2.left;
    }

    public void IncreaseMovementSpeed(int speedMultiplier)
    { 
        
    }

    public void OnBoundaryPointPassed(UnityAction listener)
    { 
        onBoundaryPointPassed.AddListener(listener);
    }

    public void RemoveEventListener(UnityAction listener)
    {
        onBoundaryPointPassed.RemoveListener(listener);
    }

    void ZeroOutVelocity()
    { 
        rb.linearVelocity = Vector2.zero;
    }
}
