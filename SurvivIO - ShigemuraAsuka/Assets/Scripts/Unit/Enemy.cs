using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private Weapon weapon;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private List<Gun> gunTypes;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject nozzle;

    private void Start()
    {
        Initialize("Enemy", 100, 0.2f);
        weapon = (Weapon)Random.Range(0, 3);
        weapons[(int)weapon].SetActive(true);
        currentGun = gunTypes[(int)weapon];
    }

    public override void Shoot()
    {
        currentGun.EnemyShoot(bulletPrefab, nozzle);
    }

    public override void DoDeath()
    {
        base.DoDeath();
        Destroy(gameObject);
    }
}
