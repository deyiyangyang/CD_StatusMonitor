﻿using MyTools;
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
    public partial class SkillQueCallSetForm : Form
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
        private string SettingFields_SkillQueCallPeriod1 = "";
        private string SettingFields_SkillQueCallPeriod2 = "";
        private string SettingFields_SkillQueCallPeriod3 = "";
        private string SettingFields_SkillQueCallVoice1 = "";
        private string SettingFields_SkillQueCallVoice2 = "";
        private string SettingFields_SkillQueCallVoice3 = "";
        private string SkillGroupID = "";
        private MainForm MainForm;
        private string VoicePath = "";
        public SkillQueCallSetForm(string skillGroupID, TksProfileClass iniProfile, MainForm form)
        {
            InitializeComponent();
            SkillGroupID = skillGroupID;
            _iniProfile = iniProfile;
            this.MainForm = form;
            VoicePath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            VoicePath = VoicePath + "\\Comdesign\\Voice";
        }

        /// <summary>
        /// save setting value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSetting();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SkillQueCallSetForm:btnOK_Click:SystemError:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkillQueCallSetForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(VoicePath))
                {
                    Directory.CreateDirectory(VoicePath);
                }
                LoadDefalutValue();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SkillQueCallSetForm:SkillQueCallSetForm_Load:SystemError:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// load init file value
        /// </summary>
        private void LoadDefalutValue()
        {
            try
            {
                _iniProfile.SelectSection("SVSet");
                SettingFields_SkillQueCallPeriod1 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate1, SkillGroupID), string.Empty);
                SettingFields_SkillQueCallPeriod2 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate2, SkillGroupID), string.Empty);
                SettingFields_SkillQueCallPeriod3 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate3, SkillGroupID), string.Empty);
                SettingFields_SkillQueCallVoice1 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate1, SkillGroupID), string.Empty);
                SettingFields_SkillQueCallVoice2 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate2, SkillGroupID), string.Empty);
                SettingFields_SkillQueCallVoice3 = _iniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate3, SkillGroupID), string.Empty);
                this.txtPeriod1.Text = SettingFields_SkillQueCallPeriod1;
                this.txtPeriod2.Text = SettingFields_SkillQueCallPeriod2;
                this.txtPeriod3.Text = SettingFields_SkillQueCallPeriod3;

                this.txtVoice1.Text = SettingFields_SkillQueCallVoice1;
                this.txtVoice2.Text = SettingFields_SkillQueCallVoice2;
                this.txtVoice3.Text = SettingFields_SkillQueCallVoice3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// save to ini file
        /// </summary>
        private void SaveSetting()
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
                strPeriod2 = txtPeriod2.Text;
                strPeriod3 = txtPeriod3.Text;

                if (string.IsNullOrEmpty(strPeriod1) && string.IsNullOrEmpty(strPeriod2) && string.IsNullOrEmpty(strPeriod3))
                {
                    strVoice1 = "";
                    strVoice2 = "";
                    strVoice3 = "";
                }
                else
                {

                    if (string.IsNullOrEmpty(strPeriod1))
                    {
                        txtPeriod1.Focus();
                        return;
                    }

                    if (!string.IsNullOrEmpty(strPeriod1))
                    {
                        if (!checkNumeric(strPeriod1))
                        {
                            txtPeriod1.Focus();
                            return;
                        }
                    }


                    if (!string.IsNullOrEmpty(strPeriod2))
                    {
                        if (!checkNumeric(strPeriod2))
                        {
                            txtPeriod2.Focus();
                            return;
                        }

                        if (int.Parse(strPeriod1) >= int.Parse(strPeriod2))
                        {
                            txtPeriod2.Focus();
                            return;
                        }
                    }


                    if (!string.IsNullOrEmpty(strPeriod3))
                    {
                        if (!checkNumeric(strPeriod3))
                        {
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
                            txtPeriod2.Focus();
                            strPeriod3 = "";
                            return;
                        }
                    }
                }
                if (!CheckInput()) return;
                _iniProfile.SelectSection("SVSet");
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallPeriodTemplate1, SkillGroupID), strPeriod1);
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallPeriodTemplate2, SkillGroupID), strPeriod2);
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallPeriodTemplate3, SkillGroupID), strPeriod3);
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallVoiceTemplate1, SkillGroupID), strVoice1);
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallVoiceTemplate2, SkillGroupID), strVoice2);
                _iniProfile.SetString(string.Format(ConstEntity.SkillQueCallVoiceTemplate3, SkillGroupID), strVoice3);

                _iniProfile.Save(MyTool.GetModuleIniPath());

                //SetQueCall
                string value = strPeriod1 + "," + strVoice1 + "|" + strPeriod2 + "," + strVoice2 + "|" + strPeriod3 + "," + strVoice3;
                if (MainForm.Dic_SettingFields_SkillQuecall.ContainsKey(SkillGroupID))
                    MainForm.Dic_SettingFields_SkillQuecall[SkillGroupID] = value;
                else
                    MainForm.Dic_SettingFields_SkillQuecall.Add(SkillGroupID, value);
                SkillQueCallSoundPlayerManager.ConfigSoundPlayers(SkillGroupID);
                this.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// make sure the input value in order
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            string strPeriod1 = this.txtPeriod1.Text;
            string strPeriod2 = this.txtPeriod2.Text;
            string strPeriod3 = this.txtPeriod3.Text;

            if (!string.IsNullOrEmpty(strPeriod1) && string.IsNullOrEmpty(this.txtVoice1.Text))
            {
                MessageBox.Show("音声を指定ください。");
                return false;
            }
            if (!string.IsNullOrEmpty(strPeriod2) && string.IsNullOrEmpty(this.txtVoice2.Text))
            {
                MessageBox.Show("音声を指定ください。");
                return false;
            }
            if (!string.IsNullOrEmpty(strPeriod3) && string.IsNullOrEmpty(this.txtVoice3.Text))
            {
                MessageBox.Show("音声を指定ください。");
                return false;
            }

            if (string.IsNullOrEmpty(strPeriod1) && string.IsNullOrEmpty(strPeriod2) && string.IsNullOrEmpty(strPeriod3))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(strPeriod1) && string.IsNullOrEmpty(strPeriod2) && string.IsNullOrEmpty(strPeriod3))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(strPeriod1) && !string.IsNullOrEmpty(strPeriod2) && string.IsNullOrEmpty(strPeriod3))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(strPeriod1) && !string.IsNullOrEmpty(strPeriod2) && !string.IsNullOrEmpty(strPeriod3))
            {
                return true;
            }

            return false;
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

        private void btnOpenFile1_Click(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice1.Text = "Skill_" + SkillGroupID + "_QueCallVoice1.wav";
                    System.IO.File.Copy(strFile, VoicePath + @"\" + txtVoice1.Text, true);

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
                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice2.Text = "Skill_" + SkillGroupID + "_QueCallVoice2.wav";
                    System.IO.File.Copy(strFile, VoicePath + @"\" + txtVoice2.Text, true);

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
                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;

                    txtVoice3.Text = "Skill_" + SkillGroupID + "_QueCallVoice3.wav";
                    System.IO.File.Copy(strFile, VoicePath + @"\" + txtVoice3.Text, true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
