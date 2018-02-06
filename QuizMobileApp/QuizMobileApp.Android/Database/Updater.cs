using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuizMobileApp.Droid.Database
{
    public class Updater
    {
        public static int AppVersion = 2018020601;


        public static void Update(Repository rep) {
            int actualVersion = rep.GetActualVersion();
            if (actualVersion == AppVersion)
            {
                return;
            }

            if (actualVersion < 2018020600)
            {
                //update bla bla bla
            }

            if (actualVersion < 2018020601)
            {
                //bla bla bla 
            }
            
            rep.SetActualVersion(AppVersion);
            
        }

    }
}