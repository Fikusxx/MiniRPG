using UnityEngine;
using UnityEngine.Advertisements;


public class AdsInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsInterstitial Instance { get; private set; }

    [SerializeField] private string androidAdUnitId = "Interstitial_Android";
    [SerializeField] private string iOSAdUnitId = "Interstitial_iOS";

    [SerializeField] private string adUnitId;

    private void Awake()
    {
        //adUnitId = Application.platform == RuntimePlatform.Android ? androidAdUnitId : iOSAdUnitId;
        Instance = this;
    }

    public void OnUnityAdsAdLoaded(string placementId) { }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
            case UnityAdsShowCompletionState.UNKNOWN:
                LoadAd();
                break;

            case UnityAdsShowCompletionState.COMPLETED:
                LoadAd();
                Debug.Log("You've been granted 100G");
                break;
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    /// <summary>
    /// Load an Ad
    /// </summary>
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    /// <summary>
    /// Show an Ad
    /// </summary>
    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + adUnitId);
        Advertisement.Show(adUnitId, this);
    }
}
