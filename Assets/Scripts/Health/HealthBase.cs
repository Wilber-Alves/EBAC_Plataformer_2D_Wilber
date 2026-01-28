
using System;
using System.Data;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public Action OnDamage;

    [Header ("UI / Scriptable Object Reference")]

    public SOFloat_Health SOFloat_Health;

    public float startHealth = 30.0f;
    public bool destroyOnKill = false;
    public float delayToDestroy = 0f;

    public float _currentHealth;
    private bool _isDead = false;
    private bool _isImmune = false;

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
        UpdateSO();
    }

    private void Init()
    {
        _isDead = false;
        _currentHealth = startHealth;
    }

    public void Damage(float damage)
    {
        if (_isDead || _isImmune) return;

        _currentHealth -= damage;
        UpdateSO();

       OnDamage?.Invoke();

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
    public void AddHealth(float amount = 2.5f)
    {
        if (_isDead) return;

        _currentHealth += amount;

        
        if (_currentHealth > startHealth)
        {
            _currentHealth = startHealth;
        }

        UpdateSO();

        Debug.Log($"{gameObject.name} healed {amount}. Current health: {_currentHealth}");

        
    }
    private void UpdateSO()
    {
        if (SOFloat_Health != null)
        {
            SOFloat_Health.value = _currentHealth;
        }
    }

    private void kill()
    {
        _isDead = true;
        OnKill?.Invoke();
    }

    public void SetImmunity(bool status)
    {
        _isImmune = status;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}


