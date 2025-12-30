namespace MyMauiApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        var windows = base.CreateWindow(activationState);

        windows.Height = 800;
        windows.MaximumHeight = 800;
        windows.MinimumHeight = 800;
        windows.Width = 400;
        windows.MaximumWidth = 400;
        windows.MinimumWidth = 400;

        // Center the window
        windows.X = (windows.Width / 2) + 400;
        windows.Y = 100;

        return windows;
    }

}
