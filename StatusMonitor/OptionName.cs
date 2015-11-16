using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class OptionName : Form
    {
        public StatusMonitor.MainForm mainF;
        public string Option1 = "";
        public string Option2 = "";
        public string Option3 = "";
        public string Option4 = "";
        public string Option5 = "";
        public OptionName()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            mainF.setOptionName(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim());
            this.Dispose();
        }

        private void OptionName_Load(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Text = Option1;
                this.textBox2.Text = Option2;
                this.textBox3.Text = Option3;
                this.textBox4.Text = Option4;
                this.textBox5.Text = Option5;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
