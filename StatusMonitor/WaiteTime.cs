using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class WaiteTime : Form
    {
        public StatusMonitor.MainForm mainF;
        public string waitTimes = "";
        
        public WaiteTime()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)

        {
            try
            {
                string temp = this.textBox1.Text;
                //check
                if (string.IsNullOrEmpty(temp))
                {
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(temp, @"^\d*$"))
                {
                    return;
                }
                int intTemp = 0;
                intTemp = int.Parse(temp);
                if (intTemp < 1 || intTemp > 9999)
                {
                    return;
                }
                mainF.SetWaitTime(temp);
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void WatiTime_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = waitTimes;
        }
    }
}
