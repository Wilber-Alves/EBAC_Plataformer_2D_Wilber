using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInvincibility : MonoBehaviour
{
    public HealthBase healthBase;
    public float duration = 1.5f;
    public float blinkInterval = 0.1f;
    private Player _player;
    private Dictionary<SpriteRenderer, Color> _originalColors = new Dictionary<SpriteRenderer, Color>();

    void Start()
    {
        _player = GetComponent<Player>();
        if (healthBase != null) healthBase.OnDamage += StartInvincibility;
    }

    void StartInvincibility()
    {
        StopAllCoroutines();
        StartCoroutine(InvincibilityRoutine());
    }
    IEnumerator InvincibilityRoutine()
    {
        healthBase.SetImmunity(true);

     
        var renderers = _player.GetCurrentAnimator().GetComponentsInChildren<SpriteRenderer>(true);
        _originalColors.Clear();

        foreach (var sr in renderers)
        {
            if (sr != null) _originalColors[sr] = sr.color;
        }

        float timer = 0;
        while (timer < duration)
        {
            
            SetAlpha(0.2f);
            yield return new WaitForSeconds(blinkInterval);

            
            ResetAlpha();
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2;
        }

        ResetAlpha(); 
        healthBase.SetImmunity(false);
    }

    void SetAlpha(float alpha)
    {
        foreach (var item in _originalColors)
        {
            if (item.Key == null) continue;
            Color c = item.Value; 
            c.a = alpha;          
            item.Key.color = c;
        }
    }

    void ResetAlpha()
    {
        foreach (var item in _originalColors)
        {
            if (item.Key != null) item.Key.color = item.Value;
        }
    }
}