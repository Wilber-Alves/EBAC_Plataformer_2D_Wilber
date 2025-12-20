using UnityEngine;

public class EnemyPatrolJumper : EnemyPatrol
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpInterval = 3f;
    private float _jumpTimer;
    private Rigidbody2D _rb;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;

        _jumpTimer += Time.deltaTime;
        if (_jumpTimer >= jumpInterval)
        {
            Jump();
            _jumpTimer = 0;
        }
    }

    private void Jump()
    {
        if (_rb != null)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);

            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}