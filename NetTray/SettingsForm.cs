using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace NetTray
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            //Pre-color button clicks colors if registry values exist

            //Get upload color from registry
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NetTray"))
            {
                if (key != null)
                {
                    object regUploadColor = key.GetValue("uploadColor");

                    if (regUploadColor is int argb)
                    {
                        btnUploadColor.BackColor = Color.FromArgb(argb);
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
                        btnDownloadColor.BackColor = Color.FromArgb(argb);
                    }
                }
            }

            //Get runOnStartup from registry startup items
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NetTray"))
            {
                if (key != null)
                {
                    object regRunOnStartup = key.GetValue("runOnStartup");

                    if (regRunOnStartup is int runOnStartup)
                    {
                        chkRunStartup.Checked = runOnStartup == 1;
                    }
                }
            }
        }

        private void btnDownloadColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = btnDownloadColor.BackColor;
                colorDialog.FullOpen = true; // shows advanced options

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    // Save or apply it
                    btnDownloadColor.BackColor = selectedColor;

                    //Save the selected color to the registry
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\NetTray"))
                    {
                        key.SetValue("downloadColor", selectedColor.ToArgb());
                    }

                    Application.Restart();
                    Environment.Exit(0);
                }
            }
        }
        private void btnUploadColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = btnUploadColor.BackColor;
                colorDialog.FullOpen = true; // shows advanced options

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    // Save or apply it
                    btnUploadColor.BackColor = selectedColor;

                    //Save the selected color to the registry
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\NetTray"))
                    {
                        key.SetValue("uploadColor", selectedColor.ToArgb());
                    }

                    Application.Restart();
                    Environment.Exit(0);
                }
            }
        }

        private void chkRunStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRunStartup.Checked)
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\NetTray"))
                {
                    key.SetValue("runOnStartup", 1);
                    SetStartup(true);   
                }
            }
            else
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\NetTray"))
                {
                    key.SetValue("runOnStartup", 0);
                    SetStartup(false);
                }
            }   
        }

        public static void SetStartup(bool enable)
        {
            string appName = "NetTray";
            string exePath = Application.ExecutablePath;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (enable)
                {
                    key.SetValue(appName, exePath);
                }
                else
                {
                    key.DeleteValue(appName, false);
                }
            }
        }
    }
}
