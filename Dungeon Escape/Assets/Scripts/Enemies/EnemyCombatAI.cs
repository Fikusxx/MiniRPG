using UnityEngine;
using System;


public class EnemyCombatAI : MonoBehaviour
{
    #region References
    #endregion
    private EnemyAnimation enemyAnimation;
    private IDamagable damagable;
    private Player player;

    #region Combat Data
    [Space(10)]
    [Header("Combat Data")]
    #endregion
    #region Tooltip
    [Tooltip("Distance in Unity units between player and this object. Trigger combat if it's less than this number")]
    #endregion
    [SerializeField] private float combatDistanceTrigger = 5f;
    [SerializeField] private float attackRangeDistance = 1f;
    [SerializeField] private float attackCooldown = 0.5f;
    private float lastAttackTime;

    #region States
    #endregion
    private bool isAIEnabled = true;

    #region Events
    #endregion
    public Action OnWithinAttackRange;


    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        damagable = GetComponent<IDamagable>();
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        damagable.OnDeath += DisableAI;
    }

    private void OnDisable()
    {
        damagable.OnDeath -= DisableAI;
    }

    private void Update()
    {
        if (isAIEnabled == true)
        {
            IsInCombat();
            UpdateCombatStatus();
        }
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

        if ((distance < attackRangeDistance) && IsAttackOffCooldown())
        {
            lastAttackTime = Time.time;
            OnWithinAttackRange?.Invoke();
        }
            
        return distance;
    }

    /// <summary>
    /// Check if the distance between player and this object is less than combatDistanceTrigger.
    /// Returns true if it is.
    /// </summary>
    public bool IsInCombat()
    {
        return GetPlayerDistance() < combatDistanceTrigger;
    }

    /// <summary>
    /// Returns true if there's been more time than attackCooldown value is since last attack
    /// </summary>
    private bool IsAttackOffCooldown()
    {
        return Time.time - lastAttackTime > attackCooldown;
    }

    /// <summary>
    /// Check to see if we're in combat and set combat status accordingly
    /// </summary>
    private void UpdateCombatStatus()
    {
        if (IsInCombat() == true)
            FaceThePlayer();

        SetCombatStatus(IsInCombat());
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
}
