using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class frmSetPhone : Form
    {
        private LanguageResourceManager res;
        public MainForm mainF;
        public frmSetPhone()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string phone = "";
                phone = textBox1.Text;
                //check
                if (string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show(res.GetString("SM9990001"), this.Text);
                    textBox1.Focus();
                    return;
                }
                System.Text.RegularExpressions.Regex reg1;
                //reg1 = new System.Text.RegularExpressions.Regex(@"^\d*$");
                reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
                if (!reg1.IsMatch(phone))
                {
                    MessageBox.Show(res.GetString("SM9990002"), this.Text);
                    textBox1.Focus();
                    return;
                }
                
                //save
                mainF.setPhone(phone);
                this.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmSetPhone_Load(object sender, EventArgs e)
        {
            iniForm();
            if (mainF.SVPhone.Length > 0)
                textBox1.Text = mainF.SVPhone;
            else
                textBox1.Text = "";
        }
        private void iniForm()
        {
            try
            {
                res = new LanguageResourceManager("JP");
                this.Font = new System.Drawing.Font(res.GetString("SM0000000"), float.Parse(res.GetString("SM0000001")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Byte.Parse("0"));
                this.Text = res.GetString("SM0010001");
                this.label1.Text = res.GetString("SM0010002");
                this.button1.Text = res.GetString("SM0010003");
                this.button2.Text = res.GetString("SM0010004");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}