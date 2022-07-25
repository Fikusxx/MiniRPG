using System;
using UnityEngine;


public class Skeleton : Enemy
{
    #region References
    #endregion
    private HealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        healthSystem.OnDeath += SpawnDiamond;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= SpawnDiamond;
    }
}
