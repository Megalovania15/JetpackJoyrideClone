using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float maxAcceleration = 10f;
    [SerializeField] private float raycastLength = 1.5f;

    private InputAction fly;

    private Vector2 moveDir = Vector2.zero;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        fly = InputSystem.actions.FindAction("Fly");
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Debug.Log(rb.linearVelocityY);

        if (fly.IsPressed())
        {
            Fly();
            //Debug.Log(rb.linearVelocityY);
            anim.SetTrigger("Jumping");
            //anim.SetBool("isGrounded", IsGrounded());

            if (rb.linearVelocityY > 0)
            {
                anim.SetFloat("velocity", rb.linearVelocityY);
                anim.ResetTrigger("Jumping");
                //anim.ResetTrigger("Falling");
            }
        }
        else
        {
            if (rb.linearVelocityY < 0)
            {

                anim.SetFloat("velocity", rb.linearVelocityY);
                //anim.ResetTrigger("Jumping");
            }
            else
            {
                anim.SetTrigger("Falling");
            }

            //anim.SetBool("isGrounded", IsGrounded());
        }
    }

    private void Fly()
    {
        rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength);

        if (hit.collider.CompareTag("Ground"))
        {
            return true;
        }

        return false;
    }

    public void Die()
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        anim.SetTrigger("Die");
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
