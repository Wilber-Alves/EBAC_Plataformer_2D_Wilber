using UnityEngine;

// EnemyPatrol class that extends EnemyReactive and adds patrol behavior and ground detection
public class EnemyPatrol : EnemyReactive
{
    [Header("Patrol Settings")]
    public float speed = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public float wallDetectionRange = 0.3f;
    protected int _direction = 1;
    public LayerMask groundLayer;
    
    protected virtual void Update()
    {
        if (_isFrozen) return;

        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundCheck.position, Vector2.right * _direction, wallDetectionRange, groundLayer); 

        Debug.DrawRay(groundCheck.position, Vector2.down * groundDistance, Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.right * _direction * wallDetectionRange, Color.blue); 
        
        if (groundInfo.collider == null || wallInfo.collider == true)
        {
            Flip();
        }

        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.right * _direction * speed * Time.deltaTime);
    }

    protected void Flip()
    {
        _direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
