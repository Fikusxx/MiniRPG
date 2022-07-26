using UnityEngine;


public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            shopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            shopUI.SetActive(false);
        }
    }
}
