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
    public partial class frmLineCutSet : Form
    {
        private TksProfileClass _iniProfile;
        public string LineCut;
        public frmLineCutSet(TksProfileClass iniProfile)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            _iniProfile.SelectSection("SVSet");
            LineCut = _iniProfile.GetStringDefault(ConstEntity.LINECUT, string.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LineCut = this.chkShow.Checked ? "1" : "0";
            _iniProfile.SelectSection("SVSet");
            _iniProfile.SetString(ConstEntity.LINECUT, LineCut);
            _iniProfile.Save(MyTool.GetModuleIniPath());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmLineCutSet_Load(object sender, EventArgs e)
        {
            if (LineCut == "0")
            {
                this.chkShow.Checked = false;
            }
            else
            {
                this.chkShow.Checked = true;
            }
        }
    }
}
