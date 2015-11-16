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
    public partial class KyoKuGroupForm : Form
    {
        private TksProfileClass _iniProfile;
        public string _keyvalue;
        public string KyoKuGroupInfo;
        public KyoKuGroupForm(string groupInfoFromServer,TksProfileClass iniProfile)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            _iniProfile.SelectSection("SVSet");
            _keyvalue = _iniProfile.GetStringDefault(ConstEntity.KYOKUGROUPSHOWKEY, string.Empty);
            KyoKuGroupInfo = groupInfoFromServer;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _keyvalue = string.Empty;
            foreach (Control c in this.plGroupShow.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                {
                    _keyvalue += ";" + c.Name + "," + ((c as CheckBox).Checked ? "1" : "0");
                }
            }

            //if (_keyvalue.Length > 1) _keyvalue = _keyvalue.Substring(1);
            _iniProfile.SelectSection("SVSet");
            _iniProfile.SetString(ConstEntity.KYOKUGROUPSHOWKEY, _keyvalue);
            _iniProfile.Save(MyTool.GetModuleIniPath());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void KyoKuGroupForm_Load(object sender, EventArgs e)
        {
            int index = 0;
            int offsetHeight = 25;
            int offsetLeft = 120;

            string[] rows = KyoKuGroupInfo.Split(';');
            for (int i = 0; i < rows.Length; i++)
            {
                if (!string.IsNullOrEmpty(rows[i]))
                {
                    string[] cols = rows[i].Split(',');
                    if (cols.Length == 2)
                    {
                        index++;
                        CheckBox chk = new CheckBox();
                        chk.Name = cols[0].ToString();
                        chk.Text = cols[1].ToString();
                        chk.Location = new System.Drawing.Point(12, 12 * index + (index - 1) * offsetHeight);
                        chk.Size = new System.Drawing.Size(280, 25);
                        chk.Checked = GetCheckedStatus(chk.Name);
                        this.plGroupShow.Controls.Add(chk);
                    }
                }
            }
        }

        private bool GetCheckedStatus(string groupID)
        {
            if (string.IsNullOrEmpty(_keyvalue)) return true;
            int start = _keyvalue.IndexOf(";"+groupID+",1");
            if (start >= 0)
            {
                return true;
            }

            return false ;
        }

    }
}
