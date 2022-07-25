using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour, IDamagable
{
    #region Core Data
    #endregion
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    #region Properties
    #endregion
    public int Health { get => currentHealth; }

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


    private void Start()
    {
        currentHealth = maxHealth;
    }


    /// <summary>
    /// If there's enough time passed since last time we got hit - get hit again.
    /// </summary>
    public void TakeDamage(int damage)
    {
        // if we're already at 0 hp - do nothing
        if (currentHealth <= 0)
            return;

        // get hit only only every immunityFrame seconds
        if (CanBeDamaged() == false)
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

    /// <summary>
    /// Return true is theere's been enough time since we got hit last time
    /// </summary>
    private bool CanBeDamaged()
    {
        return Time.time - lastTimeAttacked > immunityFrame;
    }
}
