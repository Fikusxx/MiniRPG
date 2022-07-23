using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class EnemyAI : MonoBehaviour
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

    #region Events
    #endregion
    public event Action OnWaypointChange;



    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        StartCoroutine(UpdateWaypointsListYAxis());
    }


    /// <summary>
    /// Move between waypoints with moveSpped
    /// </summary>
    public void Move(float moveSpeed)
    {
        // if Idle animation is currently playing - do nothing
        if (enemyAnimation.IsSpecificAnimationPlaying(EnemyAnimationTypes.Idle) 
            || enemyAnimation.IsSpecificAnimationPlaying(EnemyAnimationTypes.Hit))
        {
            return;
        }

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
}
