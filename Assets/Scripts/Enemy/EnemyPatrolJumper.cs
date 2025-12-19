using UnityEngine;

public class EnemyPatrolJumper : EnemyPatrol
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float jumpInterval = 3f;
    private float _jumpTimer;

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
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
