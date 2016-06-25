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

namespace HandleNewWindowBrowser
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

        private bool isInitialized = false;
        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if(!isInitialized)
            {
                IServiceProvider serviceProvider = (IServiceProvider)webBrowser.Document;
                Guid serviceGuid = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid iid = typeof(SHDocVw.IWebBrowser2).GUID;
                SHDocVw.DWebBrowserEvents_Event wbEvents = (SHDocVw.DWebBrowserEvents_Event)serviceProvider.QueryService(ref serviceGuid, ref iid);
                wbEvents.NewWindow += OnNewWindow;
                isInitialized = true;
            }
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
        private interface IServiceProvider
        {
            [return: MarshalAs(UnmanagedType.IUnknown)]
            object QueryService(ref Guid guidService, ref Guid riid);
        }

        private void OnNewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            Processed = true;
            webBrowser.Navigate(URL);
        }
    }
}
