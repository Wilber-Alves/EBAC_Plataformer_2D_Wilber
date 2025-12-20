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

        // 1. Try to get the HealthBase component from the collided object
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            // 2. iif the collided object is the Player (Tag "Player"), it takes damage from the spike!
            if (collision.gameObject.CompareTag("Player"))
            {
                health.Damage(damageAmount);
                OnAttack();
            }
            // 3. if the collided object is another Enemy (Tag "Enemy"), it also takes damage from the spike!
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                health.Damage(damageAmount);
                OnAttack(); // put some song ou sound effect here
            }
        }
    }

    // This method can be overridden by derived classes to implement custom behavior when the enemy attacks
    protected virtual void OnAttack()
    {



    }
}