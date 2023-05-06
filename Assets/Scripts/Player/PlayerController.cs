using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private enum PlayerDirection {Stop,Forward,Right};
    private PlayerDirection playerDirection;
    private Vector3 targetPosition;

    private float horizontalMove;
    private float verticalMove;
    public CharacterController player;  
    // Start is called before the first frame update
    void Start()
    {
      playerDirection = PlayerDirection.Stop;
      player = GetComponent<CharacterController>();
      speed = 5f;
      horizontalMove = 0f;
      verticalMove = 0f;
      
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
                    horizontalMove = 1f;
                    verticalMove = 0f;
                    targetPosition = new Vector3(transform.position.x +1 , transform.position.y, transform.position.z);
                    transform.LookAt(targetPosition);
                    break;
                case PlayerDirection.Right:
                    playerDirection = PlayerDirection.Forward;
                    horizontalMove = 0f;
                    verticalMove = 1f;
                    targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z+1);
                    transform.LookAt(targetPosition);
                    break;
             
                default:
                    break;
            }
        }
        player.Move(new Vector3(horizontalMove,0,verticalMove) * Time.deltaTime * speed);

    }


}
