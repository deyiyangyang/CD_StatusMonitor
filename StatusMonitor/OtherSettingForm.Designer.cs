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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkMessageShow = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAgentGraph = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbListFontSize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            this.chkMessageShow.Location = new System.Drawing.Point(171, 48);
            this.chkMessageShow.Name = "chkMessageShow";
            this.chkMessageShow.Size = new System.Drawing.Size(48, 16);
            this.chkMessageShow.TabIndex = 11;
            this.chkMessageShow.Text = "表示";
            this.chkMessageShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "メッセージポップアップ";
            // 
            // chkAgentGraph
            // 
            this.chkAgentGraph.AutoSize = true;
            this.chkAgentGraph.Location = new System.Drawing.Point(171, 78);
            this.chkAgentGraph.Name = "chkAgentGraph";
            this.chkAgentGraph.Size = new System.Drawing.Size(48, 16);
            this.chkAgentGraph.TabIndex = 13;
            this.chkAgentGraph.Text = "表示";
            this.chkAgentGraph.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "エージェントグラフ表示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "リストフォントサイズ";
            // 
            // cmbListFontSize
            // 
            this.cmbListFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListFontSize.FormattingEnabled = true;
            this.cmbListFontSize.Location = new System.Drawing.Point(171, 105);
            this.cmbListFontSize.Name = "cmbListFontSize";
            this.cmbListFontSize.Size = new System.Drawing.Size(73, 20);
            this.cmbListFontSize.TabIndex = 15;
            // 
            // OtherSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.cmbListFontSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkAgentGraph);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkMessageShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherSettingForm";
            this.Text = "他の設定";
            this.Load += new System.EventHandler(this.OtherSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkMessageShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAgentGraph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbListFontSize;
    }
}