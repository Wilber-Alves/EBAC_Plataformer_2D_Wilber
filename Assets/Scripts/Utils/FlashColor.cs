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
    private EnemyBase _enemyBase;
    private Tween _currentTween;

    private void Start()
    {
        _enemyBase = GetComponent<EnemyBase>();
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
            var sprite = spriteRenderers[i];
            Color targetColor = _originalColors[i];

            if (_enemyBase != null && _enemyBase.IsFrozen())
            {
                targetColor = _enemyBase.freezeColor;
            }

            sprite.DOColor(flashColor, duration)
                  .SetLoops(2, LoopType.Yoyo)
                  .OnComplete(() => {
               
                      sprite.color = (_enemyBase != null && _enemyBase.IsFrozen()) ? _enemyBase.freezeColor : _originalColors[spriteRenderers.IndexOf(sprite)];
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
}
