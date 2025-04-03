using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdmodManager : MonoBehaviour
{
   public static AdmodManager Instance;
   
   private RewardedAd _rewardedAd;
   
   [SerializeField]private string rewardedAdId; // Test ID

   
   public void Init()
   {
      Instance = this;
      MobileAds.Initialize(initStatus => { });
      LoadRewardedAd();
      
   }
   
   public void LoadRewardedAd()
   {
      // Clean up the old ad before loading a new one.
      if (_rewardedAd != null)
      {
         _rewardedAd.Destroy();
         _rewardedAd = null;
      }


      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(rewardedAdId, adRequest,
         (RewardedAd ad, LoadAdError error) =>
         {
            if (error != null || ad == null)
            {
              
               return;
            }
            _rewardedAd = ad;
            RegisterEventHandlers(_rewardedAd);
         });
      
   }
   
   public void ShowRewardedAd(Action onAdCompleted)
   {
      if (_rewardedAd != null && _rewardedAd.CanShowAd())
      {
         _rewardedAd.Show(reward =>
         {
            Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");
            onAdCompleted?.Invoke();
         });

         LoadRewardedAd(); 
      }
   }
   
   public void ShowRewardedAd(Action<int> onAdCompleted, int amount)
   {
      if (_rewardedAd != null && _rewardedAd.CanShowAd())
      {
         _rewardedAd.Show(reward =>
         {
            Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");
            // Thực hiện hành động sau khi user xem xong quảng cáo
            onAdCompleted?.Invoke(amount);
         });

         LoadRewardedAd(); // Load lại quảng cáo mới
      }
   }

   // ======================= EVENT HANDLERS =======================
   private void RegisterEventHandlers(RewardedAd ad)
   {
      ad.OnAdFullScreenContentClosed += () =>
      {
         Debug.Log("Rewarded Ad closed. Reloading...");
         LoadRewardedAd();
      };

      ad.OnAdFullScreenContentFailed += (AdError error) =>
      {
         Debug.LogError($"Rewarded Ad failed to show: {error.GetMessage()}");
      };
   }
}
