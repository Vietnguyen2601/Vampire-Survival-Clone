using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement 
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movement;
    [HideInInspector]
    public Vector2 lastMoveVector;

    //reference
    Rigidbody2D rb;
    PlayerStats player;


    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMoveVector = new Vector2(1, 0f);
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        if (movement.x != 0)
        {
            lastHorizontalVector = movement.x;
            lastMoveVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (movement.y != 0)
        {
            lastVerticalVector = movement.y;
            lastMoveVector = new Vector2(0f, lastVerticalVector);
        }

        if (movement.x != 0 && movement.y != 0)
        {
            lastMoveVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(movement.x * player.currentMoveSpeed, movement.y * player.currentMoveSpeed);
    }
}
