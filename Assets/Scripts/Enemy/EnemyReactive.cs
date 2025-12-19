using System.Collections;
using UnityEngine;

// EnemyReactive class that extends EnemyBase and adds reactive behaviors like taking damage and freezing
public class EnemyReactive : EnemyBase
{
    [Header("Reactive Attributes")]
    public HealthBase healthBase;
    public Animator animator;
    public string triggerAttack = "Attack";

    [Header("Freeze Settings")]
    public Color freezeColor = new Color(0.5f, 0.8f, 1.0f);
    protected Color _originalColor;
    protected SpriteRenderer _spriteRenderer;

    public bool IsFrozen() => _isFrozen;

    
    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null) _originalColor = _spriteRenderer.color;
    }

    // Override the OnAttack method to trigger the attack animation
    protected override void OnAttack()
    {
        if (animator != null) animator.SetTrigger(triggerAttack);
    }
    // Method to apply damage to the enemy
    public virtual void Damage(int amount)
    {
        if (healthBase != null) healthBase.Damage(amount);
    }
    // Method to freeze the enemy for a specified duration
    public void Freeze(float duration)
    {
        if (_isFrozen) return;
        StopAllCoroutines();
        StartCoroutine(FreezeRoutine(duration));
    }
    public void Unfreeze()
    {
        StopAllCoroutines();
        _spriteRenderer.color = _originalColor;
        if (animator != null) animator.speed = 1;
        _isFrozen = false;
        Debug.Log("Ice shattered!");
    }


    // Coroutine to handle the freezing effect
    private IEnumerator FreezeRoutine(float duration)
    {
        _isFrozen = true;
        if (animator != null) animator.speed = 0;
        _spriteRenderer.color = freezeColor;

        yield return new WaitForSeconds(duration);

        if (_isFrozen) Unfreeze();
    }
}
