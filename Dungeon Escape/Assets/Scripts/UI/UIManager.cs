using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIManager : MonoBehaviour
{
    #region Singleton
    #endregion
    public static UIManager Instance { get; private set; }

    #region References
    [Space(5)]
    [Header("References")]
    #endregion
    [SerializeField] private Text diamondsText;
    [SerializeField] private RectTransform selectionOverlay;

    #region States
    #endregion
    private Vector3 lastClickPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CollectablesController.Instance.OnDiamondsAdded += UpdateDiamondsText;
    }

    private void OnDisable()
    {
        CollectablesController.Instance.OnDiamondsAdded -= UpdateDiamondsText;
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
    public void BuyItem()
    {

    }
}
