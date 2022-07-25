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
}
