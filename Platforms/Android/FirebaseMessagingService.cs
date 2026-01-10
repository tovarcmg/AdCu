using Android.App;
using Android.Content;
using Firebase.Messaging;

[Service(Exported = false)]
[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
public class MyFirebaseMessagingService : FirebaseMessagingService
{
    public override void OnNewToken(string token)
    {
        base.OnNewToken(token);

        Console.WriteLine("🔥 FCM TOKEN: " + token);

        System.Diagnostics.Debug.WriteLine("🔥 FCM TOKEN:");
        System.Diagnostics.Debug.WriteLine(token);
        Preferences.Set("deviceToken", token);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        System.Diagnostics.Debug.WriteLine("🔥 FCM Service creado");
    }
}