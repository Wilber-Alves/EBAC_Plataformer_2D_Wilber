using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector2 direction;
    public float timeToDestroy = 2.4f;
    public float side = 1.0f;
    public int damageAmount = 1;

    [Header("Freeze percentage setting")]
    [Range(0, 100)]
    public float freezeChance = 15f;
    public float freezeDuration = 2.0f;


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);

    }

    private void Start()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * side;
        transform.localScale = newScale;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBase health = collision.GetComponent<HealthBase>();
        EnemyBase enemy = collision.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Damage(damageAmount);

            if (enemy != null && Random.Range(0f, 100f) <= 15f)
            {
                enemy.Freeze(freezeDuration);
                Debug.Log("I'll freeze your bones! Wait, what bones?");
            }


            Destroy(gameObject);
        }
    }
}
