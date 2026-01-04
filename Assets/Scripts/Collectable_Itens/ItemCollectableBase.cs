using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemCollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    public ParticleSystem particleSystem;

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
            OnCollect();
        }
    }

    protected virtual void Collect()
    { 
        gameObject.SetActive(false);
        OnCollect();    
    }
    protected virtual void OnCollect()
    { 
        if (particleSystem != null) particleSystem.Play();
        Destroy(gameObject);
    }
}
