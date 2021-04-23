using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.SceneManagement;

public class SDKManager : MonoBehaviour
{
    public static SDKManager sdkManager;


    private int allowInterstitialAdFormLevel = 3;   //Allow show interstitial ads from N level
    private int adsInterval = 40; //interval between ads X seconds
    [HideInInspector] public int allowBannerAdFromLevel = 3;

    private float adsTimer;

    private string interstitialAdUnitId = "cd0bbb86b7ba65d8";
    private string bannerAdUnitId = "fd3bd3f4c212b8df";
    public string rewardedAdUnitID = "3151e0c21512df41";

    [SerializeField] LevelLoader levelLoader;

    private void Awake()
    {
        if (SDKManager.sdkManager == null)
        {
            SDKManager.sdkManager = this;
        }
        else
        {
            if (SDKManager.sdkManager != this)
            {
                Destroy(this.gameObject);
            }
        }

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        
        DontDestroyOnLoad(this);
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void FixedUpdate()
    {
        adsTimer += Time.deltaTime;
    }
    private void Start()
    {     
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds();
        };

        MaxSdk.SetSdkKey("6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR");
        MaxSdk.InitializeSdk();

        YandexAppMetricaConfig config = new YandexAppMetricaConfig("7bc6a201-1a67-42bb-b347-8a5f80d971fb");
        config.SessionTimeout = 300;
        AppMetrica.Instance.ActivateWithConfiguration(config);

        MaxSdk.ShowMediationDebugger();

        adsTimer = 0;
    }

    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, new Color(1, 1, 1, 0));
    }

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load. We recommend re-trying in 3 seconds.
        Invoke("LoadInterstitial", 3);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
        
        AudioListener.pause = false;
        //PlayerData.playerData.InterWatched();

        Dictionary<string, object> RVEvent = new Dictionary<string, object>();
        RVEvent.Add("ad_type", "interstitial");
        RVEvent.Add("placement", "level_end");
        RVEvent.Add("result", "watched");
        AppMetrica.Instance.ReportEvent("video_ads_watch", RVEvent);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void ShowBanner()
    {
        MaxSdk.ShowBanner(bannerAdUnitId);
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(bannerAdUnitId);
    }

    public void ShowAd()
    {
        Debug.Log("Try to show inter ad");
        //if (adsTimer >= adsInterval && firstAdDelay <= 0)
        // if (adsTimer >= adsInterval && Plugins.DataSaver.DataSaver<Modules.Data.PlayerData>.Instance.LevelIdx > 1)
        // {
        //     
        // }
        Debug.Log("Show inter ad");
        if (adsTimer > adsInterval)
        {
            adsTimer = 0;
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                //Debug.Log("inter ad is ready");
                MaxSdk.ShowInterstitial(interstitialAdUnitId);

                Dictionary<string, object> RVEvent = new Dictionary<string, object>();
                RVEvent.Add("ad_type", "interstitial");
                RVEvent.Add("placement", "level_end");
                RVEvent.Add("result", "success");
                AppMetrica.Instance.ReportEvent("video_ads_available", RVEvent);
                AppMetrica.Instance.SendEventsBuffer();

                AudioListener.pause = true;

            }
            else
            {
                //Debug.Log("inter ad is not ready");

                Dictionary<string, object> RVEvent = new Dictionary<string, object>();
                RVEvent.Add("ad_type", "interstitial");
                RVEvent.Add("placement", "level_end");
                RVEvent.Add("result", "not_available");
                AppMetrica.Instance.ReportEvent("video_ads_available", RVEvent);
                AppMetrica.Instance.SendEventsBuffer();
            }
        }
        
    }

    public void LevelComplete(int _levelNumber)
    {
        Debug.Log("Level complete: " + _levelNumber);

        var FBarams = new Dictionary<string, object>();
        FBarams[AppEventParameterName.Level] = _levelNumber;

        FB.LogAppEvent(
            AppEventName.AchievedLevel,
            parameters: FBarams
        );

        Dictionary<string, object> levelEvent = new Dictionary<string, object>();
        levelEvent.Add("level", (_levelNumber + 1));
        levelEvent.Add("result", "win");
        levelEvent.Add("progress", 100);
        AppMetrica.Instance.ReportEvent("level_finish", levelEvent);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void LevelFailed(int _levelNumber)
    {
        Debug.Log("Level failed: " + _levelNumber);

        var FBarams = new Dictionary<string, object>();
        FBarams[AppEventParameterName.Level] = _levelNumber;

        if (FB.IsInitialized)
            FB.LogAppEvent(
                "Level_failed",
                parameters: FBarams
            );

        Dictionary<string, object> levelEvent = new Dictionary<string, object>();
        levelEvent.Add("level", (_levelNumber + 1));
        levelEvent.Add("result", "lose");
        levelEvent.Add("progress", 0);
        AppMetrica.Instance.ReportEvent("level_finish", levelEvent);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void LevelStarted(int _levelNumber)
    {
        Debug.Log("Level started: " + _levelNumber);

        var FBarams = new Dictionary<string, object>();
        FBarams[AppEventParameterName.Level] = _levelNumber;

        if (FB.IsInitialized)
            FB.LogAppEvent(
                "Level_started",
                parameters: FBarams
            );

        Dictionary<string, object> levelEvent = new Dictionary<string, object>();
        levelEvent.Add("level", (_levelNumber + 1));
        AppMetrica.Instance.ReportEvent("level_start", levelEvent);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }

    private void OnRewardedAdClickedEvent(string adUnitId) { }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    //private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    //{
    //    // Rewarded ad was displayed and user should receive the reward
    //}

    public void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        adsTimer = 0;
        Dictionary<string, object> RVEvent = new Dictionary<string, object>();
        RVEvent.Add("ad_type", "rewarded");
        RVEvent.Add("result", "watched");
        AppMetrica.Instance.ReportEvent("video_ads_watch", RVEvent);
        AppMetrica.Instance.SendEventsBuffer();
        levelLoader.OnRewardedEvent();
    }

    public void SkipLevelRV()
    {
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitID))
        {
            adsTimer = 0;
            //currentRVType = RVType.speedUp;
            MaxSdk.ShowRewardedAd(rewardedAdUnitID);

            Dictionary<string, object> RVEvent = new Dictionary<string, object>();
            RVEvent.Add("ad_type", "rewarded");
            RVEvent.Add("placement", "skip_level");
            RVEvent.Add("result", "success");
            RVEvent.Add("connection", "true");
            AppMetrica.Instance.ReportEvent("video_ads_available", RVEvent);
            AppMetrica.Instance.SendEventsBuffer();

            RVEvent = new Dictionary<string, object>();
            RVEvent.Add("ad_type", "rewarded");
            RVEvent.Add("placement", "skip_level");
            RVEvent.Add("result", "start");
            RVEvent.Add("connection", "true");
            AppMetrica.Instance.ReportEvent("video_ads_started", RVEvent);
            AppMetrica.Instance.SendEventsBuffer();
        }
        else
        {
            Dictionary<string, object> RVEvent = new Dictionary<string, object>();
            RVEvent.Add("ad_type", "rewarded");
            RVEvent.Add("placement", "skip_level");
            RVEvent.Add("result", "not_available");
            RVEvent.Add("connection", "true");
            AppMetrica.Instance.ReportEvent("video_ads_available", RVEvent);
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
}
