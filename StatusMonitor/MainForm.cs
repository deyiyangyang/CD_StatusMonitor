/**
 * 2010/02/13   AM_SHOWAGENT
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
// add
using MyTools;
using Cpfmsgacxa;
using TksProfileAcxLib;
using System.Collections;
//add,xzg,2011/06/20,S
//using CpfNameAcxLib;
using CpfMsgUpAcxaLib;
//add,xzg,2011/06/20,E

//ssl
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using StatusMonitor.TabPage;
using StatusMonitor.Model;
using System.Threading;
using StatusMonitor.SettingFile;
using StatusMonitor.Helper;

namespace StatusMonitor
{
    public partial class MainForm : Form
    {
        // private ArrayList msgFrom=new ArrayList();
        private ArrayList msgFromList = new ArrayList();
        private ArrayList msgFromID = new ArrayList();
        //add,xzg,2009/02/04,S------
        private string MONITOR_TYPE_MONITOR = "MONITOR";
        private string MONITOR_TYPE_COACH = "COACH";
        private string MONITOR_TYPE_MEETING = "MEETING";

        private string MONITOR_STATUS_IDLE = "IDLE";
        private string MONITOR_STATUS_CALLING = "CALLING";
        private string MONITOR_STATUS_MONITORING = "MONITORING";

        private string MONITOR_STATUS_IDLE_NAME = "SM0020012";
        private string MONITOR_STATUS_CALLING_NAME = "SM0020013";
        private string MONITOR_TYPE_MONITOR_NAME = "SM0020014";
        private string MONITOR_TYPE_COACH_NAME = "SM0020015";
        private string MONITOR_TYPE_MEETING_NAME = "SM0020016";

        private string MONITOR_NOPHONE_NAME = "SM0020017";
        private string NOTIFYICON_TEXT = "SM0020018";
        //add,xzg,2009/05/20,S----
        private const string UM_FORCE_LOGOUT = "UM_FORCE_LOGOUT";
        private string vSelectedAgentID = "";
        //add,xzg,2009/05/20,E----
        public string MonitorStatus = "";
        public string SVPhone = "";
        private int KeepAliveCount = 0;
        private int KeepAliveTime = 15;
        private LanguageResourceManager res;
        //added by Zhu 2014/03/19
        public string IdlePeriodLongString = "";
        public string IdlePeriodVoiceLongString = "";
        private int DtMonitorRowsCount = 0;
        private DataTable _MonitorDataTable;

        private int AgentIconListHeight = 0;
        private int AgentIconListWeight = 0;
        private int AgentPieHeigh = 0;
        private int AgentPieWeight = 0;
        private Image CurrentAgentPie = null;
        //end added

        //added by Zhu 2014/03/31
        private string skillShowSetString = "";
        public string SkillShowSetString
        {
            get { return skillShowSetString; }
            set
            {
                skillShowSetString = value;
                if (!string.IsNullOrEmpty(skillShowSetString))
                {
                    string[] arr = skillShowSetString.Split('|');
                    _ShowSkillGroupIDs = "";
                    foreach (string item in arr)
                    {
                        string[] arr1 = item.Split(':');
                        if (arr1[1] == "1")
                            _ShowSkillGroupIDs += ",'" + arr1[0] + "'";
                    }
                    if (_ShowSkillGroupIDs.Length > 0) _ShowSkillGroupIDs = _ShowSkillGroupIDs.Substring(1);
                }
            }
        }
        //deleted by zhu 2014/05/12
        //public string MonitorItemShowString = "";
        //end deleted
        public string _ShowSkillGroupIDs = "";
        public string _DefaultShowSkillGroupIDs = "";
        //end added

        //added by zhu 2014/04/17
        public string SpecialNameFlag = "";
        //end added

        //added by zhu 2014/05/12
        MonitorItemManager _MonitorItemManager;
        //end added


        //add,xzg,2009/10/30,S------------
        public int refreshTime = 0;
        private int refreshCount = 0;
        //add,xzg,2009/10/30,E------------

        //add,xzg,2010/02/23,S----------
        private int GetTimes = 2;
        private int QUECALLCount = 0;
        private int AGENTCount = 0;
        private int QUECALLTimes = 0;
        private int AGENTTimes = 0;
        //add,xzg,2010/02/23,E----------

        //add,xzg,2011/06/20,S
        private CpfMsgUpClass axCpfMsgUp;
        private List<string> lstChannel = new List<string>();

        private bool updateAllStatus = false;
        //add,xzg,2011/06/20,E

        //update,xzg,2011/09/16,S
        private bool updateAgentStatus = false;
        private bool updateLineStatus = false;
        private bool updateQueStatus = false;

        public string DefaultOverTime = "0000";
        public string LineID = "";
        private string vCurrentLineStatusName = "";
        private string WaitTimes = "";
        private string vCurrentAgentID = "";
        private string vCurrentAgentName = "";
        private string vCurrentAgentState = "";

        //add,xzg,2012/08/01,E

        private string ShowCol = "1000001111111111";

        private string OptionName1 = "拠点";
        private string OptionName2 = "種別";
        private string OptionName3 = "";
        private string OptionName4 = "";
        private string OptionName5 = "";


        private int ReportCount = 0;
        private string curDateE = "0";
        private string reportDateS = "0";
        private string reportDateE = "0";
        private bool reportForTimeHas = false;
        private string PreDate = "2000/01/01";


        private DataTable dtMontor;
        private DataTable dtGroupPersonal;
        private DataTable dtSkillGroup;
        public DataSet dsMontor = new DataSet();

        private int intMonitrTimer = 0;

        public string QuickAnswerMinutes = "0";

        private int displayGroupPre = -1;
        private int getGrouPersonCnt = 0;

        private bool MontorSortFlag = false;


        public string MonitorCol1 = "スキルグループ";
        public string MonitorCol2 = "ログオン人数";
        public string MonitorCol3 = "着座OP数";
        public string MonitorCol4 = "離席数";
        public string MonitorCol5 = "OP呼出数";
        public string MonitorCol6 = "OP応答数";
        public string MonitorCol7 = "応答率";
        public string MonitorCol8 = "即答数";
        public string MonitorCol9 = "即答率";
        public string MonitorCol10 = "待呼数";
        public string MonitorCol11 = "受付可数";
        public string MonitorCol12 = "放棄呼";
        public string MonitorCol13 = "放棄率";

        private bool RetSetCallInfo = false;
        private string ShowBackColorForCol9 = ""; //1 即答列 背景ある、
        private string MonitorGroupList = ""; //1 即答列 背景ある、

        //add,2014/03,S
        public string ShowAgentF = "";//AgentList Rule 5桁 0全部
        public string ShowCallTagF = "0";//1:回線Tag非表示        
        public string ShowChatF = "0";//0:show,1chat　不可
        public string ShowMonitorF = "0";//0:show,1:MonitorTag非表示  
        //public string QueCallRingF = "0";//0:show,1:MonitorTag非表示  

        public string QuePeriod1 = "";
        public string QuePeriodVoice1 = "";

        public string QuePeriod2 = "";
        public string QuePeriodVoice2 = "";

        public string QuePeriod3 = "";
        public string QuePeriodVoice3 = "";
        private Dictionary<string, int> DicOriginAgentListViewColumnWidth = new Dictionary<string, int>();
        private Dictionary<string, int> DicOriginLineListViewColumnWidth = new Dictionary<string, int>();
        private Dictionary<string, int> DicOriginTotalListViewColumnWidth = new Dictionary<string, int>();
        private Dictionary<string, int> DicOriginMonitorGridColumnWidth = new Dictionary<string, int>();
        public int QueueCountAll = 0;
        public bool ReShowsFlag = true;
        public string KyokuGroup = "";
        private bool[] AgentStatusListViewOrder = new bool[15]; //8->14 14->15 modified by zhu 2014/05/08
        private bool[] TotalListViewOrder = new bool[5];
        private bool[] lineStatusListViewOrder = new bool[8]; //xzg,2012/02/09,7->8
        //AutoSizeFormClass Asc;
        //added by Zhu 2014/04/21
        List<Form> ListTabPagesForms;
        //end added
        //added by Zhu 2014/09/01
        System.Timers.Timer UpdateContinueTimer;
        //end added
        //add,2014/03,E
        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, ThrowOnUnmappableChar = true)]
        private static extern bool PlaySound(string szsound, System.IntPtr hmod, playSound flag);

        [System.Flags]
        public enum playSound : int
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

        public void setFrmText(string type)
        {
            try
            {
                string sText = res.GetString("SM0020018");
                if (!string.IsNullOrEmpty(SVPhone))
                    sText = sText + "(" + SVPhone + ")";
                else
                    sText = sText + "(" + res.GetString(MONITOR_NOPHONE_NAME) + ")";
                if (!string.IsNullOrEmpty(type))
                {
                    if (type == MONITOR_STATUS_IDLE)
                        sText = sText + "-" + res.GetString(MONITOR_STATUS_IDLE_NAME);
                    else if (type == MONITOR_STATUS_CALLING)
                        sText = sText + "-" + res.GetString(MONITOR_STATUS_CALLING_NAME);
                    else if (type == MONITOR_TYPE_MONITOR)
                        sText = sText + "-" + res.GetString(MONITOR_TYPE_MONITOR_NAME);
                    else if (type == MONITOR_TYPE_COACH)
                        sText = sText + "-" + res.GetString(MONITOR_TYPE_COACH_NAME);
                    else if (type == MONITOR_TYPE_MEETING)
                        sText = sText + "-" + res.GetString(MONITOR_TYPE_MEETING_NAME);
                }
                this.Text = sText;
            }
            catch (Exception ex)
            {
                writeLog(" setFrmText System error:" + ex.Message);
            }
        }
        //add,xzg,2009/02/04,E------

        public MainForm()
        {
            //Asc = new AutoSizeFormClass();
            InitializeComponent();
            //DebugPrint("Start");

            //added by Zhu 2014/04/21
            ListTabPagesForms = new List<Form>();
            //end added
            //added by Zhu 2014/09/01
            UpdateContinueTimer = new System.Timers.Timer();
            UpdateContinueTimer.Elapsed += new System.Timers.ElapsedEventHandler(ContinueTimer); ;

            //end added
            writeLog("current version is 6.3.4");
        }


        public void DebugPrint(string format, params object[] args)
        {
            string str = String.Format(format, args);
            string line = String.Format("{0} {1}\n", MyTool.GetDateTime(), str);
            //Debug.Write(line);            
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x00000112;
            const int SC_CLOSE = 0x0000F060;

            // 閉じる ボタンで終了しないで隠す(タスクトレイアイコンだけにする)
            if ((msg.Msg == WM_SYSCOMMAND) && (msg.WParam.ToInt32() == SC_CLOSE))
            {
                this.Visible = false;
                return;
            }

            base.WndProc(ref msg);
        }
        public void setPhone(string phone)
        {
            try
            {
                SVPhone = phone;
                setFrmText(MonitorStatus);
                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("Phone", phone);
                IniProfile.Save(MyTool.GetModuleIniPath());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void delMsgForm(MessageForm msg, string agentID)
        {
            msgFromID.Remove(agentID);
            msgFromList.Remove(msg);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                //add,xzg,2009/10/07,S--------
                // SetWindowPropRun
                Comdesign.ToolsPrevApp.SetWindowPropRun(this.Handle, MyTools.MyTool.GetModuleName());
                //add,xzg,2009/10/07,E--------

                // Set Title
                iniForm();
                this.AgentIconListWeight = this.agentIconListView.Width;
                this.AgentIconListHeight = this.agentIconListView.Height;
                this.AgentPieWeight = this.agentPie.Width;
                this.AgentPieHeigh = this.agentPie.Height;

                mainNotifyIcon.Text = res.GetString(NOTIFYICON_TEXT);
                mainNotifyIcon.Icon = this.Icon;

                InitAgentListView();
                InitLineListView();
                InitTotalListView();

                IniProfile.Load(MyTool.GetModuleIniPath());

                IniProfile.SelectSection("SVSet");

                //added by zhu 2014/05/12
                _MonitorItemManager = new MonitorItemManager(IniProfile);
                _MonitorItemManager.SaveData();
                _MonitorItemManager.MonitorItemChanged += _MonitorItemManager_MonitorItemChanged;
                //end added

                GetIniSettingValueInLoadEvent();
                ShowBackColorForCol9 = IniProfile.GetStringDefault("ShowBackColorForCol9", "");
                MonitorGroupList = IniProfile.GetStringDefault("MonitorGroupList", "");
                SettingFields_KyoKuGroupShow = IniProfile.GetStringDefault(ConstEntity.KYOKUGROUPSHOWKEY, "");
                if (SettingFields_AgentGraphShow != "1")
                {
                    this.agentPie.Visible = false;
                    //this.agentIconListView.Height = 60;
                    this.agentIconListView.Dock = DockStyle.Bottom;
                }
                //add,2014/03,S
                string strSetExtend = IniProfile.GetStringDefault("sSetExtend", "");
                if (strSetExtend.Length > 0)
                {
                    ShowAgentF = strSetExtend.Substring(0, 1);
                    if (ShowAgentF == "0")
                        ShowAgentF = "";
                    if (strSetExtend.Length > 1)
                    {
                        ShowCallTagF = strSetExtend.Substring(1, 1);
                        if (ShowCallTagF == "1")
                        {

                            statusTabCtrl.TabPages.Remove(lineStatusPage);
                        }
                        if (strSetExtend.Length > 2)
                        {
                            ShowChatF = strSetExtend.Substring(2, 1);
                        }
                        if (strSetExtend.Length > 3)
                        {
                            ShowMonitorF = strSetExtend.Substring(3, 1);
                            if (ShowMonitorF == "1")
                            {

                                statusTabCtrl.TabPages.Remove(tabMonitor);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(MonitorGroupList) && strSetExtend.Length > 10)
                                {
                                    MonitorGroupList = strSetExtend.Substring(10);
                                }
                            }
                        }
                    }
                }

                //added by zhu 2015/10/20
                GetGroupInfo();
                //end added
                //added by zhu 2015/10/09
                if (statusTabCtrl.TabPages.Contains(tabMonitor))
                {
                    SettingFields_MonitorTabShow = IniProfile.GetStringDefault(ConstEntity.MONITORTAB, "");
                    if (SettingFields_MonitorTabShow == "0")
                    {
                        statusTabCtrl.TabPages.Remove(tabMonitor);
                    }
                }
                //end added

                //add,2014/03,E
                //add,2014/03 ShowMonitorF
                if (ShowMonitorF != "1")
                {
                    //add,xzg,2013/10/13,S
                    delCallInfo();
                    listMonitorInit();


                    getGroupPersonal();
                    //add,xzg,2013/10/13,E
                }

                // Init agentIconListView
                for (int i = 1; i < CTe1Helper.AgentStatusEnums.Length; ++i)
                {
                    agentIconListView.Items.Add(CTe1Helper.AgentStatusEnums[i].Key, res.GetString(CTe1Helper.AgentStatusEnums[i].StatusName), CTe1Helper.AgentStatusEnums[i].Image);
                }

                // Init lineIconListView
                for (int i = 1; i < CTe1Helper.LineStatusEnums.Length; ++i)
                {
                    lineIconListView.Items.Add(res.GetString(CTe1Helper.LineStatusEnums[i].StatusName), CTe1Helper.LineStatusEnums[i].Image);
                }

                // Init selectViewButton
                selectViewButton.Text = res.GetString("SM0020038");

                // Init groupComboBox
                GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                groupComboBox.Items.Add(groupInfo);
                groupComboBox.SelectedIndex = 0;

                //init Option
                initOPtion();
                // Init lineUseageProgressLabel
                lineUseageProgressLabel.Tag = 0;
                // useageLabel
                useageLabel.Text = "0 / 0";

                //add,xzg,2011/09/16,S
                ListUpdateTimer.Enabled = true;
                //add,xzg,2011/09/16,E

                //added by Zhu 2014/03/25
                this.MenuIdle.Enabled = false;
                this.MenuSkillShowSet.Enabled = false;
                this.subMenuKyokuGroupSetting.Enabled = false;
                //end added

                //added by Zhu 2014/04/17
                SpecialNameFlag = GetSpecialNameFlag();
                //end adde

                //added by zhu 2014/04/21 add new tab for queue call
                QueueCallForm queueTabPage = new QueueCallForm(this);
                queueTabPage.TopLevel = false;
                queueTabPage.Dock = DockStyle.Fill;
                this.tabWaitCall.Controls.Add(queueTabPage);
                queueTabPage.Show();
                this.ListTabPagesForms.Add(queueTabPage);
                //end added

                //CommandThread = new System.Threading.Thread(DoCommandUpdate);
                //CommandThread.IsBackground = true;
                //CommandThread.Start();

                //Asc.controllInitializeSize(this);

                
            }
            catch (Exception ex)
            {
                writeLog("MainForm_Load:" + ex.Message + ex.StackTrace);
                //throw ex;
            }

        }

        //added by zhu 2014/05/12
        private void _MonitorItemManager_MonitorItemChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _MonitorItemManager.MonitorItems.Count; i++)
            {
                dvMonitor.Columns[i].Visible = _MonitorItemManager.MonitorItems[i].Visible;
                dvMonitor.Columns[i].HeaderText = _MonitorItemManager.MonitorItems[i].DisplayName;
            }
        }
        //end added
        private void initOPtion()
        {
            try
            {
                GroupInfo groupInfo1 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox1.Items.Add(groupInfo1);
                comboBox1.SelectedIndex = 0;

                GroupInfo groupInfo2 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox2.Items.Add(groupInfo2);
                comboBox2.SelectedIndex = 0;

                GroupInfo groupInfo3 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox3.Items.Add(groupInfo3);
                comboBox3.SelectedIndex = 0;

                GroupInfo groupInfo4 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox4.Items.Add(groupInfo4);
                comboBox4.SelectedIndex = 0;

                GroupInfo groupInfo5 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox5.Items.Add(groupInfo5);
                comboBox5.SelectedIndex = 0;

                GroupInfo groupInfo6 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox6.Items.Add(groupInfo6);
                comboBox6.SelectedIndex = 0;

                GroupInfo groupInfo7 = new GroupInfo(-1, res.GetString("SM0020040"));
                comboBox7.Items.Add(groupInfo7);
                AddOption(7, comboBox7.Items.Count, "通常");
                AddOption(7, comboBox7.Items.Count, "ヘルプ中");
                comboBox7.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                writeLog("initOPtion:" + ex.Message + ex.StackTrace);
                //throw ex;
            }
        }

        private void resetOption()
        {
            try
            {
                if (option1InfoList != null)
                {
                    if (option1InfoList.Count > 0)
                        option1InfoList.Clear();
                    if (this.comboBox1.Items.Count > 0)
                    {
                        comboBox1.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox1.Items.Add(groupInfo);
                        comboBox1.SelectedIndex = 0;
                    }
                }

                if (option2InfoList != null)
                {
                    if (option2InfoList.Count > 0)
                        option2InfoList.Clear();
                    if (this.comboBox2.Items.Count > 0)
                    {
                        comboBox2.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox2.Items.Add(groupInfo);
                        comboBox2.SelectedIndex = 0;
                    }
                }

                if (option3InfoList != null)
                {
                    if (option3InfoList.Count > 0)
                        option3InfoList.Clear();
                    if (this.comboBox3.Items.Count > 0)
                    {
                        comboBox3.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox3.Items.Add(groupInfo);
                        comboBox3.SelectedIndex = 0;
                    }
                }

                if (option4InfoList != null)
                {
                    if (option4InfoList.Count > 0)
                        option4InfoList.Clear();
                    if (this.comboBox4.Items.Count > 0)
                    {
                        comboBox4.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox4.Items.Add(groupInfo);
                        comboBox4.SelectedIndex = 0;
                    }
                }
                if (option5InfoList != null)
                {
                    if (option5InfoList.Count > 0)
                        option5InfoList.Clear();
                    if (this.comboBox5.Items.Count > 0)
                    {
                        comboBox5.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox5.Items.Add(groupInfo);
                        comboBox5.SelectedIndex = 0;
                    }
                }

                if (option6InfoList != null)
                {
                    if (option6InfoList.Count > 0)
                        option6InfoList.Clear();
                    if (this.comboBox6.Items.Count > 0)
                    {
                        comboBox6.Items.Clear();
                        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                        comboBox6.Items.Add(groupInfo);
                        comboBox6.SelectedIndex = 0;
                    }
                }

                //if (option7InfoList != null)
                //{
                //    if (option7InfoList.Count > 0)
                //        option7InfoList.Clear();
                //    if (this.comboBox7.Items.Count > 0)
                //    {
                //        comboBox7.Items.Clear();
                //        GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                //        comboBox7.Items.Add(groupInfo);
                //        comboBox7.SelectedIndex = 0;
                //    }
                //}


            }
            catch (Exception ex)
            {
                writeLog("resetOption:" + ex.Message + ex.StackTrace);
                //throw ex;
            }
        }
        //add,xzg,2011/06/20,S
        // 接続先Cpfmsgsrvとの切断時
        private void axCpfMsgUp_OnClose()
        {
            // DebugPrint("axCpfMsgUp_OnClose");
            //lstChannel.Clear();
            ////axCpfMsgUp.Close();

            try
            {

                writeLog("axCpfMsgUp_OnClose:切断");


            }
            catch (Exception ex)
            {
                //writeLog("axCpfMsgUp_OnLogon:" + ex.Message);
            }
        }

        // クライアントからのLogonメッセージ受信時
        private void axCpfMsgUp_OnLogon(string sChannel, string sAppl, string sPhase, string sLogin)
        {
            //DebugPrint("axCpfMsgUp_OnLogon sChannel = [{0}], sAppl = [{1}], sPhase = [{2}], sLogin = [{3}]",
            //              sChannel, sAppl, sPhase, sLogin);
            try
            {
                //lstChannel.Add(sChannel);

                //updateAllStatus = true;
                //AgentMonitorが接続すると、再表示
                //CpfParams cpfParam = new CpfParams();
                //axCpfMsg.SendEvent("", "", "AM_SHOWAGENT", cpfParam);

                ////AgentMonitorが接続すると、Statusの情報をAgentMonitorに送信（NGCPサーバへの送信はしない）
                //update,xzg,2011/09/15,S
                //ログオンのChannelに送信
                //sendFirstMsg(sChannel);
                writeLog("axCpfMsgUp_OnLogon:" + sChannel);
                sendFirstMsg(sChannel);

                //update,xzg,2011/09/15,E

            }
            catch (Exception ex)
            {
                writeLog("axCpfMsgUp_OnLogon:" + ex.Message);
            }
        }
        //AgentMonitorが接続すると、Statusの情報をAgentMonitorに送信
        private void sendFirstMsg(string sChannel)
        {
            try
            {

                ////自分のテナント情報を送信
                ////String strTenant = MyTenant;
                //CpfParams cpfParam1 = new CpfParams();
                //cpfParam1.AddString("MyTenant", MyTenant);

                //axCpfMsgUp.SendCommand(sChannel, "MAIN", "dumy", "SM_TENANTINFO", cpfParam1.GetParams());
                //writeLog("sendFirstMsg:" + cpfParam1.GetParams());


                //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", e.sPhase, e.sField, e.sCommand, e.objCpfParams.GetParams());
                int iCount = queueStatusList.Count;
                CpfParams cpfParam = new CpfParams();
                cpfParam.AddLong("iNumberOfList", iCount);
                //update,xzg,2011/09/15,S
                //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", "MAIN", "dumy", "QUECALLCOUNT", cpfParam.GetParams());
                axCpfMsgUp.SendCommand(sChannel, "MAIN", "dumy", "QUECALLCOUNT", cpfParam.GetParams());
                writeLog("sendFirstMsg:" + cpfParam.GetParams());
                //update,xzg,2011/09/15,E

                //update,xzg,2011/09/15,S
                //foreach (QueueStatus obj in queueStatusList)
                for (int i = 0; i < queueStatusList.Count; i++)
                //update,xzg,2011/09/15,E
                {
                    System.Threading.Thread.Sleep(50);
                    //add,xzg,2011/09/15,S
                    QueueStatus obj = queueStatusList[i];
                    //add,xzg,2011/09/15,E
                    cpfParam = new CpfParams();
                    cpfParam.AddLong("iSkillID", obj.Group);
                    cpfParam.AddString("vSkillName", obj.GroupName);
                    cpfParam.AddLong("iNumberOfQue", obj.QueueCount);
                    //update,xzg,2011/09/15,S
                    //cpfParam.AddLong("iMsgDone", 0);
                    if (i == queueStatusList.Count - 1)
                        cpfParam.AddLong("iMsgDone", 0);
                    else
                        cpfParam.AddLong("iMsgDone", 1);
                    //update,xzg,2011/09/15,E
                    //update,xzg,2011/09/15,S
                    //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", "MAIN", "dumy", "QUECALL", cpfParam.GetParams());
                    axCpfMsgUp.SendCommand(sChannel, "MAIN", "dumy", "QUECALL", cpfParam.GetParams());
                    writeLog("sendFirstMsg:" + cpfParam.GetParams());
                    //update,xzg,2011/09/15,E


                }

                iCount = agentStatusList.Count;
                cpfParam = new CpfParams();
                cpfParam.AddLong("iNumberOfList", iCount);
                //update,xzg,2011/09/15,S
                //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", "MAIN", "dumy", "AGENTCOUNT", cpfParam.GetParams());
                axCpfMsgUp.SendCommand(sChannel, "MAIN", "dumy", "AGENTCOUNT", cpfParam.GetParams());
                writeLog("sendFirstMsg:" + cpfParam.GetParams());
                //update,xzg,2011/09/15,E

                //update,xzg,2011/09/15,S
                //foreach (AgentStatus obj in agentStatusList)
                for (int i = 0; i < agentStatusList.Count; i++)
                //update,xzg,2011/09/15,E
                {
                    System.Threading.Thread.Sleep(50);
                    //add,xzg,2011/09/15,S
                    AgentStatus obj = agentStatusList[i];
                    //add,xzg,2011/09/15,E

                    cpfParam = new CpfParams();
                    cpfParam.AddLong("iSkillID", obj.Group);
                    cpfParam.AddString("vSkillName", obj.GroupName);
                    cpfParam.AddString("vAgentID", obj.Agent);
                    cpfParam.AddString("vAgentName", obj.AgentName);
                    cpfParam.AddString("vExtension", obj.Extension);
                    cpfParam.AddString("iSessionProfileID", obj.Session.ToString());
                    cpfParam.AddString("iElapsedTime", obj.StatusContinueTime);
                    cpfParam.AddLong("iStatus", obj.iStatus);  //obj.Status->obj.iStatus
                    cpfParam.AddString("dtLogin", obj.LoginTime.ToString());
                    cpfParam.AddString("dtStatus", obj.StatusTime.ToString());

                    cpfParam.AddString("iSkillName", obj.iSkillName);
                    cpfParam.AddString("vReason", obj.vReason);
                    cpfParam.AddString("vMemo", obj.vMemo);

                    if (obj.Help)
                        cpfParam.AddLong("iHelp", 1);
                    else
                        cpfParam.AddLong("iHelp", 0);
                    cpfParam.AddString("vCaller", obj.Caller);
                    cpfParam.AddString("iElapsedTime", obj.Elapsed.ToString());

                    //add,2013/12/03,Option,S
                    cpfParam.AddString("vOption1", obj.Option1.ToString());
                    cpfParam.AddString("vOption2", obj.Option2.ToString());
                    cpfParam.AddString("vOption3", obj.Option3.ToString());
                    cpfParam.AddString("vOption4", obj.Option4.ToString());
                    cpfParam.AddString("vOption5", obj.Option5.ToString());
                    //add,2013/12/03,Option,E

                    //update,xzg,2011/09/15,S
                    //cpfParam.AddLong("iMsgDone", 0);
                    if (i == agentStatusList.Count - 1)
                        cpfParam.AddLong("iMsgDone", 0);
                    else
                        cpfParam.AddLong("iMsgDone", 1);
                    //update,xzg,2011/09/15,E


                    //update,xzg,2011/09/15,S
                    //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", "MAIN", "dumy", "AGENT", cpfParam.GetParams());
                    axCpfMsgUp.SendCommand(sChannel, "MAIN", "dumy", "AGENT", cpfParam.GetParams());
                    writeLog("sendFirstMsg:" + cpfParam.GetParams());
                    //update,xzg,2011/09/15,E

                }
            }
            catch (Exception ex)
            {
                writeLog("sendFirstMsg:" + ex.Message);
            }

        }
        // クライアントからのLogoffメッセージ受信時
        private void axCpfMsgUp_OnLogoff(string sChannel)
        {
            //DebugPrint("axCpfMsgUp_OnLogoff sChannel = [{0}]", sChannel);
            try
            {
                //lstChannel.Remove(sChannel);
            }
            catch (Exception ex)
            {
                writeLog("axCpfMsgUp_OnLogoff:" + ex.Message);
            }
        }

        // クライアントからのEventメッセージ受信時
        void axCpfMsgUp_OnEvent(string sChannel, string sPhase, string sField, string sEvent, string sCpfParams)
        {
            //DebugPrint("axCpfMsgUp_OnEvent sChannel = [{0}], sEvent = [{1}], sCpfParams = [{2}]",
            //              sChannel, sEvent, sCpfParams);
            try
            {
                //add,xzg,2011/09/15,S
                // add AgentMonitor からの AM_SHOWAGENT は StatusMonitor の情報で更新する
                //if (sEvent == "AM_SHOWAGENT")
                //{
                //    sendFirstMsg(sChannel);
                //    return;
                //}
                ////add,xzg,2011/09/15,E

                //// クライアントからマスター側のCpfmsgsrvに送る場合
                //CpfParams objCpfParams = new CpfParams();
                //objCpfParams.SetParams(sCpfParams);
                //axCpfMsg.SendEvent(sPhase, sField, sEvent, objCpfParams);
            }
            catch (Exception ex)
            {
                writeLog("axCpfMsgUp_OnEvent:" + ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //add,xzg,2011/06/20,E

        private void iniForm()
        {
            try
            {
                res = new LanguageResourceManager("JP");
                this.Font = new System.Drawing.Font(res.GetString("SM0000000"), float.Parse(res.GetString("SM0000001")), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Byte.Parse("0"));
                this.Text = res.GetString("SM0020001");
                this.menuFile.Text = res.GetString("SM0020002");
                this.menuFileEnd.Text = res.GetString("SM0020003");
                this.menuSet.Text = res.GetString("SM0020004");
                this.subMenuSet.Text = res.GetString("SM0020005");
                this.label1.Text = res.GetString("SM0020006");
                this.selectViewButton.Text = res.GetString("SM0020007");
                this.label2.Text = res.GetString("SM0020008");
                //this.useageLabel.Text = res.GetString("SM0020009");
                this.agentStatusPage.Text = res.GetString("SM0020010");
                this.lineStatusPage.Text = res.GetString("SM0020011");
                this.mainNotifyMenuShow.Text = res.GetString("SM0020059");
                this.mainNotifyMenuEnd.Text = res.GetString("SM0020060");
                //add,xzg,2009/05/20,S---------
                this.closeAgentMenu.Text = res.GetString("SM0070002");
                //add,xzg,2009/05/20,E---------

                this.subMenuReFresh.Text = res.GetString("SM0020062");
                autoCtlSize(this);

            }
            catch (Exception ex)
            {
                writeLog("iniForm:" + ex.Message);
                MessageBox.Show(ex.Message);

            }
        }
        private void autoCtlSize(Control inObj)
        {
            try
            {
                if (inObj == null) return;
                Size deskTopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
                //Single fontSize = inObj.Font.Size * deskTopSize.Height / 768;
                inObj.Size = new Size((int)(inObj.Size.Width * deskTopSize.Width / 1024), (int)(inObj.Size.Height * deskTopSize.Height / 768));
                inObj.Location = new Point((int)(inObj.Location.X * deskTopSize.Width / 1024), (int)(inObj.Location.Y * deskTopSize.Height / 768));
                int i;
                for (i = 0; i < inObj.Controls.Count; i++)
                {
                    //inObj.Controls[i].Font = new Font(inObj.Controls[i].Font.FontFamily, fontSize);
                    autoCtlSize(inObj.Controls[i]);
                }
            }
            catch (Exception ex)
            {
                writeLog("autoCtlSize:" + ex.Message);


            }
        }

        private void lineUseageProgressLabel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Label ctrl = (Label)sender;
                Rectangle clientRect = ctrl.ClientRectangle;
                int value = (int)ctrl.Tag;

                // 背景の描画
                SolidBrush backBrush = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(backBrush, clientRect);

                // バーの描画
                Rectangle barRect = ctrl.ClientRectangle;
                barRect.Width = (int)(barRect.Width * (value / 100.0));
                SolidBrush barBrush = new SolidBrush(Color.Blue);
                e.Graphics.FillRectangle(barBrush, barRect);

                // Textの描画
                string text = String.Format("{0:d} %", value);
                SolidBrush textBrush = new SolidBrush(Color.White);
                // Make StringFormat
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(text, ctrl.Font, textBrush, clientRect, format);
            }
            catch (Exception ex)
            {
                writeLog("autoCtlSize:" + ex.Message);


            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //add,xzg,2011/08/10,S

            try
            {

                startService();

            }
            catch (Exception ex1)
            {
                writeLog("MainForm_Shown:startService:" + ex1.Message);

            }

            ////add,xzg,2011/08/10,E

            try
            {
                //    //    //add,xzg,SM->MSGSRV,S
                //    //    // Create axCpfMsgUp
                axCpfMsgUp = new CpfMsgUpClass();
                //    axCpfMsgUp.OnClose += new _ICpfMsgUpEvents_OnCloseEventHandler(axCpfMsgUp_OnClose);
                axCpfMsgUp.OnLogon += new _ICpfMsgUpEvents_OnLogonEventHandler(axCpfMsgUp_OnLogon);
                //    axCpfMsgUp.OnLogoff += new _ICpfMsgUpEvents_OnLogoffEventHandler(axCpfMsgUp_OnLogoff);
                //    axCpfMsgUp.OnEvent += new _ICpfMsgUpEvents_OnEventEventHandler(axCpfMsgUp_OnEvent);
                axCpfMsgUp.Open("127.0.0.1", 18210);
                //    //add,xzg,2011/06/20,E
            }
            catch (Exception ex1)
            {
                writeLog("MainForm_Shown:axCpfMsgUp.Open:" + ex1.Message);
                //MessageBox.Show(ex1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Load Profile
            try
            {
                // Load Profile
                //iniProfile.Load(MyTool.GetModuleIniPath());
                // Read Alert
                IniProfile.SelectSection("Alert");
                alertTotal = IniProfile.GetLongDefault("nTotal", 100);
                alertGroup = IniProfile.GetLongDefault("nGroup", 100);
                string phone = "";
                IniProfile.SelectSection("SVSet");
                phone = IniProfile.GetStringDefault("Phone", "");
                SVPhone = phone;
                string strShowCol = IniProfile.GetStringDefault("ShowCol", "");
                if (!string.IsNullOrEmpty(strShowCol))
                {
                    //modified by zhu 2014/05/07
                    //if (strShowCol.Length >= 14)
                    //    ShowCol = strShowCol;
                    if (strShowCol.Length >= 15)
                        ShowCol = strShowCol;
                    //end modified
                }
                setAgentStatusView();

                SettingFields_LineCutShow = IniProfile.GetStringDefault(ConstEntity.LINECUT, "");
                SettingFields_MonitorTabShow = IniProfile.GetStringDefault(ConstEntity.MONITORTAB, "");

                OptionName1 = IniProfile.GetStringDefault("Option1", "拠点");
                OptionName2 = IniProfile.GetStringDefault("Option2", "種別");
                OptionName3 = IniProfile.GetStringDefault("Option3", "Option3");
                OptionName4 = IniProfile.GetStringDefault("Option4", "Option4");
                OptionName5 = IniProfile.GetStringDefault("Option5", "Option5");
                setOptionNameView(OptionName1, OptionName2, OptionName3, OptionName4, OptionName5);


                //add,xzg,2012/08/01,S
                SettingFields_StatusOverIdelTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE1, DefaultOverTime);
                SettingFields_StatusOverIdelTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE2, DefaultOverTime);
                SettingFields_StatusOverWorkTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME1, DefaultOverTime);
                SettingFields_StatusOverWorkTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME2, DefaultOverTime);
                SettingFields_StatusOverLeaveTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE1, DefaultOverTime);
                SettingFields_StatusOverLeaveTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE2, DefaultOverTime);
                SettingFields_StatusOverTalkTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK1, DefaultOverTime);
                SettingFields_StatusOverTalkTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK2, DefaultOverTime);
                SettingFields_StatusOverHoldTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD1, DefaultOverTime);
                SettingFields_StatusOverHoldTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD2, DefaultOverTime);
                //added by zhu 2014/09/11
                SettingFields_StatusOverQuecallTime1 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL1, DefaultOverTime);
                SettingFields_StatusOverQuecallTime2 = DefaultOverTime + IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL2, DefaultOverTime);
                //end added

                SettingFields_StatusOverIdelTime1 = SettingFields_StatusOverIdelTime1.Substring(SettingFields_StatusOverIdelTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverIdelTime2 = SettingFields_StatusOverIdelTime2.Substring(SettingFields_StatusOverIdelTime2.Length - DefaultOverTime.Length);

                SettingFields_StatusOverWorkTime1 = SettingFields_StatusOverWorkTime1.Substring(SettingFields_StatusOverWorkTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverWorkTime2 = SettingFields_StatusOverWorkTime2.Substring(SettingFields_StatusOverWorkTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverLeaveTime1 = SettingFields_StatusOverLeaveTime1.Substring(SettingFields_StatusOverLeaveTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverLeaveTime2 = SettingFields_StatusOverLeaveTime2.Substring(SettingFields_StatusOverLeaveTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverTalkTime1 = SettingFields_StatusOverTalkTime1.Substring(SettingFields_StatusOverTalkTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverTalkTime2 = SettingFields_StatusOverTalkTime2.Substring(SettingFields_StatusOverTalkTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverHoldTime1 = SettingFields_StatusOverHoldTime1.Substring(SettingFields_StatusOverHoldTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverHoldTime2 = SettingFields_StatusOverHoldTime2.Substring(SettingFields_StatusOverHoldTime2.Length - DefaultOverTime.Length);
                //add,xzg,2012/08/01,E

                //added by zhu 2014/09/11
                SettingFields_StatusOverQuecallTime1 = SettingFields_StatusOverQuecallTime1.Substring(SettingFields_StatusOverQuecallTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverQuecallTime2 = SettingFields_StatusOverQuecallTime2.Substring(SettingFields_StatusOverQuecallTime2.Length - DefaultOverTime.Length);
                //end added

                QuickAnswerMinutes = IniProfile.GetStringDefault("QuickAnswerMinutes", "0");
                //add,xzg,2009/06/18,S---------
                string keepAlive = "";
                keepAlive = IniProfile.GetStringDefault("KeepAlive", "");
                if (string.IsNullOrEmpty(keepAlive))
                    keepAlive = "15";
                //add,xzg,2009/06/18,E---------

                //add,xzg,2009/10/30,S----------
                string strRefreshTime = "";
                strRefreshTime = IniProfile.GetStringDefault("Refresh", "10");
                if (string.IsNullOrEmpty(keepAlive))
                    strRefreshTime = "10";
                refreshTime = int.Parse(strRefreshTime);
                //add,xzg,2009/10/30,E----------

                QuePeriod1 = IniProfile.GetStringDefault("QuePeriod1", "");
                QuePeriodVoice1 = IniProfile.GetStringDefault("QuePeriodVoice1", "");

                QuePeriod2 = IniProfile.GetStringDefault("QuePeriod2", "");
                QuePeriodVoice2 = IniProfile.GetStringDefault("QuePeriodVoice2", "");

                QuePeriod3 = IniProfile.GetStringDefault("QuePeriod3", "");
                QuePeriodVoice3 = IniProfile.GetStringDefault("QuePeriodVoice3", "");

                //add,xzg,2009/02/04,S---                
                //MonitorStatus = "";
                MonitorStatus = MONITOR_STATUS_IDLE;
                setFrmText(MonitorStatus);
                //add,2009/02/04,S---
                // Start connectTimer
                connectTimer.Interval = 10;


                //added by Zhu 2014/03/25
                IdlePeriodLongString = IniProfile.GetStringDefault("SkillIdlePeriod", "");
                IdlePeriodVoiceLongString = IniProfile.GetStringDefault("SkillIdlePeriodVoice", "");
                SkillShowSetString = IniProfile.GetStringDefault(ConstEntity.SKILLSHOWKEY, "");
                //deleted by zhu 2014/05/12
                //MonitorItemShowString = iniProfile.GetStringDefault(ConstEntity.ITEMSHOWKEY, "");
                //end deleted
                SkillSoundPlayerManager.GetDefaultPlayersFromIni(IdlePeriodLongString);
                //end added

                //update,xzg,2009/01/21,S------
                //connectTimer.Enabled = true;
                //xzg,2013/11/28,S
                //connectTimer_Tick(sender, e);
                //xzg,2013/11/28,E
                //update,xzg,2009/01/21,E------

                //add,2014/03,ShowMonitorF
                if (ShowMonitorF == "1")
                {
                    connectTimer_Tick(sender, e);
                }
                //add,xzg,2009/06/18,S---------
                KeepAliveTime = int.Parse(keepAlive);
                //keepAlivetimer.Interval = 60000;
                //xzg,2013/11/28,S
                //keepAlivetimer.Interval = 1000;
                //keepAlivetimer.Enabled = true;
                //xzg,2013/11/28,E
                //add,xzg,2009/06/18,E---------
                //added by zhu 2014/09/01
                this.UpdateContinueTimer.Interval = this.refreshTime * 1000;
                //this.UpdateContinueTimer.Start();
                //end added;

                AjustListFontSize();
            }
            catch (Exception ex)
            {
                writeLog("MainForm_Shown:" + ex.Message);
                string desc = String.Format("Message: {0}\nSource: {1}\nType: {2}", ex.Message, ex.Source, ex.GetType());
                MessageBox.Show(desc, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Set formClosing
                formClosing = true;
                this.Close();
            }
        }

        private void setAgentStatusView()
        {
            try
            {
                // for (int i = 0; i <= 13; i++)
                //modified by zhu 2014/05/07 start
                //for (int i = 1; i <= 13; i++)
                for (int i = 1; i <= 15; i++)
                //end modified
                {
                    if (ShowCol.Substring(i, 1) == "0")
                    {
                        agentStatusListView.Columns[i].Width = 0;
                        if (i == 1)
                        {
                            this.comboBox1.Visible = false;
                            this.label3.Visible = false;
                        }
                        else if (i == 2)
                        {
                            this.comboBox2.Visible = false;
                            this.label4.Visible = false;
                        }
                        else if (i == 3)
                        {
                            this.comboBox3.Visible = false;
                            this.label5.Visible = false;
                        }
                        else if (i == 4)
                        {
                            this.comboBox4.Visible = false;
                            this.label6.Visible = false;
                        }
                        else if (i == 5)
                        {
                            this.comboBox5.Visible = false;
                            this.label7.Visible = false;
                        }
                        //else if (i == 6)
                        //{
                        //    this.comboBox6.Visible = false;
                        //    this.label8.Visible = false;
                        //}
                        else if (i == 12)//11->12 2014/05/07 modified by zhu
                        {
                            this.comboBox7.Visible = false;
                            this.label10.Visible = false;
                        }

                    }
                    else
                    {
                        agentStatusListView.Columns[i].Width = 80;
                        if (i == 1)
                        {
                            this.comboBox1.Visible = true;
                            this.label3.Visible = true;
                        }
                        else if (i == 2)
                        {
                            this.comboBox2.Visible = true;
                            this.label4.Visible = true;
                        }
                        else if (i == 3)
                        {
                            this.comboBox3.Visible = true;
                            this.label5.Visible = true;
                        }
                        else if (i == 4)
                        {
                            this.comboBox4.Visible = true;
                            this.label6.Visible = true;
                        }
                        else if (i == 5)
                        {
                            this.comboBox5.Visible = true;
                            this.label7.Visible = true;
                        }

                        //else if (i == 6)
                        //{
                        //    this.comboBox6.Visible = true;
                        //    this.label8.Visible = true;
                        //}
                        else if (i == 12)//11->12 2014/05/07 modified by zhu
                        {
                            this.comboBox7.Visible = true;
                            this.label10.Visible = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                writeLog("setAgentStatusView:" + ex.Message);

            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check formClosing
            if (formClosing) return;
            // 終了確認
            //DialogResult ret = MessageBox.Show("終了しますか？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (ret == DialogResult.Cancel) e.Cancel = true;
            //add,xzg,2009/02/06,S----------------
            else
            {
                int msgCount = 0;
                int loop = 0;
                if (msgFromList.Count > 0)
                {
                    msgCount = msgFromList.Count;
                    for (loop = 0; loop < msgCount; loop++)
                    {
                        MessageForm msgFrom = (MessageForm)msgFromList[loop];
                        msgFrom.Close();
                    }
                }

                //added by zhu 2014/05/13
                foreach (var form in ListTabPagesForms)
                {
                    form.Close();
                }
                //end adde
            }
            //add,xzg,2009/02/06,S----------------

            //add,xzg,2011/08/10,S
            try
            {
                stopService();
            }
            catch (Exception ex)
            {
                writeLog("MainForm_FormClosing:stopService:" + ex.Message);
            }

            //add,xzg,2011/08/10,E

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Set formClosing
            formClosing = true;
            // Close Cpfmagacx            
            axCpfMsg.Close();
        }

        private void menuFileEnd_Click(object sender, EventArgs e)
        {
            // Menu File End
            //update,xzg,2011/09/07,S
            //this.Close();
            this.Visible = false;
            return;
            //update,xzg,2011/09/07,E
        }

        private void connectTimer_Tick(object sender, EventArgs e)
        {


            // Open Cpfmagacx
            try
            {


                // Read StatusMonitor
                IniProfile.SelectSection("StatusMonitor");
                string cpfmsgsvrAddr = IniProfile.GetStringDefault("sCpfmsgsvrAddr", "");
                int cpfmsgsvrPort = IniProfile.GetLongDefault("nCpfmsgsvrPort", 0);
                // Read MainSess
                IniProfile.SelectSection("SessMain");
                string server = IniProfile.GetString("sServer");
                //update,xzg,2012/08/23,S
                //string media = iniProfile.GetString("sMedia") + "S"; //S cpfMsg２重化接続を避けるため 2013/09/03
                string media = IniProfile.GetStringDefault("sMediaSet", "");
                if (string.IsNullOrEmpty(media) || media == "null")
                {
                    media = IniProfile.GetString("sMedia") + "S";
                }
                //update,xzg,2012/08/23,E
                string name = IniProfile.GetString("sName");

                string appl = IniProfile.GetString("sAppl");
                string phase = IniProfile.GetStringDefault("sPhase", "Phase");
                string logon = IniProfile.GetStringDefault("sLogon", "MAIN");
                // Open Cpfmagacx
                writeLog("axCpfMsg open");

                axCpfMsg.Open(cpfmsgsvrAddr, cpfmsgsvrPort, server, media, name, appl, phase, logon);

                // Set Title
                //this.Text = "NGCP 状態モニタ";

                //add,xzg,2013/11/28,S
                keepAlivetimer.Interval = 1000;
                keepAlivetimer.Enabled = true;
                //add,xzg,2013/11/28,E


                ListUpdateTimer.Enabled = true;


                AgentTimer.Enabled = true;
                CallTimer.Enabled = true;

                setFrmText(MonitorStatus);
                mainNotifyIcon.Text = res.GetString(NOTIFYICON_TEXT);
                // Stop connectTimer
                connectTimer.Enabled = false;




            }
            catch (Exception ex)
            {
                writeLog("connectTimer_Tick:" + ex.Message);
                string desc = String.Format("Message: {0}\nSource: {1}\nType: {2}", ex.Message, ex.Source, ex.GetType());
                //DebugPrint("connectTimer_Tick/" + desc);
                connectTimer.Interval = 3000;
            }
        }

        private void axCpfMsg_OnCommand(object sender, AxCpfmsgacxa._ICpfMsgEvents_OnCommandEvent e)
        {
            //axCpfMsgUp.SendCommand("Server:MONITOR_*", e.sPhase, e.sField, e.sCommand, e.objCpfParams.GetParams());
            try
            {

                writeLog(e.sCommand + ":" + e.objCpfParams.GetParams());
                //foreach (string sChannel in lstChannel)
                //{
                //    axCpfMsgUp.SendCommand(sChannel, e.sPhase, e.sField, e.sCommand, e.objCpfParams.GetParams());                    
                //}
                //axCpfMsgUp.SendCommand("Server:AGENTMONITOR_*", e.sPhase, e.sField, e.sCommand, e.objCpfParams.GetParams());

            }
            catch (Exception ex1)
            {
                writeLog("axCpfMsg_OnCommand:axCpfMsgUp:" + ex1.Message);
            }
            try
            {

                OnRecvCommand(e.sCommand, e.objCpfParams);
            }
            catch (Exception ex)
            {
                writeLog("axCpfMsg_OnCommand:OnRecvCommand:" + ex.Message + ex.StackTrace);
                //string desc = String.Format("Message: {0}\nSource: {1}\nType: {2}", ex.Message, ex.Source, ex.GetType());
                //MessageBox.Show(desc, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void axCpfMsg_OnClose(object sender, EventArgs e)
        {
            // Check formClosing
            try
            {

                writeLog("axCpfMsg_OnClose");
                keepAlivetimer.Enabled = false;
                ListUpdateTimer.Enabled = false;
                AgentTimer.Enabled = false;
                CallTimer.Enabled = false;
            }
            catch { }

            if (formClosing) return;
            //MessageBox.Show("axCpfMsg_OnClose");

            // Set Title
            //this.Text = "NGCP 状態モニタ - 未接続";
            //del,xzg,2009/12/07,S---------
            //setFrmText(MonitorStatus);
            //mainNotifyIcon.Text = res.GetString(NOTIFYICON_TEXT);
            //// Clear lineUseageProgressLabel
            //lineUseageProgressLabel.Tag = 0;
            //lineUseageProgressLabel.Invalidate();
            //useageLabel.Text = "0 / 0";
            //del,xzg,2009/12/07,E---------
            //del,xzg,2009/12/07,S---------
            // Clear groupComboBox
            //for(int i = groupComboBox.Items.Count - 1; i >= 0; --i)
            //{
            //    GroupInfo groupInfo = (GroupInfo)groupComboBox.Items[i];
            //    if(groupInfo.Group != -1) groupComboBox.Items.RemoveAt(i);
            //}
            //groupComboBox.SelectedIndex = 0;
            //del,xzg,2009/12/07,E---------
            // Clear Variables
            //del,xzg,2009/12/07,S---------
            //lineUseage.LineNumber = 0;
            //lineUseage.BusyCount = 0;
            //groupInfoList.Clear();
            //queueStatusList.Clear();
            //lineStatusList.Clear();
            //agentStatusList.Clear();
            //del,xzg,2009/12/07,E---------
            // Update Display
            //del,xzg,2009/12/07,S---------
            //DisplayLine();
            //DisplayAgent();
            //DisplayTotal();
            //del,xzg,2009/12/07,E---------

            // Start connectTimer
            connectTimer.Interval = 3000;
            connectTimer.Enabled = true;
        }

        private void mainNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
            //modified by Zhu 2014/04/09
            if (this.Visible) this.WindowState = FormWindowState.Normal;
            if (this.Visible) this.WindowState = FormWindowState.Maximized;
            //end modified
        }

        private void mainNotifyMenuShow_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
            //modified by Zhu 2014/04/09
            if (this.Visible) this.WindowState = FormWindowState.Normal;
            if (this.Visible) this.WindowState = FormWindowState.Maximized;
            //end modified
        }

        private void selectViewButton_Click(object sender, EventArgs e)
        {
            if (selectViewButton.Text == res.GetString("SM0020039"))
            {
                selectViewButton.Text = res.GetString("SM0020038");
                agentStatusListView.View = View.Details;
                lineStatusListView.View = View.Details;
            }
            else
            {
                selectViewButton.Text = res.GetString("SM0020039");
                agentStatusListView.View = View.Tile;
                lineStatusListView.View = View.Tile;
            }
        }

        //private void menuHelpAboutBox_Click(object sender, EventArgs e)
        //{
        //    AboutBox aboutBox = new AboutBox();
        //    aboutBox.ShowDialog();
        //}

        private void groupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                displayGroup = groupInfo.Group;
                // Update Display
                DisplayLine();
                DisplayAgent();

                //add,2014/03,ShowMonitorF
                if (ShowMonitorF != "1")
                {
                    //added by zhu 2015/10/09 
                    if (SettingFields_MonitorTabShow == "0") return;
                    //end added
                    //add,xzg,monitorReport,S
                    setMonitorCall();
                    //add,xzg,monitorReport,E
                }
            }
            catch (Exception ex)
            {
                writeLog("groupComboBox_SelectedValueChanged:" + ex.Message);


            }
        }
        //add,xzg,2008/04/22,S-----------
        private void agentStatusListView_DoubleClick(object sender, EventArgs e)
        //private void agentStatusListView_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) 
        {


            //if (MessageBox.Show("通話をモニターしますか？", "NGCP", MessageBoxButtons.OKCancel) == DialogResult.Cancel )
            //{
            //    return;
            //}
            try
            {
                ListView lv = (ListView)sender;
                if (lv.SelectedItems.Count < 1)
                {
                    return;
                }
                AgentStatus agentStatus = new AgentStatus();
                ListViewItem item = new ListViewItem();
                item = lv.SelectedItems[0];
                if (item.SubItems.Count < 6)
                {
                    return;
                }
                //string vAgentID = item.SubItems[7].Text;
                string vAgentID = item.SubItems["Agent"].Text;  //2011/07/06 8->9,9->14,modified by zhu 2014/04/17 14->15
                //item.SubItems["111"].Text;
                if (vAgentID.Length < 1)
                {
                    return;
                }
                if (msgFromID.IndexOf(vAgentID) >= 0)
                {

                }
                else
                {
                    MessageForm msgFrom = new MessageForm();
                    msgFrom.axCpfMsg1 = this.axCpfMsg;
                    msgFrom.SkillID = item.SubItems["GroupName"].Text;//0->6,modified by zhu 2014/04/17 6->7
                    //msgFrom.AgentID = item.SubItems[7].Text;
                    msgFrom.AgentID = item.SubItems["Agent"].Text; //2011/07/06 8->9,9->14,modified by zhu 2014/04/17 14->15
                    msgFrom.AgentName = item.SubItems["AgentName"].Text; //1->6 ,6->0
                    msgFrom.Status = item.SubItems["State"].Text;//2->7,modified by zhu 2014/04/17 7->8
                    msgFrom.mainF = this;
                    msgFromID.Add(vAgentID);
                    msgFromList.Add(msgFrom);
                    msgFrom.Show();
                    msgFrom.Activate();
                }
            }
            catch (Exception ex)
            {
                writeLog("agentStatusListView_DoubleClick:" + ex.Message);
            }

        }
        //add,xzg,2008/04/22,E-----------
        private void NoSelectListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count != 0)
            {
                //lv.SelectedItems[0].Focused = false;
                //lv.SelectedItems[0].Selected = false;
            }
        }

        class StatusListViewItemComparer : System.Collections.IComparer
        {
            private int Column = 0;
            private bool Ascending = true;

            public StatusListViewItemComparer(int column, bool ascending)
            {
                Column = column;
                Ascending = ascending;
            }
            public int Compare(object x, object y)
            {
                // Get Value
                string tx = ((ListViewItem)x).SubItems[Column].Text;
                string ty = ((ListViewItem)y).SubItems[Column].Text;
                // Compare
                int ret = String.Compare(tx, ty);
                if (!Ascending) ret *= -1;
                if (ret == 0)
                {
                    tx = ((ListViewItem)x).SubItems[0].Text;
                    ty = ((ListViewItem)y).SubItems[0].Text;
                    ret = String.Compare(tx, ty);
                }
                if (ret == 0)
                {
                    tx = ((ListViewItem)x).SubItems[1].Text;
                    ty = ((ListViewItem)y).SubItems[1].Text;
                    ret = String.Compare(tx, ty);
                }
                return ret;
            }
        }


        private void agentStatusListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            bool order = !AgentStatusListViewOrder[e.Column];
            AgentStatusListViewOrder[e.Column] = order;
            ((ListView)sender).ListViewItemSorter = new StatusListViewItemComparer(e.Column, order);
        }


        private void lineStatusListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool order = !lineStatusListViewOrder[e.Column];
            lineStatusListViewOrder[e.Column] = order;
            ((ListView)sender).ListViewItemSorter = new StatusListViewItemComparer(e.Column, order);
        }

        class TotalListViewItemComparer : System.Collections.IComparer
        {
            private int Column = 0;
            private bool Ascending = true;
            //private static bool[] ColumnItemInt = { false, true, true, true, true };
            private static bool[] ColumnItemInt = { false, true, true, true, true, false, true, true, true, true, true };

            public TotalListViewItemComparer(int column, bool ascending)
            {
                Column = column;
                Ascending = ascending;
            }
            public int Compare(object x, object y)
            {
                // Check Top
                int row = ((ListViewItem)x).Index;
                if (row == 0) return 0;
                // Get Value
                string tx = ((ListViewItem)x).SubItems[Column].Text;
                string ty = ((ListViewItem)y).SubItems[Column].Text;
                // Compare
                int ret = 0;
                if (ColumnItemInt[Column]) ret = Int32.Parse(tx).CompareTo(Int32.Parse(ty));
                else ret = String.Compare(tx, ty);

                //update,xzg,2014/05/13,S
                //return (Ascending ? ret: -ret);
                if (!Ascending) ret *= -1;
                //update,xzg,2014/05/13,E

                //add,2014/05/13
                if (ret == 0)
                {
                    tx = ((ListViewItem)x).SubItems[0].Text;
                    ty = ((ListViewItem)y).SubItems[0].Text;
                    ret = String.Compare(tx, ty);
                }
                return ret;
            }
        }



        private void totalListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                bool order = !TotalListViewOrder[e.Column];
                TotalListViewOrder[e.Column] = order;
                ((ListView)sender).ListViewItemSorter = new TotalListViewItemComparer(e.Column, order);
            }
            catch (Exception ex)
            {
                writeLog("totalListView_ColumnClick system error:" + ex.Message);
            }
        }


        //add,xzg,2009/05/20,S----

        private void agentStatusListView_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (MouseButtons.Right == e.Button)
                {
                    ListView lv = (ListView)sender;
                    if (lv.SelectedItems.Count < 1)
                    {
                        return;
                    }

                    ListViewItem item = new ListViewItem();
                    item = lv.SelectedItems[0];
                    if (item.SubItems.Count < 6)
                    {
                        return;
                    }
                    string vAgentID = item.SubItems["Agent"].Text; //2011/07/06 8->9,9->14,modified by zhu 2014/04/17 14->15
                    if (vAgentID.Length < 1)
                    {
                        return;
                    }
                    vSelectedAgentID = vAgentID;
                    vCurrentAgentID = vAgentID;
                    vCurrentAgentName = item.SubItems["AgentName"].Text; //1->6,6->0
                    vCurrentAgentState = item.SubItems["Status"].Text;//10->15,modified by zhu 2014/04/17 15->16
                    if (string.IsNullOrEmpty(vCurrentAgentState)) vCurrentAgentState = "";
                    if (vCurrentAgentState == "1" || vCurrentAgentState == "5" || vCurrentAgentState == "6")
                        subMenuAgentState.Visible = true;
                    else
                        subMenuAgentState.Visible = false;
                    RightMenu.Show(this.agentStatusListView, e.X, e.Y);
                }
            }
            catch (Exception ex)
            {
                writeLog("agentStatusListView_MouseClick:" + ex.Message);
            }
        }

        private void closeAgentMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(vSelectedAgentID))
                {
                    if ((vCurrentAgentState != "0" && vCurrentAgentState != "1" && vCurrentAgentState != "5" && vCurrentAgentState != "6") && MessageBox.Show("対象のエージェントは通話中ですが、終了しますか？", "alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    CpfParams cpfParam = new CpfParams();
                    cpfParam.AddString("vAgentID", vSelectedAgentID);
                    axCpfMsg.SendEvent("", "", UM_FORCE_LOGOUT, cpfParam);
                    writeLog("closeAgentMenu_Click:UM_FORCE_LOGOUT" + cpfParam.GetParams());

                    vSelectedAgentID = "";
                    System.Threading.Thread.Sleep(50);
                    //added by zhu 2014/12/09 only get the command AGENTCOUNT， then can do again
                    if (this.ReShowsFlag)
                    {
                        ReShowsFlag = false;
                        this.button1.Enabled = false;
                        //add,xzg,2010/04/12,S-----------
                        cpfParam = new CpfParams();
                        axCpfMsg.SendEvent("", "", "AM_SHOWAGENT", cpfParam);
                    }


                    //add,xzg,2010/04/12,E-----------
                }
            }
            catch (Exception ex)
            {
                writeLog("closeAgentMenu_Click:" + ex.Message);
            }
        }

        private void keepAlivetimer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (lblHelpON.Text.CompareTo("0") > 0)
                {
                    //if (lblHelpON.Visible)
                    //    lblHelpON.Visible = false;
                    //else
                    //    lblHelpON.Visible = true;

                    if (lblHelpON.ForeColor == Color.Red)
                        lblHelpON.ForeColor = Color.White;
                    else
                        lblHelpON.ForeColor = Color.Red;

                }
                else
                {

                    if (lblHelpON.ForeColor != label9.ForeColor)
                        lblHelpON.ForeColor = label9.ForeColor;
                    //if (lblHelpON.Visible == false)
                    //    lblHelpON.Visible = true;
                }

                KeepAliveCount = KeepAliveCount + 1;
                //add,xzg,2009/10/30,S--------
                refreshCount = refreshCount + 1;
                //add,xzg,2009/10/30,E--------
                if (KeepAliveTime == KeepAliveCount)
                {
                    KeepAliveCount = 0;
                    CpfParams cpfParam = new CpfParams();
                    cpfParam.AddString("vKeepAlive", "1");
                    axCpfMsg.SendEvent("", "", "UM_KEEPALIVE", cpfParam);


                    //
                    //writeLog("keepAlivetimer_Tick:axCpfMsgUp:再Open");
                    //axCpfMsgUp.Open("127.0.0.1", 18210);

                }
                //add,xzg,2009/10/30,S--------
                if (refreshTime <= refreshCount)
                {
                    refreshCount = 0;
                    //update 継続時間
                    //deleted by zhu 2014/09/01
                    //use system.timers to do the update
                    updateContinue();
                    //end deleted
                }
                //add,xzg,2009/10/30,E--------

                //add,xzg,2011/06/20,S--
                //if (updateAllStatus == true)
                //{
                //    updateAllStatus = false;
                //    CpfParams cpfParam = new CpfParams();
                //    axCpfMsg.SendEvent("", "", "AM_SHOWAGENT", cpfParam);
                //}
                //add,xzg,2011/06/20,E--
            }
            catch (Exception ex)
            {
                writeLog("keepAlivetimer_Tick:" + ex.Message);
                //string desc = String.Format("Message: {0}\nSource: {1}\nType: {2}", ex.Message, ex.Source, ex.GetType());
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //add,xzg,2009/05/20,E----
        //add,xzg,2013/01/29,S

        //add,xzg,2013/01/29,E
        private void DoContinueAgentStatus()
        {
            try
            {
                agentStatusListView.BeginUpdate();
                int listCount = agentStatusListView.Items.Count;
                int i = 0;
                for (i = 0; i < listCount; i++)
                {
                    ListViewItem item = agentStatusListView.Items[i];
                    //DateTime continueTime = DateTime.Parse(item.SubItems[4].Text);                
                    //continueTime=continueTime.AddSeconds(1);
                    //agentStatusListView.Items[i].SubItems[4].Text  = continueTime.ToString("HH:mm:ss");
                    //continueTime =System.Convert.ToString(int.Parse(continueTime) + 1);

                    string continueTime = item.SubItems["StatusContinueTime"].Text; //4->9,modified by zhu 2014/04/17 9->10
                    string sumSS = "";
                    string[] timeList = continueTime.Split(':');

                    string strTime = "00";
                    //int iTime;
                    if (timeList.Length == 1)
                    {
                        sumSS = timeList[0];

                    }
                    else if (timeList.Length == 2)
                    {
                        sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 60 + int.Parse(timeList[1]));
                    }
                    else if (timeList.Length == 3)
                    {
                        sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 3600 + int.Parse(timeList[1]) * 60 + int.Parse(timeList[2]));
                    }

                    if (string.IsNullOrEmpty(sumSS))
                    {
                        sumSS = "00";
                    }
                    strTime = DoContinueTime(sumSS);
                    agentStatusListView.Items[i].SubItems["StatusContinueTime"].Text = strTime;    //4->9,modified by zhu 2014/04/17 9->10

                    //modified by zhu 2015/10/23 change help color to green
                    //if (item.BackColor != Color.Pink)
                    if (item.BackColor != CTe1Helper.AgentHelpColor)
                    {
                        string status = item.SubItems["Status"].Text;//10->15,modified by zhu 2014/04/17 15->16
                        agentStatusListView.Items[i].BackColor = Color.Empty;
                        agentStatusListView.Items[i].BackColor = GetAgentListItemBackColor(strTime, int.Parse(status));
                    }


                }
                agentStatusListView.EndUpdate();
            }
            catch (Exception ex)
            {
                writeLog("DoContinueAgentStatus System Error:" + ex.Message);
            }
        }
        private void DoContinueLineStatus()
        {
            try
            {
                lineStatusListView.BeginUpdate();
                int listCount = lineStatusListView.Items.Count;
                int i = 0;
                for (i = 0; i < listCount; i++)
                {

                    ListViewItem item = lineStatusListView.Items[i];
                    //added by zhu add the case quecall,which continue time is current time - status time
                    if (item.SubItems["Service"].Text.ToUpper() == "QUECALL")
                    {
                        if (item.SubItems["StatusTime"] != null)
                        {
                            DateTime statusTime = DateTime.Parse(item.SubItems["StatusTime"].Text);
                            if (DateTime.Compare(DateTime.Now, statusTime) <= 0)
                            {
                                lineStatusListView.Items[i].SubItems["StatusContinueTime"].Text = "00:00:00";
                            }
                            else
                            {
                                string strTime = Convert.ToDateTime(DateTime.Now.Subtract(statusTime).ToString()).ToString("HH:mm:ss");
                                lineStatusListView.Items[i].SubItems["StatusContinueTime"].Text = strTime;
                            }
                        }
                        else
                        {

                            lineStatusListView.Items[i].SubItems["StatusContinueTime"].Text = "00:00:00";
                        }
                    }
                    else
                    {
                        string continueTime = item.SubItems["StatusContinueTime"].Text;
                        string sumSS = "";
                        string[] timeList = continueTime.Split(':');

                        string strTime = "00";
                        //int iTime;
                        if (timeList.Length == 1)
                        {
                            sumSS = timeList[0];

                        }
                        else if (timeList.Length == 2)
                        {
                            sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 60 + int.Parse(timeList[1]));
                        }
                        else if (timeList.Length == 3)
                        {
                            sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 3600 + int.Parse(timeList[1]) * 60 + int.Parse(timeList[2]));
                        }

                        if (string.IsNullOrEmpty(sumSS))
                        {
                            sumSS = "00";
                        }
                        strTime = DoContinueTime(sumSS);

                        lineStatusListView.Items[i].SubItems["StatusContinueTime"].Text = strTime;
                    }

                }
                lineStatusListView.EndUpdate();
            }
            catch (Exception ex)
            {
                writeLog("DoContinueLineStatus System error:" + ex.Message);
            }
        }
        private void updateContinue()
        {
            try
            {
                DoContinueAgentStatus();
                DoContinueLineStatus();
                //added by zhu 2014/04/21
                RefreshTabForm();
                //end added
                //added by zhu 2015/10/09
                DoContinueTotalList();
                //end added
                //added by zhu 2014/05/13
                DoContinueMonitor();
                //end adde
            }
            catch (Exception ex)
            {
                writeLog("updateContinue error:" + ex.Message);
            }

        }


        //added by zhu 2014/04/21
        private void RefreshTabForm()
        {
            (this.ListTabPagesForms[0] as QueueCallForm).DoContinueLineStatus();
        }
        //end added
        //added by zhu 2014/05/13
        private void DoContinueMonitor()
        {
            if (SettingFields_MonitorTabShow == "0") return;
            int index = 0;
            try
            {
                List<LineStatus> templineStatusList = new List<LineStatus>();
                //List<LineStatus> tempCurrentlineStatusList = new List<LineStatus>(lineStatusList.ToArray());
                //dsMontor.Tables["dtMonitor"].BeginLoadData();
                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    string curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();
                    //added by Zhu 2014/06/26
                    if (i == DtMonitorRowsCount - 1)
                        break;
                    //end added

                    if (curGroupID == "-1") continue;
                    templineStatusList = lineStatusList.FindAll(p => p.iSkillGroupID == int.Parse(curGroupID) && p.Service == "QUECALL");
                    //templineStatusList.Sort((x, y) => y.StatusContinueTime.CompareTo(x.StatusContinueTime));
                    if (templineStatusList.Count > 0)
                    {
                        string strTime = "00:00:00";
                        DateTime statusTime = templineStatusList[0].StatusTime;
                        try
                        {
                            if (DateTime.Compare(DateTime.Now, statusTime) <= 0)
                            {
                                strTime = "00:00:00";
                            }
                            else
                            {
                                strTime = Convert.ToDateTime(DateTime.Now.Subtract(statusTime).ToString()).ToString("HH:mm:ss");
                            }
                        }
                        catch
                        {
                            strTime = "00:00:00";
                        }

                        //string continueTime = templineStatusList[0].StatusContinueTime;
                        //string sumSS = "";
                        //string[] timeList = continueTime.Split(':');

                        //string strTime = "00";
                        //if (timeList.Length == 1)
                        //{
                        //    sumSS = timeList[0];

                        //}
                        //else if (timeList.Length == 2)
                        //{
                        //    sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 60 + int.Parse(timeList[1]));
                        //}
                        //else if (timeList.Length == 3)
                        //{
                        //    sumSS = System.Convert.ToString(int.Parse(timeList[0]) * 3600 + int.Parse(timeList[1]) * 60 + int.Parse(timeList[2]));
                        //}

                        //if (string.IsNullOrEmpty(sumSS))
                        //{
                        //    sumSS = "00";
                        //}
                        //strTime = DoContinueTime(sumSS);
                        dsMontor.Tables["dtMonitor"].Rows[i]["queCallContinueTime"] = strTime;
                    }
                    else
                    {
                        dsMontor.Tables["dtMonitor"].Rows[i]["queCallContinueTime"] = "00:00:00";
                    }
                    index = i;
                }
                // dsMontor.Tables["dtMonitor"].EndLoadData();
            }
            catch (Exception ex)
            {
                writeLog("DoContinueMonitor current index is" + index.ToString() + " system error:" + ex.Message + ex.StackTrace);
            }
        }
        //end added
        //add,xzg,2009/10/30,S-----
        public void setRefresh(string strRefresh)
        {
            try
            {

                if (string.IsNullOrEmpty(strRefresh))
                {
                    return;
                }
                int intRefresh = 0;
                intRefresh = int.Parse(strRefresh);
                if (intRefresh < 1 || intRefresh > 999)
                {
                    return;
                }
                refreshTime = int.Parse(strRefresh);
                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("Refresh", strRefresh);
                IniProfile.Save(MyTool.GetModuleIniPath());

                //addeed by zhu 2014/09/01
                this.UpdateContinueTimer.Interval = this.refreshTime * 1000;
                //end added
            }
            catch (Exception ex)
            {
                writeLog("setRefresh:" + ex.Message);


            }
        }



        //add,xzg,2009/10/30,E-----
        //add,xzg,2010/02/23,S-------
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //added by zhu 2014/12/09 only get the command AGENTCOUNT， then can do again
                if (this.ReShowsFlag)
                {
                    ReShowsFlag = false;
                    this.button1.Enabled = false;
                    CpfParams cpfParam = new CpfParams();
                    axCpfMsg.SendEvent("", "", "AM_SHOWAGENT", cpfParam);
                    writeLog("button1_Click:" + cpfParam.GetParams());
                }
            }
            catch (Exception ex)
            {
                writeLog("button1_Click:" + ex.Message);
            }

            //DisplayLine();
            //DisplayAgent();
            //DisplayTotal();
        }
        //add,xzg,2010/02/23,E-------

        public void writeLog(string msg)
        {
            try
            {
                Microsoft.VisualBasic.Logging.Log log = new Microsoft.VisualBasic.Logging.Log();
                log.DefaultFileLogWriter.Location = Microsoft.VisualBasic.Logging.LogFileLocation.Custom;

                //add,xzg,2011/09/12,S
                string strFile = "";

                //strFile = log.DefaultFileLogWriter.FullLogFileName;
                //strFile = log.DefaultFileLogWriter.FullLogFileName.Replace(log.DefaultFileLogWriter.BaseFileName + ".log"
                //                                , "StatusMonitor" + DateTime.Now.DayOfWeek + ".log");

                strFile = log.DefaultFileLogWriter.CustomLocation + "\\" + "StatusMonitor" + DateTime.Now.DayOfWeek + ".log";
                if (System.IO.File.Exists(strFile))
                {

                    if (System.IO.File.GetLastWriteTime(strFile).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                    {

                    }
                    else
                    {
                        System.IO.File.Delete(strFile);

                    }
                }
                //add,xzg,2011/09/12,E

                //log.DefaultFileLogWriter.BaseFileName = "StatusMonitor" + DateTime.Now.ToString("yyyyMMdd");
                log.DefaultFileLogWriter.MaxFileSize = 50000000;
                log.DefaultFileLogWriter.BaseFileName = "StatusMonitor" + DateTime.Now.DayOfWeek;

                //log.DefaultFileLogWriter.CustomLocation = "log_vb";
                string strMsg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                strMsg = strMsg + "    " + msg;
                //log.WriteEntry(strMsg);
                log.DefaultFileLogWriter.WriteLine(strMsg);
                //log.TraceSource.Flush();
                log.DefaultFileLogWriter.Flush();
                log.DefaultFileLogWriter.Close();

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //xzg,2011/08/10,S
        private void stopService()
        {
            try
            {
                return;//サービスを停止しないように変更
                System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("CpfNameSvr");
                if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                    sc.Stop();

                sc.Close();


                sc = new System.ServiceProcess.ServiceController("Cpfmsgsrv");
                //sc.ServiceName = "Cpfmsgsrv "; //CpfNameSvr
                if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    sc.Stop();


                }


                sc.Close();



            }
            catch (Exception ex1)
            {
                throw ex1;

            }
        }

        private void startService()
        {
            try
            {

                System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("Cpfmsgsrv");
                //sc.ServiceName = "Cpfmsgsrv "; //CpfNameSvr
                if (sc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    sc.Start();

                    System.Threading.Thread.Sleep(3000);
                }

                int i = 0;
                while (sc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    i++;
                    if (i >= 2)
                        break;
                    System.Threading.Thread.Sleep(1000);

                }
                sc.Close();

                sc = new System.ServiceProcess.ServiceController("CpfNameSvr");
                if (sc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
                    sc.Start();

                sc.Close();

            }
            catch (Exception ex1)
            {
                throw ex1;

            }
        }
        //xzg,2011/08/10,E
        //Add,xzg,2011/09/07,S
        private void mainNotifyMenuEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Add,xzg,2011/09/07,E

        //Add,xzg,2011/09/16,S
        private void ListUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                bool showReport = false;
                //add,2014/03,ShowMonitorF
                if (ShowMonitorF != "1")
                {
                    if (RetSetCallInfo == true)
                    {
                        RetSetCallInfo = false;
                        delCallInfo();
                    }
                }
                if (updateAgentStatus == true)
                {
                    updateAgentStatus = false;
                    DisplayAgent();
                    DisplayTotal();
                    listMonitorShow();
                    showReport = true;
                }
                if (updateLineStatus == true)
                {
                    updateLineStatus = false;
                    DisplayLine();
                    DisplayTotal();

                    //added by zhu 2014/04/21
                    (this.ListTabPagesForms[0] as QueueCallForm).DisplayLine();
                    //end added

                }
                if (updateQueStatus == true)
                {
                    updateQueStatus = false;
                    DisplayTotal();
                    showReport = true;

                    //add,xzg,2013/11/14,S
                    //setMonitorQue();
                    //add,xzg,2013/11/14,E

                }

                //add,xzg,2013/10/13,S
                //if (showReport == true )
                //    listMonitorShow();
                //intMonitrTimer = intMonitrTimer + 1;
                //if (intMonitrTimer >= 1)
                //{
                //    intMonitrTimer = 0;
                //    listMonitorShow();
                //}
                //add,xzg,2013/10/13,E
            }
            catch (Exception ex)
            {
                writeLog("ListUpdateTimer_Tick:" + ex.Message);
            }
        }



        private string DoContinueTime(string iTimeSS)
        {

            string sumSS = iTimeSS;


            int iTime;

            try
            {
                if (string.IsNullOrEmpty(sumSS))
                {
                    sumSS = "00:00:00";
                    return sumSS;
                }

                //if (sumSS=="00")
                //{
                //    sumSS = "00";
                //    return sumSS;
                //}

                iTime = int.Parse(sumSS) + refreshTime;
                string strTime = ConvertTimeHHMMSS(iTime);


                return strTime;
            }
            catch (Exception ex)
            {
                return "00:00:00";
            }

        }
        private string ConvertTimeHHMMSS(string iTimeSS)
        {



            string strTemp;
            int iTime;
            int iTimeTemp;
            int iHH;
            int iMM;
            int iSS;
            string strMM;
            string strHH;
            string strTime = "";

            try
            {
                iTime = int.Parse(iTimeSS) * 60; //単位分ので、＊60
                iTimeTemp = iTime % 3600;
                iHH = iTime / 3600;
                if (iHH > 0)
                {
                    strHH = System.Convert.ToString(iHH);
                    if (strHH.Length < 2)

                        strTime = "0" + strHH + ":";
                    else
                        strTime = strHH + ":";
                }
                else
                {
                    strTime = "00:";
                }

                iTime = iTimeTemp % 60;
                iMM = iTimeTemp / 60;
                iSS = iTime;
                if (iMM > 0)
                {
                    strMM = System.Convert.ToString(iMM);
                    //strTime = strTime + strMM + ":";
                    strMM = "00" + strMM;
                    strMM = strMM.Substring(strMM.Length - 2);
                    strTime = strTime + strMM + ":";
                }
                else
                {
                    //if (iHH > 0)
                    //{
                    //    strTime = strTime + "00:";
                    //}
                    strTime = strTime + "00:";
                }
                if (iSS > 0)
                {
                    //strTime = strTime + iSS;
                    strTemp = "00" + iSS;
                    strTemp = strTemp.Substring(strTemp.Length - 2);
                    strTime = strTime + strTemp;
                }
                else
                    strTime = strTime + "00";


                return strTime;
            }
            catch (Exception ex)
            {
                return "00";
            }

        }
        private string ConvertTimeHHMMSS(int iTimeSS)
        {



            string strTemp;
            int iTime;
            int iTimeTemp;
            int iHH;
            int iMM;
            int iSS;
            string strMM;
            string strHH;
            string strTime = "";

            try
            {
                iTime = iTimeSS;
                iTimeTemp = iTime % 3600;
                iHH = iTime / 3600;
                if (iHH > 0)
                {
                    strHH = System.Convert.ToString(iHH);
                    if (strHH.Length < 2)

                        strTime = "0" + strHH + ":";
                    else
                        strTime = strHH + ":";
                }
                else
                {
                    strTime = "00:";
                }

                iTime = iTimeTemp % 60;
                iMM = iTimeTemp / 60;
                iSS = iTime;
                if (iMM > 0)
                {
                    strMM = System.Convert.ToString(iMM);
                    //strTime = strTime + strMM + ":";
                    strMM = "00" + strMM;
                    strMM = strMM.Substring(strMM.Length - 2);
                    strTime = strTime + strMM + ":";
                }
                else
                {
                    //if (iHH > 0)
                    //{
                    //    strTime = strTime + "00:";
                    //}
                    strTime = strTime + "00:";
                }
                if (iSS > 0)
                {
                    //strTime = strTime + iSS;
                    strTemp = "00" + iSS;
                    strTemp = strTemp.Substring(strTemp.Length - 2);
                    strTime = strTime + strTemp;
                }
                else
                    strTime = strTime + "00";


                return strTime;
            }
            catch (Exception ex)
            {
                return "00";
            }

        }
        private void lineStatusListView_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (SettingFields_LineCutShow == "0") return;
                if (MouseButtons.Right == e.Button)
                {
                    ListView lv = (ListView)sender;
                    if (lv.SelectedItems.Count < 1)
                    {
                        return;
                    }

                    ListViewItem item = new ListViewItem();
                    item = lv.SelectedItems[0];
                    if (item.SubItems.Count < 6)
                    {
                        return;
                    }
                    string strPickUpID = item.SubItems["ISessionprofileID"].Text;             //iSessionID
                    string strSelectedStatusName = item.SubItems["Status"].Text;
                    if (strPickUpID.Length < 1)
                    {
                        return;
                    }
                    LineID = strPickUpID;
                    vCurrentLineStatusName = strSelectedStatusName;
                    LineRightMenu.Show(this.lineStatusListView, e.X, e.Y);

                }
            }
            catch (Exception ex)
            {
                writeLog("lineStatusListView_MouseClick:" + ex.Message);


            }
        }

        public void setStatusOverTimes(string time1, string time2, string time3, string time4, string time5
                                , string time6, string time7, string time8, string time9, string time10, string time11, string time12)
        {
            try
            {
                SettingFields_StatusOverIdelTime1 = DefaultOverTime + time1;
                SettingFields_StatusOverIdelTime2 = DefaultOverTime + time2;
                SettingFields_StatusOverWorkTime1 = DefaultOverTime + time3;
                SettingFields_StatusOverWorkTime2 = DefaultOverTime + time4;
                SettingFields_StatusOverLeaveTime1 = DefaultOverTime + time5;
                SettingFields_StatusOverLeaveTime2 = DefaultOverTime + time6;
                SettingFields_StatusOverTalkTime1 = DefaultOverTime + time7;
                SettingFields_StatusOverTalkTime2 = DefaultOverTime + time8;
                SettingFields_StatusOverHoldTime1 = DefaultOverTime + time9;
                SettingFields_StatusOverHoldTime2 = DefaultOverTime + time10;
                //added by zhu 2014/09/11
                SettingFields_StatusOverQuecallTime1 = DefaultOverTime + time11;
                SettingFields_StatusOverQuecallTime2 = DefaultOverTime + time12;
                //end added

                SettingFields_StatusOverIdelTime1 = SettingFields_StatusOverIdelTime1.Substring(SettingFields_StatusOverIdelTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverIdelTime2 = SettingFields_StatusOverIdelTime2.Substring(SettingFields_StatusOverIdelTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverWorkTime1 = SettingFields_StatusOverWorkTime1.Substring(SettingFields_StatusOverWorkTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverWorkTime2 = SettingFields_StatusOverWorkTime2.Substring(SettingFields_StatusOverWorkTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverLeaveTime1 = SettingFields_StatusOverLeaveTime1.Substring(SettingFields_StatusOverLeaveTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverLeaveTime2 = SettingFields_StatusOverLeaveTime2.Substring(SettingFields_StatusOverLeaveTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverTalkTime1 = SettingFields_StatusOverTalkTime1.Substring(SettingFields_StatusOverTalkTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverTalkTime2 = SettingFields_StatusOverTalkTime2.Substring(SettingFields_StatusOverTalkTime2.Length - DefaultOverTime.Length);
                SettingFields_StatusOverHoldTime1 = SettingFields_StatusOverHoldTime1.Substring(SettingFields_StatusOverHoldTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverHoldTime2 = SettingFields_StatusOverHoldTime2.Substring(SettingFields_StatusOverHoldTime2.Length - DefaultOverTime.Length);

                //added by zhu 2014/09/11
                SettingFields_StatusOverQuecallTime1 = SettingFields_StatusOverQuecallTime1.Substring(SettingFields_StatusOverQuecallTime1.Length - DefaultOverTime.Length);
                SettingFields_StatusOverQuecallTime2 = SettingFields_StatusOverQuecallTime2.Substring(SettingFields_StatusOverQuecallTime2.Length - DefaultOverTime.Length);
                //end added

                IniProfile.SelectSection("SVSet");
                IniProfile.SetString(ConstEntity.STATUSTIMEIDLE1, time1);
                IniProfile.SetString(ConstEntity.STATUSTIMEIDLE2, time2);
                IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME1, time3);
                IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME2, time4);
                IniProfile.SetString(ConstEntity.STATUSTIMELEAVE1, time5);
                IniProfile.SetString(ConstEntity.STATUSTIMELEAVE2, time6);
                IniProfile.SetString(ConstEntity.STATUSTIMETALK1, time7);
                IniProfile.SetString(ConstEntity.STATUSTIMETALK2, time8);
                IniProfile.SetString(ConstEntity.STATUSTIMEHOLD1, time9);
                IniProfile.SetString(ConstEntity.STATUSTIMEHOLD2, time10);
                //added by zhu 2014/09/11
                IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL1, time11);
                IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL2, time12);
                //end added

                IniProfile.Save(MyTool.GetModuleIniPath());
            }
            catch (Exception ex)
            {
                writeLog("setStatusOverTimes System Error:" + ex.Message);
            }

        }



        private void menuDropLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LineID)) return;
                if (vCurrentLineStatusName == res.GetString("SM0020045") && MessageBox.Show("対象の回線は通話中ですが、切断しますか？", "alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(LineID))
                {
                    CpfParams cpfParam = new CpfParams();
                    cpfParam.AddString("iSessionProfileID", LineID);
                    axCpfMsg.SendEvent("", "", "UM_DROPCALL", cpfParam);
                    writeLog("menuDropLine_Click:UM_DROPCALL" + cpfParam.GetParams());
                    LineID = "";
                    System.Threading.Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                writeLog("menuDropLine_Click:" + ex.Message);
            }
        }


        public void SetWaitTime(string waitTimes)
        {
            try
            {
                if (string.IsNullOrEmpty(waitTimes)) return;
                if (!string.IsNullOrEmpty(waitTimes))
                {
                    CpfParams cpfParam = new CpfParams();
                    cpfParam.AddString("iWaitTimes", waitTimes);
                    axCpfMsg.SendEvent("", "", "UM_SETWAITTIMES", cpfParam);
                    writeLog("SetWaitTime:UM_SETWAITTIMES" + cpfParam.GetParams());
                    WaitTimes = waitTimes;
                    System.Threading.Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                writeLog("SetWaitTime:" + ex.Message);
            }
        }

        private void subMenuAgentState_Click(object sender, EventArgs e)
        {
            try
            {
                AgentState frm = new AgentState();
                frm.mainF = this;
                frm.AgentID = vCurrentAgentID;
                frm.AgentName = vCurrentAgentName;
                frm.CurAgentState = vCurrentAgentState;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("subMenuAgentState_Click:" + ex.Message);
            }
        }
        public void SetAgetState(string agentState)
        {
            try
            {
                if (string.IsNullOrEmpty(agentState)) return;
                if (!string.IsNullOrEmpty(agentState))
                {
                    CpfParams cpfParam = new CpfParams();
                    cpfParam.AddLong("iAgentState", int.Parse(agentState));
                    cpfParam.AddString("vAgentID", vCurrentAgentID);
                    axCpfMsg.SendEvent("", "", "UM_SETAGETSTATE", cpfParam);
                    writeLog("SetAgetState:UM_SETAGETSTATE" + cpfParam.GetParams());

                    System.Threading.Thread.Sleep(50);
                    //iniProfile.SelectSection("SVSet");
                    //iniProfile.SetString("ShowCol", ShowCol);
                    //iniProfile.Save(MyTool.GetModuleIniPath());
                }
            }
            catch (Exception ex)
            {
                writeLog("SetAgetState:" + ex.Message);
            }
        }



        public void setShowCol(string strShowCol)
        {
            try
            {
                ShowCol = strShowCol;
                setAgentStatusView();

                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("ShowCol", ShowCol);
                IniProfile.Save(MyTool.GetModuleIniPath());

                //added by zhu 2015/12/21 ajust the column width
                AjustAgentListSize();
                // end added

            }
            catch (Exception ex)
            {
                writeLog("setShowCol:" + ex.Message);
            }
        }

        void agentStatusListView_ColumnWidthChanged(object sender, System.Windows.Forms.ColumnWidthChangedEventArgs e)
        {
            try
            {
                if (ShowCol.Substring(e.ColumnIndex, 1) == "0" && agentStatusListView.Columns[e.ColumnIndex].Width > 0)
                    agentStatusListView.Columns[e.ColumnIndex].Width = 0;
            }
            catch (Exception ex)
            {
                writeLog("agentStatusListView_ColumnWidthChanged:" + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                //displayGroup = groupInfo.Group;

                //DisplayLine();
                DisplayAgent();
            }
            catch (Exception ex)
            {
                writeLog("comboBox1_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                //displayGroup = groupInfo.Group;

                //DisplayLine();
                DisplayAgent();
            }
            catch (Exception ex)
            {
                writeLog("comboBox2_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                //displayGroup = groupInfo.Group;

                //DisplayLine();
                DisplayAgent();
            }
            catch (Exception ex)
            {
                writeLog("comboBox3_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                //displayGroup = groupInfo.Group;

                //DisplayLine();
                DisplayAgent();
            }
            catch (Exception ex)
            {
                writeLog("comboBox4_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GroupInfo groupInfo = (GroupInfo)groupComboBox.SelectedItem;
                //displayGroup = groupInfo.Group;

                //DisplayLine();
                DisplayAgent();
            }
            catch (Exception ex)
            {
                writeLog("comboBox5_SelectedIndexChanged:" + ex.Message);
            }
        }





        public void setOptionName(string strOption1, string strOption2, string strOption3, string strOption4, string strOption5)
        {
            try
            {

                OptionName1 = strOption1;
                OptionName2 = strOption2;
                OptionName3 = strOption3;
                OptionName4 = strOption4;
                OptionName5 = strOption5;
                setOptionNameView(strOption1, strOption2, strOption3, strOption4, strOption5);

                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("Option1", strOption1);
                IniProfile.SetString("Option2", strOption2);
                IniProfile.SetString("Option3", strOption3);
                IniProfile.SetString("Option4", strOption4);
                IniProfile.SetString("Option5", strOption5);
                IniProfile.Save(MyTool.GetModuleIniPath());


            }
            catch (Exception ex)
            {
                writeLog("setOptionName:" + ex.Message);
            }
        }
        public void setOptionNameView(string strOption1, string strOption2, string strOption3, string strOption4, string strOption5)
        {
            try
            {
                label3.Text = OptionName1;
                label4.Text = OptionName2;
                label5.Text = OptionName3;
                label6.Text = OptionName4;
                label7.Text = OptionName5;

                agentStatusListView.Columns[1].Text = OptionName1;
                agentStatusListView.Columns[2].Text = OptionName2;
                agentStatusListView.Columns[3].Text = OptionName3;
                agentStatusListView.Columns[4].Text = OptionName4;
                agentStatusListView.Columns[5].Text = OptionName5;
            }
            catch (Exception ex)
            {
                writeLog("setOptionNameView:" + ex.Message);
            }
        }
        private void setDoubleBuffered(Control ctrl, bool value)
        {
            try
            {

                if (ctrl == null) return;
                //DoubleBuffered   OptimizedDoubleBuffer
                PropertyInfo propinfo = ctrl.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propinfo != null) propinfo.SetValue(ctrl, value, null);
            }
            catch (Exception ex)
            {
                writeLog("setDoubleBuffered:" + ex.Message);
            }
        }

        private void ShowBalloonTipHelp(string msg, string agent)
        {
            try
            {
                //if this.MaximumSize 
                //mainNotifyIcon.BalloonTipTitle = "エージェントモニター";
                string strMsg = "";
                strMsg = Environment.NewLine + agent;
                strMsg = strMsg + Environment.NewLine + msg;

                mainNotifyIcon.BalloonTipText = strMsg;
                mainNotifyIcon.ShowBalloonTip(20, "状態モニタ", strMsg, ToolTipIcon.Info);

                mainNotifyIcon.Visible = true;
                this.Focus();

            }
            catch (Exception ex)
            {
                writeLog("ShowBalloonTipInfoMsg:" + ex.Message);
            }

        }

        /// <summary>
        /// play special sound for skillgroup
        /// </summary>
        /// <returns>return quecall cound</returns>
        private int PlaySkillQuecallSound()
        {
            try
            {
                int playQueueCount = 0;
                foreach (var skillVoice in Dic_SettingFields_SkillQuecall)
                {
                    int iQueCount = 0;
                    string skillGroupID = skillVoice.Key;
                    string[] arrValue = skillVoice.Value.Split('|');
                    string strQuePeriod1 = arrValue[0].Split(',')[0];
                    string strQueVoice1 = arrValue[0].Split(',')[1];
                    string strQuePeriod2 = arrValue[1].Split(',')[0];
                    string strQueVoice2 = arrValue[1].Split(',')[1];
                    string strQuePeriod3 = arrValue[2].Split(',')[0];
                    string strQueVoice3 = arrValue[2].Split(',')[1];

                    int iQuePeriod1 = 0;
                    int iQuePeriod2 = 0;
                    int iQuePeriod3 = 0;
                    writeLog("PlaySkillQuecallSound:" + skillVoice.Value);
                    foreach (ListViewItem item in totalListView.Items)
                    {
                        //SubItems[5] is groupID ; SubItems[4] is queue call count
                        if (skillGroupID == item.SubItems["Group"].Text)
                        {
                            if (string.IsNullOrEmpty(item.SubItems["Queue"].Text) || int.Parse(item.SubItems["Queue"].Text) <= 0)
                            {
                                SkillQueCallSoundPlayerManager.StopSkillGroupPlayer(skillGroupID);
                                break;
                            }
                            else
                            {
                                iQueCount = int.Parse(item.SubItems["Queue"].Text);
                                if (string.IsNullOrEmpty(strQuePeriod1) || !System.Text.RegularExpressions.Regex.IsMatch(strQuePeriod1, @"^\d*$"))
                                {
                                    SkillQueCallSoundPlayerManager.StopSkillGroupPlayer(skillGroupID);
                                    break;
                                }

                                iQuePeriod1 = int.Parse(strQuePeriod1);

                                if (!string.IsNullOrEmpty(strQuePeriod2))
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(strQuePeriod2, @"^\d*$"))
                                    {
                                        iQuePeriod2 = int.Parse(strQuePeriod2);
                                    }
                                }

                                if (!string.IsNullOrEmpty(strQuePeriod3))
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(strQuePeriod3, @"^\d*$"))
                                    {
                                        iQuePeriod3 = int.Parse(strQuePeriod3);
                                    }
                                }

                                string path = "";
                                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                path = path + "\\Comdesign\\Voice";

                                if (iQueCount >= iQuePeriod3 && iQuePeriod3 > 0 && !string.IsNullOrEmpty(strQueVoice3))
                                {
                                    string fullPath = path + @"\" + strQueVoice3; ;
                                    SkillQueCallSoundPlayerManager.OnlyRunSkillGroupPeriodPlayer(skillGroupID, 3, fullPath);
                                    playQueueCount += iQueCount;
                                    break;
                                }
                                if (iQueCount >= iQuePeriod2 && iQuePeriod2 > 0 && !string.IsNullOrEmpty(strQueVoice2))
                                {
                                    string fullPath = path + @"\" + strQueVoice2; ;
                                    SkillQueCallSoundPlayerManager.OnlyRunSkillGroupPeriodPlayer(skillGroupID, 2, fullPath);
                                    playQueueCount += iQueCount;
                                    break;
                                }
                                if (iQueCount >= iQuePeriod1 && iQuePeriod1 > 0 && !string.IsNullOrEmpty(strQueVoice1))
                                {
                                    string fullPath = path + @"\" + strQueVoice1; ;
                                    SkillQueCallSoundPlayerManager.OnlyRunSkillGroupPeriodPlayer(skillGroupID, 1, fullPath);
                                    playQueueCount += iQueCount;
                                }
                            }
                        }
                    }
                }
                return playQueueCount;
            }
            catch (Exception ex)
            {
                writeLog("PlaySkillQuecallSound system error:" + ex.Message + ex.StackTrace);
                return 0;
            }
        }

        private void PlaySound()
        {
            try
            {
                int iHasPlayedQueCount = PlaySkillQuecallSound();
                writeLog("PlaySound total QueCall count:" + QueueCountAll.ToString() + "; has played in special :" + iHasPlayedQueCount.ToString());
                int iQueCount = QueueCountAll;
                if (iHasPlayedQueCount > 0 && iHasPlayedQueCount >= iQueCount) return;
                int iQuePeriod1 = 0;
                int iQuePeriod2 = 0;
                int iQuePeriod3 = 0;
                if (iQueCount < 1)
                {
                    PlaySound(null, new IntPtr(), playSound.SND_ASYNC);
                    return;
                }
                if (string.IsNullOrEmpty(QuePeriod1))
                {
                    PlaySound(null, new IntPtr(), playSound.SND_ASYNC);
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(QuePeriod1, @"^\d*$"))
                {
                    return;
                }
                iQuePeriod1 = int.Parse(QuePeriod1);

                //if (iQuePeriod1 == 0) return;

                if (!string.IsNullOrEmpty(QuePeriod2))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(QuePeriod2, @"^\d*$"))
                    {
                        iQuePeriod2 = int.Parse(QuePeriod2);
                    }
                }

                if (!string.IsNullOrEmpty(QuePeriod3))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(QuePeriod3, @"^\d*$"))
                    {
                        iQuePeriod3 = int.Parse(QuePeriod3);
                    }
                }

                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign";

                if (iQueCount >= iQuePeriod3 && iQuePeriod3 > 0)
                {
                    PlaySound(path + @"\" + QuePeriodVoice3);
                    return;
                }
                if (iQueCount >= iQuePeriod2 && iQuePeriod2 > 0)
                {
                    PlaySound(path + @"\" + QuePeriodVoice2);
                    return;
                }
                if (iQueCount >= iQuePeriod1 && iQuePeriod1 > 0)
                    PlaySound(path + @"\" + QuePeriodVoice1);
                else
                    PlaySound(null, new IntPtr(), playSound.SND_ASYNC);


            }
            catch (Exception ex)
            {
                //alert(e.description);
                writeLog("PlaySound " + ex.StackTrace);
            }
        }

        private void PlaySound(string voiceFile)
        {
            try
            {

                if (string.IsNullOrEmpty(voiceFile)) return;

                if (!System.IO.File.Exists(voiceFile)) return;

                PlaySound(voiceFile, new IntPtr(), playSound.SND_LOOP | playSound.SND_ASYNC);

            }
            catch (Exception ex)
            {
                //alert(e.description);
                writeLog("PlaySound errMsg:" + ex.Message + "\r\nTrace:" + ex.StackTrace);
            }
        }

        private void showVoiceMailQueCall()
        {
            try
            {
                //QUECALLCount get
                int iQueCount = QueueCountAll;

                //if (iQueCount > 0)
                PlaySound();

            }
            catch (Exception ex)
            {
                writeLog("showVoiceMailQueCall:" + ex.Message);
            }
        }
        private void statusTabCtrl_TabIndexChanged(object sender, System.EventArgs e)
        {
            //try
            //{
            //    GetReport();
            //}
            //catch (Exception ex)
            //{
            //    writeLog("statusTabCtrl_TabIndexChanged SysteError:" + ex.Message + ex.StackTrace);
            //}
        }
        void statusTabCtrl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (statusTabCtrl.SelectedIndex == 2)
                {
                    //GetReport();
                    ////ListUpdateTimer.Start();
                    //reportForTimeHas = true;

                }
                if (statusTabCtrl.SelectedIndex == 3)
                {
                    //listMonitorShow();


                }
                else
                {
                    //Reportの以外のタグを開いても、データ要求コマンドも発行します。
                    reportForTimeHas = true;
                }
            }
            catch (Exception ex)
            {
                writeLog("statusTabCtrl_TabIndexChanged SysteError:" + ex.Message + ex.StackTrace);
            }
        }
        //private void GetReportForTime()
        //{
        //    try
        //    {
        //        if (PreDate != DateTime.Now.ToString("yyyy/MM/dd"))
        //        {
        //            PreDate = DateTime.Now.ToString("yyyy/MM/dd");
        //            ClearReport();
        //            //PreDate = DateTime.Now.ToString("yyyy/MM/dd");
        //        }

        //        if (reportForTimeHas == false) return;
        //        if (cmbReportDateE.Text.CompareTo(DateTime.Now.ToString("HH")) < 0) return;
        //        GetReportCmd(DateTime.Now.ToString("HH:00"), DateTime.Now.AddHours(1).ToString("HH:00"));

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("GetReportForTime SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void ClearReport()
        //{
        //    try
        //    {
        //        reportDateS = "0";
        //        reportDateE = "0";
        //        curDateE = "0";
        //        ReportList.Clear();
        //        lineMaxBusy.Clear();


        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("ClearReport SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void GetReport()
        //{
        //    try
        //    {

        //        //if (cmbReportTimeE.Text != "00")
        //        //{

        //        //    //reportDateE = reportDateE + ":" + cmbReportTimeE.Text;
        //        //    if (int.Parse(curDateE) >= int.Parse(cmbReportDateE.Text)+1)
        //        //        return;

        //        //}
        //        //else
        //        //{
        //        //    if (int.Parse(curDateE) >= int.Parse(cmbReportDateE.Text))
        //        //        return;
        //        //}
        //        if (PreDate != DateTime.Now.ToString("yyyy/MM/dd"))
        //        {
        //            PreDate = DateTime.Now.ToString("yyyy/MM/dd");
        //            ClearReport();
        //            //PreDate = DateTime.Now.ToString("yyyy/MM/dd");
        //        }

        //        reportDateS = curDateE;
        //        if (reportDateS.CompareTo(DateTime.Now.ToString("HH:mm")) > 0)
        //            return;

        //        if (reportDateS.CompareTo(cmbReportDateS.Text + ":" + cmbReportTimeS.Text) > 0)
        //        {
        //            reportDateS = curDateE;//(Convert.ToDateTime(reportDateS)).AddHours(1).ToString("HH:mm");
        //        }
        //        else
        //        {
        //            reportDateS = cmbReportDateS.Text;
        //            reportDateS = reportDateS + ":" + cmbReportTimeS.Text;
        //        }

        //        reportDateE = Convert.ToDateTime(reportDateS).AddHours(1).ToString("HH:00");

        //        //if (int.Parse(cmbReportDateS.Text) + 1 < int.Parse(reportDateE))
        //        //{
        //        //    if (curDateE == "0")
        //        //        curDateE = (int.Parse(cmbReportDateS.Text) + 1).ToString();
        //        //    else
        //        //        curDateE = (int.Parse(curDateE) + 1).ToString();
        //        //}
        //        //reportDateE = reportDateE + ":00";

        //        if (reportDateE.CompareTo(cmbReportDateE.Text + ":" + cmbReportTimeE.Text) > 0)
        //        {
        //            reportDateE = cmbReportDateE.Text + ":" + cmbReportTimeE.Text;
        //        }

        //        if (reportDateS.CompareTo(reportDateE) >= 0)
        //        {
        //            return;
        //        }

        //        GetReportCmd(reportDateS, reportDateE);

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("GetReport SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void GetReportCmd(string dateS, string dateE)
        //{
        //    try
        //    {
        //        GroupInfo reportGrup = (GroupInfo)cmbReportGrop.SelectedItem;

        //        string reportDateS = DateTime.Now.ToString("yyyy/MM/dd") + " " + dateS;

        //        string reportDateE = DateTime.Now.ToString("yyyy/MM/dd") + " " + dateE;

        //        CpfParams cpfParam = new CpfParams();
        //        cpfParam.AddString("dtFrom", reportDateS);
        //        cpfParam.AddString("dtTo", reportDateE);
        //        if (cmbReportGrop.SelectedIndex == 0)
        //            cpfParam.AddString("iType", "0");
        //        else
        //            cpfParam.AddString("iType", "1");

        //        cpfParam.AddString("iGroupID", reportGrup.Group.ToString());
        //        axCpfMsg.SendEvent("", "", "UM_GET_REPORT", cpfParam);
        //        writeLog("GetReportCmd  UM_GET_REPORT:" + cpfParam.GetParams());
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("GetReportCmd SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void lvReportInit()
        //{
        //    try
        //    {

        //        dvReport.Columns.Clear();
        //        dvReport.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

        //        //dvReport.ColumnCount = 11;
        //        dvReport.RowHeadersVisible = false;

        //        DataGridViewColumn column;


        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "時間";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;

        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);


        //        //着信
        //        //時間内
        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "着信";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "放棄";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "完了";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);

        //        //累計
        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "着信(累)";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 60;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "放棄(累)";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 60;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "完了(累)";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightBlue;
        //        column.Width = 60;
        //        dvReport.Columns.Add(column);

        //        //発信
        //        //時間内
        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "発信";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightGray;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "完了";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightGray;
        //        column.Width = 50;
        //        dvReport.Columns.Add(column);

        //        //累計
        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "発信(累)";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightGray;
        //        column.Width = 60;
        //        dvReport.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "完了(累)";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightGray;
        //        column.Width = 60;
        //        dvReport.Columns.Add(column);


        //        column = new DataGridViewTextBoxColumn();
        //        column.HeaderText = "最大同時通話数";
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        column.DefaultCellStyle.BackColor = Color.LightYellow;
        //        column.Width = 100;
        //        dvReport.Columns.Add(column);

        //        dvReport.ReadOnly = true;

        //        //string[] row1 = new string[]{"test1","test2","test3",test4" +};

        //        //string[] row2 = new string[]{"test11","test12","test13", test14" +};


        //        //    object[] rows = new object[] { row1, row2 };

        //        //    foreach (string[] rowArray in rows)
        //        //    {
        //        //        this.dvReport.Rows.Add(rowArray);
        //        //    }


        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("dvReportInit SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void dvReportShow(ArrayList arrayAll)
        //private void dvReportShow()
        //{
        //    int col = 0;

        //    try
        //    {
        //        lvReportInit();
        //        CallReport arrayCol;
        //        string strDtSum = "";
        //        int iSelectedGroup = 0;
        //        string allIncom = "0";
        //        string allInRefuse = "0";
        //        string allInComp = "0";
        //        string allOut = "0";
        //        string allComp = "0";
        //        string dtStart = "0:0";
        //        string dtEnd = "0:0";
        //        iSelectedGroup = ((GroupInfo)cmbReportGrop.SelectedItem).Group;
        //        for (int i = 1; i < ReportList.Count + 1; i++)
        //        {


        //            arrayCol = (CallReport)ReportList[i - 1];

        //            strDtSum = arrayCol.dtSum;// arrayCol[0].ToString();
        //            dtStart = (Convert.ToDateTime(strDtSum)).ToString("HH:ss");
        //            dtEnd = (Convert.ToDateTime(strDtSum).AddHours(1)).ToString("HH:00");
        //            if (dtStart.CompareTo(cmbReportDateE.Text + ":" + cmbReportTimeE.Text) > 0)
        //            {
        //                continue;
        //            }
        //            else if (dtEnd.CompareTo(cmbReportDateS.Text + ":" + cmbReportTimeS.Text) < 0)
        //            {
        //                continue;
        //            }

        //            if (iSelectedGroup.ToString() != arrayCol.iGroupID)
        //            {
        //                continue;
        //            }
        //            int iMaxCount = 0;
        //            for (int j = 0; j < lineMaxBusy.Count; j++)
        //            {
        //                LineMaxBusy lineMaxBusyTemp = (LineMaxBusy)lineMaxBusy[j];
        //                if (cmbReportGrop.SelectedIndex == 0)
        //                {

        //                    if ((lineMaxBusyTemp.TimeS + ":00") == strDtSum)
        //                        iMaxCount = iMaxCount + lineMaxBusyTemp.MaxBusy;
        //                }
        //                else
        //                {
        //                    if (iSelectedGroup.ToString() == lineMaxBusyTemp.iGroupID)
        //                    {
        //                        if ((lineMaxBusyTemp.TimeS + ":00") == strDtSum)
        //                        {
        //                            iMaxCount = lineMaxBusyTemp.MaxBusy;
        //                            break;
        //                        }
        //                    }
        //                }


        //            }
        //            arrayCol.iMaxCount = iMaxCount.ToString();
        //            //arrayCol.Add(iMaxCount.ToString());
        //            allIncom = (int.Parse(allIncom) + int.Parse(arrayCol.iInCall)).ToString();
        //            allInRefuse = (int.Parse(allInRefuse) + int.Parse(arrayCol.iRefuse)).ToString();
        //            allInComp = (int.Parse(allInComp) + int.Parse(arrayCol.iInComp)).ToString();

        //            allOut = (int.Parse(allOut) + int.Parse(arrayCol.iOutCall)).ToString();
        //            allComp = (int.Parse(allComp) + int.Parse(arrayCol.iOutComp)).ToString();
        //            object[] row1 = { arrayCol.dtSum, arrayCol.iInCall, arrayCol.iRefuse
        //                             , arrayCol.iInComp, allIncom,allInRefuse,allInComp
        //                               ,arrayCol.iOutCall ,arrayCol.iOutComp,allOut,allComp,arrayCol.iMaxCount};
        //            dvReport.Rows.Add(row1);
        //            //for (int j = 1; j < arrayCol.Count; j++)
        //            //{

        //            //    //if (arrayCol[j - 1] == null) continue;
        //            //    //if (string.IsNullOrEmpty(arrayCol[j - 1].ToString())) continue;
        //            //    //dvReport[j - 1, col].Value = arrayCol[j - 1];

        //            //}
        //            col = col + 1;
        //        }
        //        dvReport.ClearSelection();

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("lvReportShow SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void btnReportShow_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        reportDateS = "0";
        //        reportDateE = "0";
        //        curDateE = "0";
        //        GetReport();
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("btnReportShow_Click SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private void ReportInit()
        //{

        //    try
        //    {

        //        ReportDateInit();
        //        ReportGroupInit();

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("ReportInit SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void ReportDateInit()
        //{

        //    try
        //    {

        //        //時
        //        string temp = "";
        //        for (int i = 0; i < 24; i++)
        //        {
        //            temp = i.ToString("00");
        //            cmbReportDateS.Items.Add(temp);
        //            cmbReportDateE.Items.Add(temp);
        //        }
        //        cmbReportDateS.SelectedIndex = 0;
        //        cmbReportDateE.SelectedIndex = cmbReportDateE.Items.Count - 1;

        //        //分
        //        for (int i = 0; i < 60; i++)
        //        {
        //            temp = i.ToString("00");
        //            cmbReportTimeS.Items.Add(temp);
        //            cmbReportTimeE.Items.Add(temp);
        //        }
        //        cmbReportTimeS.SelectedIndex = 0;
        //        cmbReportTimeE.SelectedIndex = cmbReportTimeE.Items.Count - 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("ReportDateInit SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private void ReportGroupInit()
        //{

        //    try
        //    {
        //        cmbReportGrop.Items.Clear();
        //        GroupInfo groupInfo = new GroupInfo(-1, "全体");
        //        cmbReportGrop.Items.Add(groupInfo);
        //        cmbReportGrop.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("ReportGroupInit SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private void AddReportGroup(int iGroup, string sGroupName)
        //{

        //    try
        //    {

        //        GroupInfo groupInfo = new GroupInfo(iGroup, sGroupName);
        //        AddReportGroup(groupInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("AddReportGroup SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        //private void AddReportGroup(GroupInfo iGroupInfo)
        //{

        //    try
        //    {


        //        cmbReportGrop.Items.Add(iGroupInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("AddReportGroup SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private static int SortReportCompare(CallReport obj1, CallReport obj2)
        //{
        //    int res = 0;
        //    if ((obj1 == null) && (obj2 == null))
        //    {
        //        return 0;
        //    }
        //    else if ((obj1 != null) && (obj2 == null))
        //    {
        //        return 1;
        //    }
        //    else if ((obj1 == null) && (obj2 != null))
        //    {
        //        return -1;
        //    }
        //    if (obj1.dtSum.CompareTo(obj2.dtSum) >= 0)
        //    {
        //        res = 1;
        //    }
        //    else if (obj1.dtSum.CompareTo(obj2.dtSum) < 0)
        //    {
        //        res = -1;
        //    }
        //    return res;
        //}

        //private void AddReportData(CpfParams recvParams)
        //{
        //    try
        //    {


        //        string dtSum = recvParams.GetStringDefault("dtSum", "00:00");
        //        string iInCall = recvParams.GetStringDefault("iInCall", "0");
        //        string iInComp = recvParams.GetStringDefault("iInComp", "0");
        //        string iOutCall = recvParams.GetStringDefault("iOutCall", "0");
        //        string iOutComp = recvParams.GetStringDefault("iOutComp", "0");
        //        string sMsgFlag = recvParams.GetStringDefault("sMsgFlag", "S");
        //        string iGroupID = recvParams.GetStringDefault("iGroupID", "-1");

        //        CallReport curReport = new CallReport();
        //        curReport.iGroupID = iGroupID;
        //        curReport.dtSum = Convert.ToDateTime(dtSum).ToString("HH:ss");
        //        curReport.iInCall = iInCall;
        //        curReport.iInComp = iInComp;
        //        curReport.iOutCall = iOutCall;
        //        curReport.iOutComp = iOutComp;
        //        curReport.sMsgFlag = sMsgFlag;


        //        curReport.iRefuse = (int.Parse(iInCall) - int.Parse(iInComp)).ToString();
        //        ReportList.RemoveAll(delegate(CallReport obj)
        //        { return obj.iGroupID == curReport.iGroupID && obj.dtSum == curReport.dtSum; });


        //        //CallReport tempReport = ReportList.Find(delegate(CallReport obj)
        //        //{ return obj.iGroupID == curReport.iGroupID && obj.dtSum == curReport.dtSum; });


        //        ReportList.Add(curReport);
        //        ReportList.Sort(SortReportCompare);

        //        this.dvReportShow();
        //        curDateE = reportDateE;
        //        GetReport();
        //        //if (sMsgFlag.ToUpper() == "S")    //メッセージ開始
        //        //{
        //        //    ReportList.Clear();
        //        //    ReportList.Add(curReport);
        //        //}
        //        //else if (sMsgFlag.ToUpper() == "E") //メッセージ開始終了
        //        //{
        //        //    ReportList.Add(curReport);
        //        //    this.dvReportShow(ReportList);
        //        //}
        //        //else
        //        //{
        //        //    ReportList.Add(curReport);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("AddReportData SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}
        private void AddLineMaxBusy(int iGroup, int iCount)
        {

            try
            {
                string curHour = DateTime.Now.Hour.ToString();
                if (lineMaxBusy.Count == 0)
                {
                    LineMaxBusy maxBusy = new LineMaxBusy();
                    maxBusy.MaxBusy = iCount;
                    maxBusy.iGroupID = iGroup.ToString();
                    maxBusy.TimeS = curHour;
                    lineMaxBusy.Add(maxBusy);
                    return;
                }

                for (int i = 0; i < lineMaxBusy.Count; i++)
                {
                    LineMaxBusy maxBusyTemp = (LineMaxBusy)lineMaxBusy[i];
                    if (maxBusyTemp.iGroupID == iGroup.ToString() && maxBusyTemp.TimeS == curHour)
                    {
                        if (maxBusyTemp.MaxBusy < iCount)
                        {
                            maxBusyTemp.MaxBusy = iCount;
                            lineMaxBusy[i] = maxBusyTemp;
                            break;
                        }
                    }
                    else
                    {
                        LineMaxBusy maxBusy = new LineMaxBusy();
                        maxBusy.MaxBusy = iCount;
                        maxBusy.iGroupID = iGroup.ToString();
                        maxBusy.TimeS = curHour;
                        lineMaxBusy.Add(maxBusy);
                    }
                }


            }
            catch (Exception ex)
            {
                writeLog("AddLineMaxBusy SysteError:" + ex.Message + ex.StackTrace);
            }
        }

        //private void cmbReportGrop_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (statusTabCtrl.SelectedIndex != 2) return;
        //        reportDateS = "0";
        //        reportDateE = "0";
        //        curDateE = "0";
        //        GetReport();
        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("cmbReportGrop_SelectedIndexChanged SysteError:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //added by zhu 2014/04/17
        // get special flag from liensce with key = name
        private string GetSpecialNameFlag()
        {
            try
            {
                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                if (Reg == null)
                {
                    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                    if (Reg == null) return "";
                }
                string flag = Reg.GetValue("NAME", "").ToString();
                if (!string.IsNullOrEmpty(flag) && flag.Length == 2)
                {
                    //if ("0,1,2".Contains(flag.Substring(1)))
                    return flag.Substring(1);
                }

                return "";
            }
            catch (Exception ex)
            {
                writeLog("GetBuffaloFlag SysteError:" + ex.Message + ex.StackTrace);
                return "";
            }
        }
        //end adde


        public void getGroupPersonal()
        {
            try
            {

                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                if (Reg == null)
                {
                    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                    if (Reg == null) return;
                }

                // changed by zhu 2015/11/30 use url from ini file
                //string webServer = Reg.GetValue("WebServer", "").ToString();
                string webServer = GetServerUrl();
                //end changed
                string tid = Reg.GetValue("TenantID", "").ToString();
                string tpwd = Reg.GetValue("TenantPass", "").ToString();
                Reg.Close();
                wbGroupPersonal.AllowWebBrowserDrop = false;
                wbGroupPersonal.IsWebBrowserContextMenuEnabled = false;
                wbGroupPersonal.WebBrowserShortcutsEnabled = false;

                string postdata = "";
                string mainUrl = webServer + "getGroupPersonal.asp";
                //added by Zhu 2014/04/07
                mainUrl = mainUrl + "?u=" + tid;
                mainUrl = mainUrl + "&p=" + tpwd;
                //end added
                postdata = "u =" + tid;
                postdata = postdata + "&p=" + tpwd;
                postdata = postdata + "&l=" + MonitorGroupList;

                System.Text.Encoding a = System.Text.Encoding.UTF8;
                Byte[] byte1 = a.GetBytes(postdata);

                writeLog("mainUrl:" + mainUrl + ",postdata:" + postdata);
                wbGroupPersonal.Navigate(mainUrl, "", byte1, "Content-Type: application/x-www-form-urlencoded");

            }
            catch (Exception ex)
            {
                writeLog("getGroupPersonal SysteError:" + ex.Message + ex.StackTrace);
            }

        }
        void wbGroupPersonal_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            //throw new System.NotImplementedException();
            try
            {
                writeLog("wbGroupPersonal_DocumentCompleted");
                //getGrouPersonCnt = getGrouPersonCnt + 1;
                //if (getGrouPersonCnt > 1) return;
                //xzg,2013/11/28,S
                //connectTimer_Tick(sender, e);
                //xzg,2013/11/28,E

                //System.Console.WriteLine(wbGroupPersonal.DocumentText);
                if (string.IsNullOrEmpty(wbGroupPersonal.DocumentText)) return;
                //writeLog(wbGroupPersonal.DocumentText);
                //CpfParams objGroupPersonal = new CpfParams();
                //objGroupPersonal.SetParams(wbGroupPersonal.DocumentText);
                string[] rows = wbGroupPersonal.DocumentText.Split(';');
                DataRow csDataRow1 = dsMontor.Tables["dtGroupPersonal"].NewRow(); //dsMontor.Tables["dtGroupPersonal"].NewRow(); dtGroupPersonal.NewRow();
                for (int i = 0; i < rows.Length; i++)
                {
                    if (!string.IsNullOrEmpty(rows[i]))
                    {

                        string[] cols = rows[i].Split(',');
                        if (cols.Length == 3)
                        {
                            csDataRow1 = dtGroupPersonal.NewRow();
                            csDataRow1[0] = cols[0];
                            csDataRow1[1] = cols[1];
                            csDataRow1[2] = cols[2];
                            dsMontor.Tables["dtGroupPersonal"].Rows.Add(csDataRow1);
                            //dtGroupPersonal.Rows.Add(cols);
                        }
                    }
                }
                csDataRow1 = dsMontor.Tables["dtGroupPersonal"].NewRow(); //dtGroupPersonal.NewRow();
                csDataRow1[0] = "-1";
                //modified by zhu 2014/04/17
                //csDataRow1[1] = "総合計";
                csDataRow1[1] = "  総合計";
                //end modified 2014/04/17
                //dtGroupPersonal.Rows.Add(csDataRow1);
                dsMontor.Tables["dtGroupPersonal"].Rows.Add(csDataRow1);
                setGroup();
                if (dsMontor.Tables["dtGroupPersonal"] != null)
                    DtMonitorRowsCount = dsMontor.Tables["dtMonitor"].Rows.Count;
                connectTimer_Tick(sender, e);
                MonitorTimer.Enabled = false;
                //added by Zhu 2014/03/25
                if (dtGroupPersonal.Rows.Count > 1)
                {
                    this.MenuIdle.Enabled = true;
                    this.MenuSkillShowSet.Enabled = true;
                    dtSkillGroup = dtGroupPersonal.DefaultView.ToTable(true, new string[] { "groupId", "groupName" });
                    GetSkillQuecallSetting(dtSkillGroup);
                }
                //end added
            }
            catch (Exception ex)
            {
                writeLog("wbGroupPersonal_DocumentCompleted SysteError:" + ex.Message + ex.StackTrace);
            }

        }
        private void setGroup()
        {
            try
            {
                DataRow csDataRow1 = dsMontor.Tables["dtMonitor"].NewRow(); //dtMontor.NewRow();
                //DataRow csDataRow1 = dsMontor.Tables["dtMonitor"].NewRow();
                //agentID "groupId", "groupName"
                DataTable dtTemp = dsMontor.Tables["dtGroupPersonal"].DefaultView.ToTable(true, new string[] { "groupId", "groupName" });
                //DataTable dtTemp = dtGroupPersonal.DefaultView.ToTable(true, new string[] { "groupId", "groupName" });

                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {

                    if (i == dtTemp.Rows.Count - 1)
                    {
                        csDataRow1 = dsMontor.Tables["dtMonitor"].NewRow();//dsMontor.Tables["dtMonitor"].NewRow();
                        csDataRow1[0] = dtTemp.Rows[i][1];
                        csDataRow1[1] = "0";
                        csDataRow1[2] = "0";
                        csDataRow1[3] = "0";
                        csDataRow1[4] = "0";
                        csDataRow1[5] = "0";
                        csDataRow1[6] = "0.0%";
                        csDataRow1[7] = "0";
                        csDataRow1[8] = "0.0%";
                        csDataRow1[9] = "0";
                        csDataRow1[10] = "-";//added by zhu 2014/05/13
                        csDataRow1[11] = "0";//change 10->11
                        csDataRow1[12] = "0";//11->12
                        csDataRow1[13] = "0.0%";//12->13
                        csDataRow1[14] = dtTemp.Rows[i][0];//13->14
                    }

                    else
                    {
                        //added by Zhu 2014-04-01
                        if (!GetCheckedStatus(dtTemp.Rows[i][0].ToString()))
                        { continue; }
                        //end added
                        csDataRow1 = dsMontor.Tables["dtMonitor"].NewRow(); //dsMontor.Tables["dtMonitor"].NewRow(); dtMontor.NewRow();
                        csDataRow1[0] = dtTemp.Rows[i][1];
                        csDataRow1[1] = "0";
                        csDataRow1[2] = "0";
                        csDataRow1[3] = "0";
                        csDataRow1[4] = "0";
                        csDataRow1[5] = "0";
                        csDataRow1[6] = "0.0%";
                        csDataRow1[7] = "0";
                        csDataRow1[8] = "0.0%";
                        csDataRow1[9] = "0";
                        csDataRow1[10] = "00:00:00";//added by zhu 2014/05/13
                        csDataRow1[11] = "0";//change 10->11
                        csDataRow1[12] = "0";//11->12
                        csDataRow1[13] = "0.0%";//12->13
                        csDataRow1[14] = dtTemp.Rows[i][0];//13->14

                        //added by zhu 2014/06/18
                        if (!_DefaultShowSkillGroupIDs.Contains(dtTemp.Rows[i][0].ToString()))
                            _DefaultShowSkillGroupIDs += ",'" + dtTemp.Rows[i][0] + "'";
                        //end added
                    }
                    if (!string.IsNullOrEmpty(_DefaultShowSkillGroupIDs) && _DefaultShowSkillGroupIDs.Substring(0, 1) == ",")
                        _DefaultShowSkillGroupIDs = _DefaultShowSkillGroupIDs.Substring(1);
                    //dtMontor.Rows.Add(csDataRow1);
                    dsMontor.Tables["dtMonitor"].Rows.Add(csDataRow1);

                }

                //System.Data.DataRelation dr = new DataRelation("relation", dsMontor.Tables["dtMonitor"].Columns["groupId"]
                //                                  , dsMontor.Tables["dtGroupPersonal"].Columns["groupId"]);
                //dsMontor.Relations.Add(dr);
            }
            catch (Exception ex)
            {
                writeLog("setGroup SysteError:" + ex.Message + ex.StackTrace);
            }
        }

        ////test
        //private  void dvMonitor_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        //{
        //    //throw new System.NotImplementedException();
        //    try
        //    {
        //        if (e.ColumnIndex != 0) return;
        //        if (MontorSortFlag == false)
        //        {
        //            dvMonitor.Sort(new RowComparer(SortOrder.Descending));

        //            MontorSortFlag = true;
        //        }
        //        else
        //        {
        //            dvMonitor.Sort(new RowComparer(SortOrder.Ascending));
        //            MontorSortFlag = false ;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("dvMonitor_SortCompare:" + ex.Message + ex.StackTrace);
        //    }
        //}

        //private class RowComparer : System.Collections.IComparer
        //{
        //    private static int sortOrderModifier = 1;

        //    public RowComparer(SortOrder sortOrder)
        //    {
        //        if (sortOrder == SortOrder.Descending)
        //        {
        //            sortOrderModifier = -1;
        //        }
        //        else if (sortOrder == SortOrder.Ascending)
        //        {
        //            sortOrderModifier = 1;
        //        }
        //    }

        //    public int Compare(object x, object y)
        //    {
        //        DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
        //        DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

        //        // Try to sort based on the Last Name column.
        //        int CompareResult = System.String.Compare(
        //            DataGridViewRow1.Cells[0].Value.ToString(),
        //            DataGridViewRow2.Cells[0].Value.ToString());

        //        // If the Last Names are equal, sort based on the First Name.
        //        //if (CompareResult == 0)
        //        //{
        //        //    CompareResult = System.String.Compare(
        //        //        DataGridViewRow1.Cells[0].Value.ToString(),
        //        //        DataGridViewRow2.Cells[0].Value.ToString());
        //        //}
        //        return CompareResult * sortOrderModifier;
        //    }
        //}

        ////test
        //private void dvMonitor_SortCompare(object sender, System.Windows.Forms.DataGridViewSortCompareEventArgs e)
        //{
        //    //throw new System.NotImplementedException();
        //    try
        //    {
        //        if (e.RowIndex1 == dvMonitor.RowCount - 1) return;
        //        e.SortResult = System.String.Compare(
        //           e.CellValue1.ToString(), e.CellValue2.ToString());

        //        // If the cells are equal, sort based on the ID column.
        //        if (e.SortResult == 0 && e.Column.Name != "groupName")
        //        {
        //               e.SortResult = System.String.Compare(
        //                dvMonitor.Rows[e.RowIndex1].Cells["groupName"].Value.ToString(),
        //                dvMonitor.Rows[e.RowIndex2].Cells["groupName"].Value.ToString());
        //        }
        //        e.Handled = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("dvMonitor_SortCompare:" + ex.Message + ex.StackTrace);
        //    }
        //}


        private void listMonitorInit()
        {
            try
            {

                dtGroupPersonal = new DataTable("dtGroupPersonal");

                dtGroupPersonal.Columns.Add("groupId", typeof(int));
                dtGroupPersonal.Columns.Add("groupName");
                dtGroupPersonal.Columns.Add("agentID");
                dtGroupPersonal.Columns.Add("status", typeof(int));

                dtSkillGroup = new DataTable("dtSkillGroup");
                dtSkillGroup.Columns.Add("groupId", typeof(int));
                dtSkillGroup.Columns.Add("groupName");

                dsMontor.Tables.Add(dtGroupPersonal);

                dvMonitor.Columns.Clear();
                dvMonitor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                //dvReport.ColumnCount = 11;
                dvMonitor.RowHeadersVisible = false;



                ////deleted by zhu 2014/05/12
                //MonitorItemShowString = iniProfile.GetStringDefault(ConstEntity.ITEMSHOWKEY, "");
                //if (string.IsNullOrEmpty(MonitorItemShowString)) MonitorItemShowString = "1,1,1,1,1,1,1,1,1,1,1,1,1,1";
                //string[] visibleArr = MonitorItemShowString.Split(',');
                //end deleted

                DataGridViewColumn column;


                column = new DataGridViewTextBoxColumn();
                //modified by zhu 2014/05/12
                //column.HeaderText = MonitorCol1;// "スキルグループ";
                column.HeaderText = _MonitorItemManager.MonitorItems[0].DisplayName;
                //end modified
                column.Name = "groupName";
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                //column.SortMode = DataGridViewColumnSortMode.Programmatic;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.Width = 100;
                column.DataPropertyName = "groupName";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[0].Visible;
                //end added
                dvMonitor.Columns.Add(column);



                //ログオン人数
                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[1].DisplayName; //"ログオン人数";
                column.Name = "allLogon";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 50;
                column.DataPropertyName = "allLogon";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[1].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[2].DisplayName;// "着座OP数";
                column.Name = "opCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 50;
                column.DataPropertyName = "opCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[2].Visible;
                //end added
                dvMonitor.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[3].DisplayName;// "離席数";
                column.Name = "seatLeaveCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 100;
                column.DataPropertyName = "seatLeaveCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[3].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = "ACD着信数";
                column.HeaderText = _MonitorItemManager.MonitorItems[4].DisplayName; //"OP呼出数";　　//通話数->着信数->OP呼出数
                column.Name = "acdCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 50;
                column.DataPropertyName = "acdCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[4].Visible;
                //end added
                dvMonitor.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[5].DisplayName;// "OP応答数";//応答数
                column.Name = "answerCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "answerCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[5].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[6].DisplayName;// "応答率";
                column.Name = "answerPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "answerPer";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[6].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                //add,S
                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[7].DisplayName;// "即答数";
                column.Name = "answerNowCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "answerNowCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[7].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[8].DisplayName;// "即答率";
                column.Name = "answerNowPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //特定ユーザだけ、設定
                if (ShowBackColorForCol9 == "1")
                    column.DefaultCellStyle.BackColor = Color.LightBlue;

                column.Width = 60;

                column.DataPropertyName = "answerNowPer";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[8].Visible;
                //end added
                dvMonitor.Columns.Add(column);
                //add,E

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[9].DisplayName;// "待呼数";
                column.Name = "queCallCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "queCallCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[9].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                //added by zhu 2014/05/12
                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[10].DisplayName;//経過時間
                column.Name = "queCallContinueTime";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 80;
                column.Visible = _MonitorItemManager.MonitorItems[10].Visible;
                column.DataPropertyName = "queCallContinueTime";
                dvMonitor.Columns.Add(column);
                //end added

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[11].DisplayName;// "受付可数";
                column.Name = "waitCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 50;
                column.DataPropertyName = "waitCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[11].Visible;
                //end added
                dvMonitor.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[12].DisplayName;// "放棄数";
                column.Name = "failCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "failCnt";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[12].Visible;
                //end added
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = _MonitorItemManager.MonitorItems[13].DisplayName;// "放棄率";
                column.Name = "failPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "failPer";
                //added by Zhu 2014/04/01
                column.Visible = _MonitorItemManager.MonitorItems[13].Visible;
                //end added
                dvMonitor.Columns.Add(column);


                //column = new DataGridViewTextBoxColumn();
                //column.HeaderText = "離席数";
                //column.SortMode = DataGridViewColumnSortMode.NotSortable;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                //column.Width = 100;
                //column.DataPropertyName = "seatLeaveCnt";
                //dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = "";
                column.Name = "groupId";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 0;
                column.DataPropertyName = "groupId";
                column.Visible = false;
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = "";
                column.Name = "iSessionProfileID";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 0;
                column.DataPropertyName = "iSessionProfileID";
                column.Visible = false;
                dvMonitor.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.HeaderText = "";
                column.Name = "groupKId";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 0;
                column.DataPropertyName = "groupKId";
                column.Visible = false;
                dvMonitor.Columns.Add(column);

                dvMonitor.ReadOnly = true;
                dvMonitor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtMontor = new DataTable("dtMonitor");
                dsMontor.Tables.Add(dtMontor);



                //dtMontor.Columns.Add("id");
                //dtMontor.Columns[0].AutoIncrement = true;
                //dtMontor.Columns[0].AutoIncrementSeed = 1;
                //dtMontor.Columns[0].AutoIncrementStep = 1; 

                dtMontor.Columns.Add("groupName");
                //dtMontor.Columns.Add("allLogon").Expression = "sum(child(relation).status)";
                dtMontor.Columns.Add("allLogon", typeof(int));
                dtMontor.Columns.Add("opCnt", typeof(int));
                dtMontor.Columns.Add("seatLeaveCnt", typeof(int));
                dtMontor.Columns.Add("acdCnt", typeof(int));
                dtMontor.Columns.Add("answerCnt", typeof(int));
                dtMontor.Columns.Add("answerPer");
                dtMontor.Columns.Add("answerNowCnt", typeof(int));
                dtMontor.Columns.Add("answerNowPer");
                dtMontor.Columns.Add("queCallCnt", typeof(int));
                //added by zhu 2014/05/12
                dtMontor.Columns.Add("queCallContinueTime", typeof(string));
                //end added
                dtMontor.Columns.Add("waitCnt", typeof(int));
                dtMontor.Columns.Add("failCnt", typeof(int));
                dtMontor.Columns.Add("failPer");
                //dtMontor.Columns.Add("seatLeaveCnt", typeof(int));
                dtMontor.Columns.Add("groupId", typeof(int));


                dtMontor.Columns.Add("iSessionProfileID", typeof(int));
                dtMontor.Columns.Add("groupKId", typeof(int)); //局番ID


                //dtMontor.Columns["allLogon"].Expression = "sum(child(relation).status)";
                //dvMonitor.DataSource = dtMontor;

                //modified by zhu 2014/06/25
                dvMonitor.DataSource = dsMontor.Tables["dtMonitor"];
                //dvMonitor.DataSource = dvBindingSource;
                //end  modified

                //System.Data.DataRelation dr = new DataRelation("relation", dtMontor.Columns["groupId"], dtGroupPersonal.Columns["groupId"]);

                //test,s

                //csDataRow1["id"] = 1;
                //csDataRow1[0] = "スキルグループ1";
                //csDataRow1[1] = "3";
                //csDataRow1[2] = "3";
                //csDataRow1[3] = "2";
                //csDataRow1[4] = "2";
                //csDataRow1[5] = "100.0%";
                //csDataRow1[6] = "0";
                //csDataRow1[7] = "2";
                //csDataRow1[8] = "2";
                //csDataRow1[9] = "0";
                //csDataRow1[10] = "0.0%";
                //csDataRow1[11] = "1";

                //dtMontor.Rows.Add(csDataRow1);


                //csDataRow1 = dtMontor.NewRow();
                //csDataRow1[0] = "スキルグループ2";
                //csDataRow1[1] = "3";
                //csDataRow1[2] = "3";
                //csDataRow1[3] = "2";
                //csDataRow1[4] = "2";
                //csDataRow1[5] = "100.0%";
                //csDataRow1[6] = "0";
                //csDataRow1[7] = "2";
                //csDataRow1[8] = "2";
                //csDataRow1[9] = "0";
                //csDataRow1[10] = "0.0%";
                //csDataRow1[11] = "1";
                //dtMontor.Rows.Add(csDataRow1);

                //csDataRow1 = dtMontor.NewRow();
                //csDataRow1[0] = "スキルグループ3";
                //csDataRow1[1] = "3";
                //csDataRow1[2] = "3";
                //csDataRow1[3] = "2";
                //csDataRow1[4] = "2";
                //csDataRow1[5] = "100.0%";
                //csDataRow1[6] = "0";
                //csDataRow1[7] = "2";
                //csDataRow1[8] = "2";
                //csDataRow1[9] = "0";
                //csDataRow1[10] = "0.0%";
                //csDataRow1[11] = "1";
                //dtMontor.Rows.Add(csDataRow1);
                //test,E

                //added by Zhu 2014/04/09
                setDoubleBuffered(this.dvMonitor, true);
                //end added

                DicOriginMonitorGridColumnWidth.Clear();
                foreach (DataGridViewColumn col in dvMonitor.Columns)
                {
                    DicOriginMonitorGridColumnWidth.Add(col.Name, col.Width);
                }

            }
            catch (Exception ex)
            {
                writeLog("listMonitorInit SysteError:" + ex.Message + ex.StackTrace);
            }
        }
        public void setQuickAnswerMinutes(string inTime)
        {
            try
            {
                QuickAnswerMinutes = inTime;


                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("QuickAnswerMinutes", inTime);

                IniProfile.Save(MyTool.GetModuleIniPath());
            }
            catch (Exception ex)
            {
                writeLog("setQuickAnswerMinutes System Error:" + ex.Message);
            }

        }


        private void resetMonitorCall()
        {
            try
            {

                System.Threading.Thread callThread = new System.Threading.Thread(delegate () { setMonitorCall1(); });
                callThread.IsBackground = true;
                callThread.Start();

            }
            catch (Exception ex)
            {
                writeLog("sumMenuQuickAnswer_Click:" + ex.Message);
            }
        }

        private void setMonitorCall()
        {
            try
            {
                if (SettingFields_MonitorTabShow == "0") return;
                if (displayGroupPre == displayGroup) return;
                displayGroupPre = displayGroup;

                System.Threading.Thread callThread = new System.Threading.Thread(delegate () { setMonitorCall1(); });
                callThread.IsBackground = true;
                callThread.Start();


                return;
                string curGroupID = "";
                string curgroupKId = "";
                int acdCount = 0;

                int answerCnt = 0;
                int failCnt = 0;
                int answerNowCnt = 0;
                int answerNowPer = 0;

                int acdCountPre = 0;
                int answerCntPre = 0;
                int failCntPre = 0;
                int answerNowCntPre = 0;
                int answerNowPerPre = 0;



                ArrayList rs = new ArrayList();
                ArrayList rsPre = new ArrayList();
                //dtAcdcallがある時,acdCallLocalDBに登録

                //addCallInfo(lineStatus);
                //if (lineStatus.Status != 0) return; //通話完了後、計算
                //if (displayGroup == -1)

                //    rs = getCallInfo("".ToString(), "");
                //else
                //    rs = getCallInfo("".ToString(), displayGroup.ToString());
                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();
                    //curgroupKId = dsMontor.Tables["dtMonitor"].Rows[i]["curgroupKId"].ToString();

                    //queueStatusList.Find();
                    //LineStatus callFind = lineStatusList.Find(delegate(LineStatus obj)
                    //{ return obj.Group == int.Parse(curGroupID); });
                    //if (lineStatus.Group.ToString() == curGroupID)

                    System.Threading.Thread.Sleep(50);
                    if (curGroupID != "-1")
                    {
                        rs = new ArrayList();
                        rsPre = new ArrayList();
                        if (displayGroup == -1)
                        {
                            rs = getCallInfo(curGroupID, "");
                            rsPre = getCallInfoPre(curGroupID, "");
                        }
                        else
                        {
                            rs = getCallInfo(curGroupID, displayGroup.ToString());
                            rsPre = getCallInfoPre(curGroupID, displayGroup.ToString());
                        }


                        //if (lineStatus.Service == "ACDCALL" && lineStatus.Status == 2) //acdcall
                        //{
                        //    acdCountPre = int.Parse(dsMontor.Tables["dtMonitor"].Rows[i]["acdCnt"].ToString());
                        //    acdCount = acdCountPre + 1;
                        //    dsMontor.Tables["dtMonitor"].Rows[i]["acdCnt"] = acdCount;
                        //}
                        //else if (lineStatus.Status == 4) //3->4
                        //{
                        //    answerCnt = int.Parse(dsMontor.Tables["dtMonitor"].Rows[i]["answerCnt"].ToString());
                        //    answerCnt = answerCnt + 1;
                        //    dsMontor.Tables["dtMonitor"].Rows[i]["answerCnt"] = acdCount;
                        //    if (acdCount > 0)
                        //    {
                        //        dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = (answerCnt * 1.0 * 100 / acdCount).ToString() + "%";
                        //    }
                        //    else
                        //    {
                        //        dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = "100%";
                        //    }
                        //}

                        if (rsPre.Count > 0)
                        {
                            ArrayList fsPre = (ArrayList)rsPre[0];
                            if (string.IsNullOrEmpty(fsPre[1].ToString()))

                                acdCountPre = 0;
                            else
                                acdCountPre = int.Parse(fsPre[1].ToString());
                            if (string.IsNullOrEmpty(fsPre[2].ToString()))
                                answerCntPre = 0;
                            else
                                answerCntPre = int.Parse(fsPre[2].ToString());
                            if (string.IsNullOrEmpty(fsPre[3].ToString()))
                                answerNowCntPre = 0;
                            else
                                answerNowCntPre = int.Parse(fsPre[3].ToString());

                        }
                        else
                        {
                            //acdCount = 0;
                            //answerCnt = 0;
                            //answerNowCnt = 0;
                            //answerNowPer = 0;
                            acdCountPre = 0;
                            answerCntPre = 0;
                            answerNowCntPre = 0;
                            answerNowPerPre = 0;
                        }

                        if (rs.Count > 0)
                        {
                            ArrayList fs = (ArrayList)rs[0];
                            if (string.IsNullOrEmpty(fs[1].ToString()))
                                acdCount = 0;
                            else
                                acdCount = int.Parse(fs[1].ToString());
                            if (string.IsNullOrEmpty(fs[2].ToString()))
                                answerCnt = 0;
                            else
                                answerCnt = int.Parse(fs[2].ToString());
                            if (string.IsNullOrEmpty(fs[3].ToString()))
                                answerNowCnt = 0;
                            else
                                answerNowCnt = int.Parse(fs[3].ToString());

                        }
                        else
                        {
                            acdCount = 0;
                            answerCnt = 0;
                            answerNowCnt = 0;
                            answerNowPer = 0;
                        }

                        acdCount = acdCountPre + acdCount;
                        answerCnt = answerCntPre + answerCnt;
                        answerNowCnt = answerNowCntPre + answerNowCnt;

                        dsMontor.Tables["dtMonitor"].Rows[i]["acdCnt"] = acdCount;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerCnt"] = answerCnt;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerNowCnt"] = answerNowCnt;

                        if (answerCnt > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = "0.0%";
                        }
                        if (acdCount > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";

                            failCnt = acdCount - answerCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = failCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = "0.0%";
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = 0;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = "0.0%";
                        }
                        //break;
                    }

                }
                //総合計
                if (DtMonitorRowsCount > 0)
                {
                    //deleted by Zhu 2014/04/01
                    //acdCount = 0;
                    //answerCnt = 0;
                    //answerNowCnt = 0;
                    //failCnt = 0;

                    //DataRow[] foundRows = dsMontor.Tables["dtMonitor"].Select("groupId<>'-1'");
                    //dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["queCallCnt"] = 0;
                    //for (int j = 0; j < foundRows.Length; j++)
                    //{

                    //    acdCount = acdCount + int.Parse(foundRows[j]["acdCnt"].ToString());
                    //    answerCnt = answerCnt + int.Parse(foundRows[j]["answerCnt"].ToString());
                    //    answerNowCnt = answerNowCnt + int.Parse(foundRows[j]["answerNowCnt"].ToString());
                    //    //failCnt = failCnt + 0;
                    //}
                    //failCnt = acdCount - answerCnt;
                    //if (failCnt < 1) failCnt = 0;
                    //dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["acdCnt"] = acdCount;
                    //dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerCnt"] = answerCnt;
                    //dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerNowCnt"] = answerNowCnt;

                    //if (answerCnt > 0)
                    //{
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerNowPer"] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
                    //}
                    //else
                    //{
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerNowPer"] = "0.0%";
                    //}
                    //if (acdCount > 0)
                    //{
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerPer"] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";

                    //    failCnt = acdCount - answerCnt;
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["failCnt"] = failCnt;
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["failPer"] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
                    //}
                    //else
                    //{
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["answerPer"] = "0.0%";
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["failCnt"] = 0;
                    //    dsMontor.Tables["dtMonitor"].Rows[dsMontor.Tables["dtMonitor"].Rows.Count - 1]["failPer"] = "0.0%";
                    //}

                    //end Deleted
                    //added by Zhu 2014/04/01
                    SetDvTotalRow("CALLLEG", false);
                    //end added
                }

                //writeLog("setMonitorCall");
            }
            catch (Exception ex)
            {
                writeLog("setMonitorCall SysteError:" + ex.Message + ex.StackTrace);
            }
        }

        private void setMonitorCall1()
        {
            try
            {
                //added by zhu 2014/06/27
                //this.CommandThread.Suspend();
                //end added
                if (SettingFields_MonitorTabShow == "0") return;
                string curGroupID = "";
                string curgroupKId = "";
                int acdCount = 0;

                int answerCnt = 0;
                int failCnt = 0;
                int answerNowCnt = 0;
                int answerNowPer = 0;

                int acdCountPre = 0;
                int answerCntPre = 0;
                int failCntPre = 0;
                int answerNowCntPre = 0;
                int answerNowPerPre = 0;

                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].BeginLoadData();
                //end added

                ArrayList rs = new ArrayList();
                ArrayList rsPre = new ArrayList();

                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    //added by Zhu 2014/06/26
                    if (i == DtMonitorRowsCount - 1)
                        break;
                    //end added

                    curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();


                    System.Threading.Thread.Sleep(10);
                    if (curGroupID != "-1")
                    {
                        rs = new ArrayList();
                        rsPre = new ArrayList();
                        if (displayGroup == -1)
                        {
                            rs = getCallInfo(curGroupID, "");
                            rsPre = getCallInfoPre(curGroupID, "");
                        }
                        else
                        {
                            rs = getCallInfo(curGroupID, displayGroup.ToString());
                            rsPre = getCallInfoPre(curGroupID, displayGroup.ToString());
                        }




                        if (rsPre.Count > 0)
                        {
                            ArrayList fsPre = (ArrayList)rsPre[0];
                            if (string.IsNullOrEmpty(fsPre[1].ToString()))

                                acdCountPre = 0;
                            else
                                acdCountPre = int.Parse(fsPre[1].ToString());
                            if (string.IsNullOrEmpty(fsPre[2].ToString()))
                                answerCntPre = 0;
                            else
                                answerCntPre = int.Parse(fsPre[2].ToString());
                            if (string.IsNullOrEmpty(fsPre[3].ToString()))
                                answerNowCntPre = 0;
                            else
                                answerNowCntPre = int.Parse(fsPre[3].ToString());

                        }
                        else
                        {
                            acdCountPre = 0;
                            answerCntPre = 0;
                            answerNowCntPre = 0;
                            answerNowPerPre = 0;
                        }

                        if (rs.Count > 0)
                        {
                            ArrayList fs = (ArrayList)rs[0];
                            if (string.IsNullOrEmpty(fs[1].ToString()))
                                acdCount = 0;
                            else
                                acdCount = int.Parse(fs[1].ToString());
                            if (string.IsNullOrEmpty(fs[2].ToString()))
                                answerCnt = 0;
                            else
                                answerCnt = int.Parse(fs[2].ToString());
                            if (string.IsNullOrEmpty(fs[3].ToString()))
                                answerNowCnt = 0;
                            else
                                answerNowCnt = int.Parse(fs[3].ToString());

                        }
                        else
                        {
                            acdCount = 0;
                            answerCnt = 0;
                            answerNowCnt = 0;
                            answerNowPer = 0;
                        }

                        acdCount = acdCountPre + acdCount;
                        answerCnt = answerCntPre + answerCnt;
                        answerNowCnt = answerNowCntPre + answerNowCnt;


                        dsMontor.Tables["dtMonitor"].Rows[i]["acdCnt"] = acdCount;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerCnt"] = answerCnt;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerNowCnt"] = answerNowCnt;

                        if (answerCnt > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = "0.0%";
                        }
                        if (acdCount > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";

                            failCnt = acdCount - answerCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = failCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = "0.0%";
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = 0;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = "0.0%";
                        }
                        //break;
                    }

                }
                //総合計
                if (DtMonitorRowsCount > 0)
                {
                    //added by Zhu 2014/04/01
                    SetDvTotalRow("CALLLEG", true);
                    //end added
                }
                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].EndLoadData();
                //end added
                //writeLog("setMonitorCall");

                //added by zhu 2014/06/27
                //this.CommandThread.Resume();
                //end added
            }
            catch (Exception ex)
            {
                writeLog("setMonitorCall1 SysteError:" + ex.Message + ex.StackTrace);
            }
        }
        private void setMonitorCall(LineStatus lineStatus)
        {
            try
            {
                if (SettingFields_MonitorTabShow == "0") return;

                string curGroupID = "";
                string curgroupKId = "";
                int acdCount = 0;

                int answerCnt = 0;
                int failCnt = 0;
                int answerNowCnt = 0;
                int answerNowPer = 0;

                int acdCountPre = 0;
                int answerCntPre = 0;
                int failCntPre = 0;
                int answerNowCntPre = 0;
                int answerNowPerPre = 0;
                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].BeginLoadData();
                //end added

                ArrayList rs = new ArrayList();
                ArrayList rsPre = new ArrayList();
                //dtAcdcallがある時,acdCallLocalDBに登録

                addCallInfo(lineStatus);
                if (lineStatus.Status != 0) return; //通話完了後、計算
                if (displayGroup == -1)
                {
                    rs = getCallInfo(lineStatus.iSkillGroupID.ToString(), "");
                    rsPre = getCallInfoPre(lineStatus.iSkillGroupID.ToString(), "");
                }
                else
                {
                    rs = getCallInfo(lineStatus.iSkillGroupID.ToString(), displayGroup.ToString());
                    rsPre = getCallInfoPre(lineStatus.iSkillGroupID.ToString(), displayGroup.ToString());
                }
                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();

                    if (lineStatus.iSkillGroupID.ToString() == curGroupID)
                    {

                        if (rsPre.Count > 0)
                        {
                            ArrayList fsPre = (ArrayList)rsPre[0];
                            if (string.IsNullOrEmpty(fsPre[1].ToString()))
                                acdCountPre = 0;
                            else
                                acdCountPre = int.Parse(fsPre[1].ToString());
                            if (string.IsNullOrEmpty(fsPre[2].ToString()))
                                answerCntPre = 0;
                            else
                                answerCntPre = int.Parse(fsPre[2].ToString());
                            if (string.IsNullOrEmpty(fsPre[3].ToString()))
                                answerNowCntPre = 0;
                            else
                                answerNowCntPre = int.Parse(fsPre[3].ToString());

                        }
                        else
                        {
                            acdCountPre = 0;
                            answerCntPre = 0;
                            answerNowCntPre = 0;
                            answerNowPerPre = 0;
                        }

                        if (rs.Count > 0)
                        {
                            ArrayList fs = (ArrayList)rs[0];
                            if (string.IsNullOrEmpty(fs[1].ToString()))
                                acdCount = 0;
                            else
                                acdCount = int.Parse(fs[1].ToString());
                            if (string.IsNullOrEmpty(fs[2].ToString()))
                                answerCnt = 0;
                            else
                                answerCnt = int.Parse(fs[2].ToString());
                            if (string.IsNullOrEmpty(fs[3].ToString()))
                                answerNowCnt = 0;
                            else
                                answerNowCnt = int.Parse(fs[3].ToString());

                        }
                        else
                        {
                            acdCount = 0;
                            answerCnt = 0;
                            answerNowCnt = 0;
                            answerNowPer = 0;
                        }
                        acdCount = acdCountPre + acdCount;
                        answerCnt = answerCntPre + answerCnt;
                        answerNowCnt = answerNowCntPre + answerNowCnt;

                        dsMontor.Tables["dtMonitor"].Rows[i]["acdCnt"] = acdCount;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerCnt"] = answerCnt;
                        dsMontor.Tables["dtMonitor"].Rows[i]["answerNowCnt"] = answerNowCnt;

                        if (answerCnt > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerNowPer"] = "0.0%";
                        }
                        if (acdCount > 0)
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";

                            failCnt = acdCount - answerCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = failCnt;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
                        }
                        else
                        {
                            dsMontor.Tables["dtMonitor"].Rows[i]["answerPer"] = "0.0%";
                            dsMontor.Tables["dtMonitor"].Rows[i]["failCnt"] = 0;
                            dsMontor.Tables["dtMonitor"].Rows[i]["failPer"] = "0.0%";
                        }
                        break;
                    }

                }
                //総合計
                if (DtMonitorRowsCount > 0)
                {
                    //added by Zhu 2014/04/01
                    SetDvTotalRow("CALLLEG", false);
                    //end added
                }

                //added by Zhu 2014/04/10 
                // dsMontor.Tables["dtMonitor"].EndLoadData();
                //end added
                //writeLog("setMonitorCall");
            }
            catch (Exception ex)
            {
                writeLog("setMonitorCall SysteError:" + ex.Message + ex.StackTrace);
            }
        }

        //private ArrayList getCallInfo( string iSkillID)
        //{
        //    Database db = new Database();
        //    ArrayList rs = new ArrayList();
        //    try
        //    {

        //        if (!db.openDB(Application.StartupPath, "")) return rs;


        //        string sql = "SELECT iSkillGroupID ,SUM(Switch(iStatus='0',1,iStatus<>'0', 0)) as acdCallCount ";
        //        sql = sql + "  ,SUM(Switch(iStatus='3',1,iStatus<>'3', 0)) as completeCallCount";
        //        sql = sql + " ,SUM(Switch(iStatus='3',Switch(dtDiff<=" + QuickAnswerMinutes + ",1,dtDiff>" + QuickAnswerMinutes + ", 0),iStatus<>'3',0)) as notOverCall";
        //        sql = sql + " FROM (";
        //        sql = sql + " SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid, DateDiff('s',dtAcdCall,dtstatus) as dtDiff";
        //        sql = sql + " FROM callInfo ";
        //        sql = sql + " WHERE   iSkillGroupID=iSkillGroupID";
        //        if (!string.IsNullOrEmpty(iSkillID))
        //            sql = sql + " AND   iSkillID='" + iSkillID + "'";
        //        sql = sql + " AND   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
        //        sql = sql + "  ) a GROUP BY iSkillGroupID";
        //        if (false == db.readDB(sql, rs))
        //        {
        //            db.closeDB();
        //            return rs;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("getCallInfo:" + ex.Message);
        //    }

        //    db.closeDB();
        //    return rs;
        //}

        private string getCallInfoMinID()
        {
            Database db = new Database();
            //ArrayList rs=new ArrayList();
            string iSessionid = "0";
            try
            {

                if (!db.openDB(Application.StartupPath, "")) return "";


                string sql = "SELECT Min(iSessionProfileid)";
                sql = sql + " FROM callInfo  ";
                iSessionid = db.readDB(sql);


            }
            catch (Exception ex)
            {
                writeLog("getCallInfoMinID:" + ex.Message);
            }

            db.closeDB();
            return iSessionid;
        }

        private ArrayList getCallInfoPre(string iSkillGroupID, string iSkillID)
        {
            Database db = new Database();
            ArrayList rs = new ArrayList();
            try
            {

                if (!db.openDB1(Application.StartupPath, "")) return rs;

                string sql = "SELECT iSkillGroupID  ,COUNT(iSessionProfileid) as acdCallCount ";
                sql = sql + " ,SUM(Switch(dtDiff>0,1,dtDiff<=0, 0)) as completeCallCount ,SUM(Switch(dtDiff<=" + QuickAnswerMinutes + ",1,dtDiff>" + QuickAnswerMinutes + ", 0)) as notOverCall ";
                sql = sql + " FROM (SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',b.dtAcdCall1,b.dtstatus1) as dtDiff ";
                sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid   FROM callInfoPre";
                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                if (!string.IsNullOrEmpty(iSkillID))
                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                sql = sql + "  )a LEFT JOIN (  ";
                //sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1,max(dtAcdCall) as dtAcdCall1  FROM callInfoPre ";
                sql = sql + " SELECT iSessionProfileid,dtstatus as dtstatus1,dtAcdCall as dtAcdCall1  FROM callInfoPre ";
                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                if (!string.IsNullOrEmpty(iSkillID))
                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //sql = sql + "  GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                sql = sql + "  ) b on a.iSessionProfileid=b.iSessionProfileid ";
                sql = sql + "  ) a GROUP BY iSkillGroupID";

                if (false == db.readDB(sql, rs))
                {
                    db.closeDB();
                    return rs;
                }

            }
            catch (Exception ex)
            {
                writeLog("getCallInfo:" + ex.Message);
            }

            db.closeDB();
            return rs;
        }

        private ArrayList getCallInfo(string iSkillGroupID, string iSkillID)
        {
            Database db = new Database();
            ArrayList rs = new ArrayList();
            try
            {

                if (!db.openDB(Application.StartupPath, "")) return rs;

                /*
                                string sql = "SELECT iSkillGroupID ,SUM(Switch(iStatus='0',1,iStatus<>'0', 0)) as acdCallCount ";
                                sql = sql + "  ,SUM(Switch(iStatus='3',1,iStatus<>'3', 0)) as completeCallCount";
                                sql = sql + " ,SUM(Switch(iStatus='3',Switch(dtDiff<=" + QuickAnswerMinutes + ",1,dtDiff>" + QuickAnswerMinutes + ", 0),iStatus<>'3',0)) as notOverCall";
                                sql = sql + " FROM (";
                                sql = sql + " SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',a.dtAcdCall,b.dtstatus1) as dtDiff";
                                sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid,dtAcdCall ";
                                sql = sql + "  FROM callInfo ";
                                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                                if (!string.IsNullOrEmpty(iSkillID))
                                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                                sql = sql + " AND   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
                                sql = sql + " )a LEFT JOIN ( ";
                                sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1 ";
                                sql = sql + " FROM callInfo  ";
                                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                                if (!string.IsNullOrEmpty(iSkillID))
                                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                                sql = sql + " GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                                sql = sql + "  ) a GROUP BY iSkillGroupID";
                                */

                //modified by zhu 2014/05/28 change status from 3 to 4
                //string sql = "SELECT iSkillGroupID ,Min(acdCallCount1) as acdCallCount "; //update,2014/04/08
                //sql = sql + "  ,SUM(Switch(iStatus='4',1,iStatus<>'4', 0)) as completeCallCount";
                //sql = sql + " ,SUM(Switch(iStatus='4',Switch(dtDiff<=" + QuickAnswerMinutes + ",1,dtDiff>" + QuickAnswerMinutes + ", 0),iStatus<>'4',0)) as notOverCall";
                //sql = sql + " FROM (";
                //sql = sql + " SELECT distinct a.*,b.acdCallCount1 FROM ( "; //add,2014/04/08
                //sql = sql + " SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',a.dtAcdCall,b.dtstatus1) as dtDiff";
                //sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid,dtAcdCall ";
                //sql = sql + "  FROM callInfo ";
                //sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                //if (!string.IsNullOrEmpty(iSkillID))
                //    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //sql = sql + " AND   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
                //sql = sql + " )a LEFT JOIN ( ";
                //sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1 ";
                //sql = sql + " FROM callInfo  ";
                //sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                //if (!string.IsNullOrEmpty(iSkillID))
                //    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //sql = sql + " GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                ////add,2014/04/08,S
                //sql = sql + ") a LEFT JOIN ( SELECT iSkillGroupID,COUNT(iSessionProfileid ) as acdCallCount1  FROM (";
                //sql = sql + "  SELECT DISTINCT iSkillGroupID,iSessionProfileid,dtacdcall    FROM callInfo  ";
                //sql = sql + " WHERE   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')) a1  GROUP BY iSkillGroupID )  b";
                //sql = sql + " ON a.iSkillGroupID=b.iSkillGroupID ";
                ////add,2014/04/08,E
                //sql = sql + "  ) a GROUP BY iSkillGroupID";

                string sql = "SELECT iSkillGroupID ,Min(acdCallCount1) as acdCallCount "; //update,2014/04/08
                sql = sql + "  ,SUM(Switch(iStatus='4',1,iStatus<>'4', 0)) as completeCallCount";
                sql = sql + " ,SUM(Switch(iStatus='4',Switch(dtDiff<=" + QuickAnswerMinutes + ",1,dtDiff>" + QuickAnswerMinutes + ", 0),iStatus<>'4',0)) as notOverCall";
                sql = sql + " FROM (";
                sql = sql + " SELECT distinct a.*,b.acdCallCount1 FROM ( "; //add,2014/04/08
                sql = sql + " SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',b.dtAcdCall1,b.dtstatus1) as dtDiff";
                sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid ";
                sql = sql + "  FROM callInfo ";
                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                if (!string.IsNullOrEmpty(iSkillID))
                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                sql = sql + " AND   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
                sql = sql + " )a LEFT JOIN ( ";
                sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1,MIN(dtAcdCall) as dtAcdCall1 ";
                sql = sql + " FROM callInfo  ";
                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                if (!string.IsNullOrEmpty(iSkillID))
                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                sql = sql + " GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                //add,2014/04/08,S
                sql = sql + ") a LEFT JOIN ( SELECT iSkillGroupID,COUNT(iSessionProfileid ) as acdCallCount1  FROM (";
                sql = sql + "  SELECT DISTINCT iSkillGroupID,iSessionProfileid    FROM callInfo  ";
                sql = sql + " WHERE   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')) a1  GROUP BY iSkillGroupID )  b";
                sql = sql + " ON a.iSkillGroupID=b.iSkillGroupID ";
                //add,2014/04/08,E
                sql = sql + "  ) a GROUP BY iSkillGroupID";

                if (false == db.readDB(sql, rs))
                {
                    db.closeDB();
                    return rs;
                }
            }
            catch (Exception ex)
            {
                writeLog("getCallInfo:" + ex.Message);
            }

            db.closeDB();
            return rs;
        }

        //added by Zhu 2014/04/08
        /// <summary>
        /// get acdcall count 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="rs1"></param>
        /// <param name="iSkillGroupID"></param>
        /// <param name="iSkillID"></param>
        /// <returns></returns>
        private ArrayList ResetCallInfo(Database db, ArrayList rs1, string iSkillGroupID, string iSkillID)
        {
            try
            {
                ArrayList rsAcdCall = new ArrayList();
                string sql = "select iSkillGroupID,count(iSessionProfileid) as acdCallCount,sum(0),sum(0) from (SELECT DISTINCT iSkillGroupID,iSessionProfileid from callinfo";
                sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                if (!string.IsNullOrEmpty(iSkillID))
                    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                sql = sql + ")group by iSkillGroupID";

                if (false == db.readDB(sql, rsAcdCall))
                {
                    db.closeDB();
                }
                if (rsAcdCall.Count > 0)
                {
                    if (rs1.Count > 0)
                    {
                        string skillGroup = (rs1[0] as ArrayList)[0].ToString();
                        string acdskillGroup = (rsAcdCall[0] as ArrayList)[0].ToString();
                        if (skillGroup == acdskillGroup)
                        {
                            (rs1[0] as ArrayList)[1] = (rsAcdCall[0] as ArrayList)[1];
                        }
                    }
                    else
                    {
                        return rsAcdCall;
                    }
                }
                return rs1;
            }
            catch (Exception ex)
            {
                writeLog("ResetCallInfo:" + ex.Message);
                return rs1;
            }
        }

        //end added

        private void delCallInfo()
        {
            Database db = new Database();
            try
            {
                if (!db.openDB(Application.StartupPath, "")) return;
                string sql = "DELETE FROM callInfo";
                db.excuteSql(sql);

                if (!db.openDB1(Application.StartupPath, "")) return;
                sql = "DELETE FROM callInfoPre";
                db.excuteSql(sql);

            }
            catch (Exception ex)
            {
                writeLog("delCallInfo:" + ex.Message);
                throw ex;
            }
            db.closeDB();
        }
        public void addCallInfo(LineStatus lineStatus)
        {


            Database db = new Database();
            try
            {
                writeLog("addCallInfo isessionprofileID = " + lineStatus.iSessionProfileID);
                string iSessionProfileID = lineStatus.iSessionProfileID;
                string iSkillID = lineStatus.Group.ToString();
                string vSkillName = lineStatus.GroupName;
                string iSkillGroupID = lineStatus.iSkillGroupID.ToString();
                string dtConnected = "";
                string dtStatus = lineStatus.StatusTime.ToString();
                string dtAcdcall = lineStatus.dtAcdcall.ToString();
                string iStatus = lineStatus.Status.ToString();
                string vService = lineStatus.Service;

                if (string.IsNullOrEmpty(iSessionProfileID)) return;
                if (string.IsNullOrEmpty(iSkillID)) return;
                if (string.IsNullOrEmpty(iSkillGroupID)) return;

                if (!db.openDB(Application.StartupPath, ""))
                {
                    writeLog("addCallInfo opendb ERROR");
                    return;
                }

                string sql = "";
                sql = "INSERT INTO callInfo(iSessionProfileID,iSkillID,vSkillName";
                sql = sql + ",iSkillGroupID,dtConnected,dtStatus,dtAcdcall,iStatus,vService)";

                sql = sql + "VALUES(" + iSessionProfileID + "";
                sql = sql + ",'" + iSkillID + "'";
                sql = sql + ",'" + vSkillName + "'";
                sql = sql + ",'" + iSkillGroupID + "'";
                sql = sql + ",'" + dtConnected + "'";
                sql = sql + ",'" + dtStatus + "'";
                sql = sql + ",'" + dtAcdcall + "'";
                sql = sql + ",'" + iStatus + "'";
                sql = sql + ",'" + vService + "'";
                sql = sql + ")";
                db.excuteSql(sql);
            }

            catch (Exception ex)
            {
                writeLog("addCall:" + ex.Message);
                throw ex;
            }
            db.closeDB();
            //getContact();

        }
        public void addCallInfo(string iSessionProfileID, string iSkillID, string vSkillName, string iSkillGroupID, string dtConnected
                                , string dtStatus, string dtAcdcall, string iStatus, string vService)
        {
            if (string.IsNullOrEmpty(iSessionProfileID)) return;
            if (string.IsNullOrEmpty(iSkillID)) return;
            if (string.IsNullOrEmpty(iSkillGroupID)) return;

            Database db = new Database();
            try
            {
                if (!db.openDB(Application.StartupPath, "")) return;

                string sql = "";
                sql = "INSERT INTO callInfo(iSessionProfileID,iSkillID,vSkillName";
                sql = sql + ",iSkillGroupID,dtConnected,dtStatus,dtAcdcall,iStatus,vService)";

                sql = sql + "VALUES('" + iSessionProfileID + "'";
                sql = sql + ",'" + iSkillID + "'";
                sql = sql + ",'" + vSkillName + "'";
                sql = sql + ",'" + iSkillGroupID + "'";
                sql = sql + ",'" + dtConnected + "'";
                sql = sql + ",'" + dtStatus + "'";
                sql = sql + ",'" + dtAcdcall + "'";
                sql = sql + ",'" + iStatus + "'";
                sql = sql + ",'" + vService + "'";
                sql = sql + ")";
                db.excuteSql(sql);
            }

            catch (Exception ex)
            {
                writeLog("addCall:" + ex.Message);
                throw ex;
            }
            db.closeDB();
            //getContact();

        }
        private void setMonitorQue(QueueStatus queueStatus)
        {
            try
            {
                if (SettingFields_MonitorTabShow == "0") return;
                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].BeginLoadData();
                //end added

                string curGroupID = "";
                int queCount = 0;
                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    //added by Zhu 2014/06/26
                    if (i == DtMonitorRowsCount - 1)
                        break;
                    //end added
                    curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();

                    if (curGroupID == queueStatus.Group.ToString())
                    {
                        dsMontor.Tables["dtMonitor"].Rows[i]["queCallCnt"] = queueStatus.QueueCount;
                        break;
                    }

                }

                //総合計
                if (DtMonitorRowsCount > 0)
                {
                    SetDvTotalRow("QUECALL", false);
                }
                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].EndLoadData();
                //end added

                //writeLog("setMonitorQue");
            }
            catch (Exception ex)
            {
                writeLog("setMonitorQue SysteError:" + ex.Message + ex.StackTrace);
            }
        }
        private void listMonitorShow()
        {
            try
            {
                if (SettingFields_MonitorTabShow == "0") return;
                //added by Zhu 2014/03/31
                string showSkillGroupIds = "";
                //end added

                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].BeginLoadData();
                //end added
                string curGroupID = "";
                int queCount = 0;

                for (int i = 0; i < DtMonitorRowsCount; i++)
                {
                    curGroupID = dsMontor.Tables["dtMonitor"].Rows[i]["groupId"].ToString();


                    //総合計
                    if (curGroupID == "-1")
                    {
                        //エージェントの総合計は問題があります。
                        continue;

                    }
                    //added by Zhu 2014/06/26
                    if (i == DtMonitorRowsCount - 1)
                        break;
                    //end added

                    //added by Zhu 2014/03/31
                    showSkillGroupIds += ",'" + curGroupID + "'";
                    //end added

                    //added by zhu 2014/06/02 avoid  SysteError:DataTable の内部インデックスが破損しています。'5' 

                    DataTable tempTable = dsMontor.Tables["dtGroupPersonal"].Copy();
                    //lock (tempTable)
                    //{
                    string strWhere = "groupId=" + curGroupID;
                    DataView viewTemp = new DataView();
                    viewTemp.Table = tempTable;
                    viewTemp.RowFilter = strWhere;
                    DataTable tblTemp = viewTemp.ToTable(false, "status");

                    //ログオン
                    DataRow[] foundRows = tblTemp.Select(" status>0");

                    dsMontor.Tables["dtMonitor"].Rows[i]["allLogon"] = foundRows.Length;

                    //離席
                    DataRow[] foundRows1 = tblTemp.Select(" status=6");
                    dsMontor.Tables["dtMonitor"].Rows[i]["seatLeaveCnt"] = foundRows1.Length;


                    //着座OP数＝ログインOPE数―離席OPE数
                    //dsMontor.Tables["dtMonitor"].Columns["opCnt"].Expression = "allLogon";
                    dsMontor.Tables["dtMonitor"].Rows[i]["opCnt"] = foundRows.Length - foundRows1.Length;

                    //受付可
                    DataRow[] foundRows2 = tblTemp.Select(" status=1");
                    dsMontor.Tables["dtMonitor"].Rows[i]["waitCnt"] = foundRows2.Length;

                    //added by zhu 2014/03/25
                    SetIdleSound(int.Parse(curGroupID), foundRows2.Length);
                    //end added

                    //}
                }

                //総合計
                //
                if (DtMonitorRowsCount > 0)
                {
                    //added by Zhu 2014/04/01
                    SetDvTotalRow("AGENT", false);
                    //end added

                }
                //added by Zhu 2014/04/10 
                //dsMontor.Tables["dtMonitor"].EndLoadData();
                //end added
            }
            catch (Exception ex)
            {
                writeLog("listMonitorShow SysteError:" + ex.Message + ex.StackTrace);
            }
        }
        void webGetCall_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            //throw new System.NotImplementedException();
            try
            {



                if (string.IsNullOrEmpty(webGetCall.DocumentText)) return;
                string strData = webGetCall.DocumentText;


                //System.Threading.Thread getCall = new System.Threading.Thread(new System.Threading.ThreadStart(getCallData));
                System.Threading.Thread getCall = new System.Threading.Thread(delegate () { getCallData(strData); });
                getCall.IsBackground = true;
                getCall.Start();


            }
            catch (Exception ex)
            {
                writeLog("wbGroupPersonal_DocumentCompleted SysteError:" + ex.Message + ex.StackTrace);
            }

        }
        private void btnInitMonitor_Click(object sender, EventArgs e)
        {
            int err = 0;
            try
            {

                string iSessionID = getCallInfoMinID();
                if (string.IsNullOrEmpty(iSessionID))
                    return;

                if (iSessionID == "0")
                    return;

                //added by zhu 2014
                iSessionID = (int.Parse(iSessionID) + 4000).ToString();
                writeLog("btnInitMonitor_Click start sessionprofileID:" + iSessionID);
                //end added
                this.btnInitMonitor.Enabled = false;

                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                if (Reg == null)
                {
                    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                    if (Reg == null) return;
                }

                // changed by zhu 2015/11/30 use url from ini file
                //string webServer = Reg.GetValue("WebServer", "").ToString();
                string webServer = GetServerUrl();
                //end changed
                string tid = Reg.GetValue("TenantID", "").ToString();
                string tpwd = Reg.GetValue("TenantPass", "").ToString();
                Reg.Close();
                webGetCall.AllowWebBrowserDrop = false;
                webGetCall.IsWebBrowserContextMenuEnabled = false;
                webGetCall.WebBrowserShortcutsEnabled = false;

                string postdata = "";
                string mainUrl = webServer + "getCallDataForMonitor.asp";
                //added by zhu 2014/04/07
                mainUrl = mainUrl + "?u =" + tid;
                mainUrl = mainUrl + "&p=" + tpwd;
                mainUrl = mainUrl + "&iSess=" + iSessionID;
                //end added
                postdata = "u =" + tid;
                postdata = postdata + "&p=" + tpwd;
                postdata = postdata + "&iSess=" + iSessionID;
                postdata = postdata + "&l=" + MonitorGroupList;


                System.Text.Encoding a = System.Text.Encoding.UTF8;
                Byte[] byte1 = a.GetBytes(postdata);

                //writeLog(mainUrl);
                webGetCall.Navigate(mainUrl, "", byte1, "Content-Type: application/x-www-form-urlencoded");

                //System.Threading.Thread getCall = new System.Threading.Thread(new System.Threading.ThreadStart(getCallData));
                //System.Threading.Thread getCall = new System.Threading.Thread(delegate() { getCallData(iSessionID); });
                //getCall.IsBackground = true;
                //getCall.Start();

            }
            catch (Exception ex)
            {
                writeLog("btnInitMonitor_Click SysteError:" + ex.Message + ex.StackTrace);
            }


        }
        private delegate void dd();
        private void getCallData(string minSessionID)
        {
            int err = 0;
            //minSessionID = "12051327";
            try
            {
                //Microsoft.Win32.RegistryKey Reg;
                //Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                //if (Reg == null)
                //{
                //    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                //    if (Reg == null) return;
                //}

                //string webServer = Reg.GetValue("WebServer", "").ToString();
                //string tid = Reg.GetValue("TenantID", "").ToString();
                //string tpwd = Reg.GetValue("TenantPass", "").ToString();
                //Reg.Close();

                ////System.Console.WriteLine(minSessionID);
                ////string url = webServer + "getCallDataForMonitor.asp?u=05058245171&p=TNdR265M&iSess=" + minSessionID;
                //string url = webServer + "getCallDataForMonitor.asp";
                //System.Net.WebRequest ObjWebRequest = System.Net.WebRequest.Create(url);
                //err = 1;
                //ObjWebRequest.Method = "POST";
                //ObjWebRequest.ContentType = "application/x-www-form-urlencoded";
                ////Post ObjWebRequest.ContentType="text/xml";//SOAP 
                //System.IO.Stream dataStream = ObjWebRequest.GetRequestStream();
                //System.Threading.Thread.Sleep(20);
                //err = 2;

                //string postdata = "u =" + tid;
                //postdata = postdata + "&p=" + tpwd;
                //postdata = postdata + "&iSess=" + minSessionID;
                //System.Text.Encoding a = System.Text.Encoding.UTF8;
                //Byte[] byte1 = a.GetBytes(postdata);


                //dataStream.Write(byte1, 0, byte1.Length);
                //dataStream.Close();
                //err = 3;
                //WebResponse Objresponse = ObjWebRequest.GetResponse();

                //System.Threading.Thread.Sleep(20);

                //if (((HttpWebResponse)Objresponse).StatusDescription != "OK")
                //{
                //    return;
                //}
                //err = 4;
                //System.IO.Stream data = Objresponse.GetResponseStream();
                //err = 5;
                //System.IO.StreamReader r = new System.IO.StreamReader(data);
                ////Response.Write(HttpUtility.HtmlEncode(r.ReadToEnd()));
                //err = 6;
                ////System.Console.WriteLine(r.ReadToEnd());
                //string strCalldata = r.ReadToEnd();

                string strCalldata = minSessionID;
                if (string.IsNullOrEmpty(strCalldata)) return;
                string[] rows = strCalldata.Split(';');

                Database db = new Database();
                Database dbReal = new Database();
                string iSessionProfileID = "";
                string iSkillID = "";
                string iSkillGroupID = "";
                string dtStatus = "";
                string dtAcdcall = "";
                string iStatus = "";
                bool hasTrans = false;
                System.Data.OleDb.OleDbTransaction trans = null;
                System.Data.OleDb.OleDbCommand command = null; ;
                try
                {
                    if (!db.openDB1(Application.StartupPath, "")) return;
                    if (!dbReal.openDB(Application.StartupPath, "")) return;
                    trans = db.connection.BeginTransaction();

                    hasTrans = true;
                    string sql = "";
                    sql = "DELETE FROM callInfoPre";
                    //db.excuteSql(sql);
                    command = new System.Data.OleDb.OleDbCommand();
                    command.Connection = db.connection;
                    command.CommandText = sql;
                    command.Transaction = trans;

                    command.ExecuteNonQuery();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(rows[i]))
                        {
                            System.Threading.Thread.Sleep(5);
                            string[] cols = rows[i].Split(',');
                            if (cols.Length == 6)
                            {
                                iSessionProfileID = cols[0];
                                //added by zhu 2014/04/14
                                if (CheckCallExist(dbReal, iSessionProfileID, cols[2]))
                                    continue;
                                //end added
                                iSkillID = cols[1];
                                iSkillGroupID = cols[2];
                                dtStatus = cols[3];
                                dtAcdcall = cols[4];
                                iStatus = cols[5];
                                sql = "INSERT INTO callInfoPre(iSessionProfileID,iSkillID,vSkillName";
                                sql = sql + ",iSkillGroupID,dtStatus,dtAcdcall,iStatus)";

                                sql = sql + "VALUES('" + iSessionProfileID + "'";
                                sql = sql + ",'" + iSkillID + "'";
                                sql = sql + ",''";
                                sql = sql + ",'" + iSkillGroupID + "'";
                                if (string.IsNullOrEmpty(dtStatus))
                                    sql = sql + ",null";
                                else
                                    sql = sql + ",'" + dtStatus + "'";
                                if (string.IsNullOrEmpty(dtAcdcall))
                                    sql = sql + ",null";
                                else
                                    sql = sql + ",'" + dtAcdcall + "'";
                                sql = sql + ",'" + iStatus + "'";
                                sql = sql + ")";
                                //db.excuteSql(sql);

                                command.CommandText = sql;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    //db.connection.co
                    trans.Commit();

                }
                catch (Exception ex1)
                {
                    writeLog("getCallData insert:" + ex1.Message);
                    if (hasTrans == true)
                        trans.Rollback();
                }
                db.closeDB();
                err = 7;



            }
            catch (Exception ex)
            {
                err = 8;
                writeLog("getCallData SysteError:" + ex.Message + ex.StackTrace);

            }
            err = 9;

            this.Invoke(new dd(() =>
            {
                this.btnInitMonitor.Enabled = true;
                retTimer.Enabled = true;
            }));

            //dd = delegate()
            //{
            //    this.btnInitMonitor.Enabled = true;
            //};
            //this.btnInitMonitor.Invoke(dd);


        }
        //added by Zhu 2014/04/14
        private bool CheckCallExist(Database db, string isessionProfileID, string iGroupID)
        {
            try
            {
                //Database db = new Database();
                //if (!db.openDB(Application.StartupPath, "")) return false;
                string sql = "Select top 1 iSessionProfileID from callInfo where iSessionProfileID = " + isessionProfileID + " and iSkillGroupID = '" + iGroupID + "'";
                ArrayList rs = new ArrayList();
                if (db.readDB(sql, rs))
                {
                    if (rs.Count > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                writeLog("CheckCallExist SysteError:" + ex.Message + ex.StackTrace);
                return false;
            }
        }
        //end added

        private void retTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                retTimer.Enabled = false;
                resetMonitorCall();
            }
            catch (Exception ex)
            {
                writeLog("retTimer_Tick Error:" + ex.Message);
            }
        }

        void dvMonitor_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //throw new System.NotImplementedException();
            try
            {

                if (dvMonitor.SelectedRows.Count < 1) return;
                if (dvMonitor.RowCount < 1 || dvMonitor.SelectedRows[0].Index > dvMonitor.RowCount - 1)
                    return;
                int curRow = dvMonitor.SelectedRows[0].Index;
                //string curGroup = dsMontor.Tables["dtMonitor"].Rows[curRow]["groupId"].ToString();
                //string curGroupName = dsMontor.Tables["dtMonitor"].Rows[curRow][0].ToString();

                string curGroup = dvMonitor[_MonitorItemManager.MonitorItems.Count, curRow].Value.ToString(); //updated by zhu 2014/06/02 change index from 13 to 14
                string curGroupName = dvMonitor[0, curRow].Value.ToString();

                //writeLog(curGroup);
                MonitorDetail md = new MonitorDetail();
                md.mainF = this;
                md.iGroup = curGroup;
                md.sGroupName = curGroupName;
                md.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("dvMonitor_CellDoubleClick Error:" + ex.Message);
            }
        }



        //deleted by Zhu 2014/05/12
        //public void setMonitorCol(string strCol1, string strCol2, string strCol3, string strCol4, string strCol5, string strCol6
        //                         , string strCol7, string strCol8, string strCol9, string strCol10, string strCol11, string strCol12, string strCol13)
        //{
        //    try
        //    {

        //        MonitorCol1 = strCol1;
        //        MonitorCol2 = strCol2;
        //        MonitorCol3 = strCol3;
        //        MonitorCol4 = strCol4;
        //        MonitorCol5 = strCol5;
        //        MonitorCol6 = strCol6;
        //        MonitorCol7 = strCol7;
        //        MonitorCol8 = strCol8;
        //        MonitorCol9 = strCol9;
        //        MonitorCol10 = strCol10;
        //        MonitorCol11 = strCol11;
        //        MonitorCol12 = strCol12;
        //        MonitorCol13 = strCol13;



        //        setMonitorTitle(strCol1, strCol2, strCol3, strCol4, strCol5, strCol6
        //                          , strCol7, strCol8, strCol9, strCol10, strCol11, strCol12, strCol13);

        //        iniProfile.SelectSection("SVSet");
        //        iniProfile.SetString("MonitorCol1", MonitorCol1);
        //        iniProfile.SetString("MonitorCol2", MonitorCol2);
        //        iniProfile.SetString("MonitorCol3", MonitorCol3);
        //        iniProfile.SetString("MonitorCol4", MonitorCol4);
        //        iniProfile.SetString("MonitorCol5", MonitorCol5);
        //        iniProfile.SetString("MonitorCol6", MonitorCol6);
        //        iniProfile.SetString("MonitorCol7", MonitorCol7);
        //        iniProfile.SetString("MonitorCol8", MonitorCol8);
        //        iniProfile.SetString("MonitorCol9", MonitorCol9);
        //        iniProfile.SetString("MonitorCol10", MonitorCol10);
        //        iniProfile.SetString("MonitorCol11", MonitorCol11);
        //        iniProfile.SetString("MonitorCol12", MonitorCol12);
        //        iniProfile.SetString("MonitorCol13", MonitorCol13);

        //        iniProfile.Save(MyTool.GetModuleIniPath());


        //    }
        //    catch (Exception ex)
        //    {
        //        writeLog("setOptionName:" + ex.Message);
        //    }
        //}

        //end added
        private void setMonitorTitle(string strCol1, string strCol2, string strCol3, string strCol4, string strCol5, string strCol6
                                 , string strCol7, string strCol8, string strCol9, string strCol10, string strCol11, string strCol12, string strCol13)
        {
            try
            {
                dvMonitor.Columns[0].HeaderText = strCol1;
                dvMonitor.Columns[1].HeaderText = strCol2;
                dvMonitor.Columns[2].HeaderText = strCol3;
                dvMonitor.Columns[3].HeaderText = strCol4;
                dvMonitor.Columns[4].HeaderText = strCol5;
                dvMonitor.Columns[5].HeaderText = strCol6;
                dvMonitor.Columns[6].HeaderText = strCol7;
                dvMonitor.Columns[7].HeaderText = strCol8;
                dvMonitor.Columns[8].HeaderText = strCol9;
                dvMonitor.Columns[9].HeaderText = strCol10;
                dvMonitor.Columns[10].HeaderText = strCol11;
                dvMonitor.Columns[11].HeaderText = strCol12;
                dvMonitor.Columns[12].HeaderText = strCol13;

            }
            catch (Exception ex)
            {
                writeLog("setMonitorTitle:" + ex.Message);
            }
        }

        private void AgentTimer_Tick(object sender, EventArgs e)
        {

        }

        private void CallTimer_Tick(object sender, EventArgs e)
        {

        }



        public void SetQueCall(string iPeriod1, string iPeriodVoice1, string iPeriod2, string iPeriodVoice2, string iPeriod3, string iPeriodVoice3)
        {
            try
            {
                QuePeriod1 = iPeriod1;
                QuePeriodVoice1 = iPeriodVoice1;

                QuePeriod2 = iPeriod2;
                QuePeriodVoice2 = iPeriodVoice2;

                QuePeriod3 = iPeriod3;
                QuePeriodVoice3 = iPeriodVoice3;

                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("QuePeriod1", QuePeriod1);
                IniProfile.SetString("QuePeriodVoice1", QuePeriodVoice1);
                IniProfile.SetString("QuePeriod2", QuePeriod2);
                IniProfile.SetString("QuePeriodVoice2", QuePeriodVoice2);
                IniProfile.SetString("QuePeriod3", QuePeriod3);
                IniProfile.SetString("QuePeriodVoice3", QuePeriodVoice3);


                IniProfile.Save(MyTool.GetModuleIniPath());


            }
            catch (Exception ex)
            {
                writeLog("SetQueCall Error:" + ex.StackTrace);
            }

        }


        //added by Zhu 2014/03/19


        public void SetIdle(string iPeriod1, string iPeriodVoice1)
        {
            try
            {
                IdlePeriodLongString = iPeriod1;
                IdlePeriodVoiceLongString = iPeriodVoice1;

                IniProfile.SelectSection("SVSet");
                IniProfile.SetString("SkillIdlePeriod", IdlePeriodLongString);
                IniProfile.SetString("SkillIdlePeriodVoice", IdlePeriodVoiceLongString);

                IniProfile.Save(MyTool.GetModuleIniPath());


            }
            catch (Exception ex)
            {
                writeLog("SetIdle Error:" + ex.StackTrace);
            }

        }


        private void SetIdleSound(int groupID, int idleCount)
        {
            try
            {
                int iIdlePeriod1;
                string idleVoice;
                if (string.IsNullOrEmpty(IdlePeriodLongString) || !IdlePeriodLongString.Contains(groupID.ToString()))
                {
                    //modified by zhu 2015/07/02 remove playsound
                    if (SkillSoundPlayerManager.Players.ContainsKey(groupID))
                    {
                        var player = SkillSoundPlayerManager.Players[groupID];
                        if (player != null && !string.IsNullOrEmpty(player.URL))
                        {
                            player.URL = "";
                            player.controls.stop();
                        }
                    }

                    //PlaySound(null, new IntPtr(), playSound.SND_ASYNC);
                    //end modified
                    return;
                }
                string value = GetValueByGroupID(IdlePeriodLongString, groupID);

                if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d*$"))
                {
                    return;
                }
                iIdlePeriod1 = int.Parse(value);



                string path = "";
                path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = path + "\\Comdesign\\Voice";

                idleVoice = GetValueByGroupID(IdlePeriodVoiceLongString, groupID);
                if (idleCount <= iIdlePeriod1)
                {
                    if (!string.IsNullOrEmpty(idleVoice) && SkillSoundPlayerManager.Players.Count > 0)
                    {
                        var player = SkillSoundPlayerManager.Players[groupID];
                        if (player != null && string.IsNullOrEmpty(player.URL))
                        {
                            player.URL = path + @"\" + idleVoice;
                        }
                    }
                    //SkillSoundPlayerManager.AudioFiles.Add(path + @"\" + idleVoice);
                    //PlaySound(path + @"\" + idleVoice);
                }
                else
                {
                    //PlaySound(null, new IntPtr(), playSound.SND_ASYNC);
                    //SkillSoundPlayerManager.AudioFiles.Remove(path + @"\" + idleVoice);
                    if (SkillSoundPlayerManager.Players.Count > 0)
                    {
                        var player = SkillSoundPlayerManager.Players[groupID];
                        if (player != null && !string.IsNullOrEmpty(player.URL))
                        {
                            player.URL = "";
                            player.controls.stop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //alert(e.description);
                writeLog("PlaySound errMsg:" + ex.Message + "\r\nTrace:" + ex.StackTrace);
            }
        }

        private void PlayIdleSound()
        {
            // SkillSoundPlayerManager.PlaySound();
        }

        private string GetValueByGroupID(string inputvalue, int groupID)
        {
            string result = "";
            int start = inputvalue.IndexOf(groupID.ToString());
            if (start >= 0)
            {
                int end = inputvalue.IndexOf('|', start);
                if (end < 0) end = inputvalue.Length;
                result = inputvalue.Substring(start, end - start).Split(':')[1];
            }
            return result;
        }



        private bool GetCheckedStatus(string groupID)
        {
            try
            {
                if (string.IsNullOrEmpty(SkillShowSetString)) return true;
                int start = SkillShowSetString.IndexOf(groupID);
                if (start >= 0)
                {
                    int end = SkillShowSetString.IndexOf('|', start);
                    if (end <= 0) end = SkillShowSetString.Length;
                    string result = SkillShowSetString.Substring(start, end - start).Split(':')[1];
                    if (result == "1") return true;
                    else if (result == "0") return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                writeLog("GetCheckedStatus Error:" + ex.StackTrace);
                return false;
            }
        }


        private void MenuMonitorItemShow_Click(object sender, EventArgs e)
        {
            try
            {
                MonitorItemSet mis = new MonitorItemSet(_MonitorItemManager);
                if (mis.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //deleted by zhu 2014/05/12
                    //MonitorItemShowString = mis._keyvalue;
                    //SetDvMonitorColumnShow();
                    //end deleted
                }
            }
            catch (Exception ex)
            {
                writeLog("MenuMonitorItemShow_Click Error:" + ex.StackTrace);
            }
        }

        //deleted by zhu 2014/05/12
        //private void SetDvMonitorColumnShow()
        //{
        //    if (string.IsNullOrEmpty(MonitorItemShowString)) return;
        //    string[] arr = MonitorItemShowString.Split(',');
        //    for (int i = 1; i <= arr.Length; i++)
        //    {
        //        dvMonitor.Columns[i].Visible = arr[i] == "0" ? false : true;
        //    }
        //}
        //end deleted

        //総合計
        private void SetDvTotalRow(string command, bool threadFlag)
        {
            try
            {
                string showSkillGroupIds = _ShowSkillGroupIDs;
                string strWhere = "";
                if (string.IsNullOrEmpty(_ShowSkillGroupIDs))
                    strWhere = "groupId<>'-1'";
                else
                    strWhere = "groupId in (" + showSkillGroupIds + ")";

                if (command == "AGENT")
                {
                    //lock (dsMontor.Tables["dtGroupPersonal"])
                    //{
                    DataView viewTemp = new DataView();
                    viewTemp.Table = dsMontor.Tables["dtGroupPersonal"];
                    viewTemp.RowFilter = strWhere;

                    DataTable tblTemp = viewTemp.ToTable(true, "agentID", "status");


                    DataRow[] foundRows00 = tblTemp.Select("status>0");
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["allLogon"] = foundRows00.Length;

                    DataRow[] foundRows01 = tblTemp.Select("status=6");
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["seatLeaveCnt"] = foundRows01.Length;

                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["opCnt"] = foundRows00.Length - foundRows01.Length;
                    DataRow[] foundRows02 = tblTemp.Select("status=1");
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["waitCnt"] = foundRows02.Length;
                    //}
                }
                else if (command == "CALLLEG")
                {
                    int acdCount = 0;
                    int answerCnt = 0;
                    int answerNowCnt = 0;
                    int failCnt = 0;

                    //added by zhu 2014/06/26
                    //DataRow[] foundRows = dsMontor.Tables["dtMonitor"].Select(strWhere);
                    DataTable tempTable = dsMontor.Tables["dtMonitor"].Copy();

                    DataRow[] foundRows = tempTable.Select(strWhere);
                    //end added
                    for (int j = 0; j < foundRows.Length; j++)
                    {
                        if (threadFlag)
                            System.Threading.Thread.Sleep(10);
                        if (!string.IsNullOrEmpty(foundRows[j]["acdCnt"].ToString()))
                            acdCount = acdCount + int.Parse(foundRows[j]["acdCnt"].ToString());
                        if (!string.IsNullOrEmpty(foundRows[j]["answerCnt"].ToString()))
                            answerCnt = answerCnt + int.Parse(foundRows[j]["answerCnt"].ToString());
                        if (!string.IsNullOrEmpty(foundRows[j]["answerNowCnt"].ToString()))
                            answerNowCnt = answerNowCnt + int.Parse(foundRows[j]["answerNowCnt"].ToString());
                        //failCnt = failCnt + 0;
                    }


                    failCnt = acdCount - answerCnt;
                    if (failCnt < 1) failCnt = 0;
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["acdCnt"] = acdCount;
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerCnt"] = answerCnt;
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerNowCnt"] = answerNowCnt;

                    if (answerCnt > 0)
                    {
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerNowPer"] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
                    }
                    else
                    {
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerNowPer"] = "0.0%";
                    }
                    if (acdCount > 0)
                    {
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerPer"] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";

                        failCnt = acdCount - answerCnt;
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["failCnt"] = failCnt;
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["failPer"] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
                    }
                    else
                    {
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["answerPer"] = "0.0%";
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["failCnt"] = 0;
                        dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["failPer"] = "0.0%";
                    }

                }
                else if (command == "QUECALL")
                {
                    int queCount = 0;
                    //modified by zhu 2014/06/26
                    DataTable tempTable = dsMontor.Tables["dtMonitor"].Copy();
                    //lock (tempTable)
                    //{
                    DataRow[] foundRows = tempTable.Select(strWhere + " AND queCallCnt>0");
                    // DataRow[] foundRows = tempTable.Select(strWhere + " AND queCallCnt>0");
                    //end modified

                    for (int j = 0; j < foundRows.Length; j++)
                    {
                        queCount = queCount + int.Parse(foundRows[j]["queCallCnt"].ToString());

                    }
                    dsMontor.Tables["dtMonitor"].Rows[DtMonitorRowsCount - 1]["queCallCnt"] = queCount;
                    //}
                }

            }
            catch (Exception ex)
            {
                writeLog("SetDvTotalRow SysteError: command is " + command + " errMsg:" + ex.Message + ex.StackTrace);
            }
            //end added

        }

        private bool firstBindMonitor = true;
        private void MonitorTimer_Tick(object sender, EventArgs e)
        {
            _MonitorDataTable = dsMontor.Tables["dtMonitor"].Copy();
            //if (_MonitorDataTable.Rows.Count > 0 && firstBindMonitor)
            //    this.dvMonitor.DataSource = _MonitorDataTable;
            //firstBindMonitor = false;
            int r = dvMonitor.FirstDisplayedScrollingRowIndex;
            dvBindingSource.DataSource = _MonitorDataTable;
            this.dvMonitor.FirstDisplayedScrollingRowIndex = r;
        }

        private void dvMonitor_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            writeLog("dvMonitor_DataError: rowIndex is " + e.RowIndex.ToString() + ";columnIndex is" + e.ColumnIndex.ToString() + e.Exception.Message.ToString());
        }

        //added by zhu 2014/09/01       
        private void ContinueTimer(object source, System.Timers.ElapsedEventArgs e)
        {
            //sw.Stop();
            //System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds.ToString());
            this.Invoke(new dd(() =>
            {
                updateContinue();
            }));

            // sw.Start();

        }

        private void MenuLineCutItem_Click(object sender, EventArgs e)
        {
            frmLineCutSet frm = new frmLineCutSet(IniProfile);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SettingFields_LineCutShow = frm._keyvalue;
            }
        }

        #region Main Form
        public void SendEvent(string sEvent, CpfParams cpfParam)
        {
            axCpfMsg.SendEvent("", "", sEvent, cpfParam);
        }

        private void subMenuOtherSetting_Click(object sender, EventArgs e)
        {
            OtherSettingForm frm = new OtherSettingForm(IniProfile);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SettingFields_MonitorTabShow = frm.MonitorTab;
                SettingFields_MessagePop = frm.MessagePop;
                SettingFields_AgentGraphShow = frm.AgentGraphShow;
                SettingFields_ListFontSize = frm.ListFontSize;
                if (SettingFields_MonitorTabShow == "0")
                {
                    if (statusTabCtrl.TabPages.Contains(tabMonitor))
                        statusTabCtrl.TabPages.Remove(tabMonitor);
                }

                else
                {
                    if (!statusTabCtrl.TabPages.Contains(tabMonitor))
                        statusTabCtrl.TabPages.Insert(2, tabMonitor);
                }

                if (SettingFields_AgentGraphShow == "1")
                {
                    this.agentPie.Visible = true;
                    this.agentIconListView.Width = this.AgentIconListWeight;
                    this.agentIconListView.Height = this.AgentIconListHeight;
                    this.agentPie.Width = this.AgentPieWeight;
                    this.agentPie.Height = this.AgentPieHeigh;
                    this.agentPie.Location = new Point(this.agentPie.Location.X, this.agentIconListView.Location.Y);
                }
                else
                {
                    this.agentPie.Visible = false;
                    //this.agentIconListView.Height = 60;
                    this.agentIconListView.Dock = DockStyle.Bottom;
                }

                if (frm.FontSizeChanged)
                {
                    AjustListFontSize();
                }

            }
        }

        public string GetServerUrl()
        {
            if (string.IsNullOrEmpty(SettingFields_WebServer))
            {
                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                if (Reg == null)
                {
                    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                    if (Reg == null) return "";
                }

                SettingFields_WebServer = Reg.GetValue("WebServer", "").ToString();
            }
            return SettingFields_WebServer;
        }

        private void InitAgentListView()
        {
            // Init agentStatusListView
            agentStatusListView.View = View.Details;
            //agentStatusListView.TileSize = new Size(130, 90);
            agentStatusListView.TileSize = new Size(130, 130);
            agentStatusListView.MultiSelect = false;
            agentStatusListView.FullRowSelect = true;
            //agentStatusListView.Columns.Add("GroupName", res.GetString("SM0020019"), 100, HorizontalAlignment.Left, -1);
            agentStatusListView.Columns.Add("AgentName", res.GetString("SM0020020"), 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Option1", "Option1", 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Option2", "Option2", 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Option3", "Option3", 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Option4", "Option4", 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Option5", "Option5", 100, HorizontalAlignment.Center, -1);

            //agentStatusListView.Columns.Add("AgentName", res.GetString("SM0020020"), 100, HorizontalAlignment.Center, -1);
            //added by zhu 2014/04/17
            agentStatusListView.Columns.Add("Extension", "内線番号", 100, HorizontalAlignment.Center, -1);
            //added end
            agentStatusListView.Columns.Add("GroupName", res.GetString("SM0020019"), 100, HorizontalAlignment.Left, -1);
            agentStatusListView.Columns.Add("Status", res.GetString("SM0020021"), 70, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("StatusTime", res.GetString("SM0020022"), 60, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("StatusContinueTime", res.GetString("SM0020061"), 60, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Conntype", "発着信", 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Caller", res.GetString("SM0020023"), 100, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("Help", res.GetString("SM0020024"), 50, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("LoginTime", res.GetString("SM0020025"), 80, HorizontalAlignment.Center, -1);
            agentStatusListView.Columns.Add("MyMsg", "コメント", 80, HorizontalAlignment.Center, -1);

            // Init ItemSorter
            agentStatusListView.ListViewItemSorter = new StatusListViewItemComparer(0, true);
            //add,xzg,2013/08/27,S
            setDoubleBuffered(agentStatusListView, true);
            //add,xzg,2013/08/27,E
            AgentStatusListViewOrder = new bool[agentStatusListView.Columns.Count];

            foreach (ColumnHeader col in agentStatusListView.Columns)
            {
                DicOriginAgentListViewColumnWidth.Add(col.Name, col.Width);
            }
        }

        private void InitLineListView()
        {
            // Init lineStatusListView
            lineStatusListView.View = View.Details;
            lineStatusListView.TileSize = new Size(130, 110);
            lineStatusListView.MultiSelect = false;
            lineStatusListView.FullRowSelect = true;
            //modified by zhu 2015/06/08 スキルグループ=>局番グループ
            //lineStatusListView.Columns.Add("GroupName", res.GetString("SM0020026"), 100, HorizontalAlignment.Left, -1);
            lineStatusListView.Columns.Add("GroupName", "局番グループ", 100, HorizontalAlignment.Left, -1);
            //end modified
            lineStatusListView.Columns.Add("Conntype", "発着信", 100, HorizontalAlignment.Left, -1);
            lineStatusListView.Columns.Add("Caller", res.GetString("SM0020027"), 100, HorizontalAlignment.Center, -1);
            lineStatusListView.Columns.Add("Callee", res.GetString("SM0020028"), 100, HorizontalAlignment.Center, -1);
            lineStatusListView.Columns.Add("ConnectedTime", res.GetString("SM0020029"), 80, HorizontalAlignment.Center, -1);
            lineStatusListView.Columns.Add("Status", res.GetString("SM0020030"), 60, HorizontalAlignment.Center, -1);
            lineStatusListView.Columns.Add("StatusTime", res.GetString("SM0020031"), 80, HorizontalAlignment.Center, -1);
            //add,xzg,2012/02/09,S            
            lineStatusListView.Columns.Add("StatusContinueTime", res.GetString("SM0020061"), 80, HorizontalAlignment.Center, -1);
            //add,xzg,2012/02/09,E
            lineStatusListView.Columns.Add("Service", res.GetString("SM0020032"), 100, HorizontalAlignment.Center, -1);


            lineStatusListViewOrder = new bool[lineStatusListView.Columns.Count];
            // Init ItemSorter
            lineStatusListView.ListViewItemSorter = new StatusListViewItemComparer(0, true);
            //add,xzg,2013/08/27,S
            setDoubleBuffered(lineStatusListView, true);
            //add,xzg,2013/08/27,E

            foreach (ColumnHeader col in lineStatusListView.Columns)
            {
                DicOriginLineListViewColumnWidth.Add(col.Name, col.Width);
            }
        }
        private void InitTotalListView()
        {
            // Init totalListView
            totalListView.View = View.Details;
            totalListView.MultiSelect = false;
            totalListView.FullRowSelect = true;
            totalListView.Columns.Add("GroupName", res.GetString("SM0020033"), 100, HorizontalAlignment.Left, -1);
            totalListView.Columns.Add("WaitCount", res.GetString("SM0020034"), 60, HorizontalAlignment.Center, -1);
            totalListView.Columns.Add("ConnectCount", res.GetString("SM0020035"), 60, HorizontalAlignment.Center, -1);
            totalListView.Columns.Add("OtherCount", res.GetString("SM0020036"), 60, HorizontalAlignment.Center, -1);
            totalListView.Columns.Add("QueueCount", res.GetString("SM0020037"), 60, HorizontalAlignment.Center, -1);
            totalListView.Columns.Add("MaxQuecallContinueTime", res.GetString("SM0020061"), 60, HorizontalAlignment.Center, -1);
            //add,xzg,2013/08/27,S
            setDoubleBuffered(totalListView, true);
            //add,xzg,2013/08/27,E
            TotalListViewOrder = new bool[totalListView.Columns.Count];
            // Init ItemSorter
            totalListView.ListViewItemSorter = new TotalListViewItemComparer(0, true);

            foreach (ColumnHeader col in totalListView.Columns)
            {
                DicOriginTotalListViewColumnWidth.Add(col.Name, col.Width);
            }
        }
        /// <summary>
        /// 局番グループだけを取る
        /// </summary>
        public void GetGroupInfo()
        {
            try
            {
                Microsoft.Win32.RegistryKey Reg;
                Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
                if (Reg == null)
                {
                    Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
                    if (Reg == null) return;
                }

                // changed by zhu 2015/11/30 use url from ini file
                //string webServer = Reg.GetValue("WebServer", "").ToString();
                string webServer = GetServerUrl();
                //end changed
                string tid = Reg.GetValue("TenantID", "").ToString();
                string tpwd = Reg.GetValue("TenantPass", "").ToString();
                Reg.Close();
                webGetGroup.AllowWebBrowserDrop = false;
                webGetGroup.IsWebBrowserContextMenuEnabled = false;
                webGetGroup.WebBrowserShortcutsEnabled = false;

                string postdata = "";
                string mainUrl = webServer + "getAllGroup.asp";
                //added by Zhu 2014/04/07
                mainUrl = mainUrl + "?u=" + tid;
                mainUrl = mainUrl + "&p=" + tpwd;
                //end added
                postdata = "u =" + tid;
                postdata = postdata + "&p=" + tpwd;
                //postdata = postdata + "&l=" + MonitorGroupList;

                System.Text.Encoding a = System.Text.Encoding.UTF8;
                Byte[] byte1 = a.GetBytes(postdata);

                writeLog("mainUrl:" + mainUrl + ",postdata:" + postdata);
                webGetGroup.Navigate(mainUrl, "", byte1, "Content-Type: application/x-www-form-urlencoded");

            }
            catch (Exception ex)
            {
                writeLog("webGetGroup SysteError:" + ex.Message + ex.StackTrace);
            }
        }

        private void webGetGroup_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                writeLog("webGetGroup_DocumentCompleted");
                if (string.IsNullOrEmpty(webGetGroup.DocumentText))
                {
                    writeLog("webGetGroup_DocumentCompleted and response is empty");
                    return;
                }
                KyokuGroup = webGetGroup.DocumentText;
                this.subMenuKyokuGroupSetting.Enabled = true;
            }
            catch (Exception ex)
            {
                writeLog("webGetGroup_DocumentCompleted SysteError:" + ex.Message + ex.StackTrace);
            }

        }

        #region subMenu click
        private void subMenuKyokuGroupSetting_Click(object sender, EventArgs e)
        {
            try
            {
                KyoKuGroupForm sss = new KyoKuGroupForm(KyokuGroup, IniProfile);
                if (sss.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SettingFields_KyoKuGroupShow = sss._keyvalue;
                }
            }
            catch (Exception ex)
            {
                writeLog("MenuSkillShowSet_Click Error:" + ex.StackTrace);
            }
        }

        private void MenuSkillShowSet_Click(object sender, EventArgs e)
        {
            try
            {
                SkillShowSet sss = new SkillShowSet(dsMontor.Tables["dtGroupPersonal"].DefaultView.ToTable(true, new string[] { "groupId", "groupName" }), IniProfile);
                sss.MainForm = this;
                if (sss.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SkillShowSetString = sss._keyvalue;
                    dsMontor.Tables["dtMonitor"].Rows.Clear();
                    setGroup();
                    listMonitorShow();
                    MessageBox.Show("状態モニタを再起動ください", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                writeLog("MenuSkillShowSet_Click Error:" + ex.StackTrace);
            }
        }

        private void subMenuSet_Click(object sender, EventArgs e)
        {
            frmSetPhone frmPhone = new frmSetPhone();
            frmPhone.mainF = this;
            frmPhone.ShowDialog();
        }
        private void subMenuReFresh_Click(object sender, EventArgs e)
        {
            AgentStatusMonitor.RefreshForm refreshFrom = new AgentStatusMonitor.RefreshForm();
            refreshFrom.mainF = this;
            refreshFrom.ShowDialog();
        }

        private void subMenuOverTimeSet_Click(object sender, EventArgs e)
        {
            StatusOverTimeSet statusOverTimeSetForm = new StatusOverTimeSet();
            statusOverTimeSetForm.mainF = this;
            statusOverTimeSetForm.ShowDialog();
        }

        //Add,xzg,2011/09/16,E

        private void menuGetLog_Click(object sender, EventArgs e)
        {
            try
            {
                string strFolder = "";
                string[] strSrcFile;
                string strDescFile = "";

                if (folderDia.ShowDialog() == DialogResult.OK)
                {
                    strFolder = folderDia.SelectedPath;
                }
                if (!string.IsNullOrEmpty(strFolder))
                {
                    Microsoft.VisualBasic.Logging.Log log = new Microsoft.VisualBasic.Logging.Log();

                    log.DefaultFileLogWriter.Location = Microsoft.VisualBasic.Logging.LogFileLocation.Custom;
                    //log.DefaultFileLogWriter.FullLogFileName; 
                    strSrcFile = System.IO.Directory.GetFiles(log.DefaultFileLogWriter.CustomLocation);

                    for (int i = 0; i < strSrcFile.Length; i++)
                    {
                        strDescFile = strFolder + "\\" + System.IO.Path.GetFileName(strSrcFile[i]);
                        System.IO.File.Copy(strSrcFile[i], strDescFile, true);
                    }

                }
            }
            catch (Exception ex)
            {
                writeLog("menuGetLog_Click:" + ex.Message);
            }
        }

        private void subMenuWaitTime_Click(object sender, EventArgs e)
        {
            try
            {
                WaiteTime frm = new WaiteTime();
                frm.mainF = this;
                frm.waitTimes = WaitTimes;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("subMenuWaitTime_Click:" + ex.Message);
            }
        }

        private void sumMenuCol_Click(object sender, EventArgs e)
        {
            try
            {
                ColSelect frmColSelect = new ColSelect();
                frmColSelect.ShowCol = ShowCol;
                frmColSelect.Option1 = OptionName1;
                frmColSelect.Option2 = OptionName2;
                frmColSelect.Option3 = OptionName3;
                frmColSelect.Option4 = OptionName4;
                frmColSelect.Option5 = OptionName5;
                frmColSelect.mainF = this;
                frmColSelect.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("sumMenuCol_Click:" + ex.Message);
            }
        }

        private void subMenuOption_Click(object sender, EventArgs e)
        {
            try
            {
                OptionName frmOptionName = new OptionName();
                frmOptionName.Option1 = OptionName1;
                frmOptionName.Option2 = OptionName2;
                frmOptionName.Option3 = OptionName3;
                frmOptionName.Option4 = OptionName4;
                frmOptionName.Option5 = OptionName5;
                frmOptionName.mainF = this;
                frmOptionName.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("subMenuOption_Click:" + ex.Message);
            }
        }

        private void sumMenuQuickAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                QuickAnswerSet frm = new QuickAnswerSet();
                frm.mainF = this;

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("sumMenuQuickAnswer_Click:" + ex.Message);
            }
        }

        private void menuMonitorTitle_Click(object sender, EventArgs e)
        {
            try
            {
                frmMonitorTitle frmMontor = new frmMonitorTitle(_MonitorItemManager);
                frmMontor.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("subMenuOption_Click:" + ex.Message);
            }
        }

        private void menuQueCall_Click(object sender, EventArgs e)
        {
            try
            {
                QueCallSet qc = new QueCallSet();
                qc.mainF = this;
                qc.Period1 = QuePeriod1;
                qc.Period2 = QuePeriod2;
                qc.Period3 = QuePeriod3;
                qc.PeriodVoice1 = QuePeriodVoice1;
                qc.PeriodVoice2 = QuePeriodVoice2;
                qc.PeriodVoice3 = QuePeriodVoice3;

                qc.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("menuQueCall_Click Error:" + ex.StackTrace);
            }
        }

        private void MenuIdle_Click(object sender, EventArgs e)
        {
            try
            {
                SkillIdleSet qc = new SkillIdleSet(dsMontor.Tables["dtGroupPersonal"].DefaultView.ToTable(true, new string[] { "groupId", "groupName" }));
                qc.mainF = this;
                qc.PeriodLongString = IdlePeriodLongString;
                qc.PeriodVoiceLongString = IdlePeriodVoiceLongString;
                qc.ShowDialog();
            }
            catch (Exception ex)
            {
                writeLog("MenuIdle_Click Error:" + ex.StackTrace);
            }
        }


        #endregion

        private void AjustListFontSize()
        {
            try
            {
                //return;
                float size = 9f;
                AjustAgentListSize();

                lineStatusListView.Font = new Font(this.lineStatusListView.Font.FontFamily, size * SettingFields_ListFontSize);
                foreach (ColumnHeader col in this.lineStatusListView.Columns)
                {
                    col.Width = DicOriginLineListViewColumnWidth[col.Name] * SettingFields_ListFontSize;
                }

                this.totalListView.Font = new Font(this.totalListView.Font.FontFamily, size * SettingFields_ListFontSize);
                foreach (ColumnHeader col in this.totalListView.Columns)
                {
                    col.Width = DicOriginTotalListViewColumnWidth[col.Name] * SettingFields_ListFontSize;
                }

                this.dvMonitor.Font = new Font(this.dvMonitor.Font.FontFamily, size * SettingFields_ListFontSize);
                foreach (DataGridViewColumn col in this.dvMonitor.Columns)
                {
                    col.Width = DicOriginMonitorGridColumnWidth[col.Name] * SettingFields_ListFontSize;
                }

                (this.ListTabPagesForms[0] as QueueCallForm).AjustListFontSize();
            }
            catch (Exception ex)
            {
                writeLog("AjustListFontSize system error:" + ex.Message.ToString()+ex.StackTrace);
            }

        }

        private void AjustAgentListSize()
        {
            float size = 9.0f;
            agentStatusListView.Font = new Font(this.agentStatusListView.Font.FontFamily, size * SettingFields_ListFontSize,this.agentStatusListView.Font.Style);
            foreach (ColumnHeader col in this.agentStatusListView.Columns)
            {
                col.Width = DicOriginAgentListViewColumnWidth[col.Name] * SettingFields_ListFontSize;
            }
        }
        #endregion

        #region Agent TAB
        private Image CreateAgentPie(int idle, int worktime, int connect, int hold, int offerring, int seatOff, int calling, int transfer)
        {
            try
            {
                int total = idle + worktime + connect + hold + offerring + seatOff + calling + transfer;
                if (total == 0) return null;
                //Font fontlegend = new Font("verdana", 9);
                //Font fonttitle = new Font("verdana", 10, FontStyle.Bold);
                int width = this.agentPie.Width;
                int bufferspace = 15;
                //int legendheight = fontlegend.Height * 10 + bufferspace; 
                //int titleheight = fonttitle.Height + bufferspace;
                //int height = width + legendheight + titleheight + bufferspace;
                int pieheight = width;

                int height = this.agentPie.Height;

                if (width / height >= 2)
                {
                    width = height * 2;
                }
                if (width >= this.agentPie.Width)
                {
                    width = this.agentPie.Width - 2;
                }
                Rectangle pierect = new Rectangle(0, 2, height, height - 5);

                //create bitmap
                Bitmap objbitmap = new Bitmap(width, height);
                Graphics objgraphics = Graphics.FromImage(objbitmap);
                objgraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                Pen borderPen = Pens.Black;
                //create background
                // objgraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

                //create background
                objgraphics.FillRectangle(new SolidBrush(System.Drawing.SystemColors.Control), pierect);

                // objgraphics.FillRectangle(new SolidBrush(System.Drawing.Color.Black), pierect);
                float currentdegree = 270.0f;

                //draw connect
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfConnectColor), pierect, currentdegree, (float)connect * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)connect * 360 / total);
                currentdegree += (float)connect * 360 / total;
                //if (currentdegree > 360.0) currentdegree = (float)360.0 - currentdegree;
                //draw offerring
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfOfferringColor), pierect, currentdegree, (float)offerring * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)offerring * 360 / total);
                currentdegree += (float)offerring * 360 / total;

                //draw calling
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfCallingColor), pierect, currentdegree, (float)calling * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)calling * 360 / total);
                currentdegree += (float)calling * 360 / total;

                //draw hold
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfHoldColor), pierect, currentdegree, (float)hold * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)hold * 360 / total);
                currentdegree += (float)hold * 360 / total;

                //draw transfer
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfTransferColor), pierect, currentdegree, (float)transfer * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)transfer * 360 / total);
                currentdegree += (float)transfer * 360 / total;

                //draw idle
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfIdleColor), pierect, currentdegree, (float)idle * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)idle * 360 / total);
                currentdegree += (float)idle * 360 / total;

                //draw worktime
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfWorktimeColor), pierect, currentdegree, (float)worktime * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)worktime * 360 / total);
                currentdegree += (float)worktime * 360 / total;

                //draw seatOff
                objgraphics.FillPie(new SolidBrush(CTe1Helper.AgentStatusOfSeatOffColor), pierect, currentdegree, (float)seatOff * 360 / total);
                objgraphics.DrawPie(borderPen, pierect, currentdegree, (float)seatOff * 360 / total);
                currentdegree += (float)seatOff * 360 / total;

                objgraphics.Dispose();
                return (Image)objbitmap;
            }
            catch (Exception ex)
            {
                writeLog("CreateAgentPie  system error:" + ex.Message + ex.StackTrace);
                return null;
            }
        }

        private void agentPie_VisibleChanged(object sender, EventArgs e)
        {
            if (agentPie.Visible)
            {
                agentPie.Image = CurrentAgentPie;
            }
        }

        public Color GetAgentListItemBackColor(string strContinueTime, int iStatus)
        {
            Color returnValue = Color.Empty;
            try
            {
                //idle
                if (iStatus == 1)
                {
                    if (SettingFields_StatusOverIdelTime1 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverIdelTime1))) >= 0)
                        {
                            returnValue = Color.LightYellow;
                        }

                    }
                    if (SettingFields_StatusOverIdelTime2 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverIdelTime2))) >= 0)
                        {
                            returnValue = Color.LightPink;
                        }
                    }
                    return returnValue;
                }

                //worktime
                if (iStatus == 5)
                {
                    if (SettingFields_StatusOverWorkTime1 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverWorkTime1))) >= 0)
                        {
                            returnValue = Color.LightYellow;
                        }
                    }
                    if (SettingFields_StatusOverWorkTime2 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverWorkTime2))) >= 0)
                        {
                            returnValue = Color.LightPink;
                        }
                    }
                    return returnValue;
                }

                //leave
                if (iStatus == 6)
                {
                    if (SettingFields_StatusOverLeaveTime1 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverLeaveTime1))) >= 0)
                        {
                            returnValue = Color.LightYellow; ;
                        }
                    }
                    if (SettingFields_StatusOverLeaveTime2 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverLeaveTime2))) >= 0)
                        {
                            returnValue = Color.LightPink;
                        }
                    }
                    return returnValue;
                }

                //talk
                if (iStatus == 10)
                {
                    if (SettingFields_StatusOverTalkTime1 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverTalkTime1))) >= 0)
                        {
                            returnValue = Color.LightYellow;
                        }
                    }
                    if (SettingFields_StatusOverTalkTime2 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverTalkTime2))) >= 0)
                        {
                            returnValue = Color.LightPink;
                        }
                    }
                    return returnValue;
                }

                //hold
                if (iStatus == 30)
                {
                    if (SettingFields_StatusOverHoldTime1 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverHoldTime1))) >= 0)
                        {
                            returnValue = Color.LightYellow;
                        }
                    }
                    if (SettingFields_StatusOverHoldTime2 != DefaultOverTime)
                    {
                        if (strContinueTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(SettingFields_StatusOverHoldTime2))) >= 0)
                        {
                            returnValue = Color.LightPink;
                        }
                    }
                    return returnValue;
                }
                return Color.Empty;
            }
            catch
            {
                return Color.Empty;
            }
        }
        #endregion

        #region TotalListView
        public void DoContinueTotalList()
        {
            try
            {
                this.totalListView.BeginUpdate();
                int listCount = totalListView.Items.Count;
                int i = 0;
                for (i = 0; i < listCount; i++)
                {
                    ListViewItem item = totalListView.Items[i];
                    if (item.SubItems["Queue"] != null && !string.IsNullOrEmpty(item.SubItems["Queue"].Text))
                    {
                        if (int.Parse(item.SubItems["Queue"].Text) > 0)
                        {
                            if (item.SubItems["QueCallStatusDateTime"].Text == "")
                                item.SubItems["MaxQueCallContinueTime"].Text = "";
                            else
                                item.SubItems["MaxQueCallContinueTime"].Text = UtilityHelper.GetContinueTime(DateTime.Parse(item.SubItems["QueCallStatusDateTime"].Text));
                        }
                    }
                }
                this.totalListView.EndUpdate();
            }
            catch (Exception ex)
            {
                writeLog("DoContinueTotalList  system error:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// create total list item
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        private ListViewItem LoadTotalListItem(TotalStatus total)
        {
            try
            {


                ListViewItem item = new ListViewItem();
                item.Text = total.GroupName;
                item.BackColor = Color.White;
                if (total.QueueCount > 0) item.BackColor = Color.Magenta;


                ListViewItem.ListViewSubItem subIdle = new ListViewItem.ListViewSubItem(item, total.WaitCount.ToString());
                subIdle.Name = "Idle";
                item.SubItems.Add(subIdle);

                ListViewItem.ListViewSubItem subConnect = new ListViewItem.ListViewSubItem(item, total.ConnectCount.ToString());
                subConnect.Name = "Connect";
                item.SubItems.Add(subConnect);

                ListViewItem.ListViewSubItem subOther = new ListViewItem.ListViewSubItem(item, total.OtherCount.ToString());
                subOther.Name = "Other";
                item.SubItems.Add(subOther);

                ListViewItem.ListViewSubItem subQueue = new ListViewItem.ListViewSubItem(item, total.QueueCount.ToString());
                subQueue.Name = "Queue";
                item.SubItems.Add(subQueue);

                ListViewItem.ListViewSubItem subQueueContinueTime = new ListViewItem.ListViewSubItem(item, total.MaxQueCallContinueTime);
                subQueueContinueTime.Name = "MaxQueCallContinueTime";
                item.SubItems.Add(subQueueContinueTime);
                if (total.QueueCount == 0)
                {
                    subQueueContinueTime.Text = "";
                }


                ListViewItem.ListViewSubItem subQueueStatusDateTime = new ListViewItem.ListViewSubItem(item, "");
                subQueueStatusDateTime.Name = "QueCallStatusDateTime";
                if (total.QueCallStatusDateTime == null)
                {
                    subQueueStatusDateTime.Text = "";
                }
                else
                {
                    subQueueStatusDateTime.Text = total.QueCallStatusDateTime.ToString("yyyy/MM/dd HH:mm:ss");
                }
                item.SubItems.Add(subQueueStatusDateTime);

                ListViewItem.ListViewSubItem subGroup = new ListViewItem.ListViewSubItem(item, total.Group.ToString());
                subGroup.Name = "Group";
                item.SubItems.Add(subGroup);
                return item;
            }
            catch (Exception ex)
            {
                writeLog("LoadTotalListItem system error:" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// whether show current group in totallistview
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool IsShowCurrentGroup(GroupInfo group)
        {
            bool continueForGroupFlag = true;
            bool continueForSkillGroupFlag = true;

            if (string.IsNullOrEmpty(SettingFields_KyoKuGroupShow))
            {
                if (KyokuGroup.Contains(";" + group.Group.ToString() + ","))
                {
                    continueForGroupFlag = false;
                }
            }
            else
            {
                if (SettingFields_KyoKuGroupShow.Contains(";" + group.Group.ToString() + ",1"))
                {
                    continueForGroupFlag = false;
                }
            }


            if (string.IsNullOrEmpty(_ShowSkillGroupIDs))
            {
                if (_DefaultShowSkillGroupIDs.Contains("'" + group.Group.ToString() + "'"))
                {
                    continueForSkillGroupFlag = false;
                }
            }
            else
            {
                if (_ShowSkillGroupIDs.Contains("'" + group.Group.ToString() + "'"))
                {
                    continueForSkillGroupFlag = false;
                }
            }

            if (continueForGroupFlag && continueForSkillGroupFlag)
            {
                return false;
            }

            return true;
        }



        #endregion

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //Asc.controlAutoSize(this);
        }

    }

    class DoubleBufferListView : ListView
    {
        public DoubleBufferListView()
        {
            //SetStyle( ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            UpdateStyles();
        }
    }
}