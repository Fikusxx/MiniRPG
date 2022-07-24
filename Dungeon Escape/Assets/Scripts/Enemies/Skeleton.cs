using System;
using UnityEngine;


public class Skeleton : Enemy, IDamagable
{
    #region Core Data
    #endregion
    public int Health { get => currentHealth; }

    #region References
    #endregion
    // Placeholder for now

    #region Events
    #endregion
    public event Action OnTakeDamage;
    public event Action OnDeath;

    #region Extra Data
    [Space(10)]
    [Header("Extra Data")]
    #endregion
    private float lastTimeAttacked;
    [SerializeField] private float immunityFrame;



    /// <summary>
    /// If there's enough time passed since last time we got hit - get hit again.
    /// </summary>
    public void TakeDamage(int damage)
    {
        // if we're already at 0 hp - do nothing
        if (currentHealth <= 0)
            return;

        // get hit only only every immunityFrame seconds
        if (Time.time - lastTimeAttacked < immunityFrame)
            return;

        currentHealth -= 1;
        lastTimeAttacked = Time.time;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
            return;
        }

        // if we didnt die after getting hit
        OnTakeDamage?.Invoke();
    }
}
