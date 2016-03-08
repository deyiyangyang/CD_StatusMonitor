using MyTools;
using StatusMonitor.Helper;
using StatusMonitor.Model;
using StatusMonitor.SettingFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TksProfileAcxLib;

namespace StatusMonitor
{
    public partial class MonitorItemSet : Form
    {
        public MonitorItemManager MonitorItemManager;
        public MonitorItemSet(MonitorItemManager monitorItemManager)
        {
            InitializeComponent();
            MonitorItemManager = monitorItemManager;
        }

        private void MonitorItemSet_Shown(object sender, EventArgs e)
        {
            try
            {
                if (MonitorItemManager != null && MonitorItemManager.MonitorItems.Count > 0)
                {
                    for (int i = 1; i <= MonitorItemManager.MonitorItems.Count; i++)
                    {
                        (this.Controls.Find("chbMonitorCol" + i.ToString(), true)[0] as CheckBox).Checked = MonitorItemManager.MonitorItems[i - 1].Visible;
                        (this.Controls.Find("chbMonitorCol" + i.ToString(), true)[0] as CheckBox).Text = MonitorItemManager.MonitorItems[i - 1].DisplayName;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("MonitorItemSet_Shown system error:" + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                for (int i = 1; i <= ConstEntity.MANAGEMONITORITEMCOUNT; i++)
                {
                    if ((this.Controls.Find("chbMonitorCol" + i.ToString(), true)[0] as CheckBox).Checked)
                    {
                        MonitorItemManager.MonitorItems[i - 1].Visible = true;
                    }
                    else
                    {
                        MonitorItemManager.MonitorItems[i - 1].Visible = false;
                    }

                }
                MonitorItemManager.SaveData();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnOK_Click system error:" + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MonitorItemSet_Load(object sender, EventArgs e)
        {

        }
    }
}
