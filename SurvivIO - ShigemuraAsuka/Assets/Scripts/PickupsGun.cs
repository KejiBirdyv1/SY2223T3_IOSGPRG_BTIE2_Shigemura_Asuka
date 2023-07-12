using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsGun : Pickups
{
    public GunType gunType;

    private UIManager hud;
    private Player player;

    private void Start()
    {
        hud = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit != null)
        {
            if (!unit._hasPrimary && (gunType == GunType.AutomaticRifle || gunType == GunType.Shotgun))
            {
                GameObject newGun = Instantiate(guns[(int)gunType], unit._handle.transform.position, Quaternion.identity, unit.transform);
                unit._hasPrimary = true;
                unit._primaryGun = newGun;
                newGun.transform.localEulerAngles = Vector3.zero;
                hud.GunCheck();
                player.swapToPrimary();
                base.OnTriggerEnter2D(collision);
            }

            if (!unit._hasSecondary && gunType == GunType.Pistol)
            {
                GameObject newGun = Instantiate(guns[(int)gunType], unit._handle.transform.position, Quaternion.identity, unit.transform);
                unit._hasSecondary = true;
                unit._secondaryGun = newGun;
                newGun.transform.localEulerAngles = Vector3.zero;
                hud.GunCheck();
                player.swapToPrimary();
                base.OnTriggerEnter2D(collision);
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
