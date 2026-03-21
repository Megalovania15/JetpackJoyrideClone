using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LevelElement : MonoBehaviour, IMoveable
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;

    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        rb.linearVelocity = movementSpeed * Vector2.left;
    }

    public void IncreaseMovementSpeed(int speedMultiplier)
    { 
        
    }
}
