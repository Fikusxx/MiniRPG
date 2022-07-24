using UnityEngine;


public class MossGiant : Enemy
{
    #region References
    #endregion
    private EnemyMovementAI enemyAI;


    private void Awake()
    {
        enemyAI = GetComponent<EnemyMovementAI>();
    }

    private void Update()
    {
        enemyAI.MoveTowardsWaypoints(moveSpeed);
    }
}
