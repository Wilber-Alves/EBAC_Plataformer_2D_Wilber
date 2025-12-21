using System.Net.Security;
using UnityEngine;

// EnemyPatrol class that extends EnemyReactive and adds patrol behavior and ground detection
public class EnemyPatrol : EnemyReactive
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Detection Settings")]
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    [Header("Animation Parameters")]
    public string boolRun = "Run";
    
    protected Transform _player;
    protected bool _isChasing = false;

    [Header("Ground Detection Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDistance = 0.5f;
    public float wallDetectionRange = 0.3f;
    protected int _direction = 1;

    protected override void Start()
    {
        base.Start();
         GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;
    }

    protected virtual void Update()
    {
        if (_isFrozen || _isDead) return;

        CheckForPlayer();
        HandlePlataformDetection();
        Move();
    }

    private void CheckForPlayer()
    {
        if (_player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            _isChasing = true;
           
            float directionToPlayer = _player.position.x - transform.position.x;
            if ((directionToPlayer > 0 && _direction < 0) || (directionToPlayer < 0 && _direction > 0))
            {
                Flip();
            }
        }
        else
        {
            _isChasing = false;
        }

        if (animator != null) animator.SetBool(boolRun, _isChasing);
    }

    private void HandlePlataformDetection()
    {

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundCheck.position, Vector2.right * _direction, wallDetectionRange, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundDistance, Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.right * _direction * wallDetectionRange, Color.blue);

        if (groundInfo.collider == null || wallInfo.collider == true)
        {
            Flip();
            _isChasing = false;
        }
    }

    protected virtual void Move()
    {
        float currentSpeed = _isChasing ? chaseSpeed : patrolSpeed;
        transform.Translate(Vector2.right * _direction * currentSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    protected void Flip()
    {
        _direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
