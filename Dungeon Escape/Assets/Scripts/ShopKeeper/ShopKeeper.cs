using UnityEngine;


public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            shopUI.SetActive(true);
            player.SetCanAttack(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            shopUI.SetActive(false);
            player.SetCanAttack(true);
            
        }
    }
}
