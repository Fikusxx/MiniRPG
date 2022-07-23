using UnityEngine;


public class EnemyAnimation : MonoBehaviour
{
    #region References
    #endregion
    private Animator animator;
    private EnemyAI enemyAI;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void OnEnable()
    {
        enemyAI.OnWaypointChange += PlayIdleAnimation;
    }

    private void OnDisable()
    {
        enemyAI.OnWaypointChange -= PlayIdleAnimation;
    }


    /// <summary>
    /// Play Idle animation
    /// </summary>
    public void PlayIdleAnimation()
    {
        animator.SetTrigger(EnemyAnimationTypes.Idle.ToString());
    }

    /// <summary>
    /// Return true if specific animation is currently playing
    /// </summary>
    public bool IsSpecificAnimationPlaying(EnemyAnimationTypes animationType)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationType.ToString());
    }
}
