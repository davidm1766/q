using System;
using System.Collections.Generic;
using System.Text;

namespace Ads
{
    public interface IAdIntersticial
    {
        void Init(IIntersticialNotify adsNotify);
        void ShowAd();
        void LoadAd();
    }
}
