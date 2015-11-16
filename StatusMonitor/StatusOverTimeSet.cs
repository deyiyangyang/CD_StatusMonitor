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
                  
                if (!checkTime(txtTime1)) return;
                if (!checkTime(txtTime2)) return;
                if (!checkTime(txtTime3)) return;
                if (!checkTime(txtTime4)) return;
                if (!checkTime(txtTime5)) return;
                if (!checkTime(txtTime6)) return;
                if (!checkTime(txtTime7)) return;
                if (!checkTime(txtTime8)) return;
                if (!checkTime(txtTime9)) return;
                if (!checkTime(txtTime10)) return;
                //added by zhu 2014/09/11
                if (!checkTime(this.txtQueCallOverTime1)) return;
                if (!checkTime(this.txtQueCallOverTime2)) return;
                //end added

                string time1 = this.txtTime1.Text.Trim();
                string time2 = this.txtTime2.Text.Trim();
                string time3 = this.txtTime3.Text.Trim();
                string time4 = this.txtTime4.Text.Trim();
                string time5 = this.txtTime5.Text.Trim();
                string time6 = this.txtTime6.Text.Trim();
                string time7 = this.txtTime7.Text.Trim();
                string time8 = this.txtTime8.Text.Trim();
                string time9= this.txtTime9.Text.Trim();
                string time10 = this.txtTime10.Text.Trim();
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
                txtTime1.Text = int.Parse(mainF.StatusOverTime1).ToString();
                txtTime2.Text = int.Parse(mainF.StatusOverTime2).ToString();

                txtTime3.Text = int.Parse(mainF.StatusOverTime3).ToString();
                txtTime4.Text = int.Parse(mainF.StatusOverTime4).ToString();

                txtTime5.Text = int.Parse(mainF.StatusOverTime5).ToString();
                txtTime6.Text =int.Parse( mainF.StatusOverTime6).ToString();

                txtTime7.Text = int.Parse(mainF.StatusOverTime7).ToString();
                txtTime8.Text = int.Parse(mainF.StatusOverTime8).ToString();

                txtTime9.Text = int.Parse(mainF.StatusOverTime9).ToString();
                txtTime10.Text = int.Parse(mainF.StatusOverTime10).ToString();

                //added by zhu 2014/09/11
                txtQueCallOverTime1.Text = int.Parse(mainF.QueCallStatusOverTime1).ToString();
                txtQueCallOverTime2.Text = int.Parse(mainF.QueCallStatusOverTime2).ToString();
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
