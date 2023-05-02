using System.Collections.Generic;
using UnityEngine;


public class BaseTile : MonoBehaviour {
    
    //public List<TileWeight> tileWeight;
    public Direction tileOrientation; 
    public int offset;

    public bool rotateTile;
    
    void Start(){  
        if (tileOrientation == Direction.X) transform.Rotate(0, 90, 0);
    }

    // Update is called once per frame
    void Update(){
    }

    bool canRotate(){
        return rotateTile;
    }

}