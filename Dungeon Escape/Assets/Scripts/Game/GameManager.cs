using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton
    #endregion
    public static GameManager Instance;

    #region Core Data
    #endregion
    private bool hasKeyToCastle = false;

    #region Properties
    #endregion
    public bool HasKeyToCastle { get => hasKeyToCastle; }

    private void Awake()
    {
        Instance = this;
    }


    public void GetKeyToCastle()
    {
        hasKeyToCastle = true;
    }
}
