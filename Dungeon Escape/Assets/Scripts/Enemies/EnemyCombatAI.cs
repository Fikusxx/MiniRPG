using UnityEngine;
using System;


public class EnemyCombatAI : MonoBehaviour
{
    #region References
    #endregion
    private EnemyAnimation enemyAnimation;
    private IDamagable myHealth;
    private IEnemyWeapon myWeapon;
    private Player player;
    private IDamagable playerHealth;

    #region Combat Data
    [Space(10)]
    [Header("Combat Data")]
    #endregion
    #region Tooltip
    [Tooltip("Distance in Unity units between player and this object. Trigger combat if it's less than this number")]
    #endregion
    [SerializeField] private float combatDistanceTrigger = 5f;
    private float lastAttackTime;

    #region States
    #endregion
    private bool isAIEnabled = true;
    private bool inCombat = false;
    private bool isTargetDead = false;

    #region Events
    #endregion
    public Action OnWithinAttackRange;


    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        myWeapon = GetComponentInChildren<IEnemyWeapon>();
        myHealth = GetComponent<IDamagable>();
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<Player>().GetComponent<IDamagable>();
    }

    private void OnEnable()
    {
        myHealth.OnDeath += DisableAI;
        playerHealth.OnDeath += SetTargetAsDead;
    }

    private void OnDisable()
    {
        myHealth.OnDeath -= DisableAI;
        playerHealth.OnDeath -= SetTargetAsDead;
    }

    private void Update()
    {
        if (isAIEnabled == false)
            return;

        UpdateCombatStatus();
    }


    /// <summary>
    /// Set InCombat status as a value
    /// </summary>
    private void SetCombatStatus(bool value)
    {
        enemyAnimation.SetInCombatStatus(value);
    }

    /// <summary>
    /// Return the distance between the player and this object
    /// </summary>
    private float GetPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance;
    }

    /// <summary>
    /// Check if the distance between player and this object is less than combatDistanceTrigger.
    /// Returns true if it is.
    /// </summary>
    public bool IsInCombat()
    {
        if (isTargetDead == true)
        {
            inCombat = false;
            return inCombat;
        }

        inCombat = GetPlayerDistance() < combatDistanceTrigger;
        return inCombat;
    }

    /// <summary>
    /// Returns true if there's been more time than attackCooldown value is since last attack
    /// </summary>
    private bool IsAttackOffCooldown()
    {
        return Time.time - lastAttackTime > myWeapon.AttackCooldown;
    }

    /// <summary>
    /// Attack the player if he's in weapon's reach and weapon is off cooldown
    /// </summary>
    private void TryAttackThePlayer()
    {
        if ((GetPlayerDistance() < myWeapon.AttackRangeDistance) && IsAttackOffCooldown())
        {
            lastAttackTime = Time.time;
            OnWithinAttackRange?.Invoke();
        }
    }

    /// <summary>
    /// Check to see if we're in combat and set combat status accordingly
    /// </summary>
    private void UpdateCombatStatus()
    {
        IsInCombat();

        if (inCombat == true)
        {
            FaceThePlayer();
            TryAttackThePlayer();
        }

        SetCombatStatus(inCombat);
    }

    /// <summary>
    /// Change local scale, so this object faces the player, when inCombat == true
    /// </summary>
    private void FaceThePlayer()
    {
        Vector3 distance = player.transform.position - transform.position;

        if (distance.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (distance.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    /// <summary>
    /// Disable AI 
    /// </summary>
    private void DisableAI()
    {
        isAIEnabled = false;
    }

    /// <summary>
    /// Set player target as dead
    /// </summary>
    private void SetTargetAsDead()
    {
        isTargetDead = true;
    }
}
