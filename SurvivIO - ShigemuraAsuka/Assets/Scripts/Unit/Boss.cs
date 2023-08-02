using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private GameObject nozzle;
    [SerializeField] private GameObject rocketLauncherPickUp;

    public override void Initialize(string name, int maxHealth, float speed)
    {
        base.Initialize(name, maxHealth, speed);
    }

    public override void Shoot()
    {
        currentGun.EnemyShoot(missilePrefab, nozzle);
    }

    public override void DoDeath()
    {
       // LevelDesign.instance.DecreaseUnitCount(this);
        DropRocketLauncher();
        Destroy(gameObject);
    }

    private void DropRocketLauncher()
    {
        Instantiate(rocketLauncherPickUp, transform.position, Quaternion.identity);
    }
}
