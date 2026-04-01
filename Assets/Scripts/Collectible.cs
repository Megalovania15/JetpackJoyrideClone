using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    private UnityEvent<GameObject> onObjectCollect = new();

    public void OnObjectCollect(UnityAction<GameObject> listener)
    { 
        onObjectCollect.AddListener(listener);
    }

    public void RemoveEventListener(UnityAction<GameObject> listener)
    { 
        onObjectCollect.RemoveListener(listener);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScore>().AddToScore();
            Debug.Log("Coin collected");
            onObjectCollect.Invoke(gameObject);
        }
    }
}
