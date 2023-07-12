using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    Shotgun,
    AutomaticRifle
}

public enum AmmoType
{
    PistolAmmo,
    ShotgunAmmo,
    RifleAmmo
}

public class PickupsAmmo : Pickups
{
    public AmmoType ammoType;
    public int ammoToAdd;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit != null)
        {
            Gun _primaryGun = unit._primaryGun.GetComponent<Gun>();
            if (unit._hasPrimary && (int)_primaryGun.gunType == (int)ammoType)
            {
                _primaryGun.currentAmmo += ammoToAdd;
                base.OnTriggerEnter2D(collision);
            }

            Gun _secondaryGun = unit._secondaryGun.GetComponent<Gun>();
            if (unit._hasSecondary && (int)_secondaryGun.gunType == (int)ammoType)
            {
                _secondaryGun.currentAmmo += ammoToAdd;
                base.OnTriggerEnter2D(collision);
            }
        }

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
