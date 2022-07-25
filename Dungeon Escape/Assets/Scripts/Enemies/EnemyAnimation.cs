using UnityEngine;


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
        animator.SetTrigger(AnimationType.Idle.ToString());
    }

    /// <summary>
    ///  Play Walk animation
    /// </summary>
    private void PlayWalkAnimation()
    {
        animator.SetTrigger(AnimationType.Walk.ToString());
    }

    /// <summary>
    /// Play Attack Animation
    /// </summary>
    private void PlayAttackAnimation()
    {
        animator.SetTrigger(AnimationType.Attack.ToString());
    }

    /// <summary>
    /// Play Hit animation when get hit
    /// </summary>
    private void PlayHitAnimation()
    {
        animator.SetTrigger(AnimationType.Hit.ToString());
    }

    /// <summary>
    ///  Play Death animation
    /// </summary>
    private void PlayDeathAnimation()
    {
        animator.SetTrigger(AnimationType.Death.ToString());

    }

    /// <summary>
    /// Return true if specific animation is currently playing
    /// </summary>
    public bool IsAnimationPlaying(AnimationType animationType)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationType.ToString());
    }

    /// <summary>
    /// Set InCombat parameter for the animator
    /// </summary>
    public void SetInCombatStatus(bool value)
    {
        // This is primarily for testing purpose right now
        animator.SetBool(AnimationType.InCombat.ToString(), value);

        // Atm we always walk
        if (value == true)
            PlayWalkAnimation();

        if (value == false)
            PlayWalkAnimation();
    }
}
