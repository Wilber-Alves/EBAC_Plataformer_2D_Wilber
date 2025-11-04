using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class MenuButtonManager : MonoBehaviour
{
  public  List<GameObject> buttons;

    [ Header("Animation")]
    public float duration = 0.5f;
    public float delay = 0.02f;
    public Ease ease = Ease.OutBack;

    private void OnEnable()
    {
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.SetActive(false);
            b.transform.localScale = Vector3.zero;
        }
    }


    private void ShowButtons()
    {
        // foreach (var b in buttons)
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i*delay).SetEase(ease);
        }
    }
}
