using System;


public interface IDamagable
{
    public void TakeDamage(int damage);
    public event Action OnTakeDamage;
}