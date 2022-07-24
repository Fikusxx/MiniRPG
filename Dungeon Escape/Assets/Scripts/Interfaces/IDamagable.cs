using System;


public interface IDamagable
{
    public int Health { get; }
    public void TakeDamage(int damage);
    public event Action OnTakeDamage;
    public event Action OnDeath;
}