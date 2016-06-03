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
    public partial class QuickAnswerSet : Form
    {
        private TksProfileClass _iniProfile;
        private StatusMonitor.MainForm mainF;
        public QuickAnswerSet(TksProfileClass iniProfile, MainForm form)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            mainF = form;
        }


        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                _iniProfile.SelectSection("SVSet");
                if (CheckInput(this.txtMinutes.Text))
                {
                    _iniProfile.SetString(ConstEntity.QuickAnswerSeconds1, this.txtMinutes.Text);
                    this.mainF.QuickAnswerMinutes = this.txtMinutes.Text;
                }               
                if (CheckInput(this.txtAnswerSecond2.Text))
                {
                    _iniProfile.SetString(ConstEntity.QuickAnswerSeconds2, this.txtAnswerSecond2.Text);
                    this.mainF.SettingFields_QuickAnswerSeconds2 = this.txtAnswerSecond2.Text;
                }
                    
                if (CheckInput(this.txtAnswerSecond3.Text))
                {
                    _iniProfile.SetString(ConstEntity.QuickAnswerSeconds3, this.txtAnswerSecond3.Text);
                    this.mainF.SettingFields_QuickAnswerSeconds3 = this.txtAnswerSecond3.Text;
                }
                                
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckInput(string strRefresh)
        {
            if (string.IsNullOrEmpty(strRefresh))
            {
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(strRefresh, @"^\d*$"))
            {
                return false;
            }
            int intRefresh = 0;
            intRefresh = int.Parse(strRefresh);
            if (intRefresh < 0 || intRefresh > 99)
            {
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void QuickAnswerSet_Load(object sender, EventArgs e)
        {

            try
            {
                _iniProfile.SelectSection("SVSet");
                //txtMinutes.Text = int.Parse(mainF.QuickAnswerMinutes).ToString();
                txtMinutes.Text= _iniProfile.GetStringDefault(ConstEntity.QuickAnswerSeconds1, "0");
                this.txtAnswerSecond2.Text = _iniProfile.GetStringDefault(ConstEntity.QuickAnswerSeconds2, "0");
                this.txtAnswerSecond3.Text = _iniProfile.GetStringDefault(ConstEntity.QuickAnswerSeconds3, "0");
            }
            catch (Exception ex)
            {

            }
        }

        private void txtAnswerSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAnswerSecond_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Name == "txtMinutes")
            {
                this.txtAnswerSecond2.Enabled = true;
            }
            if ((sender as TextBox).Name == "txtAnswerSecond2")
            {
                this.txtAnswerSecond3.Enabled = true;
            }
        }
    }
}
