using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Gms.Common.Apis;
using Firebase.Auth;
using Firebase;
using Android.Content;
using Android.Gms.Tasks;
using Xamarin.Forms;
using Workflow.Models;



namespace Workflow.Droid
{
    [Activity(Label = "Ворк флоу", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnSuccessListener, IOnFailureListener
    {
        public GoogleSignInOptions gso;
        public GoogleApiClient googleApiClient;

        public FirebaseAuth FireAuth;
        App app;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Plugin.Media.CrossMedia.Current.Initialize();
            global::Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CreateNotificationChannel();
            app = new App();
            LoadApplication(app);

            gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("270394628656-4ri5kuhj5h2uvk2mmume2u2rblligt4n.apps.googleusercontent.com")
                .RequestEmail()
                .Build();
            googleApiClient = new GoogleApiClient.Builder(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();

            googleApiClient.Connect();
            FireAuth = GetFirebaseAuth();
        }
        public void SignInClick()
        {
            var intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            StartActivityForResult(intent, 1);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (result.IsSuccess)
                {
                    GoogleSignInAccount account = result.SignInAccount;
                    LoginWithFirebaseAccount(account);
                }
            }
        }

        void LoginWithFirebaseAccount(GoogleSignInAccount acc)
        {
            var credentials = GoogleAuthProvider.GetCredential(acc.IdToken, null);
            FireAuth.SignInWithCredential(credentials)
                .AddOnSuccessListener(this)
                .AddOnFailureListener(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel("work_flow",
                                                  "Work Flow",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };
            

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        FirebaseAuth GetFirebaseAuth()
        {
            var app = Firebase.FirebaseApp.InitializeApp(this);
            FirebaseAuth mAuth;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("workflow-f69ad")
                    .SetApplicationId("workflow-f69ad")
                    .SetApiKey("AIzaSyB_7gJm2pidnxGuNtitB4h_7IK_F4HGdbw")
                    .SetDatabaseUrl("https://workflow-f69ad.firebaseio.com")
                    .SetStorageBucket("workflow-f69ad.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(this, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }
            return mAuth;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            UserAuthModel UserAuth = new UserAuthModel();
            var Names = FireAuth.CurrentUser.DisplayName.Split(" ");
            UserAuth.Email = FireAuth.CurrentUser.Email;
            UserAuth.Name = Names[0];
            if (Names.Length > 1)
                UserAuth.Surname = Names[1];
            UserAuth.Phone = FireAuth.CurrentUser.PhoneNumber;
            MessagingCenter.Send<Object, UserAuthModel>(app, "GoogleLogin", UserAuth);
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            throw new NotImplementedException();
        }

        [Service]
        [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        public class MyFirebaseIIDService : FirebaseInstanceIdService
        {
            const string TAG = "MyFirebaseIIDService";
            public override void OnTokenRefresh()
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                Log.Debug(TAG, "Refreshed token: " + refreshedToken);
                SendRegistrationToServer(refreshedToken);
            }
            void SendRegistrationToServer(string token)
            {
                // Add custom implementation, as needed.
            }
        }
    }
}