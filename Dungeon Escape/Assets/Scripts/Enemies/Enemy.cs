using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Enemy Core Data
    [Space(10)]
    [Header("Enemy Core Data")]
    #endregion
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int gemsDrop;

    #region Properties
    #endregion
    public float MoveSpeed { get => moveSpeed; }


    protected virtual void Attack() { }

    /// <summary>
    /// Spawn diamond upon certian event
    /// </summary>
    protected virtual void SpawnDiamond()
    {
        CollectablesController.Instance.SpawnDiamond(gemsDrop, transform.position);
    }
}
