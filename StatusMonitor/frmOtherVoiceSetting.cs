using MyTools;
using StatusMonitor.Helper;
using StatusMonitor.SettingFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TksProfileAcxLib;

namespace StatusMonitor
{
    public partial class frmOtherVoiceSetting : Form
    {
        #region play sound
        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, ThrowOnUnmappableChar = true)]
        private static extern bool PlaySound(string szsound, System.IntPtr hmod, playSound flag);

        [System.Flags]
        private enum playSound : int
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

        #endregion
        private TksProfileClass _iniProfile;
        private MainForm MainForm;
        private string VoicePath = "";
        public string helpOnVoice = "";
        public frmOtherVoiceSetting(TksProfileClass iniProfile, MainForm form)
        {
            InitializeComponent();
            _iniProfile = iniProfile;
            this.MainForm = form;
            VoicePath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            VoicePath = VoicePath + "\\Comdesign\\Voice";
        }

        private void btnOpenFile1_Click(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtHelpVoice.Text = "Help_HelpVoice.wav";
                    System.IO.File.Copy(strFile, VoicePath + @"\" + txtHelpVoice.Text, true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }

        private void btnPlay1_Click(object sender, EventArgs e)
        {
            try
            {


                if (!string.IsNullOrEmpty(txtHelpVoice.Text))
                {
                    playVoice(txtHelpVoice.Text);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void frmOtherVoiceSetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(VoicePath))
                {
                    Directory.CreateDirectory(VoicePath);
                }
                _iniProfile.SelectSection("SVSet");
                this.txtHelpVoice.Text = _iniProfile.GetStringDefault(ConstEntity.HelpVoice, string.Empty);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmOtherVoiceSetting:frmOtherVoiceSetting_Load:SystemError:" + ex.Message + ex.StackTrace);
            }
        }

        private void playVoice(string strVoices)
        {
            try
            {
                if (!string.IsNullOrEmpty(strVoices))
                {
                    string path = VoicePath + @"\" + strVoices;
                    if (System.IO.File.Exists(path))
                        PlaySound(path, new IntPtr(), playSound.SND_ASYNC);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SkillQueCallSetForm:playVoice:SystemError:" + ex.Message + ex.StackTrace);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSetting();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SkillQueCallSetForm:btnOK_Click:SystemError:" + ex.Message + ex.StackTrace);
            }
        }

        private void SaveSetting()
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtHelpVoice.Text))
                    return;
                _iniProfile.SelectSection("SVSet");
                _iniProfile.SetString(ConstEntity.HelpVoice, this.txtHelpVoice.Text);

                _iniProfile.Save(MyTool.GetModuleIniPath());

                this.helpOnVoice = this.txtHelpVoice.Text;
                //this.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
