using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Direction { Z=0, X=1 };


public class TileMapController : MonoBehaviour
{
    public int maxTiles;
    public Vector3 tileLocation;
    public BaseTileSO currentTile;

    public BaseTileSO rotateTile;
    Direction currentDirection = Direction.Z;

    private int centerOffset = 0;
    public int centerOffsetMax = 4;

    // Start is called before the first frame update
    void Start()
    { 
        spawnCurrentTile();
        CreateMap();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap(){
        for (int i = 0; i < maxTiles; i++)
        {

            nextTile();
            
            centerOffset += currentDirection == Direction.X ? -1 : 1;
            
            if (currentTile.isSafe)
            {
                switch (currentDirection)
                {
                    case Direction.X:
                    if (Random.Range(0, -centerOffsetMax) > centerOffset) changeDirection();
                    break;
                    case Direction.Z:
                    if (Random.Range(0, centerOffsetMax) < centerOffset) changeDirection();
                    break;                    
                }
            }
            
            spawnCurrentTile();
        }
    }

    void nextTile(){
        addStride();
        currentTile = currentTile.nextTile(this);
        addStride();
    
    }

    BaseTile spawnCurrentTile(){
        BaseTile newTile = currentTile.spawnTile(tileLocation, currentDirection);
        newTile.offset = centerOffset;
        newTile.gameObject.transform.parent = gameObject.transform;
        return newTile;
    }

    void addStride(){
        switch(currentDirection) 
        {
        case Direction.Z:
            tileLocation.z += currentTile.tileStride;
        break;
        case Direction.X:
            tileLocation.x += currentTile.tileStride;
        break;
        }
    }

    void changeDirection(){
        switch(currentDirection) 
        {
        case Direction.Z:
            currentDirection = Direction.X;
        break;
        case Direction.X:
            currentDirection = Direction.Z;
        break;
        }
        currentTile = rotateTile;
    }

    public bool reachedMaxOffset(){
        if (centerOffset <  -centerOffsetMax && currentDirection == Direction.X) {
            return true;
        }
        if (centerOffset >  centerOffsetMax && currentDirection == Direction.Z) {
            return true;
        }
        return false;
    }
}
