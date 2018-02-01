using System;
using System.Collections.Generic;
using System.Text;

namespace Ads
{
    public interface IIntersticialNotify
    {
        void RewardedVideoAdFailedToLoad(int error);
        void RewardedVideoAdLoaded();
        void RewardedVideoAdLeftApplication();
        void RewardedVideoAdClosed();
        void Rewarded();
    }
}
