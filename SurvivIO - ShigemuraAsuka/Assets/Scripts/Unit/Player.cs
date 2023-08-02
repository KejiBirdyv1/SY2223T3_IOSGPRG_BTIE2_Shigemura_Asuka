using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private GameObject nozzle;
    [SerializeField] private Image fireButton;
    public bool isReloading;
    private Inventory inventory;

    private void Start()
    {
        Initialize("Bob", 100, 0.2f);
        isReloading = false;
        inventory = GetComponent<Inventory>();
    }

    public void OnPointerDown()
    {
        if (!isReloading && currentGun != inventory.gunTypes[(int)Weapon.AutomaticRifle])
        {
            Shoot();
        }
        currentGun.OnPointerDown();
    }

    public void OnPointerUp()
    {
        currentGun.OnPointerUp();
    }

    public override void Shoot()
    {
        base.Shoot();
        if (currentGun.GetComponent<RocketLauncher>() != null)
        {
            currentGun.Shoot(missilePrefab, nozzle);
        }
        else
        {
            currentGun.Shoot(bulletPrefab, nozzle);
        }
    }

    public void SetCurrentGun(Gun gun)
    {
        currentGun = gun;
        currentGun.canShoot = true;
    }

    public override void DoDeath()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
