using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBetweenShots = 0.3f;
    public Transform playerSideReference;


    private Coroutine _currentCoroutine;    

    // Update is called once per frame
    void Update()
    {
       
            if (!InventoryBase.hasWeapon) return;

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_currentCoroutine == null)
                    _currentCoroutine = StartCoroutine(StartShoot());
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                if (_currentCoroutine != null)
                {
                    StopCoroutine(_currentCoroutine);
                    _currentCoroutine = null;
                }
            }
    }
    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile, positionToShoot.position, Quaternion.identity);
        projectile.side = Mathf.Sign(playerSideReference.localScale.x); // Note: Mathf.Sign checks which way the player's body is currently facing. If the 'X scale' is positive, you move to the right (1). If it's negative, you move to the left (-1).
    }

}
