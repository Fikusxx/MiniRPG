using UnityEngine;


public class Spider : Enemy
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
