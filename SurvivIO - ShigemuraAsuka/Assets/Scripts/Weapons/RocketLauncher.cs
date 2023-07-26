using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{
    public Inventory inventoryScript;
    private bool triggerReleased;

    private void Start()
    {
        damage = 100;
        canShoot = true;
        triggerReleased = true;
        fireRate = 5f;
        reloadTime = 2.3f;
        currentMagazineAmmo = 1;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (inventoryScript.currentRocketLauncherMagazineAmmo > 0 && canShoot && triggerReleased)
        {
            GameObject rocket = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            rb.velocity = dir;
            canShoot = false;
            StartCoroutine(FireRateTimer());
            inventoryScript.ExpendAmmo();
            Debug.Log("Rocket Fired");
        }
    }

    public override void EnemyShoot(GameObject prefab, GameObject nozzle)
    {
        if (canShoot && currentMagazineAmmo > 0)
        {
            GameObject rocket = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            rb.velocity = dir;
            canShoot = false;
            currentMagazineAmmo--;
            if (currentMagazineAmmo > 0)
            {
                StartCoroutine(FireRateTimer());
            }
            else
            {
                StartCoroutine(EnemyReload());
            }
        }
    }

    public override IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public override IEnumerator EnemyReload()
    {
        Debug.Log("Enemy is reloading rocket launcher");
        yield return new WaitForSeconds(reloadTime * 2f);
        currentMagazineAmmo = 1;
        canShoot = true;
        Debug.Log("Enemy finished reloading rocket launcher");
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        triggerReleased = false;
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        triggerReleased = true;
    }
}
