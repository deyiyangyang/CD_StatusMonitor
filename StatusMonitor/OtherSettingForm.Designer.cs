namespace StatusMonitor
{
    partial class OtherSettingForm
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
            this.chkMonitorShow = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkMessageShow = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkMonitorShow
            // 
            this.chkMonitorShow.AutoSize = true;
            this.chkMonitorShow.Location = new System.Drawing.Point(171, 37);
            this.chkMonitorShow.Name = "chkMonitorShow";
            this.chkMonitorShow.Size = new System.Drawing.Size(48, 16);
            this.chkMonitorShow.TabIndex = 9;
            this.chkMonitorShow.Text = "表示";
            this.chkMonitorShow.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "モニタタブ表示";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(208, 188);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 188);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 29);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkMessageShow
            // 
            this.chkMessageShow.AutoSize = true;
            this.chkMessageShow.Location = new System.Drawing.Point(171, 65);
            this.chkMessageShow.Name = "chkMessageShow";
            this.chkMessageShow.Size = new System.Drawing.Size(48, 16);
            this.chkMessageShow.TabIndex = 11;
            this.chkMessageShow.Text = "表示";
            this.chkMessageShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "メッセージポップアップ";
            // 
            // OtherSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.chkMessageShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkMonitorShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherSettingForm";
            this.Text = "他の設定";
            this.Load += new System.EventHandler(this.OtherSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMonitorShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkMessageShow;
        private System.Windows.Forms.Label label2;
    }
}