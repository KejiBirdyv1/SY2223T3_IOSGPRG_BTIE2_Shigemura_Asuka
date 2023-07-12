using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void Start()
    {
        HealthBar();
        ManageHealth();
    }
}
