using System;
using System.Collections.Generic;
using System.Text;

namespace Ads
{
    public interface IAdInterstitial
    {
        void ShowAd();
        void Rewarded(bool state);
    }

}
