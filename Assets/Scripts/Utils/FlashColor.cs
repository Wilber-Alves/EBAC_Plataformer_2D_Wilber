using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color flashColor = Color.yellow;
    public float duration = 0.1f;

    private List<Color> _originalColors = new List<Color>();
    private EnemyReactive _enemyReactive;
    private Tween _currentTween;

    private void Start()
    {
        _enemyReactive = GetComponent<EnemyReactive>();
        SetupFlash();
    }

    public void SetupFlash()
    {
        spriteRenderers.Clear();
        _originalColors.Clear();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true).ToList();

        foreach (var sprite in spriteRenderers)
        {
            _originalColors.Add(sprite.color);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Flash();
        }
    }
    public void Flash()
    {
        if (spriteRenderers.Count == 0) SetupFlash();

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            if (spriteRenderers[i] == null) continue;

            int index = i;
            spriteRenderers[i].DOKill();

            spriteRenderers[i].DOColor(flashColor, duration)
                  .SetLoops(2, LoopType.Yoyo)
                  .OnComplete(() => {
                      if (_enemyReactive != null && _enemyReactive.IsFrozen())
                      {
                          spriteRenderers[index].color = _enemyReactive.freezeColor;
                      }
                      else
                      {
                          spriteRenderers[index].color = _originalColors[index];
                      }
                  });
        }
    }
    private void OnValidate()
    {

        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        //foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        //{
        //   spriteRenderers.Add(child);
        //}
    }

    public void ResetAllColors()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].color = _originalColors[i];
        }
    }

    public Color GetOriginalColor(int index)
    {
        return _originalColors[index];
    }

}
