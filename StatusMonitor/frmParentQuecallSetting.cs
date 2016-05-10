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
    public partial class frmParentQuecallSetting : Form
    {

        private TksProfileClass _iniProfile;
  
        public string _keyvalue;
        public MainForm MainForm;
        public frmParentQuecallSetting(TksProfileClass iniProfile)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            _iniProfile.SelectSection("SVSet");
            //_keyvalue = _iniProfile.GetStringDefault(ConstEntity.SKILLSHOWKEY, string.Empty);
        }

        private void frmParentQuecallSetting_Load(object sender, EventArgs e)
        {
            int index = 0;
            int offsetHeight = 25;
            int offsetLeft = 120;
            foreach (var item in MainForm.DicParentGroup)
            {
                var parentGroupName = item.Key.Split(',')[1];
                var parentGroupID = item.Key.Split(',')[0];
                var groupIds = item.Value;
                if (item.Value == "-1") continue;
                index++;
                Label label = new Label();
                label.Name = parentGroupID;
                label.Text = parentGroupName;
                label.Location = new System.Drawing.Point(12, 12 * index + (index - 1) * offsetHeight);
                label.Size = new System.Drawing.Size(180, 25);
                //chk.Checked = GetCheckedStatus(chk.Name);
                //chk.Click += chk_Click;

                Button btn = new Button();
                btn.Name = "btnSkillQuecall_" + parentGroupID;
                btn.Text = "待ち呼警告設定";
                btn.Location = new System.Drawing.Point(200, 12 * index + (index - 1) * offsetHeight);
                btn.Size = new System.Drawing.Size(100, 25);
                btn.Click += btnQueCallSetting_Click;
                this.plSkillShow.Controls.Add(label);
                this.plSkillShow.Controls.Add(btn);
            }
        }

        private void btnQueCallSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    Button btn = (Button)sender;
                    string parentGroupID = btn.Name.Substring(btn.Name.LastIndexOf('_') + 1);
                    frmParentGroupQueCallSetForm quecallSet = new frmParentGroupQueCallSetForm(parentGroupID, _iniProfile, MainForm);
                    quecallSet.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MainForm.writeLog("SkillShowSet:btnQueCallSetting_Click:system error:" + ex.Message + ex.StackTrace);
            }
        }

        //private void chk_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (sender is CheckBox)
        //        {
        //            CheckBox chk = (CheckBox)sender;
        //            if (chk.Checked)
        //            {
        //                (this.plSkillShow.Controls.Find("btnSkillQuecall_" + chk.Name, false)[0] as Button).Enabled = true;
        //            }
        //            else
        //            {
        //                (this.plSkillShow.Controls.Find("btnSkillQuecall_" + chk.Name, false)[0] as Button).Enabled = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MainForm.writeLog("SkillShowSet:chk_Click:system error:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private bool GetCheckedStatus(string groupID)
        //{
        //    if (string.IsNullOrEmpty(_keyvalue)) return true;
        //    int start = _keyvalue.IndexOf(groupID);
        //    if (start >= 0)
        //    {
        //        int end = _keyvalue.IndexOf('|', start);
        //        if (end <= 0) end = _keyvalue.Length;
        //        string result = _keyvalue.Substring(start, end - start).Split(':')[1];
        //        if (result == "1") return true;
        //        else if (result == "0") return false;
        //    }

        //    return false;
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            //_keyvalue = string.Empty;
            //foreach (Control c in this.plSkillShow.Controls)
            //{
            //    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
            //    {

            //        _keyvalue += "|" + c.Name + ":" + ((c as CheckBox).Checked ? "1" : "0");
            //    }
            //}

            //if (_keyvalue.Length > 1) _keyvalue = _keyvalue.Substring(1);
            //_iniProfile.SelectSection("SVSet");
            //_iniProfile.SetString(ConstEntity.SKILLSHOWKEY, _keyvalue);
            //_iniProfile.Save(MyTool.GetModuleIniPath());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
