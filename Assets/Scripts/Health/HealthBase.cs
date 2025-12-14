using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startHealth = 10;
    
    public bool destroyOnKill = false;
    public float delayToDestroy = 0f;

    private float _currentHealth;
    private bool _isDead = false;

    public FlashColor _flashColor;

    private void Awake()
    {
        Init();
        if (_flashColor == null)
        { 
            _flashColor = GetComponent<FlashColor>();

        }

    }
    private void Init()
    {
        _isDead = false;
        _currentHealth = startHealth;
    }

    public void Damage(int damage)
    { 
        if (_isDead) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }

    }
    private void kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject, delayToDestroy);
        }
    }
}


