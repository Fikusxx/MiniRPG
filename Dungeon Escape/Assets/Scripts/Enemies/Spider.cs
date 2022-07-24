using UnityEngine;


public class Spider : Enemy
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
