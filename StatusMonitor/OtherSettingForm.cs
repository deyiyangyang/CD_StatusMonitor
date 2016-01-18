using MyTools;
using StatusMonitor.Helper;
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
        //public string MonitorTab;
        public string MessagePop;
        public string AgentGraphShow;
        public int ListFontSize;
        public bool FontSizeChanged = false;
        public OtherSettingForm(TksProfileClass iniProfile)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            _iniProfile.SelectSection("SVSet");
            //MonitorTab = _iniProfile.GetStringDefault(ConstEntity.MONITORTAB, string.Empty);
            MessagePop = _iniProfile.GetStringDefault(ConstEntity.MESSAGEPOP, string.Empty);
            AgentGraphShow = _iniProfile.GetStringDefault(ConstEntity.AGENTGRAPH, string.Empty);
            ListFontSize = _iniProfile.GetLongDefault(ConstEntity.LISTFONTSIZE, 1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //MonitorTab = this.chkMonitorShow.Checked ? "1" : "0";
            MessagePop = this.chkMessageShow.Checked ? "1" : "0";
            AgentGraphShow = this.chkAgentGraph.Checked ? "1" : "0";
            _iniProfile.SelectSection("SVSet");
            //_iniProfile.SetString(ConstEntity.MONITORTAB, MonitorTab);
            _iniProfile.SetString(ConstEntity.MESSAGEPOP, MessagePop);
            _iniProfile.SetString(ConstEntity.AGENTGRAPH, AgentGraphShow);
            int currentIndex = this.cmbListFontSize.SelectedIndex;
            currentIndex++;
            if (ListFontSize != currentIndex)
            {
                FontSizeChanged = true;
            }
            ListFontSize = currentIndex;
            _iniProfile.SetLong(ConstEntity.LISTFONTSIZE, ListFontSize);
            _iniProfile.Save(MyTool.GetModuleIniPath());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void OtherSettingForm_Load(object sender, EventArgs e)
        {
            //if (MonitorTab == "0")
            //{
            //    this.chkMonitorShow.Checked = false;
            //}
            //else
            //{
            //    this.chkMonitorShow.Checked = true;
            //}
            if (MessagePop == "0")
            {
                this.chkMessageShow.Checked = false;
            }
            else
            {
                this.chkMessageShow.Checked = true;
            }
            if (AgentGraphShow == "1")
            {
                this.chkAgentGraph.Checked = true;
            }
            else
            {
                this.chkAgentGraph.Checked = false;
            }
            InitCmbListFontSize();
            this.cmbListFontSize.SelectedIndex = ListFontSize - 1;
        }

        private void InitCmbListFontSize()
        {
            this.cmbListFontSize.Items.Add(new ComboBoxItem { Value = 1, Text = "100%" });
            this.cmbListFontSize.Items.Add(new ComboBoxItem { Value = 2, Text = "200%" });
            this.cmbListFontSize.Items.Add(new ComboBoxItem { Value = 3, Text = "300%" });
        }
    }
}
