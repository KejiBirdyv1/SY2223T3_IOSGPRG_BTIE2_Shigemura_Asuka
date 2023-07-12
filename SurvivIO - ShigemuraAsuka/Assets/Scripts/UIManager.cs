using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int minimum;
    [SerializeField] private int maximum;
    [SerializeField] private int current;
    [SerializeField] private Image Mask;

    [SerializeField] private Button button;

    [SerializeField] private Player player;

    [SerializeField] private Sprite[] gunImages;
    [SerializeField] private Image primaryButton;
    [SerializeField] private Image secondaryButton;

    [SerializeField] private TextMeshProUGUI clipAmmo;
    [SerializeField] private TextMeshProUGUI totalAmmo;

    [SerializeField] private GameObject reloadText;
    [SerializeField] private Slider hpSlider;

    private Health health;

    private void Start()
    {
         health = player.GetComponent<Health>();
    }

    public void SetAlpha()
    {
        Color col = button.GetComponent<Image>().color;
        col.a = 0.25f;
        button.GetComponent<Image>().color = col;
    }

    private void Update()
    {
        hpSlider.value = (float)health.CurrentHealth / (float)health.MaxHealth;

        // GetCurrentFill();
        if (player._hasPrimary && player.onPrimaryWep)
        {
            clipAmmo.text = player._primaryGun.GetComponent<Gun>().currentClip.ToString();
            totalAmmo.text = player._primaryGun.GetComponent<Gun>().currentAmmo.ToString();

            reloadText.SetActive(player._primaryGun.GetComponent<Gun>().isReloading);
        }
        else if (player._hasSecondary && !player.onPrimaryWep)
        {
            clipAmmo.text = player._secondaryGun.GetComponent<Gun>().currentClip.ToString();
            totalAmmo.text = player._secondaryGun.GetComponent<Gun>().currentAmmo.ToString();

            reloadText.SetActive(player._secondaryGun.GetComponent<Gun>().isReloading);
        }
    }

    public void GunCheck()
    {
        if (player._primaryGun != null)
        {
            if (player._primaryGun.GetComponent<Gun>().gunType == GunType.AutomaticRifle)
            {
                primaryButton.sprite = gunImages[0];
            }
            else if (player._primaryGun.GetComponent<Gun>().gunType == GunType.Shotgun)
            {
                primaryButton.sprite = gunImages[1];
            }
        }

        if (player._secondaryGun != null)
        {
            if (player._secondaryGun.GetComponent<Gun>().gunType == GunType.Pistol)
            {
                secondaryButton.sprite = gunImages[2];
            }
        }
    }

    // private void GetCurrentFill()
    // {
    //     current = player.health;
    //     float fillAmount = (float)current / (float)maximum;
    //     Mask.fillAmount = fillAmount;
    //     if (current > maximum)
    //     {
    //         current = maximum;
    //     }
    //     if (current < 0)
    //     {
    //         current = 0;
    //     }
    // }
}
