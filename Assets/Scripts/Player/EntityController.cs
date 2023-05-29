using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    //public TileMapController tileMapController;

    public enum EntityState { 
        IDLE, MOVING, SLOWED, JUMPING, FALLING, DEAD 
        };

    protected EntityState state = EntityState.IDLE;    

    protected CharacterController controller;

    [SerializeField]
    protected BaseTile currentTile;

    [SerializeField]
    protected bool move = true;

    [Space]
    [field: SerializeField]
    public EntityData entityData;
    [SerializeField]
    protected int currentHealth;
    protected float moveSpeed = 4f;
    protected int jumpsLeft;

    protected Vector3 velocity = Vector3.zero;
    protected Vector2 directionVector = new Vector2(0, 0);
    protected Direction currentDirection = Direction.Z;
    public Direction CurrentDirection {
        get => currentDirection; 
    }

    protected bool debug = false;


    protected void Start()
    {
        moveSpeed = entityData.MoveSpeed;
        jumpsLeft = entityData.MaxJumps;
        currentHealth = entityData.Health;
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit){
        BaseTile tile = hit.gameObject.GetComponent<BaseTile>();
        if (tile != null){    
            if (currentTile == null || 
            tile.GetInstanceID() != currentTile.GetInstanceID())
                if (debug) Debug.Log("Calling onCollision: " + tile.name + ' ' + tile.GetType());
                currentTile = tile;
                //currentTile.onColission(this);
        }
    }
    


    // Update is called once per frame
    public void Update()
    {
        // Check for state changes
        stateTransition();
        // Update the entity based on the current state
        switch(state){
            case EntityState.IDLE:
                velocity = Vector3.zero;
            break;
            case EntityState.SLOWED:
                moveStateUpdate(0.9f);
            break;
            case EntityState.MOVING:
                moveStateUpdate(1);
            break;
            case EntityState.FALLING:
                fallStateUpdate();
            break;
            case EntityState.JUMPING:
                jumpStateUpdate();
            break;
            case EntityState.DEAD:
            break;
        }

        if (entityData.GodMode){
            if (currentTile != null) currentTile.onTap(this);
        }

        moveEnity();
    }

#region Health

    public void TakeDamage(int damage) {
        
        if (entityData.GodMode) return;

        if (debug) Debug.Log(gameObject.name + " - Taking Damage: " + damage);
        damage = Mathf.Abs(damage);
        currentHealth -= damage;
        if (entityData.Health <= 0){
            changeState(EntityState.DEAD);
        }
    }

    public void Heal(int healAmount) {
        if(debug) Debug.Log(gameObject.name + " - Healing: " + healAmount);
        healAmount = Mathf.Abs(healAmount);
        currentHealth += healAmount;
        if (entityData.Health < currentHealth){
            currentHealth = entityData.Health;
        }
    }

#endregion

    public void forceJump(float jumpForce) {
        
        if (entityData.GodMode) return;

        Debug.Log("Forcing Jump");
        changeState(EntityState.JUMPING);
        velocity.y = jumpForce;
    }

    public void forceJump() => forceJump(entityData.JumpSpeed);

#region StateUpdates

    protected virtual void moveStateUpdate(float speedMultiplier){
        velocity = new Vector3 (directionVector.x, entityData.Gravity * Time.deltaTime , directionVector.y) * moveSpeed * speedMultiplier;
    }

    protected virtual void fallStateUpdate(){
        velocity.y += entityData.Gravity * entityData.FallMultiplier * Time.deltaTime;
    }

    protected virtual void jumpStateUpdate(){
        velocity.y += entityData.Gravity * Time.deltaTime;        
    }
    
    public virtual void moveEnity(){
        if (move){
            //transform.position += velocity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
             
        }
    }

#endregion

#region StateTransitions
    // Determine if the entity should jump
    public virtual bool Jump(){
        return false;
    }

    public virtual void rotateEntity(Vector3 rotationPoint, Direction direction){
        if (direction == currentDirection) {
            if (debug) Debug.Log("Already facing that direction");
            return;
        }
        currentDirection = direction;
        switch(currentDirection){
            case Direction.X:
            directionVector = new Vector2(1, 0);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            break;
            case Direction.Z:
            directionVector = new Vector2(0, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            break;
        }
        var point = new Vector3(rotationPoint.x, transform.position.y, rotationPoint.z);
        controller.Move(point - transform.position);
    }


#endregion

#region StateManagement

// Check if the state needs to be changed
    protected void stateTransition(){
        bool isGrounded = controller.isGrounded;
        bool isJumping = Jump();
        //Debug.Log("State: " + state + ' ' + "Grounded: " + isGrounded + ' ' + "Jumping: " + isJumping);


        switch(state){
            case EntityState.IDLE:
                if (isJumping){
                   changeState(EntityState.JUMPING);
                }
                else if (isGrounded){
                    changeState(EntityState.MOVING);
                } else {
                    changeState(EntityState.FALLING);
                }
            break;
            case EntityState.SLOWED:
            case EntityState.MOVING:
                if (isJumping){
                    changeState(EntityState.JUMPING);
                }
                else if (!isGrounded){
                    changeState(EntityState.FALLING);
                }
            break;
            case EntityState.JUMPING:
                /*
                if (isGrounded){
                    changeState(EntityState.MOVING);
                } else */
                if (velocity.y < 0) {
                    changeState(EntityState.FALLING);
                }
            break;
            case EntityState.FALLING:
                if (isJumping && jumpsLeft > 0){
                    changeState(EntityState.JUMPING);
                }
                else if (isGrounded){
                    changeState(EntityState.MOVING);
                }
                else if (transform.position.y < -4){
                    changeState(EntityState.DEAD);
                }
            break;
            case EntityState.DEAD:
                // Debug.Log("DEAD");
            break;
            default:
                if (debug) Debug.Log("Unknown state: " + state);
            break;
            
        }
    }

// Change current state (calls enter/leave state)

    public void changeState(EntityState nextState){
        leaveState();
        state = nextState;
        enterState(nextState);
    }

#region Enter States

    private void enterState(EntityState nextState){
        switch(state){
            case EntityState.IDLE:
                jumpsLeft = entityData.MaxJumps;
                directionVector = Vector2.zero;
                velocity = Vector3.zero;
                enterIdleState();
            break;
            case EntityState.MOVING:
                jumpsLeft = entityData.MaxJumps;
                switch(currentDirection){
                    case Direction.X:
                    directionVector = new Vector2(1, 0);
                    break;
                    case Direction.Z:
                    directionVector = new Vector2(0, 1);
                    break;
                }
                enterMovingState();
            break;
            case EntityState.SLOWED:
                enterGroundedState();
            break;
            case EntityState.FALLING:
                enterFallingState();
            break;
            case EntityState.JUMPING:
                //Debug.Log("Jumping");
                velocity.y = entityData.JumpSpeed;
                jumpsLeft--;
                enterJumpingState();
            break;
            case EntityState.DEAD:
                directionVector = Vector2.zero;
                velocity = Vector3.zero;
                enterDeadState();
            break;
        }
    }

    virtual protected void enterIdleState(){
        if (debug) Debug.Log("[ENT] Entering IDLE");
    }
    virtual protected void enterMovingState(){
        if (debug) Debug.Log("[ENT] Entering MOVING");
    }

    virtual protected void enterGroundedState(){
        if (debug) Debug.Log("[ENT] Entering GROUNDED");
    } 
    virtual protected void enterJumpingState(){
        if (debug) Debug.Log("[ENT] Entering JUMPING");
    }
    virtual protected void enterFallingState(){
        if (debug) Debug.Log("[ENT] Entering FALLING");
    }
    virtual protected void enterDeadState(){
        if (debug) Debug.Log("[ENT] Entering DEAD");
    }

#endregion

#region Exit States
    private void leaveState(){
        switch(state){
            case EntityState.IDLE:
                exitIdleState();
            break;
            case EntityState.MOVING:
               exitMovingState();
            break;
            case EntityState.SLOWED:
                exitGroundedState();
            break;
            case EntityState.FALLING:
                exitFallingState();
            break;
            case EntityState.JUMPING:
                exitJumpingState();
            break;
            case EntityState.DEAD:
                exitDeadState();
            break;
        }

    }

    virtual protected void exitIdleState(){
        if (debug) Debug.Log("[ENT] Leaving IDLE");
    }
    virtual protected void exitMovingState(){
        if (debug) Debug.Log("[ENT] Leaving MOVING");
    } 
    virtual protected void exitGroundedState(){
        if (debug) Debug.Log("[ENT] Leaving GROUNDED");
    }
    virtual protected void exitJumpingState(){
        if (debug) Debug.Log("[ENT] Leaving JUMPING");
    }
    virtual protected void exitFallingState(){
        if (debug) Debug.Log("[ENT] Leaving FALLING");
    }
    virtual protected void exitDeadState(){
        if (debug) Debug.Log("[ENT] Leaving DEAD");
    }

#endregion

#endregion

}
