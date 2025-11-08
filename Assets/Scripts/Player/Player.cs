using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    public Vector2 velocity;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.linearVelocity = new Vector2(velocity.x, myRigidbody.linearVelocity.y);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           myRigidbody.linearVelocity = new Vector2(-velocity.x, myRigidbody.linearVelocity.y);
        }

    }
}
