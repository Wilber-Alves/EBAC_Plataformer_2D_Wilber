using Unity.VisualScripting;
using UnityEngine;

public class ItemCollectableHealth : ItemCollectableBase
{
    [Header("Health Settings")]
    public int healthAmount = 5; //NOTE:I don't know what's happening, but I had to halve the item's value
                                 //because it's counting double! Just like it did with the coins.

    protected override void OnCollect()
    {

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var health = player.GetComponent<HealthBase>();
            if (health != null)
            {
                health.AddHealth(healthAmount);
                base.OnCollect();
            }
        }
    }
}
