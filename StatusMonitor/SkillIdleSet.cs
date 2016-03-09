using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class SkillIdleSet : Form
    {
        public StatusMonitor.MainForm mainF;
        public string PeriodLongString = "";
        public string PeriodVoiceLongString = "";
        private DataTable _dtGroupPersonal;
        private Dictionary<int, WMPLib.WindowsMediaPlayer> tempPlayers;
        public SkillIdleSet(DataTable dtGroupPersonal)
        {
            InitializeComponent();
            _dtGroupPersonal = dtGroupPersonal;
            tempPlayers = new Dictionary<int, WMPLib.WindowsMediaPlayer>();
        }

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

        private void SkillIdleSet_Load(object sender, EventArgs e)
        {
            int index = 0;
            int offsetHeight = 15;
            int offsetLeft = 150;
            foreach (DataRow item in _dtGroupPersonal.Rows)
            {
                if (item["groupId"].ToString() == "-1") continue;
                index++;
                Label labelSkillName = new Label();
                labelSkillName.Name = "lblIdle" + item["groupId"].ToString();
                labelSkillName.Text = item["groupName"].ToString();
                labelSkillName.Location = new System.Drawing.Point(12, 12 * index + (index - 1) * offsetHeight);
                labelSkillName.Size = new System.Drawing.Size(offsetLeft, 12);

                TextBox txtPeriod1 = new TextBox();
                txtPeriod1.ImeMode = System.Windows.Forms.ImeMode.Disable;
                txtPeriod1.Location = new System.Drawing.Point(72 + offsetLeft, 12 * index + (index - 1) * offsetHeight);
                txtPeriod1.MaxLength = 3;
                txtPeriod1.Name = "txtIdlePeriod" + item["groupId"].ToString();
                txtPeriod1.Size = new System.Drawing.Size(33, 19);
                txtPeriod1.TabIndex = 2;

                Label labelTemp1 = new Label();
                labelTemp1.AutoSize = true;
                labelTemp1.Location = new System.Drawing.Point(109 + offsetLeft, 12 * index + (index - 1) * offsetHeight);
                labelTemp1.Name = "labelTemp1";
                labelTemp1.Size = new System.Drawing.Size(29, 12);
                labelTemp1.TabIndex = 3;
                labelTemp1.Text = "以下";

                TextBox txtVoice1 = new TextBox();
                txtVoice1.Enabled = false;
                txtVoice1.Location = new System.Drawing.Point(144 + offsetLeft, 12 * index + (index - 1) * offsetHeight);
                txtVoice1.Name = "txtIdleVoice" + item["groupId"].ToString();
                txtVoice1.Size = new System.Drawing.Size(117, 19);
                txtVoice1.TabIndex = 4;

                Button btnOpenFile1 = new Button();
                btnOpenFile1.Location = new System.Drawing.Point(264 + offsetLeft, 12 * index + (index - 1) * offsetHeight);
                btnOpenFile1.Name = "btnIdleOpenFile" + item["groupId"].ToString();
                btnOpenFile1.Size = new System.Drawing.Size(37, 20);
                btnOpenFile1.TabIndex = 5;
                btnOpenFile1.Text = "参照";
                btnOpenFile1.UseVisualStyleBackColor = true;
                btnOpenFile1.Click += new System.EventHandler(this.btnOpenFile_Click);

                Button btnPlay1 = new Button();
                btnPlay1.Location = new System.Drawing.Point(304 + offsetLeft, 12 * index + (index - 1) * offsetHeight);
                btnPlay1.Name = "btnIdlePlay" + item["groupId"].ToString();
                btnPlay1.Size = new System.Drawing.Size(37, 20);
                btnPlay1.TabIndex = 6;
                btnPlay1.Text = "再生";
                btnPlay1.UseVisualStyleBackColor = true;
                btnPlay1.Click += new System.EventHandler(this.btnPlay_Click);

                var player = new WMPLib.WindowsMediaPlayer();
                player.settings.setMode("loop", true);
                tempPlayers.Add(int.Parse(item["groupId"].ToString()), player);

                this.plSkill.Controls.Add(btnPlay1);
                this.plSkill.Controls.Add(btnOpenFile1);
                this.plSkill.Controls.Add(txtVoice1);
                this.plSkill.Controls.Add(labelSkillName);
                this.plSkill.Controls.Add(labelTemp1);
                this.plSkill.Controls.Add(txtPeriod1);
            }
            //index++;
            //this.btnOK.Location = new Point(btnOK.Location.X, 12 * index + (index - 1) * offsetHeight);
            //this.btnClose.Location = new Point(btnClose.Location.X, 12 * index + (index - 1) * offsetHeight);
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
                string strVoice1 = "";

                foreach (Control c in this.plSkill.Controls)
                {
                    if (c.Name.Contains("txtIdleVoice"))
                    {
                        string groupId = c.Name.Substring("txtIdleVoice".Length);
                        if (!string.IsNullOrEmpty(this.plSkill.Controls.Find("txtIdlePeriod" + groupId, true)[0].Text))
                        {
                            if (!string.IsNullOrEmpty(c.Text))
                                strVoice1 += "|" + c.Name.Substring("txtIdleVoice".Length) + ":" + c.Text;
                        }
                    }
                    else if (c.Name.Contains("txtIdlePeriod"))
                    {
                        if (!string.IsNullOrEmpty(c.Text))
                        {
                            if (!checkNumeric(c.Text))
                            {
                                c.Focus();
                                return;
                            }
                            else
                            {
                                strPeriod1 += "|" + c.Name.Substring("txtIdlePeriod".Length) + ":" + c.Text;
                            }
                        }

                    }
                }


                if (strVoice1.Length > 1) strVoice1 = strVoice1.Substring(1);
                if (strPeriod1.Length > 1) strPeriod1 = strPeriod1.Substring(1);



                //SetQueCall
                mainF.SetIdle(strPeriod1, strVoice1);

                foreach (var p in tempPlayers)
                {
                    if (!SkillSoundPlayerManager.Players.ContainsKey(p.Key))
                    {
                        SkillSoundPlayerManager.Players.Add(p.Key, p.Value);
                    }
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                mainF.writeLog("btnOK_Click:"+ex.Message);
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
                path = path + "\\Comdesign\\Voice";

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
        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (sender as Button).Name;
                string txtVoice1 = this.plSkill.Controls.Find("txtIdleVoice" + name.Substring("btnIdlePlay".Length), true)[0].Text;
                if (!string.IsNullOrEmpty(txtVoice1))
                {
                    playVoice(txtVoice1);
                }
            }
            catch (Exception ex)
            {

            }


        }


        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string strFile = "";
            string name = (sender as Button).Name;
            string groupId = name.Substring("btnIdleOpenFile".Length);
            try
            {
                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign\\Voice";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (ofdVoice.ShowDialog() == DialogResult.OK)
                {
                    strFile = ofdVoice.FileName;
                    string reName = "IdleVoice" + groupId + ".wav";
                    this.plSkill.Controls.Find("txtIdleVoice" + groupId, true)[0].Text = reName;
                    System.IO.File.Copy(strFile, path + @"\" + reName, true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("音声ファイルアップロードができませんでした。", "状態モニタ");
            }
        }

        private void SkillIdleSet_Shown(object sender, EventArgs e)
        {
            try
            {
                ofdVoice.Filter = "(*.wav)|*.wav";

                if (!string.IsNullOrEmpty(PeriodLongString))
                {
                    string[] periods = PeriodLongString.Split('|');
                    for (int i = 0; i < periods.Length; i++)
                    {
                        if (string.IsNullOrEmpty(periods[i])) continue;
                        string[] temp = periods[i].Split(':');
                        string groupID = temp[0];
                        this.plSkill.Controls.Find("txtIdlePeriod" + groupID, true)[0].Text = temp[1];
                    }
                }

                if (!string.IsNullOrEmpty(PeriodVoiceLongString))
                {
                    string[] periodVoices = PeriodVoiceLongString.Split('|');
                    for (int i = 0; i < periodVoices.Length; i++)
                    {
                        if (string.IsNullOrEmpty(periodVoices[i])) continue;
                        string[] temp = periodVoices[i].Split(':');
                        string groupID = temp[0];
                        this.plSkill.Controls.Find("txtIdleVoice" + groupID, true)[0].Text = temp[1];
                    }
                }
            }
            catch (Exception ex)
            {
                mainF.writeLog("SkillIdleSet_Shown:" + ex.Message);
            }
        }

    }
}
