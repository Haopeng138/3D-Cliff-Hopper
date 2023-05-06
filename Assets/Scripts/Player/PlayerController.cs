using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Hotizontal and Vertical move
    public float speed;
    private enum PlayerDirection {Stop,Forward,Right};
    private PlayerDirection playerDirection;
    private Vector3 targetPosition;

    private float horizontalMove;
    private float verticalMove;
    public CharacterController player;  
    private Vector3 playerMovement;
    // Jump and Gravity
    Rigidbody rg;
    public float jumpForce = 5.0f;
    bool isJumping=false;
    public int maxJumps = 2;
    private int jumpsLeft;

    // Start is called before the first frame update
    void Start()
    {
      playerDirection = PlayerDirection.Stop;
      player = GetComponent<CharacterController>();
      speed = 5f;
      horizontalMove = 0f;
      verticalMove = 0f;

      rg = GetComponent<Rigidbody>();
      jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal and Vertical move
        /*
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
        */

        

        // isJumping = Input.GetKeyDown(KeyCode.Space);
        // if(isJumping && jumpsLeft > 0 )
        // {
        //     Debug.Log("Jumping");
        //     rg.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
        //     jumpsLeft--;
        // }
        // player.Move(new Vector3(horizontalMove,0,verticalMove) * Time.deltaTime * speed);

        // Rigidbody movement 

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
                    // targetPosition = new Vector3(transform.position.x +1 , transform.position.y, transform.position.z);
                    // transform.LookAt(targetPosition);
                    break;
                case PlayerDirection.Right:
                    playerDirection = PlayerDirection.Forward;
                    horizontalMove = 0f;
                    verticalMove = 1f;
                    // targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z+1);
                    // transform.LookAt(targetPosition);
                    break;
             
                default:
                    break;
            }
        }
        Vector3 dir = transform.right * verticalMove + transform.forward * horizontalMove;
        Debug.Log(dir);
        rg.MovePosition(transform.position + dir * speed * Time.deltaTime);


    }

    void OnCollisionEnter(Collision col) {
        // Change this to a other tags
        if (col.gameObject.name == "Plane") {
            jumpsLeft = maxJumps;
            Debug.Log("Reset Jumps");
        }

    }


}
