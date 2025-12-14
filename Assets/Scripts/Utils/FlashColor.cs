using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color1 = Color.red;
    public float duration = 0.1f;
    private Tween _currentTween;

    private void OnValidate()
    {

        spriteRenderers = new List<SpriteRenderer>();

        foreach (var child in GetComponentsInChildren<SpriteRenderer>() )
        {
           spriteRenderers.Add(child);
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

        if(_currentTween != null)
        {
          _currentTween.Kill();
          spriteRenderers.ForEach(i => i.color = Color.white);

        }

        foreach (var spriteRenderer in spriteRenderers)
        {
            _currentTween = spriteRenderer.DOColor(color1, duration).SetLoops(2, LoopType.Yoyo);
    
        }

    }
}
