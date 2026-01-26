using UnityEngine;

public class BossController : EnemyPatrolJumper
{
    [Header("Boss Combat")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float projectileSpeed = 10f;
    private float _fireTimer;

    [Header("Targeting")]
    private Transform _playerTransform;

    [Header("End Game UI")]
    public GameObject endGameCanvas;

    protected override void Start()
    {
        base.Start();

        // search for the player in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _playerTransform = player.transform;
    }

    protected override void Update()
    {
        base.Update();

        if (_isDead || _isFrozen || _playerTransform == null) return;

        HandleFlip();

        _fireTimer += Time.deltaTime;
        if (_fireTimer >= fireRate)
        {
            ShootAtPlayer();
            _fireTimer = 0;
        }
    }

    private void ShootAtPlayer()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // 1. instantiate projectile at firePoint position
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // 2. calculate direction towards the player
            Vector2 direction = (_playerTransform.position - firePoint.position).normalized;

            // 3. applies velocity to the projectile
            if (proj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }

    private void HandleFlip()
    {
        // take current scale values
        float posX = transform.localScale.x;
        float posY = transform.localScale.y;

        // compare the boss position with the player position
        if (_playerTransform.position.x < transform.position.x)
        {
            // if player are on the left: force X scale to be negative
            transform.localScale = new Vector3(-Mathf.Abs(posX), posY);
        }
        else
        {
            // if player are on the right: force X scale to be positive
            transform.localScale = new Vector3(Mathf.Abs(posX), posY);
        }
    }

    private void OnDisable()
    {
        // activate end game canvas when boss is defeated
        if (_isDead && endGameCanvas != null)
        {
            endGameCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}