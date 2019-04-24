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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessingStudy
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;

        const uint SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        public MainWindow()
        {
            InitializeComponent();

            IsEnabledChanged += (sender, args) => UpdateCloseButton();
        }

        private void UpdateCloseButton()
        {
            if (PresentationSource.FromVisual(this) is HwndSource hWndSource)
            {
                var hMenu = GetSystemMenu(hWndSource.Handle, false);
                if (hMenu != IntPtr.Zero)
                {
                    if (IsEnabled)
                    {
                        EnableMenuItem(hMenu, SC_CLOSE, MF_ENABLED);
                    }
                    else
                    {
                        EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                    }
                }
            }
        }
    }
}
