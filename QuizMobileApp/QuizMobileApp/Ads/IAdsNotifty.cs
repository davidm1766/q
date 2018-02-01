using System;
using System.Collections.Generic;
using System.Text;

namespace Ads
{
    public interface IAdsNotifty
    {
        void OnRewarded();
        void OnRewardedVideoAdFailedToLoad(int errorCode);
        void OnRewardedVideoAdOpened();
        void OnRewardedVideoAdLoaded();
        void OnRewardedVideoAdLeftApplication();
        void OnRewardedVideoAdClosed();
    }
}
