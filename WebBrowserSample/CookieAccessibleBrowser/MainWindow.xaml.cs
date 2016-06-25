using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookieAccessibleBrowser
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate(addressTextBox.Text);
        }

        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref uint pcchCookieData, int dwFlags, IntPtr lpReserved);
        
        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            uint length = 1024;
            StringBuilder cookieData = new StringBuilder((int)length);
            bool result = InternetGetCookieEx(
                               e.Uri.AbsoluteUri,
                               null,
                               cookieData,
                               ref length,
                               INTERNET_COOKIE_HTTPONLY,
                               IntPtr.Zero);
            if (result && 0 < cookieData.Length)
            {
                cookieValueLabel.Content = cookieData.ToString();
            }

        }

    }
}
