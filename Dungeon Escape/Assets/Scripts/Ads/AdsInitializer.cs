using UnityEngine;
using UnityEngine.Advertisements;


public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string adroidGameId;
    [SerializeField] private string iOSGameId;
    [SerializeField] private bool testMode = true;

    [SerializeField] private string gameId;

    private void Awake()
    {
        InitAds();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Init complete");
        AdsInterstitial.Instance.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Init failed. Error + " + error.ToString() + " " + message);
    }

    private void InitAds()
    {
        //gameId = Application.platform == RuntimePlatform.Android ? adroidGameId : iOSGameId;
        Debug.Log("Initialization Started");
        Advertisement.Initialize(gameId, testMode, this);
    }
}
