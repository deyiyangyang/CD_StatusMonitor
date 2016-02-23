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
            this.SuspendLayout();
            // 
            // lineStatusListView
            // 
            this.quecallStatusListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.quecallStatusListView.FullRowSelect = true;
            this.quecallStatusListView.GridLines = true;
            this.quecallStatusListView.Location = new System.Drawing.Point(0, 0);
            this.quecallStatusListView.MultiSelect = false;
            this.quecallStatusListView.Name = "quecallStatusListView";
            this.quecallStatusListView.Size = new System.Drawing.Size(634, 498);
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
            // QueueCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 582);
            this.Controls.Add(this.quecallStatusListView);
            this.Name = "QueueCallForm";
            this.Text = "QueueCallForm";
            this.Load += new System.EventHandler(this.QueueCallForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView quecallStatusListView;
        private System.Windows.Forms.ContextMenuStrip quecallRightMenu;
    }
}