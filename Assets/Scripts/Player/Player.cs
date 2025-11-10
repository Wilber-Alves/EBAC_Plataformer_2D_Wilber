using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    public Vector2 friction = new Vector2(.1f, 0);

    public float speed;
    public float forceJump = 2;

    void Update()
    {
        HandleMovement();
        HandleJump();
    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.linearVelocity = new Vector2(-speed, myRigidbody.linearVelocity.y);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.linearVelocity = new Vector2(speed, myRigidbody.linearVelocity.y);
        }


        if(myRigidbody.linearVelocity.x > 0)
        {
            myRigidbody.linearVelocity += friction;

        }
        else if(myRigidbody.linearVelocity.x < 0)
        {
            myRigidbody.linearVelocity -= friction; ;
        }

    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            myRigidbody.linearVelocity = Vector2.up * forceJump;
    }
}
