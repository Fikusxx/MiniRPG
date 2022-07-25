using UnityEngine;


public class PlayerWeapon : MonoBehaviour, IWeapon
{
    #region Core Data
    [Space(10)]
    [Header("Core Data")]
    #endregion
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;

    #region Properties
    #endregion
    public int Damage { get => damage; }
    public float AttackCooldown { get => attackCooldown; }


    /// <summary>
    /// We dont have any InventoryManager with SO asset weapons, but this way we would be able to Init our weapon
    /// with properties according to equipped weapon. Or we could simply pass WeaponSO object if we had one.
    /// If we had bone animation - I'd add spriteRenderer aswell
    /// </summary>
    public void Init(int damage, float attackCooldown)
    {
        this.damage = damage;
        this.attackCooldown = attackCooldown;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
        }
    }
}
