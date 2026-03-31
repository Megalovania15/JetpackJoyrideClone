using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    [field: SerializeField] public Transform BoundaryPos { get; set; }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < BoundaryPos.position.x)
        {
            Destroy(gameObject);
        }
    }
}
