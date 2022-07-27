using UnityEngine;
using UnityEngine.UI;

public class CollectablesUI : MonoBehaviour
{
    #region Texts
    [Space(5)]
    [Header("Texts")]
    #endregion
    [SerializeField] private Text diamondsText;


    private void OnEnable()
    {
        Collectables.Instance.OnDiamondsChanged += UpdateDiamondsCount;
    }

    private void OnDisable()
    {
        Collectables.Instance.OnDiamondsChanged -= UpdateDiamondsCount;
    }


    /// <summary>
    /// Update diamonds text count
    /// </summary>
    private void UpdateDiamondsCount(int diamondsCount)
    {
        diamondsText.text = diamondsCount.ToString();
    }
}
