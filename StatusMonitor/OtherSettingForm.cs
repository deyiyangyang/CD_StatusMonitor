using MyTools;
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
    public partial class OtherSettingForm : Form
    {
        private TksProfileClass _iniProfile;
        public string MonitorTab;
        public string MessagePop;
        public OtherSettingForm(TksProfileClass iniProfile)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            _iniProfile.SelectSection("SVSet");
            MonitorTab = _iniProfile.GetStringDefault(ConstEntity.MONITORTAB, string.Empty);
            MessagePop = _iniProfile.GetStringDefault(ConstEntity.MESSAGEPOP, string.Empty);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MonitorTab = this.chkMonitorShow.Checked ? "1" : "0";
            MessagePop = this.chkMessageShow.Checked ? "1" : "0";
            _iniProfile.SelectSection("SVSet");
            _iniProfile.SetString(ConstEntity.MONITORTAB, MonitorTab);
            _iniProfile.SetString(ConstEntity.MESSAGEPOP, MessagePop);
            _iniProfile.Save(MyTool.GetModuleIniPath());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void OtherSettingForm_Load(object sender, EventArgs e)
        {
            if (MonitorTab == "0")
            {
                this.chkMonitorShow.Checked = false;
            }
            else
            {
                this.chkMonitorShow.Checked = true;
            }
            if (MessagePop == "0")
            {
                this.chkMessageShow.Checked = false;
            }
            else
            {
                this.chkMessageShow.Checked = true;
            }
        }
    }
}
