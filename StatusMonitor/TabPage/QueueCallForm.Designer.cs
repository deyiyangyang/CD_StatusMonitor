namespace StatusMonitor.TabPage
{
    partial class QueueCallForm
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
            this.components = new System.ComponentModel.Container();
            this.quecallStatusListView = new System.Windows.Forms.ListView();
            this.quecallRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.quecallDDLParentGroup = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // quecallStatusListView
            // 
            this.quecallStatusListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quecallStatusListView.FullRowSelect = true;
            this.quecallStatusListView.GridLines = true;
            this.quecallStatusListView.Location = new System.Drawing.Point(0, 38);
            this.quecallStatusListView.MultiSelect = false;
            this.quecallStatusListView.Name = "quecallStatusListView";
            this.quecallStatusListView.Size = new System.Drawing.Size(675, 544);
            this.quecallStatusListView.TabIndex = 0;
            this.quecallStatusListView.UseCompatibleStateImageBehavior = false;
            this.quecallStatusListView.View = System.Windows.Forms.View.Details;
            this.quecallStatusListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lineStatusListView_ColumnClick);
            this.quecallStatusListView.SelectedIndexChanged += new System.EventHandler(this.NoSelectListView_SelectedIndexChanged);
            this.quecallStatusListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lineStatusListView_MouseClick);
            // 
            // quecallRightMenu
            // 
            this.quecallRightMenu.Name = "quecallRightMenu";
            this.quecallRightMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "親グループ";
            // 
            // quecallDDLParentGroup
            // 
            this.quecallDDLParentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quecallDDLParentGroup.DropDownWidth = 200;
            this.quecallDDLParentGroup.FormattingEnabled = true;
            this.quecallDDLParentGroup.Location = new System.Drawing.Point(59, 12);
            this.quecallDDLParentGroup.Name = "quecallDDLParentGroup";
            this.quecallDDLParentGroup.Size = new System.Drawing.Size(200, 20);
            this.quecallDDLParentGroup.TabIndex = 14;
            this.quecallDDLParentGroup.SelectedIndexChanged += new System.EventHandler(this.quecallDDLParentGroup_SelectedIndexChanged);
            // 
            // QueueCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(675, 582);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.quecallDDLParentGroup);
            this.Controls.Add(this.quecallStatusListView);
            this.Name = "QueueCallForm";
            this.Text = "QueueCallForm";
            this.Load += new System.EventHandler(this.QueueCallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView quecallStatusListView;
        private System.Windows.Forms.ContextMenuStrip quecallRightMenu;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox quecallDDLParentGroup;
    }
}