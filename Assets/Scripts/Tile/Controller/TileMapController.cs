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
    public int centerOffsetMin = 2;
    public int centerOffsetMax = 4;

    public int currentHeight = 0;
    public int maxHeight = 10;
    public int minHeight = -3;


    // Start is called before the first frame update
    void Start()
    { 
        spawnCurrentTile();
        CreateMap();        
    }


    void CreateMap(){
        for (int i = 0; i < maxTiles; i++)
        {

            nextTile();
            
            centerOffset += currentDirection == Direction.X ? -1 : 1;
            
            if (currentTile.canBeReplaced)
            {
                switch (currentDirection)
                {
                    case Direction.X:
                        if (Random.Range(0, -centerOffsetMax -1) > centerOffset && centerOffset < -centerOffsetMin) changeDirection();
                    break;
                    case Direction.Z:
                    if (Random.Range(0, centerOffsetMax +1) < centerOffset && centerOffset > centerOffsetMin) changeDirection();
                    break;                    
                }
            }
            
            spawnCurrentTile();
        }
    }

    void nextTile(){
        addStride(false);
        currentTile = currentTile.nextTile(this);
        addStride(true);
    
    }

    BaseTile spawnCurrentTile(){
        BaseTile newTile = currentTile.spawnTile(tileLocation, currentDirection, this);
        newTile.offset = centerOffset;
        newTile.gameObject.transform.parent.transform.parent = gameObject.transform;
        return newTile;
    }

    void addStride(bool height){
        switch(currentDirection) 
        {
        case Direction.Z:
            tileLocation.z += currentTile.nextTileStride.x;
        break;
        case Direction.X:
            tileLocation.x += currentTile.nextTileStride.x;
        break;
        }
        if (height) {
            tileLocation.y += currentTile.nextTileStride.y;
            currentHeight += (int)currentTile.nextTileStride.y;
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
