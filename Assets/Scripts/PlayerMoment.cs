using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement 
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if( movement.x != 0 )
        {
            lastHorizontalVector = movement.x;
        } 
        if ( movement.y != 0 )
        {
            lastVerticalVector = movement.y;
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
