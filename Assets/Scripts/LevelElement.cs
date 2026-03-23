using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LevelElement : MonoBehaviour, IMoveable
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;

    void OnEnable()
    {
        if (rb is null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        Move();
    }

    void OnDisable()
    {
        ZeroOutVelocity();
    }

    public void Move()
    {
        rb.linearVelocity = movementSpeed * Vector2.left;
    }

    public void IncreaseMovementSpeed(int speedMultiplier)
    { 
        
    }

    void ZeroOutVelocity()
    { 
        rb.linearVelocity = Vector2.zero;
    }
}
