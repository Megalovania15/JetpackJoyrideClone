using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerDeath>(out PlayerDeath playerDeath))
        {
            playerDeath.Die();
        }
    }
}
