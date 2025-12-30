
using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;

    public int startHealth = 10;
    
    public bool destroyOnKill = false;
    public float delayToDestroy = 0f;

    private float _currentHealth;
    private bool _isDead = false;

    [SerializeField] public FlashColor _flashColor;

    private void Awake()
    {
        Init();
        if (_flashColor == null)
        { 
            _flashColor = GetComponentInChildren<FlashColor>();

        }

    }
    private void Start()
    {
        _currentHealth = startHealth;
        _isDead = false;
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

        Debug.Log($"{gameObject.name} received {damage} damage. Current health: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} Reached zero health. Calling Kill()");
            kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }

    }
    public void AddHealth(int amount)
    {
        if (_isDead) return;

        _currentHealth += amount;

        
        if (_currentHealth > startHealth)
        {
            _currentHealth = startHealth;
        }

        Debug.Log($"{gameObject.name} healed {amount}. Current health: {_currentHealth}");

        
    }

    private void kill()
    {
        _isDead = true;
        OnKill?.Invoke();
    }
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}


