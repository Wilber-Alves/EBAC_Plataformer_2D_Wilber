using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Base Monster Attributes - The basic enemy concept would be the ice spikes.")]
    public int damageAmount = 10;
    protected bool _isFrozen = false;
    // This method is called when the enemy collides with another 2D collider
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
       if (_isFrozen) return;

        // Filtro: Só aplica dano se o objeto for o Jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponent<HealthBase>();
            if (health != null)
            {
                health.Damage(damageAmount);
                OnAttack();
            }
        }
    }

    // This method can be overridden by derived classes to implement custom behavior when the enemy attacks
    protected virtual void OnAttack()
    {



    }
}