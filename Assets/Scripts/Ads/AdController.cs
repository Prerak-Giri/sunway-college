using System;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;

namespace Ads
{
    public class AdController : MonoBehaviour
    {
        [SerializeField] private TMP_Text initStatus;
        private BannerManager _bannerManager;
        private InterestialManager _interestialManager;
        private RewardedManager rewardedManager;
        
        private void Awake()
        {
            _bannerManager = GetComponent<BannerManager>();
            _interestialManager = GetComponent<InterestialManager>();
            rewardedManager = GetComponent<RewardedManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            MobileAds.Initialize((status =>
            {
                initStatus.text = "INIT STATUS : <color=green> Initialized </color>";
                Debug.Log("Mobile Ads Initialized ");
                
                LoadInterestial();
            }));
            
        }
        
        
        public void ShowBanner()
        {
            _bannerManager.LoadAd();
        }
    
        public void HideBanner()
        {
            _bannerManager.DestroyAd();
        }

        public void LoadInterestial()
        {
            _interestialManager.LoadInterstitialAd();
        }

        public void ShowInterestial()
        {
            _interestialManager.ShowInterstitialAd();
        }

        public void LoadRewarded()
        {
            rewardedManager.LoadRewardedAd();
        }
    
        public void ShowRewarded()
        {
            rewardedManager.ShowRewardedAd();
        }

    
    }
}
