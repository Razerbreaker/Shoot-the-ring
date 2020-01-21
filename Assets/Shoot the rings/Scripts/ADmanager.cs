using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Net;

public class ADmanager : MonoBehaviour
{
    // Start is called before the first frame update

    private string APP_ID = "ca-app-pub-1455818052373937~4159803330";

    private InterstitialAd interstitialAd;

    static DateTime lastAdCloseTime;
    static TimeSpan AdsInterval = new TimeSpan(0, 1, 0);   // 1 минута


    void Start()
    {

        //MobileAds.Initialize(APP_ID);           // для паблиша, пока закоментить

        /*RequestInterstitial();*/      // запрос межстраничной рекл
    }

    public void RequestInterstitial()
    {
        string interstisial_ID = "ca-app-pub-3940256099942544/1033173712";     // тут тестовый, нужно заменить на реальный из адмоб
        interstitialAd = new InterstitialAd(interstisial_ID);


        //for real app
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //for test
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("33BE2250B43518CCDA7DE426D04EE232").Build();

        interstitialAd.LoadAd(adRequest);
    }

    public void DisplayInterstitial()

    {
        if (interstitialAd.IsLoaded() && (DateTime.Now - lastAdCloseTime > AdsInterval)) //  проверка что реклама загружена и что прошло время с прошлого показа рекламы
        {
            interstitialAd.Show();
        }

    }       // показывает рекламу

    //Handle events
    void HandleInterstitialEvents(bool subscribe)
    {

        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            interstitialAd.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitialAd.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            interstitialAd.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitialAd.OnAdOpening -= HandleOnAdOpened;
            // Called when the ad is closed.
            interstitialAd.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }
    }


    public void HandleOnAdLoaded(object sender, EventArgs args) // реклама уже загружена - показываем
    {
        //DisplayInterstitial();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) // реклама не загружена, пробуем еще
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        lastAdCloseTime = DateTime.Now;
        interstitialAd.Destroy();
        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    private void OnEnable()
    {
        RequestInterstitial();
        HandleInterstitialEvents(true);
    }

    private void OnDisable()
    {
        HandleInterstitialEvents(false);
    }
}