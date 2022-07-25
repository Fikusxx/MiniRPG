using UnityEngine;
using System;
using System.Collections.Generic;


[DisallowMultipleComponent]
public class CollectablesController : MonoBehaviour
{
    #region Singleton
    #endregion
    public static CollectablesController Instance { get; private set; }

    #region Collectables Data
    [Space(10)]
    [Header("Collectables")]
    #endregion
    [SerializeField] private int diamonds;

    #region Properties
    #endregion
    public int Diamonds { get => diamonds; }

    #region Prefabs 
    [Space(10)]
    [Header("Collectables Prefabs")]
    #endregion
    [SerializeField] private Diamond diamondPrefab;

    #region Lists Of Collectables
    [Space(10)]
    [Header("Lists of Collectables")]
    #endregion
    [SerializeField] private List<Diamond> diamondList = new List<Diamond>();

    #region Events
    #endregion
    public Action OnDiamondsAdded;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        foreach (var diamond in diamondList)
        {
            diamond.OnDiamondCollected += ProcessDiamondPickup;
        }
    }


    /// <summary>
    /// Spawn a diamond at position with a gemsValue
    /// </summary>
    public void SpawnDiamond(int gemsValue, Vector2 position)
    {
        var diamond = Instantiate(diamondPrefab, position, Quaternion.identity);
        diamond.Init(gemsValue);

        AddDiamondToList(diamond);
        diamond.OnDiamondCollected += ProcessDiamondPickup;
    }

    /// <summary>
    /// Add value amount of diamonds
    /// </summary>
    private void AddDiamonds(int value)
    {
        diamonds += value;
        OnDiamondsAdded?.Invoke();
    }

    /// <summary>
    /// Add diamond to the list
    /// </summary>
    private void AddDiamondToList(Diamond diamond)
    {
        diamondList.Add(diamond);
    }

    /// <summary>
    /// Remove diamond from the list
    /// </summary>
    private void RemoveDiamondFromList(Diamond diamond)
    {
        diamondList.Remove(diamond);
    }

    /// <summary>
    /// Add diamonds to the diamonds count, remove diamond from diamondList and unsub from it's event
    /// </summary>
    private void ProcessDiamondPickup(int diamonds, Diamond diamond)
    {
        AddDiamonds(diamonds);
        RemoveDiamondFromList(diamond);
        diamond.OnDiamondCollected -= ProcessDiamondPickup;
    }
}
