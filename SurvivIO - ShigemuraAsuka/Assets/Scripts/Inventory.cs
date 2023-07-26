using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int pistolAmmoMaxCarry;
    [SerializeField] private int automaticRifleAmmoMaxCarry;
    [SerializeField] private int shotgunAmmoMaxCarry;
    [SerializeField] private List<GameObject> weapons;
    public Player player;
    public List<Gun> gunTypes;

    private int pistolAmmoCarry;
    private int automaticRifleAmmoCarry;
    private int shotgunAmmoCarry;
    private int healthKits;
    private Weapon weapon1;
    private Weapon weapon2;
    public Gun primaryWeapon;
    public Gun secondaryWeapon;
    public bool primaryWeaponSelected;

    public int currentPistolMagazineAmmo;
    public int currentPistolBagAmmo;
    public int currentAutomaticRifleMagazineAmmo;
    public int currentAutomaticRifleBagAmmo;
    public int currentShotgunMagazineAmmo;
    public int currentShotgunBagAmmo;
    public int currentRocketLauncherMagazineAmmo;
    public int currentRocketLauncherBagAmmo;

    private void Start()
    {
        pistolAmmoCarry = 0;
        automaticRifleAmmoCarry = 0;
        shotgunAmmoCarry = 0;
        primaryWeaponSelected = true;

        currentPistolMagazineAmmo = 0;
        currentAutomaticRifleMagazineAmmo = 0;
        currentShotgunMagazineAmmo = 0;
        currentRocketLauncherMagazineAmmo = 0;
        currentRocketLauncherBagAmmo = 10;
    }

    public void ChangeWeapon(int weaponSlot)
    {
        if (weaponSlot == (int)WeaponSlot.Primary && primaryWeapon != null && player.isReloading == false)
        {
            HideWeapons();
            player.SetCurrentGun(primaryWeapon);
            ShowWeapon(weapon1);
            if (weapon1 == Weapon.AutomaticRifle)
            {
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentAutomaticRifleMagazineAmmo, currentAutomaticRifleBagAmmo);
            }
            else if (weapon1 == Weapon.Shotgun)
            {
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentShotgunMagazineAmmo, currentShotgunBagAmmo);
            }
            else if (weapon1 == Weapon.RocketLauncher)
            {
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentRocketLauncherMagazineAmmo, currentRocketLauncherBagAmmo);
            }
            primaryWeaponSelected = true;
        }
        else if (weaponSlot == (int)WeaponSlot.Secondary && secondaryWeapon != null && player.isReloading == false)
        {
            HideWeapons();
            player.SetCurrentGun(secondaryWeapon);
            ShowWeapon(weapon2);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentPistolMagazineAmmo, currentPistolBagAmmo);
            primaryWeaponSelected = false;
        }
    }

    public IEnumerator ReloadCurrentGun()
    {
        player.isReloading = true;
        yield return new WaitForSeconds(player.currentGun.reloadTime);

        if (player.currentGun == gunTypes[(int)Weapon.Pistol])
        {
            currentPistolMagazineAmmo = Mathf.Min(15, currentPistolMagazineAmmo + currentPistolBagAmmo);
            currentPistolBagAmmo = Mathf.Max(0, pistolAmmoCarry - currentPistolMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentPistolMagazineAmmo, currentPistolBagAmmo);
        }
        else if (player.currentGun == gunTypes[(int)Weapon.AutomaticRifle])
        {
            currentAutomaticRifleMagazineAmmo = Mathf.Min(30, currentAutomaticRifleMagazineAmmo + currentAutomaticRifleBagAmmo);
            currentAutomaticRifleBagAmmo = Mathf.Max(0, automaticRifleAmmoCarry - currentAutomaticRifleMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentAutomaticRifleMagazineAmmo, currentAutomaticRifleBagAmmo);
        }
        else if (player.currentGun == gunTypes[(int)Weapon.Shotgun])
        {
            currentShotgunMagazineAmmo = Mathf.Min(2, currentShotgunMagazineAmmo + currentShotgunBagAmmo);
            currentShotgunBagAmmo = Mathf.Max(0, shotgunAmmoCarry - currentShotgunMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentShotgunMagazineAmmo, currentShotgunBagAmmo);
        }
        else if (player.currentGun == gunTypes[(int)Weapon.RocketLauncher])
        {
            currentRocketLauncherMagazineAmmo = Mathf.Min(1, currentRocketLauncherBagAmmo);
            currentRocketLauncherBagAmmo = Mathf.Max(0, currentRocketLauncherBagAmmo - 1);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentRocketLauncherMagazineAmmo, currentRocketLauncherBagAmmo);
        }

        player.isReloading = false;
        UIManager.instance._reloadingText.enabled = false;
    }

    public void UseHealthKit()
    {
        // Implement health kit usage logic
    }

    public void AddAmmo(Weapon weapon)
    {
        if (weapon == Weapon.Pistol && pistolAmmoCarry < pistolAmmoMaxCarry)
        {
            int ammoAmount = Random.Range(1, 9);
            pistolAmmoCarry += ammoAmount;
            pistolAmmoCarry = Mathf.Min(pistolAmmoCarry, pistolAmmoMaxCarry);
            currentPistolBagAmmo = Mathf.Max(0, pistolAmmoCarry - currentPistolMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, pistolAmmoCarry, currentPistolBagAmmo, currentPistolMagazineAmmo);
        }
        else if (weapon == Weapon.AutomaticRifle && automaticRifleAmmoCarry < automaticRifleAmmoMaxCarry)
        {
            int ammoAmount = Random.Range(5, 16);
            automaticRifleAmmoCarry += ammoAmount;
            automaticRifleAmmoCarry = Mathf.Min(automaticRifleAmmoCarry, automaticRifleAmmoMaxCarry);
            currentAutomaticRifleBagAmmo = Mathf.Max(0, automaticRifleAmmoCarry - currentAutomaticRifleMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, automaticRifleAmmoCarry, currentAutomaticRifleBagAmmo, currentAutomaticRifleMagazineAmmo);
        }
        else if (weapon == Weapon.Shotgun && shotgunAmmoCarry < shotgunAmmoMaxCarry)
        {
            int ammoAmount = Random.Range(1, 3);
            shotgunAmmoCarry += ammoAmount;
            shotgunAmmoCarry = Mathf.Min(shotgunAmmoCarry, shotgunAmmoMaxCarry);
            currentShotgunBagAmmo = Mathf.Max(0, shotgunAmmoCarry - currentShotgunMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, shotgunAmmoCarry, currentShotgunBagAmmo, currentShotgunMagazineAmmo);
        }
    }

    public void LootWeapon(Weapon weapon)
    {
        if (weapon == Weapon.Pistol)
        {
            secondaryWeapon = gunTypes[(int)weapon];
            weapon2 = weapon;

            if (!primaryWeaponSelected)
            {
                ShowWeapon(weapon);  // Show pistol
                player.SetCurrentGun(secondaryWeapon);
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentPistolMagazineAmmo, currentPistolBagAmmo);
            }
        }
        else if (weapon == Weapon.AutomaticRifle && primaryWeapon != gunTypes[(int)weapon])
        {
            primaryWeapon = gunTypes[(int)weapon];
            weapon1 = weapon;

            if (primaryWeaponSelected)
            {
                ShowWeapon(weapon);  // Show automatic rifle
                player.SetCurrentGun(primaryWeapon);
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentAutomaticRifleMagazineAmmo, currentAutomaticRifleBagAmmo);
            }
        }
        else if (weapon == Weapon.Shotgun && primaryWeapon != gunTypes[(int)weapon])
        {
            primaryWeapon = gunTypes[(int)weapon];
            weapon1 = weapon;

            if (primaryWeaponSelected)
            {
                ShowWeapon(weapon);  // Show shotgun
                player.SetCurrentGun(primaryWeapon);
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentShotgunMagazineAmmo, currentShotgunBagAmmo);
            }
        }
        else if (weapon == Weapon.RocketLauncher && primaryWeapon != gunTypes[(int)weapon])
        {
            primaryWeapon = gunTypes[(int)weapon];
            weapon1 = weapon;

            if (primaryWeaponSelected)
            {
                ShowWeapon(weapon);  // Show rocket launcher
                player.SetCurrentGun(primaryWeapon);
                UIManager.instance.UpdateCurrentWeaponAmmoCount(currentRocketLauncherMagazineAmmo, currentRocketLauncherBagAmmo);
            }
        }
        UIManager.instance.UpdateWeaponSlotText(weapon);
    }

    public void ShowWeapon(Weapon weapon)
    {
        HideWeapons();
        weapons[(int)weapon].SetActive(true);
    }

    public void HideWeapons()
    {
        foreach (GameObject weaponGO in weapons)
        {
            weaponGO.SetActive(false);
        }
    }

    public void ExpendAmmo()
    {
        if (!primaryWeaponSelected && secondaryWeapon == gunTypes[(int)Weapon.Pistol] &&
            currentPistolMagazineAmmo > 0)
        {
            pistolAmmoCarry--;
            currentPistolMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.Pistol, pistolAmmoCarry, currentPistolBagAmmo, currentPistolMagazineAmmo);
        }

        if (primaryWeaponSelected && primaryWeapon == gunTypes[(int)Weapon.AutomaticRifle] &&
            currentAutomaticRifleMagazineAmmo > 0)
        {
            automaticRifleAmmoCarry--;
            currentAutomaticRifleMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.AutomaticRifle, automaticRifleAmmoCarry, currentAutomaticRifleBagAmmo, currentAutomaticRifleMagazineAmmo);
        }
        else if (primaryWeaponSelected && primaryWeapon == gunTypes[(int)Weapon.Shotgun] &&
            currentShotgunMagazineAmmo > 0)
        {
            shotgunAmmoCarry--;
            currentShotgunMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.Shotgun, shotgunAmmoCarry, currentShotgunBagAmmo, currentShotgunMagazineAmmo);
        }
        else if (primaryWeaponSelected && primaryWeapon == gunTypes[(int)Weapon.RocketLauncher] &&
            currentRocketLauncherMagazineAmmo > 0)
        {
            currentRocketLauncherMagazineAmmo--;
            UIManager.instance.UpdateCurrentWeaponAmmoCount(currentRocketLauncherMagazineAmmo, currentRocketLauncherBagAmmo);
        }
    }
}
