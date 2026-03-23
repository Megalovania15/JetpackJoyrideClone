using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//rename class to more accurately describe it's function
public class LevelSpawner : MonoBehaviour
{
    public readonly Vector3 exitBoundaryPos = new Vector3(-20f, -5f); 
    public readonly Vector3 entryBoundaryPos = new Vector3(11f, -5f);
    public readonly Vector3 posToSpawnNewLevelElement = new Vector3 (5f, 0f);
    [SerializeField] private ObjectPooler levelPool;
    [SerializeField] private GameObject floorSpawn;
    //[SerializeField] private Vector2 spawnPosition = Vector2.zero;
    [SerializeField] private float secondsToSpawnArea = 5f;

    void Awake()
    {
        levelPool = GetComponentInParent<ObjectPooler>();
    }

    void Update()
    {
        AddNextElement();
        DisableElement();
    }

    void AddNextElement()
    {
        if (Vector3.Distance(transform.position + posToSpawnNewLevelElement,
            entryBoundaryPos) < 0.01f)
        {
            GameObject nextLevelElement = levelPool.RetrieveObject();
            nextLevelElement.transform.position = transform.position + posToSpawnNewLevelElement;
        }
    }

    void DisableElement()
    {
        if (Vector3.Distance(transform.position, exitBoundaryPos) < 0.01f)
        {
            levelPool.DisableObject(this.gameObject);
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
