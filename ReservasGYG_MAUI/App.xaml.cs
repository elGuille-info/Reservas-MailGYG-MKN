namespace ReservasGYG_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Indicar el tamaño para la app de Windows.
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS

                // Asignar manualmente el tamaño. 
                int winWidth = 1200;
                int winHeight = 1600;

                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                appWindow.Resize(new Windows.Graphics.SizeInt32(winWidth, winHeight));

#endif
            });

            MainPage = new AppShell();
        }
    }
}