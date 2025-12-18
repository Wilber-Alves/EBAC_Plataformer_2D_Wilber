using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int damage = 10;

    public Animator animator;
    public string triggerAttack = "Attack";

    public HealthBase healthBase;

    [Header("Freeze attack")]
    public Color freezeColor = new Color(0.5f, 0.8f, 1.0f);
    public Color _originalColor;
    private SpriteRenderer _spriteRenderer;
    private bool _isFrozen = false;

    public bool IsFrozen() => _isFrozen;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }
    private void Update()
    {
        if (_isFrozen) return;
    }
    public void Freeze(float duration)
    {
        if (_isFrozen) return;
        StartCoroutine(FreezeRoutine(duration));
    }
    private IEnumerator FreezeRoutine(float duration)
    {
        _isFrozen = true;
        var flash = GetComponent<FlashColor>();
        if (flash != null)
        {
            flash.spriteRenderers.ForEach(s => s.color = freezeColor);
        }
        else
        {
            _spriteRenderer.color = freezeColor;
        }

        if (animator != null) animator.speed = 0;

        yield return new WaitForSeconds(duration);

        if (flash != null)
        {

            _isFrozen = false;
            flash.Flash();
        }

        if (animator != null) animator.speed = 1;
        _isFrozen = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);

        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();

        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    public void Damage(int amount)
    {

        healthBase.Damage(amount);
        Debug.Log($"{transform.name} took {amount} damage. Health left: {healthBase.GetCurrentHealth()}");
    }
}
