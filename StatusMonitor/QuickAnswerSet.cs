using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class QuickAnswerSet : Form
    {
        public StatusMonitor.MainForm mainF;
        public QuickAnswerSet()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                string strRefresh = txtMinutes.Text.Trim();
                if (string.IsNullOrEmpty(strRefresh))
                {
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(strRefresh, @"^\d*$"))
                {
                    return;
                }
                int intRefresh = 0;
                intRefresh = int.Parse(strRefresh);
                if (intRefresh < 0 || intRefresh > 99)
                {
                    return;
                }
                mainF.setQuickAnswerMinutes(intRefresh.ToString());
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

        private void QuickAnswerSet_Load(object sender, EventArgs e)
        {

            try
            {
                txtMinutes.Text = int.Parse(mainF.QuickAnswerMinutes).ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
