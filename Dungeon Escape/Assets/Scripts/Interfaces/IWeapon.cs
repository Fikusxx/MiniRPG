using System;

public interface IWeapon
{
    public int Damage { get; }
    public float AttackCooldown { get; }
    public void Init(int damage, float attackCooldown);
}

public interface IEnemyWeapon
{
    public int Damage { get; }
    public float AttackCooldown { get; }
    public float AttackRangeDistance { get; }
    public void Init(int damage, float attackCooldown, float attackRangeDistance);

}