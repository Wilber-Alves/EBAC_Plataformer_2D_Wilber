
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyReactive : EnemyBase
{
    [Header("Reactive Attributes")]
    public HealthBase healthBase;
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    [Header("Freeze Settings")]
    public Color freezeColor = new Color(0.5f, 0.8f, 1.0f);
    protected Color _originalColor;
    protected SpriteRenderer _spriteRenderer;
    private Coroutine _freezeCoroutine;

    public bool IsFrozen() => _isFrozen;

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null) _originalColor = _spriteRenderer.color;
    }

    protected override void PlayAttackAnimation()
    {
        if (animator != null) animator.SetTrigger(triggerAttack);
    }
    protected override void PlayDeathAnimation()
    {
        if (animator != null) animator.SetTrigger(triggerDeath);
    }

    public virtual void Damage(int amount)
    {
        if (healthBase != null) healthBase.Damage(amount);
    }

    public void Freeze(float duration)
    {
        if (_isFrozen) return;
        if (_freezeCoroutine != null) StopCoroutine(_freezeCoroutine);
        _freezeCoroutine = StartCoroutine(FreezeRoutine(duration));
    }

    public void Unfreeze()
    {
        if (_freezeCoroutine != null) StopCoroutine(_freezeCoroutine);

        _isFrozen = false;
        // GUARANTEE: It always reverts to "Enemy" upon thawing.
        gameObject.layer = LayerMask.NameToLayer("Enemy");

        var flash = GetComponent<FlashColor>();
        if (flash != null)
        {
            flash.spriteRenderers.ForEach(s => s.DOKill());
            flash.ResetAllColors(); // Returns to the original colors
        }
        else if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _originalColor;
        }

        if (animator != null) animator.speed = 1;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.rotation = Quaternion.identity;
        }
        Debug.Log("Ice shattered!");
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        _isFrozen = true;
        int originalLayer = gameObject.layer;

        // 1. Switch to Ground to turn into a platform.
        gameObject.layer = LayerMask.NameToLayer("Ground");

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (animator != null) animator.speed = 0;

        // 2. Ice Visual Feedback (Blue)
        var flash = GetComponent<FlashColor>();
        if (flash != null)
        {
            foreach (var s in flash.spriteRenderers)
            {
                s.DOKill();
                s.color = freezeColor;
            }
        }
        else if (_spriteRenderer != null)
        {
            _spriteRenderer.color = freezeColor;
        }

        // Wait for the ice to solidify (e.g., 2 seconds).
        yield return new WaitForSeconds(duration - 1f);

        // 3. Visual Alert Feedback (Blinking)
        if (flash != null)
        {
            for (int i = 0; i < flash.spriteRenderers.Count; i++)
            {
                flash.spriteRenderers[i].DOColor(flash.GetOriginalColor(i), 0.2f).SetLoops(5, LoopType.Yoyo);
            }
        }
        else if (_spriteRenderer != null)
        {
            _spriteRenderer.DOColor(_originalColor, 0.2f).SetLoops(5, LoopType.Yoyo);
        }

        // Wait for the final second of the blink.
        yield return new WaitForSeconds(1f);

        if (_isFrozen)
        {
            gameObject.layer = originalLayer; // Restore layer before finishing
            Unfreeze();
        }
    }
}