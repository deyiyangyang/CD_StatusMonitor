using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cpfmsgacxa;
using StatusMonitor.Helper;
namespace StatusMonitor
{
    public partial class MessageForm : Form
    {        
        private const string UM_MONITORING="UM_MONITORING";
        private const string UM_MESSAGE = "UM_MESSAGE";
        private const string SM_MESSAGE = "SM_MESSAGE";
        private const string UM_CLOSEDIALOG = "UM_CLOSEDIALOG";

        private const string MONITOR_TYPE_MONITOR = "MONITOR";
        private const string MONITOR_TYPE_COACH = "COACH";
        private const string MONITOR_TYPE_MEETING = "MEETING";
        
        private const string MONITOR_STATUS_IDLE = "IDLE";
        private const string MONITOR_STATUS_CALLING = "CALLING";
        private const string MONITOR_STATUS_MONITORING = "MONITORING";



        public string SkillID="";
        public string AgentID="";
        public string AgentName = "";
        public string Status="";
        public string MessageInfo="";
        public MainForm mainF;
        //add,xzg,2010/02/12,S--------
        private string PreMonitorStatus = "";
        //add,xzg,2010/02/12,E--------
        private LanguageResourceManager res;
        public MessageForm()
        {
            InitializeComponent();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //send msg
                string msg;

                if (textBox5.Text.Length < 1)
                {
                    return;
                }

                //add,xzg,2012/10/21,S
                if (textBox5.Text.Length > 500) return;
                //add,xzg,2012/10/21,E
                msg = textBox5.Text;
                CpfParams cpfParam = new CpfParams();
                if (AgentName == cmbAgent.Text)
                {
                    cpfParam.AddString("vAgentID", AgentID);
                    cpfParam.AddString("vSkillID", "");
                }
                else
                {
                    cpfParam.AddString("vAgentID", "ALL");

                    GroupInfo groupInfo;
                    if(cmbSkill.SelectedItem==null)
                    {
                        groupInfo = new GroupInfo(-1,"");
                        cpfParam.AddString("vSkillID", "ALL");
                    }
                    else
                    {
                        groupInfo = (GroupInfo)cmbSkill.SelectedItem;
                        if (groupInfo.Group == -1)
                        {
                            cpfParam.AddString("vSkillID", "ALL");
                        }
                    }

                    cpfParam.AddString("vSkillID", groupInfo.Group.ToString());
                }
                cpfParam.AddString("vMessage", msg);
                axCpfMsg1.SendEvent("", "", UM_MESSAGE, cpfParam);
                setMsg(msg, "<");
                textBox5.Text = "";
                textBox5.Focus();
            }
            catch(Exception ex)
            {

            }

            
        }

        private void MessageForm_Activated(object sender, EventArgs e)
        {

            //string msg;
            //if (MessageInfo.Length < 1)
            //{
            //    return;
            //}
            //msg = AgentID + ">";
            //msg = msg + MessageInfo;
            //textBox4.Text = textBox4.Text + "\r\n" + msg;
            //MessageInfo = "";
        }
        //private void MessageForm_FormClosing(object sender, EventArgs e)
        //{

            //this.Dispose();
        //}

        private void MessageForm_Disposed(object sender, EventArgs e)
        {
            //this.ParentForm.Activate();
            //mainF.msgFromList.Remove(AgentID);
            mainF.delMsgForm(this,AgentID);
            try
            {
                //add,xzg,2009/02/04,S-----
                //閉じる
                CpfParams cpfParam = new CpfParams();
                cpfParam.AddString("vAgentID", AgentID);
                axCpfMsg1.SendEvent("", "", UM_CLOSEDIALOG, cpfParam);
                //add,xzg,2009/02/04,E-----
            }
            catch (Exception ex)
            {

            }

        }
        private void MessageForm_Load(object sender, EventArgs e)
        {
            try
            {
                iniForm();
                //add,2014/03,S
                if (mainF.ShowChatF == "1")
                {
                    textBox5.Enabled = false;
                    button4.Enabled = false;

                }
                //add,2014/03,E
                textBox1.Text = SkillID;
                textBox2.Text = AgentName;
                textBox3.Text = Status;
                //add,xzg,2010/02/23,S--------
                PreMonitorStatus = Status;
                //add,xzg,2010/02/23,E--------
                if (Status == res.GetString("SM0030011"))
                {


                    //button1.Enabled = true;
                    setButtonStatus(true);
                }

                else
                {
                    //button1.Enabled = false;
                    setButtonStatus(false);
                }

                //add,xzg,2013/07/17,S
                showSkill();
                //if (cmbSkill.Items.Count > 0)
                //    cmbSkill.SelectedIndex = 0;
                //add,xzg,2013/07/17,E

                //string msg;
                //msg = AgentID + ">";
                //msg =msg+  MessageInfo;
                //textBox4.Text = msg;
                if (MessageInfo.Length < 1)
                {
                    return;
                }
                setMsg(MessageInfo);
                MessageInfo = "";

               
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void showSkill()
        {
            try
            {

                foreach (GroupInfo group in mainF.groupInfoList)
                {
                    cmbSkill.Items.Add(group);
                    if (SkillID == group.GroupName)
                        cmbSkill.SelectedIndex = cmbSkill.Items.Count - 1;
                }
                if (cmbSkill.Items.Count>0)
                {
                    GroupInfo allGroup = new GroupInfo(-1, "全て");
                    cmbSkill.Items.Add(allGroup);
                }

                cmbAgent.Items.Add(AgentName);                
                cmbAgent.Items.Add("全て");
                cmbAgent.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void iniForm()
        {
            try
            {
                res = new LanguageResourceManager("JP");
                this.Font = new System.Drawing.Font(res.GetString("SM0000000"), float.Parse(res.GetString("SM0000001")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Byte.Parse("0"));
                this.Text = res.GetString("SM0030001");
                this.label1.Text = res.GetString("SM0030002");
                this.label2.Text = res.GetString("SM0030003");
                this.label3.Text = res.GetString("SM0030004");
                this.button1.Text = res.GetString("SM0030005");
                this.button3.Text = res.GetString("SM0030006");
                this.button2.Text = res.GetString("SM0030007");
                this.label4.Text = res.GetString("SM0030008");
                this.button4.Text = res.GetString("SM0030009");
                this.button5.Text = res.GetString("SM0030010");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        public void setMsg(string iMsg,string iTarget)
        {
            try
            {
                string msg;
                if (iMsg.Length < 1)
                {
                    return;
                }
                msg = AgentName + iTarget;
                msg = msg + iMsg;
                if (textBox4.Text.Length > 0)
                {
                    textBox4.Text = textBox4.Text + "\r\n" + msg;
                }
                else
                {
                    textBox4.Text = msg;
                }
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
                textBox4.ScrollToCaret();
                textBox4.SelectionLength = 0;
                
            }
            catch (Exception ex)
            {
            }
        }
        public void setMonitorStatus(string status)
        {
            try
            {
                if (status == MONITOR_STATUS_IDLE)
                {
                    if (string.IsNullOrEmpty(mainF.SVPhone))
                        setButtonStatus(false);
                    else
                    {
                        if (textBox3.Text == res.GetString("SM0030011"))
                            setButtonStatus(true);
                        else
                            setButtonStatus(false);
                    }
                }

                else
                {

                    setButtonStatus(false);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void setStatus(string status)
        {
            try
            {

                if (status.Length < 1)
                {
                    return;
                }
                if (status == res.GetString("SM0030011"))
                {
                    if (string.IsNullOrEmpty(mainF.SVPhone))
                        setButtonStatus(false);
                    else
                    {
                        if (mainF.MonitorStatus == MONITOR_STATUS_IDLE)
                            setButtonStatus(true);
                        else
                            setButtonStatus(false);
                    }
                    //button1.Enabled = true;

                }

                else
                {
                    //button1.Enabled=false;
                    setButtonStatus(false);
                }
                //add,xzg,2010/02/23,S--------
                if (mainF.MonitorStatus != MONITOR_STATUS_IDLE)
                {
                    if ((status == res.GetString("SM0020047")
                            || status == res.GetString("SM0020050")
                            || status == res.GetString("SM0020051"))
                            && (PreMonitorStatus == res.GetString("SM0020049")
                                || PreMonitorStatus == res.GetString("SM0020053")
                                || PreMonitorStatus == res.GetString("SM0020054")
                                || PreMonitorStatus == res.GetString("SM0020055")
                                || PreMonitorStatus == res.GetString("SM0020056")
                                )
                        )
                    {
                        CpfParams cpfParam = new CpfParams();
                        cpfParam.AddString("vAgentID", AgentID);
                        axCpfMsg1.SendEvent("", "", UM_CLOSEDIALOG, cpfParam);
                    }
                }
                PreMonitorStatus = status;
                //add,xzg,2010/02/23,E--------

                textBox3.Text = status;
            }
            catch (Exception ex)
            {

            }
        }

        public void setMsg(string iMsg)
        {
            try
            {
                string msg;
                if (iMsg.Length < 1)
                {
                    return;
                }
                msg = AgentName + ">";
                msg = msg + iMsg;
                if (textBox4.Text.Length > 0)
                {
                    textBox4.Text = textBox4.Text + "\r\n" + msg;
                }
                else
                {
                    textBox4.Text = msg;
                }
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
                textBox4.ScrollToCaret();
                textBox4.SelectionLength = 0;
            }
            catch (Exception ex)
            {
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //モニター
            sendMonitor(MONITOR_TYPE_MONITOR);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ささやき
            sendMonitor(MONITOR_TYPE_COACH);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //三者通話
            sendMonitor(MONITOR_TYPE_MEETING);
        }
        private void sendMonitor(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(mainF.SVPhone) || !UtilityHelper.IsNumeric(mainF.SVPhone))
                {
                    MessageBox.Show("通話モニタ電話番号を正しく設定してください。");
                    return;
                }
                CpfParams cpfParam = new CpfParams();
                string sAddress = "";
                //sAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
                //sAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
                sAddress = mainF.SVPhone;
                cpfParam.AddString("vAgentID", AgentID);
                cpfParam.AddString("vType", type);
                cpfParam.AddString("vAddress", sAddress);
                //axCpfMsg1.SendEvent("", "", "UM_MONITOR", cpfParam);
                axCpfMsg1.SendEvent("", "", UM_MONITORING, cpfParam);
                
            }
            catch (Exception ex)
            {
            }
        }
        private void setButtonStatus(bool enable)
        {
            button1.Enabled = enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private string ToASC(String DefaultString)
        {
            System.Text.Encoding tmpDefaultEncoding = System.Text.Encoding.Default;
            System.Text.Encoding tmpUnicodeEncoding = System.Text.Encoding.ASCII;
            Byte[] tmpBytes = tmpDefaultEncoding.GetBytes(DefaultString);
            Byte[] tmpUBytes = System.Text.Encoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.ASCII, tmpBytes);
            return tmpUnicodeEncoding.GetString(tmpUBytes);
        }
        private string ToUTF8(String DefaultString)
        {
            System.Text.Encoding tmpTargetEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Text.Encoding tmpDefaultEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Text.Encoding tmpUnicodeEncoding = System.Text.Encoding.UTF8;
            Byte[] tmpBytes = tmpDefaultEncoding.GetBytes(DefaultString);
            Byte[] tmpUBytes = System.Text.Encoding.Convert(tmpTargetEncoding, System.Text.Encoding.UTF8, tmpBytes);
            return tmpUnicodeEncoding.GetString(tmpUBytes);            
        }
        private string ToDefault(String UnicodeString)
        {
            System.Text.Encoding tmpTargetEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Text.Encoding tmpDefaultEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //System.Text.Encoding tmpDefaultEncoding = System.Text.Encoding.Default;
            System.Text.Encoding tmpUnicodeEncoding = System.Text.Encoding.UTF8;
            Byte[] tmpUBytes = tmpUnicodeEncoding.GetBytes(UnicodeString);
            Byte[] tmpBytes = System.Text.Encoding.Convert(System.Text.Encoding.UTF8, tmpTargetEncoding, tmpUBytes);
            return tmpDefaultEncoding.GetString(tmpBytes);
        }

        /// <summary>
        /// ctrl+enter to insert a new line
        /// enter to send the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
      
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {

            }
            else if (e.KeyCode == Keys.Enter)
            {
                button4_Click(null, null);
                this.textBox5.Focus();
            }
        }

        /// <summary>
        /// avoid to insert \r\n
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
             e.Handled = true;
        }
    }
}