using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float maxAcceleration = 10f;

    private InputAction fly;

    private Vector2 moveDir = Vector2.zero;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        fly = InputSystem.actions.FindAction("Fly");
    }

    void FixedUpdate()
    {
        if (fly.IsPressed()) 
        {
            Fly();
        }
    }

    private void Fly()
    {
        rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Force);
    }
}
