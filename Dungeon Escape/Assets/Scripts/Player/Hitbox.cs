using UnityEngine;


public class Hitbox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(5);
        }
    }
}
