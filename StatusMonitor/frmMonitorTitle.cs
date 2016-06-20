using StatusMonitor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class frmMonitorTitle : Form
    {
        //deleted by Zhu 2014/05/12
        //public StatusMonitor.MainForm mainF; 
        //end deleted
        public string col1 = "";
        public string col2 = "";
        public string col3 = "";
        public string col4 = "";
        public string col5 = "";
        public string col6 = "";
        public string col7 = "";
        public string col8 = "";
        public string col9 = "";
        public string col10 = "";
        public string col11 = "";
        public string col12 = "";
        public string col13 = "";

        //added by zhu 2014/05/12
        public string col14 = "";
        public string col15 = "";
        public string col16 = "";
        public string col17 = "";
        public string col18 = "";
        public MonitorItemManager MonitorItemManager;
        //end added
        public frmMonitorTitle(MonitorItemManager monitorItemManager)
        {
            InitializeComponent();
            this.MonitorItemManager = monitorItemManager;
        }

        private void frmMonitorTitle_Load(object sender, EventArgs e)
        {
            try
            {
                //deleted by Zhu 2014/05/12
                //this.txtCol1.Text = col1;
                //this.txtCol2.Text = col2;
                //this.txtCol3.Text = col3;
                //this.txtCol4.Text = col4;
                //this.txtCol5.Text = col5;
                //this.txtCol6.Text = col6;
                //this.txtCol7.Text = col7;
                //this.txtCol8.Text = col8;
                //this.txtCol9.Text = col9;
                //this.txtCol10.Text = col10;
                //this.txtCol11.Text = col11;
                //this.txtCol12.Text = col12;
                //this.txtCol13.Text = col13;
                //end deleted

                //added by zhu 2014/05/12

                for (int i = 0; i < MonitorItemManager.MonitorItems.Count; i++)
                {
                    (this.Controls.Find("txtCol" + (i + 1).ToString(), true)[0] as TextBox).Text = MonitorItemManager.MonitorItems[i].DisplayName;
                }


                //end added
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //check
                col1 = txtCol1.Text.Trim();
                col2 = txtCol2.Text.Trim();
                col3 = txtCol3.Text.Trim();
                col4 = txtCol4.Text.Trim();
                col5 = txtCol5.Text.Trim();
                col6 = txtCol6.Text.Trim();
                col7 = txtCol7.Text.Trim();
                //col8 = txtCol8.Text.Trim();
                //col9 = txtCol9.Text.Trim();
                //col10 = txtCol10.Text.Trim();
                //col11 = txtCol11.Text.Trim();
                //col12 = txtCol12.Text.Trim();
                //col13 = txtCol13.Text.Trim();
                //col14 = txtCol14.Text.Trim();

                //col15 = txtCol15.Text.Trim();
                //col16 = txtCol16.Text.Trim();
                //col17 = txtCol17.Text.Trim();
                //col18 = txtCol18.Text.Trim();

                if (string.IsNullOrEmpty(col1)) return;
                if (string.IsNullOrEmpty(col2)) return;
                if (string.IsNullOrEmpty(col3)) return;
                if (string.IsNullOrEmpty(col4)) return;
                if (string.IsNullOrEmpty(col5)) return;
                if (string.IsNullOrEmpty(col6)) return;
                if (string.IsNullOrEmpty(col7)) return;
                //if (string.IsNullOrEmpty(col8)) return;
                //if (string.IsNullOrEmpty(col9)) return;
                //if (string.IsNullOrEmpty(col10)) return;
                //if (string.IsNullOrEmpty(col11)) return;
                //if (string.IsNullOrEmpty(col12)) return;
                //if (string.IsNullOrEmpty(col13)) return;
                //if (string.IsNullOrEmpty(col14)) return;
                //if (string.IsNullOrEmpty(col15)) return;
                //if (string.IsNullOrEmpty(col16)) return;
                //if (string.IsNullOrEmpty(col17)) return;
                //if (string.IsNullOrEmpty(col18)) return;
                //set
                //deleted by zhu 2014/05/12
                //mainF.setMonitorCol(col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11, col12, col13);
                //end deleted

                //added by zhu 2014/05/12
                MonitorItemManager.MonitorItems[0].DisplayName = col1;
                MonitorItemManager.MonitorItems[1].DisplayName = col2;
                MonitorItemManager.MonitorItems[2].DisplayName = col3;
                MonitorItemManager.MonitorItems[3].DisplayName = col4;
                MonitorItemManager.MonitorItems[4].DisplayName = col5;
                MonitorItemManager.MonitorItems[5].DisplayName = col6;
                MonitorItemManager.MonitorItems[6].DisplayName = col7;
                //MonitorItemManager.MonitorItems[7].DisplayName = col8;
                //MonitorItemManager.MonitorItems[8].DisplayName = col9;
                //MonitorItemManager.MonitorItems[9].DisplayName = col10;
                //MonitorItemManager.MonitorItems[10].DisplayName = col11;
                //MonitorItemManager.MonitorItems[11].DisplayName = col12;
                //MonitorItemManager.MonitorItems[12].DisplayName = col13;
                //MonitorItemManager.MonitorItems[13].DisplayName = col14;
                //MonitorItemManager.MonitorItems[14].DisplayName = col15;
                //MonitorItemManager.MonitorItems[15].DisplayName = col16;
                //MonitorItemManager.MonitorItems[16].DisplayName = col17;
                //MonitorItemManager.MonitorItems[17].DisplayName = col18;
                MonitorItemManager.SaveData();
                //end added
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
