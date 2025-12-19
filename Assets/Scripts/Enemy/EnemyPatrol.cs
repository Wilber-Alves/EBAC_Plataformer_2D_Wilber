using UnityEngine;

// EnemyPatrol class that extends EnemyReactive and adds patrol behavior and ground detection
public class EnemyPatrol : EnemyReactive
{
    [Header("Patrol Settings")]
    public float speed = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    protected int _direction = 1;
    public LayerMask Ground;

    protected virtual void Update()
    {
        if (_isFrozen) return;

        // verify if there is ground ahead
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, Ground);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundDistance, Color.red);

     
        // if no ground, flip direction        
        if (groundInfo.collider == false)
        {
            Flip();
        }

        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    protected void Flip()
    {
        _direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
