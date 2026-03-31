using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

//rename class to more accurately describe it's function
public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private Transform exitBoundaryPos;
    [SerializeField] private Transform entryBoundaryPos;
    //this is the object scale
    public readonly Vector3 posToSpawnNewLevelElement = new Vector3(5f, 0f);
    [SerializeField] private int startingPlatforms = 5;
    [SerializeField] private ObjectPooler levelPool;
    //[SerializeField] private Vector2 spawnPosition = Vector2.zero;
    private Vector3 nextSpawnPos = Vector3.zero;
    private Vector3 lastSpawnedPlatform = Vector3.zero;

    private LinkedList<GameObject> spawnedObjects = new();

    void OnDisable()
    {
        foreach (var levelObject in spawnedObjects)
        {
            levelObject.GetComponent<LevelElement>().onBoundaryPointPassed.RemoveListener(RecycleLevelSegment);
        }
    }

    void Start()
    {
        levelPool = GetComponent<ObjectPooler>();

        GameObject firstPlatform = levelPool.RetrieveObject();
        InitialiseLevelSegment(firstPlatform, exitBoundaryPos.position);

        while (spawnedObjects.Last.Value.transform.position.x < entryBoundaryPos.position.x)
        {
            var previousObj = spawnedObjects.Last.Value;
            var nextPosition = 
                previousObj.transform.position + posToSpawnNewLevelElement;
            var nextPlatform = levelPool.RetrieveObject();
            InitialiseLevelSegment(nextPlatform, nextPosition);
        }
        /*newPlatform.transform.position = new Vector3(-9f, -5f);
        nextSpawnPos = newPlatform.transform.position + posToSpawnNewLevelElement;
        newPlatform.GetComponent<LevelElement>().onEntryPointPassed.AddListener(LevelElementEntryPointPassed);

        GameObject nextPlatform = null;

        for (int i = 0; i < startingPlatforms; i++)
        {
            nextPlatform = levelPool.RetrieveObject();
            nextPlatform.transform.position = nextSpawnPos;
            nextSpawnPos = nextPlatform.transform.position + posToSpawnNewLevelElement;
            lastSpawnedPlatform = nextPlatform.transform.position;
            nextPlatform.SetActive(true);
            nextPlatform.GetComponent<LevelElement>().IsTail = true;
            nextPlatform.GetComponent<LevelElement>().onEntryPointPassed.AddListener(LevelElementEntryPointPassed);
        }*/
    }

    void Update()
    {
        //AddNextElement();
        //DisableElement();
    }

    void InitialiseLevelSegment(GameObject nextPlatform, Vector3 nextPosition)
    {
        var nextLevelElement = nextPlatform.GetComponent<LevelElement>();
        nextLevelElement.BoundaryPos = entryBoundaryPos;
        nextPlatform.transform.position = nextPosition;
        nextPlatform.SetActive(true);
        nextLevelElement.OnBoundaryPointPassed(RecycleLevelSegment);
        spawnedObjects.AddLast(nextPlatform);
    }

    void RecycleLevelSegment()
    {
        //take the first value, disable it and add a new object to the end of the list
        while (spawnedObjects.First.Value.transform.position.x < exitBoundaryPos.position.x)
        {
            var firstObject = spawnedObjects.First.Value;
            firstObject.GetComponent<LevelElement>().RemoveEventListener(RecycleLevelSegment);
            levelPool.DisableObject(firstObject);
            spawnedObjects.RemoveFirst();
            var previousObject = spawnedObjects.Last.Value;
            var nextPosition = previousObject.transform.position + posToSpawnNewLevelElement;
            var nextLevelElement = levelPool.RetrieveObject();
            InitialiseLevelSegment(nextLevelElement, nextPosition);
        }
    }

    /*void MoveTileToNewPos()
    {
        if (collisionTilemap.HasTile(tileRemovalCoordinate))
        {
            Debug.Log("There is a tile here");

            var tileToMove = collisionTilemap.GetTile(tileRemovalCoordinate);

            if (!collisionTilemap.HasTile(tilePlacementCoordinate))
            { 
                collisionTilemap.SetTile(tilePlacementCoordinate, tileToMove);
            }
        }
    }*/

    //checks if the tile coordinate has a tile, and gets it.
    /*TileBase FindTileAtPos()
    {
        if (collisionTilemap.HasTile(tileRemovalCoordinate))
        { 
            return collisionTilemap.GetTile(tileRemovalCoordinate);
        }

        return null;
    }*/




}
