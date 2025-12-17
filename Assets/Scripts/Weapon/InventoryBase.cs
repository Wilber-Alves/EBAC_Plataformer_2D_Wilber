using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    [Header("Weapon configuration")]
    public GameObject Weapon;
    public static bool hasWeapon = false;
    public GameObject collectEffect;

    private void Start()
    {
        hasWeapon = false;
        if (Weapon != null) Weapon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.CompareTag("IceWand"))
        {
            EquipWand();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("IceWand"))
        {
            if (collectEffect != null) Instantiate(collectEffect, other.transform.position, Quaternion.identity);
            EquipWand();
            Destroy(other.gameObject);
        }

    }

    void EquipWand()
    {
        hasWeapon = true;
        if (Weapon != null) Weapon.SetActive(true);
        Debug.Log("Ice Wand Equipped!");
    }

}
