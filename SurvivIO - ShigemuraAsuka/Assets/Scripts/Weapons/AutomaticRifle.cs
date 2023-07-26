using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public Inventory inventoryScript;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject nozzle;

    private void Start()
    {
        damage = 15;
        canShoot = true;
        isFiring = false;
        stopFiring = false;
        fireRate = 0.35f;
        reloadTime = 2.3f;
        bulletSpread = 1.1f;
        currentMagazineAmmo = 30;
    }

    private void Update()
    {
        if (isFiring)
        {
            MakeIsFiringFalse();
            Shoot(bulletPrefab, nozzle);
        }
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (inventoryScript.currentAutomaticRifleMagazineAmmo > 0)
        {
            GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetBulletDamage(damage);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);
            rb.velocity = (dir + perpendicularDir);
            inventoryScript.ExpendAmmo();
            Debug.Log("Multi-Shot");
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
                StartCoroutine(AutomaticRifleFireRateTimer());
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
        if (!stopFiring)
        {
            MakeIsFiringTrue();
        }
    }

    private IEnumerator AutomaticRifleFireRateTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public override IEnumerator EnemyReload()
    {
        Debug.Log("Enemy is reloading assault rifle");
        yield return new WaitForSeconds(reloadTime * 2f);
        currentMagazineAmmo = 30;
        canShoot = true;
        Debug.Log("Enemy finished reloading assault rifle");
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        stopFiring = false;
        MakeIsFiringTrue();
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        isFiring = false;
        stopFiring = true;
    }

    private void MakeIsFiringTrue()
    {
        isFiring = true;
    }

    private void MakeIsFiringFalse()
    {
        isFiring = false;
        StartCoroutine(FireRateTimer());
    }
}
