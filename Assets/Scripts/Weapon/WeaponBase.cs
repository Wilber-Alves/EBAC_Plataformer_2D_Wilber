using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBetweenShots = 0.3f;
    public Transform playerSideReference;

    public AudioRandomPlayAudioClips randomShoot;


    private Player _player;


    private Coroutine _currentCoroutine;
    
    void Start()
    {
        _player = GetComponentInParent<Player>();
    }

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
        if (positionToShoot == null) return;

        if (randomShoot != null)
        {
            randomShoot.PlayRandom();
        }

        var projectile = Instantiate(prefabProjectile, positionToShoot.position, Quaternion.identity);

        projectile.side = _player.GetFacingDirection();
    }

    public void SetupWeaponReferences(GameObject visualClone)
    {
        Transform[] transforms = visualClone.GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            
            if (t.name.ToLower().Contains("ShootPosition"))
            {
                positionToShoot = t;
            }
        }
    }


}
