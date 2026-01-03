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

        GameObject visualAtual = GetComponent<Player>().GetCurrentAnimator().gameObject;
        Transform weaponOnClone = visualAtual.transform.Find("IceWand");
        if (weaponOnClone != null)
        {
            weaponOnClone.gameObject.SetActive(true);
        }

    }

    void EquipWand()
    {
        hasWeapon = true;
        Debug.Log("Ice Wand Equipped!");

        // 1. Pegamos o Astronauta que está ativo na cena agora
        Player p = GetComponent<Player>();
        GameObject visualAtual = p.GetCurrentAnimator().gameObject;

        // 2. Buscamos o script da arma DENTRO do clone (mesmo que esteja em Hips/Cannon)
        WeaponBase armaNoClone = visualAtual.GetComponentInChildren<WeaponBase>(true);

        if (armaNoClone != null)
        {
            // 3. Ativamos o objeto onde o script WeaponBase está pendurado
            armaNoClone.gameObject.SetActive(true);

            // 4. Sincronizamos o ponto de tiro (shotposition)
            armaNoClone.SetupWeaponReferences(visualAtual);
            Debug.Log("Sucesso: Arma ativada no clone variante!");
        }
        else
        {
            Debug.LogError("Erro: Não achei o script WeaponBase no visual do astronauta.");
        }
    }
}
