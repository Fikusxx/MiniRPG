using UnityEngine;
using UnityEngine.UI;
using System;

[DisallowMultipleComponent]
public class ShopManager : MonoBehaviour
{
    #region Singleton
    #endregion
    public static ShopManager Instance { get; private set; }

    #region References
    [Space(5)]
    [Header("References")]
    #endregion
    [SerializeField] private Text diamondsText;
    [SerializeField] private RectTransform selectionOverlay;

    #region States
    #endregion
    private Vector3 lastClickPosition;

    #region Events
    #endregion
    public event Action<int> OnItemPurchase;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Collectables.Instance.OnDiamondsChanged += UpdateDiamondsText;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        Collectables.Instance.OnDiamondsChanged -= UpdateDiamondsText;
    }

    /// <summary>
    /// Update diamonds count
    /// </summary>
    private void UpdateDiamondsText(int diamondsAmount)
    {
        diamondsText.text = $"{diamondsAmount}G";
    }

    /// <summary>
    /// Select the item, activate and place overlay over it. If the item is already selected - deactivate the overlay
    /// </summary>
    public void SelectItem(RectTransform button)
    {
        if (lastClickPosition == button.position && selectionOverlay.gameObject.activeSelf == true)
        {
            selectionOverlay.gameObject.SetActive(false);
            return;
        }

        selectionOverlay.gameObject.SetActive(true);
        lastClickPosition = button.position;
        selectionOverlay.position = new Vector2(selectionOverlay.position.x, button.position.y);
    }

    /// <summary>
    /// Buy an item
    /// </summary>
    public void BuyItem(int itemPrice)
    {
        if (Collectables.Instance.Diamonds >= itemPrice)
            OnItemPurchase?.Invoke(itemPrice);

        GameManager.Instance.GetKeyToCastle();
    }
}
