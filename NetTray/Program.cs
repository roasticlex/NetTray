using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace NetTray
{
    internal static class Program
    {
        private static float bytesReceivedStart;
        private static float bytesReceivedEnd;
        private static float bytesSentStart;
        private static float bytesSentEnd;
        private static float downloadSpeed;
        private static float uploadSpeed;
        public static NotifyIcon downloadTray;
        public static NotifyIcon uploadTray;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NetworkInterface internetInterface = null;            

            //Get network interfaces that are currently connected to the internet
            var internetInterfaces = NetworkInterface.GetAllNetworkInterfaces()
           .Where(ni =>
               ni.OperationalStatus == OperationalStatus.Up &&
               ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
               ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
               ni.GetIPProperties().GatewayAddresses.Any(g =>
                   g.Address != null &&
                   !g.Address.Equals(System.Net.IPAddress.Any) &&
                   !g.Address.Equals(System.Net.IPAddress.IPv6Any)));

            //Find the first/last interface that has internet access
            foreach (var ni in internetInterfaces)            
                internetInterface = ni;

            // Create context menu (right click options on tray icon)
            var menu = new ContextMenuStrip();
            var settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Click += (s, e) =>
            {
                OnSettings();
            };
            menu.Items.Add(settingsItem);

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) =>
            {
                OnExit();
            };
            menu.Items.Add(exitItem);

            downloadTray = new NotifyIcon
            {
                ContextMenuStrip = menu,
                Visible = true
            };

            uploadTray = new NotifyIcon
            {
                ContextMenuStrip = menu,
                Visible = true
            };

            Icon oldDownloadTrayIcon = null;
            Icon oldUploadTrayIcon = null;

            while (true)
            {
                Application.DoEvents();

                bytesReceivedStart = internetInterface.GetIPv4Statistics().BytesReceived;
                bytesSentStart = internetInterface.GetIPv4Statistics().BytesSent;

                //Wait for 1 second to measure the speed
                new System.Threading.ManualResetEvent(false).WaitOne(1000);

                bytesReceivedEnd = internetInterface.GetIPv4Statistics().BytesReceived;
                bytesSentEnd = internetInterface.GetIPv4Statistics().BytesSent;

                downloadSpeed = (bytesReceivedEnd - bytesReceivedStart) / 1000000;
                uploadSpeed = (bytesSentEnd - bytesSentStart) / 1000000;

                string downloadSpeedText = downloadSpeed.ToString("F0");
                string uploadSpeedText = uploadSpeed.ToString("F0");

                //Default colors for upload and download speeds if registry values are not found
                Color uploadColor = Color.Yellow;
                Color downloadColor = Color.Red;

                //Get upload color from registry
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NetTray"))
                {
                    if (key != null)
                    {
                        object regUploadColor = key.GetValue("uploadColor");

                        if (regUploadColor is int argb)
                        {
                            uploadColor = Color.FromArgb(argb);
                        }
                    }
                }


                //Get download color from registry
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NetTray"))
                {
                    if (key != null)
                    {
                        object regDownloadColor = key.GetValue("downloadColor");

                        if (regDownloadColor is int argb)
                        {
                            downloadColor = Color.FromArgb(argb);
                        }
                    }
                }

                uploadTray.Icon = TrayIconText.Create(
                    uploadSpeedText,
                    uploadColor
                );

                downloadTray.Icon = TrayIconText.Create(
                    downloadSpeedText,
                    downloadColor
                );

                downloadTray.Text = downloadSpeed.ToString() + "MB/s Down";
                uploadTray.Text = uploadSpeed.ToString() + "MB/s Up";

                oldDownloadTrayIcon?.Dispose();
                oldDownloadTrayIcon = downloadTray.Icon;

                oldUploadTrayIcon?.Dispose();
                oldUploadTrayIcon = uploadTray.Icon;
            }            
        }
        public static class TrayIconText
        {
            [DllImport("user32.dll")]
            private static extern bool DestroyIcon(IntPtr hIcon);

            public static Icon Create(string text, Color foreground)
            {
                using Bitmap bitmap = new Bitmap(32, 32);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Transparent);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                    using var font = new Font(
                        "Segoe UI",
                        20,
                        FontStyle.Bold,
                        GraphicsUnit.Pixel);

                    using var textBrush = new SolidBrush(foreground);

                    SizeF size = g.MeasureString(text, font);

                    g.DrawString(
                        text,
                        font,
                        textBrush,
                        (32 - size.Width) / 2,
                        (32 - size.Height) / 2 - 1);
                }

                IntPtr hIcon = bitmap.GetHicon();

                try
                {
                    return (Icon)Icon.FromHandle(hIcon).Clone();
                }
                finally
                {
                    DestroyIcon(hIcon);
                }
            }
        }
        public static void OnExit()
        {
            downloadTray.Visible = false;
            downloadTray.Dispose();

            uploadTray.Visible = false;
            uploadTray.Dispose();

            Application.ExitThread();
            Environment.Exit(0);
        }

        public static void OnSettings()
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog(); // modal
        }
    }
}