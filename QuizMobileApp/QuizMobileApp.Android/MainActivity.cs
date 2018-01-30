using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using QuizMobileApp.Repository;
using QuizMobileApp.Droid.Database;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;

namespace QuizMobileApp.Droid
{
    [Activity(Label = "QuizMobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            SQLiteUtil sql = new SQLiteUtil(this);
            Database.Repository dl = new Database.Repository(sql);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);

            //var id = "ca-app-pub-9312615750092757~3504098531";
            //Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, id);
            
            

            LoadApplication(new App(dl));
        }
    }
}

