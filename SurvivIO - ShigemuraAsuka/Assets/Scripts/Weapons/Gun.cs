using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunType gunType;
    public GameObject nozzle;

    public float spread;

    public bool isReloading = false;

    public int currentClip;
    public int maxClip;
    public int currentAmmo;
    public int maxAmmo;


    public virtual void FiringTypePlayer(GameObject bullet)
    {
        // Debug.Log("Firing from Gun Base Class");
    }

    public virtual void StopFire(GameObject bullet)
    {
        // Debug.Log("Stopping from Gun Base Class");
    }

    public virtual void FiringTypeAI(GameObject bullet)
    {
        // Debug.Log("Firing from Gun Base Class");
    }

    public virtual void StopFiringAI()
    {

    }

    public virtual void Reload()
    {
        // Debug.Log("Reloading from Gun Base Class");
    }
}
