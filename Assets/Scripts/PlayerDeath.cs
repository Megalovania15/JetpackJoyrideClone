using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void Die()
    { 
        gameObject.SetActive(false);
    }
}
