using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
   
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI Remaining;

    [Header("Ammunition Text")]
    [SerializeField] private TextMeshProUGUI pistolAmmoCountText;
    [SerializeField] private TextMeshProUGUI automaticRifleAmmoCountText;
    [SerializeField] private TextMeshProUGUI shotgunAmmoCountText;

    [Header("Weapons Text")]
    [SerializeField] private TextMeshProUGUI primaryWeaponText;
    [SerializeField] private TextMeshProUGUI secondaryWeaponText;

    [Header("Magazine Text")]
    [SerializeField] private TextMeshProUGUI magazineAmmoCountText;
    [SerializeField] private TextMeshProUGUI totalAmmoCountText;

    public GameObject HUD;
    public GameObject gameOverScreen;
    public TextMeshProUGUI reloadingText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        primaryWeaponText.SetText("No Weapon");
        secondaryWeaponText.SetText("No Weapon");
        reloadingText.enabled = false;
    }

    public void UpdateAmmoCount(Weapon weapon, int totalAmmoCount, int bagAmmoCount, int magazineAmmoCount)
    {
        switch (weapon)
        {
            case Weapon.Pistol:
                pistolAmmoCountText.SetText(totalAmmoCount.ToString());
                if (!inventory.primaryWeaponSelected &&
                    inventory.secondaryWeapon == inventory.gunTypes[(int)Weapon.Pistol])
                {
                    if (magazineAmmoCount <= 0 && inventory.player.currentGun ==
                        inventory.gunTypes[(int)Weapon.Pistol])
                    {
                        inventory.StartCoroutine("ReloadCurrentGun");
                        reloadingText.enabled = true;
                    }
                    magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                    totalAmmoCountText.SetText(bagAmmoCount.ToString());
                }
                break;
            case Weapon.AutomaticRifle:
                automaticRifleAmmoCountText.SetText(totalAmmoCount.ToString());
                if (inventory.primaryWeaponSelected &&
                    inventory.primaryWeapon == inventory.gunTypes[(int)Weapon.AutomaticRifle])
                {
                    if (magazineAmmoCount <= 0 && inventory.player.currentGun ==
                        inventory.gunTypes[(int)Weapon.AutomaticRifle])
                    {
                        inventory.StartCoroutine("ReloadCurrentGun");
                        reloadingText.enabled = true;
                    }
                    magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                    totalAmmoCountText.SetText(bagAmmoCount.ToString());
                }
                break;
            case Weapon.Shotgun:
                shotgunAmmoCountText.SetText(totalAmmoCount.ToString());
                if (inventory.primaryWeaponSelected &&
                    inventory.primaryWeapon == inventory.gunTypes[(int)Weapon.Shotgun])
                {
                    if (magazineAmmoCount <= 0 && inventory.player.currentGun ==
                        inventory.gunTypes[(int)Weapon.Shotgun])
                    {
                        inventory.StartCoroutine("ReloadCurrentGun");
                        reloadingText.enabled = true;
                    }
                    magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
                    totalAmmoCountText.SetText(bagAmmoCount.ToString());
                }
                break;
        }
    }

    public void UpdateWeaponSlotText(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.Pistol:
                secondaryWeaponText.SetText("Pistol");
                break;
            case Weapon.AutomaticRifle:
                primaryWeaponText.SetText("Automatic Rifle");
                break;
            case Weapon.Shotgun:
                primaryWeaponText.SetText("Shotgun");
                break;
            case Weapon.RocketLauncher:
                primaryWeaponText.SetText("Rocket Launcher");
                break;
        }
    }

    public void UpdateCurrentWeaponAmmoCount(int magazineAmmoCount, int bagAmmoCount)
    {
        if (magazineAmmoCount <= 0)
        {
            inventory.StartCoroutine("ReloadCurrentGun");
            magazineAmmoCountText.SetText("0");
            totalAmmoCountText.SetText(bagAmmoCount.ToString());
            reloadingText.enabled = true;
        }
        else
        {
            magazineAmmoCountText.SetText(magazineAmmoCount.ToString());
            totalAmmoCountText.SetText(bagAmmoCount.ToString());
        }
    }

    public void UpdateRemaining(int amount)
    {
        Remaining.SetText(amount.ToString());
    }
}
