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
        EnemyReactive enemy = collision.GetComponent<EnemyReactive>();

        if (enemy != null)
        {
            // Apply damage and freeze effect
            if (enemy.IsFrozen())
            {
                enemy.Unfreeze();
            }
            else
            {
                // If not frozen, apply damage and possibly freeze.
                enemy.Damage(damageAmount);

                if (Random.Range(0f, 100f) <= freezeChance)
                {
                    enemy.Freeze(freezeDuration);
                    Debug.Log("I'll freeze your bones!");
                }
            }

            Destroy(gameObject);
        }
    }
}
