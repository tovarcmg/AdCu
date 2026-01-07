using Android.App;
using Android.Runtime;
using Firebase;

namespace MyMauiApp;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void OnCreate()
    {
        base.OnCreate();

        FirebaseApp.InitializeApp(this);
        System.Diagnostics.Debug.WriteLine("🔥 Firebase inicializado");
    }

}
