namespace StatusMonitor
{
    partial class AgentState
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ra1 = new System.Windows.Forms.RadioButton();
            this.ra2 = new System.Windows.Forms.RadioButton();
            this.ra3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(89, 67);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(46, 22);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(142, 67);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 21);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ra1
            // 
            this.ra1.AutoSize = true;
            this.ra1.Location = new System.Drawing.Point(10, 38);
            this.ra1.Name = "ra1";
            this.ra1.Size = new System.Drawing.Size(59, 16);
            this.ra1.TabIndex = 2;
            this.ra1.TabStop = true;
            this.ra1.Text = "受付可";
            this.ra1.UseVisualStyleBackColor = true;
            // 
            // ra2
            // 
            this.ra2.AutoSize = true;
            this.ra2.Location = new System.Drawing.Point(76, 38);
            this.ra2.Name = "ra2";
            this.ra2.Size = new System.Drawing.Size(59, 16);
            this.ra2.TabIndex = 3;
            this.ra2.TabStop = true;
            this.ra2.Text = "後処理";
            this.ra2.UseVisualStyleBackColor = true;
            // 
            // ra3
            // 
            this.ra3.AutoSize = true;
            this.ra3.Location = new System.Drawing.Point(142, 38);
            this.ra3.Name = "ra3";
            this.ra3.Size = new System.Drawing.Size(47, 16);
            this.ra3.TabIndex = 4;
            this.ra3.TabStop = true;
            this.ra3.Text = "離席";
            this.ra3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // AgentState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 98);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ra3);
            this.Controls.Add(this.ra2);
            this.Controls.Add(this.ra1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgentState";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "エージェントステータス";
            this.Load += new System.EventHandler(this.AgentState_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton ra1;
        private System.Windows.Forms.RadioButton ra2;
        private System.Windows.Forms.RadioButton ra3;
        private System.Windows.Forms.Label label1;
    }
}