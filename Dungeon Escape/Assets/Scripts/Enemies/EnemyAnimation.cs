using UnityEngine;


public class EnemyAnimation : MonoBehaviour
{
    #region References
    #endregion
    private Animator animator;
    private EnemyAI enemyAI;
    private IDamagable enemy;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        enemy = GetComponent<IDamagable>();
    }

    private void OnEnable()
    {
        enemyAI.OnWaypointChange += PlayIdleAnimation;
        enemy.OnTakeDamage += PlayHitAnimation;
    }

    private void OnDisable()
    {
        enemyAI.OnWaypointChange -= PlayIdleAnimation;
        enemy.OnTakeDamage -= PlayHitAnimation;
    }


    /// <summary>
    /// Play Idle animation
    /// </summary>
    public void PlayIdleAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Idle.ToString());
    }

    /// <summary>
    /// Play Hit animation when get hit
    /// </summary>
    public void PlayHitAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Hit.ToString());
    }

    /// <summary>
    /// Return true if specific animation is currently playing
    /// </summary>
    public bool IsSpecificAnimationPlaying(EnemyAnimationTypes animationType)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationType.ToString());
    }
}
