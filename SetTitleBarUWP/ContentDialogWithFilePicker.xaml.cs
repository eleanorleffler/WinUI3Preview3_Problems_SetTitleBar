using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SetTitleBarUWP
{
    public sealed partial class ContentDialogWithFilePicker : ContentDialog
    {
        MainPage refMainPage;

        public ContentDialogWithFilePicker(MainPage refMainPage)
        {
            this.InitializeComponent();
            this.refMainPage = refMainPage;

            contentTextBlock.Text = "Title Bar is inactive." + Environment.NewLine +
                                    "Minimize, Maximize, Close buttons are active.";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }

        private async void filePickerButton_Click(object sender, RoutedEventArgs e)
        {
            refMainPage.TitleActive(false, true);

            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            refMainPage.TitleActive(true, true);
        }
    }
}
