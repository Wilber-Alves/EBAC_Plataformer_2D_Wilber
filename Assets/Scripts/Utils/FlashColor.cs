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
        DOTween.Kill(this.gameObject);

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Color original = _originalColors[i];

            spriteRenderers[i].DOColor(flashColor, duration)
                  .SetLoops(2, LoopType.Yoyo)
                  .OnComplete(() => {
                      // check if the enemy is frozen to set the correct color
                      if (_enemyReactive != null && _enemyReactive.IsFrozen())
                      {
                          spriteRenderers[i].color = _enemyReactive.freezeColor;
                      }
                      else
                      {
                          spriteRenderers[i].color = original; // return to original color
                      }
                  })
                  .SetId(this.gameObject);
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
