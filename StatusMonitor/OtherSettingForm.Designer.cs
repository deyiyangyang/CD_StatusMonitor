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
            this.chkAgentGraph = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbListFontSize = new System.Windows.Forms.ComboBox();
            this.chkLineCutShow = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkMonitorShow
            // 
            this.chkMonitorShow.AutoSize = true;
            this.chkMonitorShow.Location = new System.Drawing.Point(170, 52);
            this.chkMonitorShow.Name = "chkMonitorShow";
            this.chkMonitorShow.Size = new System.Drawing.Size(48, 16);
            this.chkMonitorShow.TabIndex = 9;
            this.chkMonitorShow.Text = "表示";
            this.chkMonitorShow.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "モニタタブ表示";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(208, 219);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 219);
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
            this.chkMessageShow.Location = new System.Drawing.Point(170, 80);
            this.chkMessageShow.Name = "chkMessageShow";
            this.chkMessageShow.Size = new System.Drawing.Size(48, 16);
            this.chkMessageShow.TabIndex = 11;
            this.chkMessageShow.Text = "表示";
            this.chkMessageShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "メッセージポップアップ";
            // 
            // chkAgentGraph
            // 
            this.chkAgentGraph.AutoSize = true;
            this.chkAgentGraph.Location = new System.Drawing.Point(170, 110);
            this.chkAgentGraph.Name = "chkAgentGraph";
            this.chkAgentGraph.Size = new System.Drawing.Size(48, 16);
            this.chkAgentGraph.TabIndex = 13;
            this.chkAgentGraph.Text = "表示";
            this.chkAgentGraph.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "エージェントグラフ表示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "リストフォントサイズ";
            // 
            // cmbListFontSize
            // 
            this.cmbListFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListFontSize.FormattingEnabled = true;
            this.cmbListFontSize.Location = new System.Drawing.Point(170, 137);
            this.cmbListFontSize.Name = "cmbListFontSize";
            this.cmbListFontSize.Size = new System.Drawing.Size(73, 20);
            this.cmbListFontSize.TabIndex = 15;
            // 
            // chkLineCutShow
            // 
            this.chkLineCutShow.AutoSize = true;
            this.chkLineCutShow.Location = new System.Drawing.Point(170, 23);
            this.chkLineCutShow.Name = "chkLineCutShow";
            this.chkLineCutShow.Size = new System.Drawing.Size(48, 16);
            this.chkLineCutShow.TabIndex = 17;
            this.chkLineCutShow.Text = "表示";
            this.chkLineCutShow.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "回線切断表示";
            // 
            // OtherSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 260);
            this.Controls.Add(this.chkLineCutShow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbListFontSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkAgentGraph);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkMessageShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkMonitorShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherSettingForm";
            this.Text = "その他";
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
        private System.Windows.Forms.CheckBox chkAgentGraph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbListFontSize;
        private System.Windows.Forms.CheckBox chkLineCutShow;
        private System.Windows.Forms.Label label5;
    }
}