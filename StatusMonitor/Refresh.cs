using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AgentStatusMonitor
{
    public partial class RefreshForm : Form
    {
        private StatusMonitor.LanguageResourceManager res;
        public StatusMonitor.MainForm mainF;
        public RefreshForm()
        {
            InitializeComponent();
        }

        private void RefreshForm_Load(object sender, EventArgs e)
        {
            iniForm();            
            textBox1.Text = mainF.refreshTime.ToString();            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void iniForm()
        {
            try
            {
                res = new StatusMonitor.LanguageResourceManager("JP");
                this.Font = new System.Drawing.Font(res.GetString("SM0000000"), float.Parse(res.GetString("SM0000001")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Byte.Parse("0"));
                this.Text = res.GetString("SM0080001");
                this.label1.Text = res.GetString("SM0080002");
                this.btnSet.Text = res.GetString("SM0080003");
                this.btnClose.Text = res.GetString("SM0080004");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
   

                string strRefresh = textBox1.Text.Trim();
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
                if (intRefresh < 1 || intRefresh > 999)
                {
                    return;
                }
                mainF.setRefresh(strRefresh);
                this.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}