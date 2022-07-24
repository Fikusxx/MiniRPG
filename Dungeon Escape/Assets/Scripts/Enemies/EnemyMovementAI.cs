using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class EnemyMovementAI : MonoBehaviour
{
    #region Waypoints
    [Space(10)]
    [Header("Waypoints To Move Between")]
    #endregion
    [SerializeField] private List<Transform> waypointsList = new List<Transform>();
    private int waypointIndex = 0;
    private Vector3 targetPosition;

    #region Additional Data
    #endregion
    private float spriteDirection = 1f;

    #region References
    #endregion
    private EnemyAnimation enemyAnimation;
    private EnemyCombatAI enemyCombatAI;
    private Player player;
    private Enemy enemy;
    private IDamagable damagable;

    #region States
    #endregion
    private bool isAIEnabled = true;

    #region Events
    #endregion
    public event Action OnWaypointChange;


    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        damagable = GetComponent<IDamagable>();
        player = FindObjectOfType<Player>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyCombatAI = GetComponent<EnemyCombatAI>();
    }

    private void OnEnable()
    {
        damagable.OnDeath += DisableAI;
    }

    private void OnDisable()
    {
        damagable.OnDeath -= DisableAI;
    }

    private void Start()
    {
        StartCoroutine(UpdateWaypointsListYAxis());
    }

    private void Update()
    {
        if (isAIEnabled == false || IsMovementDisabled())
            return;

        if (enemyCombatAI.IsInCombat() == true)
            MoveTowardsPlayer(enemy.MoveSpeed);
        else
            MoveTowardsWaypoints(enemy.MoveSpeed);
    }


    /// <summary>
    /// Move between waypoints with moveSpped
    /// </summary>
    public void MoveTowardsWaypoints(float moveSpeed)
    {
        if (waypointIndex < waypointsList.Count)
        {
            targetPosition = waypointsList[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            FlipSprite();

            // Increment waypointIndex and trigger Idle animation when enemy reaches targetPosition
            if (transform.position == targetPosition)
            {
                waypointIndex++;
                OnWaypointChange?.Invoke();
            }
        }

        // If we get to the last point of the waypointsList - set index to 0
        if (waypointIndex == waypointsList.Count) waypointIndex = 0;
    }

    /// <summary>
    /// Move towards the player with Y axis being original, X axis is clamped between waypoints
    /// </summary>
    public void MoveTowardsPlayer(float moveSpeed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);

        float clampedX = Mathf.Clamp(newPosition.x, waypointsList[0].position.x, waypointsList[1].position.x);

        newPosition = new Vector2(clampedX, transform.position.y);

        transform.position = newPosition;
    }

    /// <summary>
    /// Change localScale according to the current direction
    /// </summary>
    private void FlipSprite()
    {
        Vector3 distance = waypointsList[waypointIndex].transform.position - transform.position;

        if (distance.x > 0)
        {
            spriteDirection = 1f;
            transform.localScale = new Vector2(spriteDirection, 1f);
        }
        else if (distance.x < 0)
        {
            spriteDirection = -1f;
            transform.localScale = new Vector2(spriteDirection, 1f);
        }
        else
        {
            transform.localScale = new Vector2(spriteDirection, 1f);
        }
    }

    /// <summary>
    /// Being called at the start to align Y axis value of the Enemy with his waypoints.
    /// In case the Enemy / waypoints hasnt been properly set and their Y axis dont match
    /// </summary>
    private IEnumerator UpdateWaypointsListYAxis()
    {
        yield return new WaitForSeconds(0.3f);

        foreach (var waypoint in waypointsList)
        {
            waypoint.position = new Vector2(waypoint.position.x, transform.position.y);
        }
    }

    /// <summary>
    /// Disable movement if either of these specific animations is playing
    /// </summary>
    private bool IsMovementDisabled()
    {
        if (enemyAnimation.IsAnimationPlaying(EnemyAnimationTypes.Idle)
            || enemyAnimation.IsAnimationPlaying(EnemyAnimationTypes.Hit)
            || enemyAnimation.IsAnimationPlaying(EnemyAnimationTypes.Death)
            || enemyAnimation.IsAnimationPlaying(EnemyAnimationTypes.Attack))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Disable AI 
    /// </summary>
    private void DisableAI()
    {
        isAIEnabled = false;
    }
}
