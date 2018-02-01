using System;
using System.Collections.Generic;
using System.Text;

namespace Ads
{
    public interface IAdInterstitial
    {
        void Init(IAdsNotifty adsNotify);
        void ShowAd();
        void LoadAd();
    }

}
