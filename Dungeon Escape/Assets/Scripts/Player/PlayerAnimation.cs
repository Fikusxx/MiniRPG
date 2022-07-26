using UnityEngine;
using System;
using System.Collections;


public class PlayerAnimation : MonoBehaviour
{
    #region References
    #endregion
    private Animator playerAnimator;
    private Animator swordAnimator;
    private IDamagable damagable;


    private void Awake()
    {
        damagable = GetComponent<IDamagable>();
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        swordAnimator = transform.GetChild(1).GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        damagable.OnTakeDamage += PlayHitAnimation;
        damagable.OnDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        damagable.OnTakeDamage -= PlayHitAnimation;
        damagable.OnDeath -= PlayDeathAnimation;
    }

    /// <summary>
    /// Process switch between Idle and Run animations depending on xInput
    /// </summary>
    public void ProcessHorizontalMovement(float xInput)
    {
        playerAnimator.SetFloat("xVelocity", Mathf.Abs(xInput));
    }

    /// <summary>
    /// Process jumping animation. If isGrounded = false, then we trigger jumping animation.
    /// If it's true - we change animation state 
    /// </summary>
    public void ProcessJumping(bool isGrounded)
    {
        playerAnimator.SetBool("IsGrounded", isGrounded);
    }

    /// <summary>
    /// Process Attack animation
    /// </summary>
    public void ProcessAttack()
    {
        playerAnimator.SetTrigger(AnimationType.Attack.ToString());
        swordAnimator.SetTrigger("Sword_Arc");
    }

    /// <summary>
    /// Play hit animation
    /// </summary>
    private void PlayHitAnimation()
    {
        playerAnimator.SetTrigger(AnimationType.Hit.ToString());
    }

    /// <summary>
    /// Play death animation
    /// </summary>
    private void PlayDeathAnimation()
    {
        playerAnimator.SetTrigger(AnimationType.Death.ToString());
    }

    /// <summary>
    /// Return currently playing animation clip length
    /// </summary>
    public float GetCurrentAnimationClipLength()
    {
        var currentClipInfo = playerAnimator.GetCurrentAnimatorClipInfo(0);

        // Get the current length of the clip
        var currentClipLength = currentClipInfo[0].clip.length;

        return currentClipLength;
    }

    /// <summary>
    /// Return true if specific animation is currently playing
    /// </summary>
    public bool IsSpecificAnimationPlaying(string name)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    /// <summary>
    /// Check for a specific animation clip to start playing and then call onComplete method
    /// </summary>
    public IEnumerator CheckAnimationCompleted(string currentAnim, Action onComplete)
    {
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnim) == false)
        {
            yield return null;
        }

        if (onComplete != null)
        {
            onComplete();
        }
    }
}
