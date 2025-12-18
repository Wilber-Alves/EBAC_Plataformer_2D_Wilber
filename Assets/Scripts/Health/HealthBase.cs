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
    private void kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Debug.Log($"Destroing {gameObject.name} now.");
            Destroy(gameObject, delayToDestroy);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} He died, but 'destroyOnKill' is unchecked!");
        }
    }
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}


