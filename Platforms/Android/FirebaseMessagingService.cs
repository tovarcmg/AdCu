using Android.App;
using Android.Content;
using Android.OS;
using Firebase.Messaging;
using MyMauiApp;

[Service(Exported = false)]
[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
public class MyFirebaseMessagingService : FirebaseMessagingService
{
    public override void OnNewToken(string token)
    {
        base.OnNewToken(token);

        System.Diagnostics.Debug.WriteLine("🔥 FCM TOKEN:");
        System.Diagnostics.Debug.WriteLine(token);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        System.Diagnostics.Debug.WriteLine("🔥 FCM Service creado");
    }

}
