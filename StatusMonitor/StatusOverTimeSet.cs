using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    
    public partial class StatusOverTimeSet : Form
    {
        public MainForm mainF;
        public StatusOverTimeSet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                  
                if (!checkTime(txtIdleOverTime1)) return;
                if (!checkTime(txtIdleOverTime2)) return;
                if (!checkTime(txtWorkTimeOVerTime1)) return;
                if (!checkTime(txtWorkTimeOverTime2)) return;
                if (!checkTime(txtLeaveOverTime1)) return;
                if (!checkTime(txtLeaveOverTime2)) return;
                if (!checkTime(txtTalkOverTime1)) return;
                if (!checkTime(txtTalkOverTime2)) return;
                if (!checkTime(txtHoldOverTime1)) return;
                if (!checkTime(txtHoldOverTime2)) return;
                //added by zhu 2014/09/11
                if (!checkTime(this.txtQueCallOverTime1)) return;
                if (!checkTime(this.txtQueCallOverTime2)) return;
                //end added

                string time1 = this.txtIdleOverTime1.Text.Trim();
                string time2 = this.txtIdleOverTime2.Text.Trim();
                string time3 = this.txtWorkTimeOVerTime1.Text.Trim();
                string time4 = this.txtWorkTimeOverTime2.Text.Trim();
                string time5 = this.txtLeaveOverTime1.Text.Trim();
                string time6 = this.txtLeaveOverTime2.Text.Trim();
                string time7 = this.txtTalkOverTime1.Text.Trim();
                string time8 = this.txtTalkOverTime2.Text.Trim();
                string time9= this.txtHoldOverTime1.Text.Trim();
                string time10 = this.txtHoldOverTime2.Text.Trim();
                //added by zhu 2014/09/11
                string time11 = this.txtQueCallOverTime1.Text.Trim();
                string time12 = this.txtQueCallOverTime2.Text.Trim();
                //end added

                //modified by zhu 2014/09/11 and extra two pramaters time11,time12
                mainF.setStatusOverTimes(time1, time2, time3, time4, time5, time6, time7, time8, time9, time10, time11, time12);
                this.Dispose();
            }
            catch 
            {
                
            }
            
            
        }

        private bool checkTime( TextBox txt)
        {
            string time1 = txt.Text.Trim() ;
            if (string.IsNullOrEmpty(time1)) return true;
            if (!IsNumeric(time1))
            {
                MessageBox.Show("超過時間を正しく設定してください。", this.Text);
                txt.Focus();
                return false;
            }
            //if (int.Parse(time1) > 60)
            //{
            //    MessageBox.Show("超過時間を60以下設定してください。", this.Text);
            //    txt.Focus();
            //    return false;
            //}
            return true;
        }
        private void StatusOverTimeSet_Load(object sender, EventArgs e)
        {
            try
            {
                txtIdleOverTime1.Text = int.Parse(mainF.SettingFields_StatusOverIdelTime1).ToString();
                txtIdleOverTime2.Text = int.Parse(mainF.SettingFields_StatusOverIdelTime2).ToString();

                txtWorkTimeOVerTime1.Text = int.Parse(mainF.SettingFields_StatusOverWorkTime1).ToString();
                txtWorkTimeOverTime2.Text = int.Parse(mainF.SettingFields_StatusOverWorkTime2).ToString();

                txtLeaveOverTime1.Text = int.Parse(mainF.SettingFields_StatusOverLeaveTime1).ToString();
                txtLeaveOverTime2.Text =int.Parse( mainF.SettingFields_StatusOverLeaveTime2).ToString();

                txtTalkOverTime1.Text = int.Parse(mainF.SettingFields_StatusOverTalkTime1).ToString();
                txtTalkOverTime2.Text = int.Parse(mainF.SettingFields_StatusOverTalkTime2).ToString();

                txtHoldOverTime1.Text = int.Parse(mainF.SettingFields_StatusOverHoldTime1).ToString();
                txtHoldOverTime2.Text = int.Parse(mainF.SettingFields_StatusOverHoldTime2).ToString();

                //added by zhu 2014/09/11
                txtQueCallOverTime1.Text = int.Parse(mainF.SettingFields_StatusOverQuecallTime1).ToString();
                txtQueCallOverTime2.Text = int.Parse(mainF.SettingFields_StatusOverQuecallTime2).ToString();
                //end added
            }
            catch 
            {
            }
        }

        private bool IsNumeric(string tmp)
        {
            string pattrn = @"[0-9]+";
            if (System.Text.RegularExpressions.Regex.IsMatch(tmp, pattrn))
            {
                return true;
            }
            else
            {
                return false;

            }
        }



     
       
    }
}
