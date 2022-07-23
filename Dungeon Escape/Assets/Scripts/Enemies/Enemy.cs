using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Enemy Core Data
    [Space(10)]
    [Header("Enemy Core Data")]
    #endregion
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int gemsDrop;


    protected virtual void Attack() { }
}
