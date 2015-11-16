using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class QueCallSet : Form
    {
        public StatusMonitor.MainForm mainF;
        public string Period1 = "";
        public string PeriodVoice1 = "";
        public string Period2 = "";
        public string PeriodVoice2 = "";
        public string Period3 = "";
        public string PeriodVoice3 = "";

        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, ThrowOnUnmappableChar = true)]
        private static extern bool PlaySound(string szsound, System.IntPtr hmod, playSound flag);

        [System.Flags]
        private  enum playSound : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        public QueCallSet()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //check input
                string strPeriod1 = "";
                string strPeriod2 = "";
                string strPeriod3 = "";
                string strVoice1 = "";
                string strVoice2 = "";
                string strVoice3 = "";

                strVoice1 = txtVoice1.Text;
                strVoice2 = txtVoice2.Text;
                strVoice3 = txtVoice3.Text;

                strPeriod1 = txtPeriod1.Text;
                if (!string.IsNullOrEmpty(strPeriod1))
                {
                    if (!checkNumeric(strPeriod1))
                    {
                        //MessageBox.Show("","状態モニタ");
                        txtPeriod1.Focus();
                        return;
                    }
                }

               
                strPeriod2 = txtPeriod2.Text;
                if (!string.IsNullOrEmpty(strPeriod2))
                {
                    if (!checkNumeric(strPeriod2))
                    {
                        //MessageBox.Show("","状態モニタ");
                        txtPeriod2.Focus();
                        return;
                    }

                    if (int.Parse(strPeriod1) >= int.Parse(strPeriod2))
                    {
                        txtPeriod2.Focus();
                        return;
                    }
                }
               

                strPeriod3 = txtPeriod3.Text;
                if (!string.IsNullOrEmpty(strPeriod3))
                {
                    if (!checkNumeric(strPeriod3))
                    {
                        //MessageBox.Show("","状態モニタ");
                        txtPeriod3.Focus();
                        return;
                    }
                    if (!string.IsNullOrEmpty(strPeriod2))
                    {
                        if (int.Parse(strPeriod2) >= int.Parse(strPeriod3))
                        {
                            txtPeriod3.Focus();
                            return;
                        }
                    }
                    else
                    {
                        strPeriod3 = "";
                    }
                }

               
                //SetQueCall
                mainF.SetQueCall(strPeriod1, strVoice1, strPeriod2, strVoice2, strPeriod3, strVoice3);
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        private bool checkNumeric(string strIn)
        {
            try
            {
                if (string.IsNullOrEmpty(strIn)) return false;
                if (!System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^\d*$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void playVoice(string strVoices)
        {
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";

                if (!string.IsNullOrEmpty(strVoices))
                {
                    path = path + @"\" + strVoices;
                    if (System.IO.File.Exists(path))
                        PlaySound(path, new IntPtr(), playSound.SND_ASYNC);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void btnPlay1_Click(object sender, EventArgs e)
        {
            try
            {
            

                if (!string.IsNullOrEmpty(txtVoice1.Text))
                {
                    playVoice(txtVoice1.Text);
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void btnPlay2_Click(object sender, EventArgs e)
        {
            try
            {


                if (!string.IsNullOrEmpty(txtVoice2.Text))
                {
                    playVoice(txtVoice2.Text);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnPlay3_Click(object sender, EventArgs e)
        {
            try
            {


                if (!string.IsNullOrEmpty(txtVoice3.Text))
                {
                    playVoice(txtVoice3.Text);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void QueCallSet_Load(object sender, EventArgs e)
        {
            try
            {
                txtPeriod1.Text = Period1;
                txtPeriod2.Text = Period2;
                txtPeriod3.Text = Period3;

                txtVoice1.Text = PeriodVoice1;
                txtVoice2.Text = PeriodVoice2;
                txtVoice3.Text = PeriodVoice3;
                ofdVoice.Filter = "(*.wav)|*.wav";

            }
            catch (Exception ex)
            {

            }
        }

        private void btnOpenFile1_Click(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";

                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice1.Text = "QueCallVoice1.wav";
                    System.IO.File.Copy(strFile, path + @"\QueCallVoice1.wav", true);

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }

        private void btnOpenFile2_Click(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";

                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice2.Text = "QueCallVoice2.wav";
                    System.IO.File.Copy(strFile, path + @"\QueCallVoice2.wav", true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }

        private void btnOpenFile3_Click(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";

                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice3.Text = "QueCallVoice3.wav";
                    System.IO.File.Copy(strFile, path + @"\QueCallVoice3.wav", true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }
    }
}
