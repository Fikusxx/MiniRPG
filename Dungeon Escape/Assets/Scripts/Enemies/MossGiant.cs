using UnityEngine;


public class MossGiant : Enemy
{
    #region References
    #endregion
    private EnemyAI enemyAI;


    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        enemyAI.Move(moveSpeed);
    }
}
