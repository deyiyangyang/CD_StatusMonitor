namespace StatusMonitor
{
    partial class frmOtherVoiceSetting
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
            this.btnPlay1 = new System.Windows.Forms.Button();
            this.btnOpenFile1 = new System.Windows.Forms.Button();
            this.txtHelpVoice = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ofdVoice = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnPlay1
            // 
            this.btnPlay1.Location = new System.Drawing.Point(242, 40);
            this.btnPlay1.Name = "btnPlay1";
            this.btnPlay1.Size = new System.Drawing.Size(37, 20);
            this.btnPlay1.TabIndex = 38;
            this.btnPlay1.Text = "再生";
            this.btnPlay1.UseVisualStyleBackColor = true;
            this.btnPlay1.Click += new System.EventHandler(this.btnPlay1_Click);
            // 
            // btnOpenFile1
            // 
            this.btnOpenFile1.Location = new System.Drawing.Point(202, 40);
            this.btnOpenFile1.Name = "btnOpenFile1";
            this.btnOpenFile1.Size = new System.Drawing.Size(37, 20);
            this.btnOpenFile1.TabIndex = 35;
            this.btnOpenFile1.Text = "参照";
            this.btnOpenFile1.UseVisualStyleBackColor = true;
            this.btnOpenFile1.Click += new System.EventHandler(this.btnOpenFile1_Click);
            // 
            // txtHelpVoice
            // 
            this.txtHelpVoice.Enabled = false;
            this.txtHelpVoice.Location = new System.Drawing.Point(68, 40);
            this.txtHelpVoice.Name = "txtHelpVoice";
            this.txtHelpVoice.Size = new System.Drawing.Size(117, 19);
            this.txtHelpVoice.TabIndex = 32;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(233, 117);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(49, 28);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(172, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(49, 28);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "ヘルプ";
            // 
            // frmOtherVoiceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 196);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnPlay1);
            this.Controls.Add(this.btnOpenFile1);
            this.Controls.Add(this.txtHelpVoice);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOtherVoiceSetting";
            this.Text = "ヘルプ警告設定";
            this.Load += new System.EventHandler(this.frmOtherVoiceSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPlay1;
        private System.Windows.Forms.Button btnOpenFile1;
        private System.Windows.Forms.TextBox txtHelpVoice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog ofdVoice;
    }
}