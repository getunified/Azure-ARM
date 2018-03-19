using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetUnifiedClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        Device device;
        bool dExists = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!dExists) {
                device = new Device(TextBlock1, TextBlock2, TextBlock3, TextBlock4, StartBlock);
                dExists = true;
                StartBlock.Text = "PROGRAM STARTED";
            }
            else
            {
                StartBlock.Text = "STOP PRESSING ME!";
            }
        }
    }
}
