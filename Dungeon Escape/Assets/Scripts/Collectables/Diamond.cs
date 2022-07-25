using UnityEngine;
using System;


public class Diamond : MonoBehaviour
{
    #region Core Data
    [Space(5)]
    [Header("Core Data")]
    #endregion
    [SerializeField] private int diamondValue;

    #region Event
    #endregion
    public Action<int, Diamond> OnDiamondCollected;

    /// <summary>
    /// Init the diamond with a value
    /// </summary>
    public void Init(int value)
    {
        diamondValue = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            OnDiamondCollected?.Invoke(diamondValue, this);
            Destroy(gameObject);
        }
    }
}
