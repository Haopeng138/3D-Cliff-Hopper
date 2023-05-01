using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    Quaternion rotation;
    private enum PlayerDirection {Stop,Forward,Right};
    private PlayerDirection playerDirection;

    // Start is called before the first frame update
    void Start()
    {
      playerDirection = PlayerDirection.Stop;
    }

    // Update is called once per frame
    void Update()
    {
    
        Vector3 targetPosition;
     
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (playerDirection)
            {
                case PlayerDirection.Stop:
                    playerDirection = PlayerDirection.Forward;
                    break;
                case PlayerDirection.Forward:
                    playerDirection = PlayerDirection.Right;
                    //Vector3 targetPosition = new Vector3(1f, transform.position.y, transform.position.z);
                    
                    targetPosition = new Vector3(1f, transform.position.y, transform.position.z);
                    transform.LookAt(targetPosition);
                    break;
                case PlayerDirection.Right:
                    playerDirection = PlayerDirection.Forward;
                    //Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, 1f);
                    targetPosition = new Vector3(3f, transform.position.y, transform.position.z);
                    transform.LookAt(targetPosition);
                    break;
                
                default:
                    break;
            }
        }
        float amountToMove = speed * Time.deltaTime;
   
        transform.Translate(new Vector3(0,0,1) * amountToMove);    
    }


}
