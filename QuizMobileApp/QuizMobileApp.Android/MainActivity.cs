using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using QuizMobileApp.Droid.Database;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Plugin.Toasts;

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
            Updater.Update(dl);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.Forms.DependencyService.Register<ToastNotification>(); // Register your dependency
            
            // If you are using Android you must pass through the activity
            //ToastNotification.Init(this);

            ToastNotification.Init(this, new PlatformOptions() { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });

            LoadApplication(new App(dl));
        }
    }
}

