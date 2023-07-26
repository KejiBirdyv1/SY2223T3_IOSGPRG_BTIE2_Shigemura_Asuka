using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] private float spread;
    
    public int currentMagazineAmmo;
    public float fireRate;
    public float reloadTime;
    public float bulletSpread;
    public bool canShoot;
    public bool isFiring;
    public bool stopFiring;

    public virtual void Shoot(GameObject prefab, GameObject nozzle)
    {
        Debug.Log("Base Gun Shooting");
    }

    public virtual void EnemyShoot(GameObject prefab, GameObject nozzle)
    {

    }

    public virtual IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(fireRate);
    }

    public virtual void OnPointerDown()
    {

    }

    public virtual void OnPointerUp()
    {

    }

    public virtual IEnumerator EnemyReload()
    {
        yield return new WaitForSeconds(reloadTime);
    }
}
