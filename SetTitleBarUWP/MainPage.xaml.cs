using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SetTitleBarUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Window mainWindow;

        private readonly Color customBlue = GetSolidColorBrush(@"#FF0055A6").Color;
        private readonly Color customBlueTransparent = GetSolidColorBrush(@"#000055A6").Color;

        public MainPage()
        {
            this.InitializeComponent();
            mainWindow = Window.Current;

            SetTitleBar();
        }

        private void SetTitleBar()
        {
            // Set the Title Bar button colors to match our custom title bar
            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            titleBar.BackgroundColor = customBlue;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = customBlue;
            titleBar.ButtonForegroundColor = Colors.White;

            titleBar.InactiveBackgroundColor = titleBar.BackgroundColor;
            titleBar.InactiveForegroundColor = titleBar.ForegroundColor;
            titleBar.ButtonInactiveBackgroundColor = titleBar.ButtonBackgroundColor;
            titleBar.ButtonInactiveForegroundColor = titleBar.ButtonForegroundColor;

            // Hide default title bar.
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            // Make this act as the title bar
            mainWindow.SetTitleBar(AppTitleBar);
        }

        private void SetTitle(string text)
        {
            string title = "Set Title Bar (UWP)";
            if (!string.IsNullOrEmpty(text))
            {
                title += " - " + text;
            }

            AppTitleBarText.Foreground = new SolidColorBrush(Colors.Yellow);
            AppTitleBarText.Text = title;
            //mainWindow.Title = title;
        }

        public void TitleActive(bool isActive, bool fromDialog = false)
        {
            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            if (isActive)
            {
                titleBar.BackgroundColor = customBlue;
                titleBar.ButtonBackgroundColor = customBlue;
                titleBar.InactiveBackgroundColor = titleBar.BackgroundColor;
                titleBar.ButtonInactiveBackgroundColor = titleBar.ButtonBackgroundColor;

                AppTitleBar.Background = new SolidColorBrush(customBlue);
            }
            else if (fromDialog)
            {
                titleBar.ButtonInactiveBackgroundColor = customBlueTransparent;
            }
            else
            {
                titleBar.ButtonInactiveBackgroundColor = customBlueTransparent;
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

            FileOpenPicker picker = new FileOpenPicker();
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
            var ignored = dialog.ShowAsync();
        }

        private void contentDialogWithFilePicker_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialogWithFilePicker(this);
            var ignored = dialog.ShowAsync();
        }
    }
}
