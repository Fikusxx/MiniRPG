using System;
using UnityEngine;


public class Skeleton : Enemy, IDamagable
{
    #region References
    #endregion
    private EnemyAI enemyAI;

    #region Events
    #endregion
    public event Action OnTakeDamage;

    #region Extra Data
    [Space(10)]
    [Header("Extra Data")]
    #endregion
    private float lastTimeAttacked;
    [SerializeField] private float immunityFrame;


    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        enemyAI.Move(moveSpeed);
    }


    /// <summary>
    /// If there's enough time passed since last time we got hit - get hit again.
    /// Invoke an event OnTakeDamage
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (Time.time - lastTimeAttacked < immunityFrame)
            return;

        lastTimeAttacked = Time.time;
        OnTakeDamage?.Invoke();
    }
}
