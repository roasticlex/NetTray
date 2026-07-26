namespace NetTray
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblDownloadColor = new Label();
            btnDownloadColor = new Button();
            btnUploadColor = new Button();
            lblUploadColor = new Label();
            lblRunStartup = new Label();
            chkRunStartup = new CheckBox();
            SuspendLayout();
            // 
            // lblDownloadColor
            // 
            lblDownloadColor.AutoSize = true;
            lblDownloadColor.Location = new Point(22, 30);
            lblDownloadColor.Name = "lblDownloadColor";
            lblDownloadColor.Size = new Size(96, 15);
            lblDownloadColor.TabIndex = 0;
            lblDownloadColor.Text = "Download Color:";
            // 
            // btnDownloadColor
            // 
            btnDownloadColor.BackColor = Color.Red;
            btnDownloadColor.Location = new Point(124, 24);
            btnDownloadColor.Name = "btnDownloadColor";
            btnDownloadColor.Size = new Size(27, 26);
            btnDownloadColor.TabIndex = 1;
            btnDownloadColor.UseVisualStyleBackColor = false;
            btnDownloadColor.Click += btnDownloadColor_Click;
            // 
            // btnUploadColor
            // 
            btnUploadColor.BackColor = Color.Yellow;
            btnUploadColor.Location = new Point(124, 59);
            btnUploadColor.Name = "btnUploadColor";
            btnUploadColor.Size = new Size(27, 26);
            btnUploadColor.TabIndex = 3;
            btnUploadColor.UseVisualStyleBackColor = false;
            btnUploadColor.Click += btnUploadColor_Click;
            // 
            // lblUploadColor
            // 
            lblUploadColor.AutoSize = true;
            lblUploadColor.BackColor = SystemColors.ButtonFace;
            lblUploadColor.Location = new Point(22, 65);
            lblUploadColor.Name = "lblUploadColor";
            lblUploadColor.Size = new Size(80, 15);
            lblUploadColor.TabIndex = 2;
            lblUploadColor.Text = "Upload Color:";
            // 
            // lblRunStartup
            // 
            lblRunStartup.AutoSize = true;
            lblRunStartup.Location = new Point(22, 103);
            lblRunStartup.Name = "lblRunStartup";
            lblRunStartup.Size = new Size(89, 15);
            lblRunStartup.TabIndex = 4;
            lblRunStartup.Text = "Run on Startup:";
            // 
            // chkRunStartup
            // 
            chkRunStartup.AutoSize = true;
            chkRunStartup.Checked = true;
            chkRunStartup.CheckState = CheckState.Checked;
            chkRunStartup.Location = new Point(136, 104);
            chkRunStartup.Name = "chkRunStartup";
            chkRunStartup.Size = new Size(15, 14);
            chkRunStartup.TabIndex = 5;
            chkRunStartup.UseVisualStyleBackColor = true;
            chkRunStartup.CheckedChanged += chkRunStartup_CheckedChanged;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(213, 146);
            Controls.Add(chkRunStartup);
            Controls.Add(lblRunStartup);
            Controls.Add(btnUploadColor);
            Controls.Add(lblUploadColor);
            Controls.Add(btnDownloadColor);
            Controls.Add(lblDownloadColor);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NetTray";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDownloadColor;
        private Button btnDownloadColor;
        private Button btnUploadColor;
        private Label lblUploadColor;
        private Label lblRunStartup;
        private CheckBox chkRunStartup;
    }
}