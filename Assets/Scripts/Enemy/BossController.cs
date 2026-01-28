using UnityEngine;

public class BossController : EnemyPatrolJumper
{
    [Header("Boss Combat")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float projectileSpeed = 10f;
    private float _fireTimer;


    [Header("Angry State Settings")]
    public float maxHealth = 30.0f;
    public float angryHealthThreshold = 0.5f;
    public float angryFireRate = 0.8f;
    public float angryMoveSpeedMultiplier = 1.5f;
    private bool _isAngry = false;
    private HealthBase _bossHealth;
    private Rigidbody2D _rigidbody;

    [Header("End Game UI")]
    public GameObject endGameCanvas;

    protected override void Start()
    {
        base.Start();
        _bossHealth = GetComponent<HealthBase>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
 
        base.Update();


        if (_isDead)
        {
            _rigidbody.linearVelocity = Vector2.zero;


            if (animator != null) animator.Play("Death");


            if (endGameCanvas != null && !endGameCanvas.activeSelf)
            {
                Invoke("ActivateEndGameUI", 2f);
            }
            return;
        }

        CheckAngryState();

        float currentFireRate = _isAngry ? angryFireRate : fireRate;


        _fireTimer += Time.deltaTime;
        if (_fireTimer >= currentFireRate)
        {
            ShootAtPlayer();
            _fireTimer = 0;
        }
    }
    private void ActivateEndGameUI()
    {
        if (endGameCanvas != null)
        {
            endGameCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CheckAngryState()
    {
        if (_isAngry || _bossHealth == null) return;

        if (_bossHealth._currentHealth <= _bossHealth.startHealth * angryHealthThreshold)
        {
            EnterAngryState();
        }
    }
    private void EnterAngryState()
    {
        _isAngry = true;

        patrolSpeed *= angryMoveSpeedMultiplier;
        chaseSpeed *= angryMoveSpeedMultiplier;

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            sr.color = Color.red;
        }

        Debug.Log("The Boss is Angry!!!");
    }


    private void ShootAtPlayer()
    {
        if (_player == null) return;

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Destroy(proj, 1.0f);
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), GetComponent<Collider2D>());

 
            Vector2 direction = (_player.position - firePoint.position).normalized;

            if (proj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = direction * projectileSpeed;
            }

            if (!proj.GetComponent<ProjectileDamage>())
            {
                proj.AddComponent<ProjectileDamage>().damageAmount = 10;
            }
        }
    }

    private void OnDisable()
    {
        if (_isDead && endGameCanvas != null)
        {
            endGameCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}


public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<HealthBase>();
            if (health != null) health.Damage(damageAmount);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}