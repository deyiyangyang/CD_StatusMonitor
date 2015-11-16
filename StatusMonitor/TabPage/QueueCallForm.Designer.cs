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
            this.lineStatusListView = new System.Windows.Forms.ListView();
            this.quecallRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // lineStatusListView
            // 
            this.lineStatusListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineStatusListView.FullRowSelect = true;
            this.lineStatusListView.GridLines = true;
            this.lineStatusListView.Location = new System.Drawing.Point(0, 0);
            this.lineStatusListView.MultiSelect = false;
            this.lineStatusListView.Name = "lineStatusListView";
            this.lineStatusListView.Size = new System.Drawing.Size(634, 498);
            this.lineStatusListView.TabIndex = 0;
            this.lineStatusListView.UseCompatibleStateImageBehavior = false;
            this.lineStatusListView.View = System.Windows.Forms.View.Details;
            this.lineStatusListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lineStatusListView_ColumnClick);
            this.lineStatusListView.SelectedIndexChanged += new System.EventHandler(this.NoSelectListView_SelectedIndexChanged);
            this.lineStatusListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lineStatusListView_MouseClick);
            // 
            // quecallRightMenu
            // 
            this.quecallRightMenu.Name = "quecallRightMenu";
            this.quecallRightMenu.Size = new System.Drawing.Size(153, 26);
            // 
            // QueueCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 582);
            this.Controls.Add(this.lineStatusListView);
            this.Name = "QueueCallForm";
            this.Text = "QueueCallForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lineStatusListView;
        private System.Windows.Forms.ContextMenuStrip quecallRightMenu;
    }
}