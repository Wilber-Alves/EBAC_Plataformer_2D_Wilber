using DG.Tweening;
using EDGEE.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;

    private GameObject _currentPlayer;

    [Header("Animation")]
    public float duration = 0.5f;
    public float delay = 0.02f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {

        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position=startPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetDelay(0).SetEase(ease).From();
    }

}
