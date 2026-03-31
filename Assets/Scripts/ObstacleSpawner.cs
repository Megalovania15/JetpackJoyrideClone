using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;

    public Transform entryPointPosTop;
    public Transform entryPointPosBottom;
    public Transform exitPointPos;

    private Coroutine spawnerCoroutine;
    private LinkedList<GameObject> spawnedObstacles = new();

    private ObjectPooler obstaclePool;

    void Awake()
    {
        obstaclePool = GetComponent<ObjectPooler>();

        spawnerCoroutine = StartCoroutine(SpawnObstacleAfterSomeTime());
        //cleanupCoroutine = StartCoroutine(DeleteObstacleAtBoundary());
    }

    IEnumerator SpawnObstacleAfterSomeTime()
    {
        while (true)
        { 
            yield return new WaitForSeconds(spawnRate);
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        //randomise entry position, y value will change, but they'll always spawn at the same
        //x value
        float yPos = Random.Range(entryPointPosBottom.position.y, entryPointPosTop.position.y);

        var spawnPos = new Vector2(entryPointPosTop.position.x, yPos);

        GameObject newObstacle = obstaclePool.RetrieveObject();

        InitialiseObstacle(newObstacle, spawnPos);
    }

    void RemoveObstacle()
    {
        //take the first value, disable it and remove it from the spawned obstacles "queue"
        while (spawnedObstacles.First.Value.transform.position.x < exitPointPos.position.x)
        { 
            var firstObstacle = spawnedObstacles.First.Value;
            var obstacleLevelElement = firstObstacle.GetComponent<LevelElement>();
            obstacleLevelElement.RemoveEventListener(RemoveObstacle);
            obstaclePool.DisableObject(firstObstacle);
            spawnedObstacles.RemoveFirst();
        }
    }

    void InitialiseObstacle(GameObject obstacle, Vector3 obstacleSpawnPos)
    {
        obstacle.transform.position = obstacleSpawnPos;
        var obstacleLevelElement = obstacle.GetComponent<LevelElement>();
        obstacleLevelElement.BoundaryPos = exitPointPos;
        obstacle.SetActive(true);
        obstacleLevelElement.OnBoundaryPointPassed(RemoveObstacle);
        spawnedObstacles.AddLast(obstacle);
    }
}
