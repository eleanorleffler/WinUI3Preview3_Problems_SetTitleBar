using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.ViewManagement;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SetTitleBarWinUIPreview3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Window mainWindow;

        private readonly Color customBlue = GetSolidColorBrush(@"#FF0055A6").Color;
        private readonly Color customBlueTransparent = GetSolidColorBrush(@"#000055A6").Color;

        public MainPage(Window mainWindow)
        {
            this.InitializeComponent();
            this.mainWindow = mainWindow;

            SetTitleBar();
        }

        private void SetTitleBar()
        {
            // Set the Title Bar button colors to match our custom title bar
            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            //titleBar.BackgroundColor = customBlue;
            //titleBar.ForegroundColor = Colors.White;
            //titleBar.ButtonBackgroundColor = customBlue;
            //titleBar.ButtonForegroundColor = Colors.White;

            //titleBar.InactiveBackgroundColor = titleBar.BackgroundColor;
            //titleBar.InactiveForegroundColor = titleBar.ForegroundColor;
            //titleBar.ButtonInactiveBackgroundColor = titleBar.ButtonBackgroundColor;
            //titleBar.ButtonInactiveForegroundColor = titleBar.ButtonForegroundColor;

            // Hide default title bar.
            //CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            // Make this act as the title bar
            //mainWindow.SetTitleBar(AppTitleBar);

            AppTitleBar.Visibility = Visibility.Collapsed;
        }

        private void SetTitle(string text)
        {
            string title = "Set Title Bar (WinUI)";
            if (!string.IsNullOrEmpty(text))
            {
                title += " - " + text;
            }

            AppTitleBarText.Foreground = new SolidColorBrush(Colors.Yellow);
            AppTitleBarText.Text = title;

            mainWindow.Title = title;
        }

        public void TitleActive(bool isActive, bool fromDialog = false)
        {
            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            if (isActive)
            {
                //titleBar.BackgroundColor = customBlue;
                //titleBar.ButtonBackgroundColor = customBlue;
                //titleBar.InactiveBackgroundColor = titleBar.BackgroundColor;
                //titleBar.ButtonInactiveBackgroundColor = titleBar.ButtonBackgroundColor;

                AppTitleBar.Background = new SolidColorBrush(customBlue);
            }
            else if (fromDialog)
            {
                //titleBar.ButtonInactiveBackgroundColor = customBlueTransparent;
            }
            else
            {
                //titleBar.ButtonInactiveBackgroundColor = customBlueTransparent;
                AppTitleBar.Background.Opacity = 0.5;
            }
        }

        public static SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            SetTitle("Home Page");
        }

        private void secondPageButton_Click(object sender, RoutedEventArgs e)
        {
            SetTitle("Second Page");
        }

        private async void filePickerButton_Click(object sender, RoutedEventArgs e)
        {
            TitleActive(false);

            FileOpenPicker picker = GetFileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            TitleActive(true);
        }

        private void contentDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Content Dialog",
                Content = "Title Bar is inactive." + Environment.NewLine +
                          "Minimize, Maximize, Close buttons are active.",
                CloseButtonText = "Ok"
            };
            dialog.XamlRoot = XamlRoot;
            var ignored = dialog.ShowAsync();
        }

        private void contentDialogWithFilePicker_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialogWithFilePicker(this);
            dialog.XamlRoot = XamlRoot;
            var ignored = dialog.ShowAsync();
        }

        #region Initialize With Window

        public FileOpenPicker GetFileOpenPicker()
        {
            FileOpenPicker picker = new FileOpenPicker();
            InitializeWithWindow(picker);
            return picker;
        }

        public void InitializeWithWindow(Object obj)
        {
            // When running on win32, FileOpenPicker needs to know the top-level hwnd via IInitializeWithWindow::Initialize.
            if (Window.Current == null)
            {
                IInitializeWithWindow initializeWithWindowWrapper = obj.As<IInitializeWithWindow>();
                IntPtr hwnd = GetActiveWindow();
                initializeWithWindowWrapper.Initialize(hwnd);
            }
        }

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
        public static extern IntPtr GetActiveWindow();

        #endregion
    }

    [ComImport, System.Runtime.InteropServices.Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize([In] IntPtr hwnd);
    }
}
