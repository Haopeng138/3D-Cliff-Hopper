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
    private Vector3 playerRotation;

    // Jump and Gravity
    Rigidbody rg;
    private int jumpsLeft;
    public float jumpForce = 15.0f;
    bool isJumping=false;
    public int maxJumps = 2;

    public float DownForce = -20f;


    // Animation
    private Animator animator;
    public RuntimeAnimatorController playerAnimatorController;
    private bool AnimationIsJumping = false;
    private bool IsGrounded = true; 
    // Start is called before the first frame update
    void Start()
    {
      playerDirection = PlayerDirection.Stop;
      player = GetComponent<CharacterController>();
      horizontalMove = 0f;
      verticalMove = 0f;
      rg = GetComponent<Rigidbody>();
      jumpsLeft = maxJumps;
      animator = GetComponent<Animator>();
      // Get the animator controller
      animator.runtimeAnimatorController = playerAnimatorController ;
      playerRotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Change dir
        if(Input.GetKeyDown(KeyCode.Space))
        {
            changeDir();
        }

        // Jump 
        isJumping = Input.GetKeyDown(KeyCode.UpArrow);
        if(isJumping && jumpsLeft > 0 )
        {
            Debug.Log("Jumping");
            rg.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
            AnimationIsJumping = true;
            jumpsLeft--;
            AudioManager.instance.PlaySFX("Jump");
        }

        

        Vector3 dir = new Vector3(horizontalMove,0,verticalMove);
        Debug.Log(dir);
        rg.MovePosition(transform.position + dir * speed * Time.deltaTime);
        if (rg.velocity.y < 0){
            rg.AddForce(new Vector3(0,DownForce,0),ForceMode.Impulse);
        }   


        animation();

    }

    void FixedUpdate()
    {
        if (playerRotation != Vector3.zero){
            Quaternion deltaRotation = Quaternion.Euler(playerRotation);
            rg.MoveRotation(rg.rotation * deltaRotation);
            playerRotation = Vector3.zero;
        }

    }

    void OnCollisionEnter(Collision col) {
        // Change this to a other tags
    
        if (col.gameObject.name == "Plane") {
            jumpsLeft = maxJumps;
            AnimationIsJumping = false;
        }

    }

    void changeDir(){
        switch (playerDirection)
        {
            case PlayerDirection.Stop:
                playerDirection = PlayerDirection.Forward;
                break;
            case PlayerDirection.Forward:
                playerDirection = PlayerDirection.Right;
                horizontalMove = 1f;
                verticalMove = 0f;
                playerRotation = new Vector3(0,90,0);
                break;
            case PlayerDirection.Right:
                playerDirection = PlayerDirection.Forward;
                horizontalMove = 0f;
                verticalMove = 1f;
                playerRotation = new Vector3(0,-90,0);
                break;
            
            default:
                break;
        }
    }

    void animation(){
        if (AnimationIsJumping){
            animator.SetFloat("VelX",1);
            animator.SetFloat("VelY",1);
        }else {
            animator.SetFloat("VelX",horizontalMove);
            animator.SetFloat("VelY",verticalMove);
        }
    }


}
