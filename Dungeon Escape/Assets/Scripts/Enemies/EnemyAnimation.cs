using UnityEngine;
using System;
using System.Collections;


public class EnemyAnimation : MonoBehaviour
{
    #region References
    #endregion
    private Animator animator;
    private EnemyMovementAI enemyMovementAI;
    private EnemyCombatAI enemyCombatAI;
    private IDamagable enemy;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        enemyCombatAI = GetComponent<EnemyCombatAI>();
        enemy = GetComponent<IDamagable>();
    }

    private void Start()
    {
        PlayWalkAnimation();
    }

    private void OnEnable()
    {
        enemyMovementAI.OnWaypointChange += PlayIdleAnimation;
        enemyCombatAI.OnWithinAttackRange += PlayAttackAnimation;

        enemy.OnTakeDamage += PlayHitAnimation;
        enemy.OnDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        enemyMovementAI.OnWaypointChange -= PlayIdleAnimation;
        enemyCombatAI.OnWithinAttackRange -= PlayAttackAnimation;

        enemy.OnTakeDamage -= PlayHitAnimation;
        enemy.OnDeath -= PlayDeathAnimation;
    }


    /// <summary>
    /// Play Idle animation
    /// </summary>
    private void PlayIdleAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Idle.ToString());
    }

    /// <summary>
    ///  Play Walk animation
    /// </summary>
    private void PlayWalkAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Walk.ToString());
    }

    /// <summary>
    /// Play Attack Animation
    /// </summary>
    private void PlayAttackAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Attack.ToString());
    }

    /// <summary>
    /// Play Hit animation when get hit
    /// </summary>
    private void PlayHitAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Hit.ToString());
    }

    /// <summary>
    ///  Play Death animation
    /// </summary>
    private void PlayDeathAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Death.ToString());

    }

    /// <summary>
    /// Return true if specific animation is currently playing
    /// </summary>
    public bool IsAnimationPlaying(EnemyAnimationTypes animationType)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationType.ToString());
    }

    /// <summary>
    /// Set InCombat parameter for the animator
    /// </summary>
    public void SetInCombatStatus(bool value)
    {
        // This is primarily for testing purpose right now
        animator.SetBool(EnemyAnimationTypes.InCombat.ToString(), value);

        // Atm we always walk
        if (value == true)
            PlayWalkAnimation();

        if (value == false)
            PlayWalkAnimation();
    }
}
