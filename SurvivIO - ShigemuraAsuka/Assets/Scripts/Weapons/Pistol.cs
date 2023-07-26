using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Inventory inventoryScript;
    private bool triggerReleased;

    private void Start()
    {
        damage = 10;
        canShoot = true;
        triggerReleased = true;
        fireRate = 2.16f;
        reloadTime = 2f;
        bulletSpread = 1.2f;
        currentMagazineAmmo = 15;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (inventoryScript.currentPistolMagazineAmmo > 0 && canShoot && triggerReleased)
        {
            GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetBulletDamage(damage);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);
            rb.velocity = (dir + perpendicularDir);
            canShoot = false;
            StartCoroutine(FireRateTimer());
            inventoryScript.ExpendAmmo();
            Debug.Log("Single Shot");
        }
    }

    public override void EnemyShoot(GameObject prefab, GameObject nozzle)
    {
        if (canShoot && currentMagazineAmmo > 0)
        {
            GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetBulletDamage(damage);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);
            rb.velocity = (dir + perpendicularDir);
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
        Debug.Log("Enemy is reloading pistol");
        yield return new WaitForSeconds(reloadTime * 2f);
        currentMagazineAmmo = 15;
        canShoot = true;
        Debug.Log("Enemy finished reloading pistol");
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
