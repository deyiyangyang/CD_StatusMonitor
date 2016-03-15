using MyTools;
using StatusMonitor.Helper;
using StatusMonitor.Model;
using StatusMonitor.SettingFile;
using StatusMonitor.TabPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class MainForm : Form
    {
        //StatusMonitor section
        public string SettingFields_CpfmsgsvrAddr = "";
        public int SettingFields_CpfmsgsvrPort = 0;

        //SessMain section
        public string SettingFields_Server = "";
        public string SettingFields_Media = "";
        public string SettingFields_Name = "";
        public string SettingFields_Appl = "";
        public string SettingFields_Phase = "";
        public string SettingFields_Logon = "";

        //Alert
        public int SettingFields_AlertTotal = 100;
        public int SettingFields_AlertGroup = 100;

        //SVSet section
        public string SettingFields_StatusOverIdelTime1;
        public string SettingFields_StatusOverIdelTime2;
        public string SettingFields_StatusOverIdelTime3;
        public string SettingFields_StatusOverIdelTime4;
        public string SettingFields_StatusOverIdelTime5;
        public string SettingFields_StatusOverIdelTime6;

        public string SettingFields_StatusOverTalkTime1;
        public string SettingFields_StatusOverTalkTime2;
        public string SettingFields_StatusOverTalkTime3;
        public string SettingFields_StatusOverTalkTime4;
        public string SettingFields_StatusOverTalkTime5;
        public string SettingFields_StatusOverTalkTime6;

        public string SettingFields_StatusOverWorkTime1;
        public string SettingFields_StatusOverWorkTime2;
        public string SettingFields_StatusOverWorkTime3;
        public string SettingFields_StatusOverWorkTime4;
        public string SettingFields_StatusOverWorkTime5;
        public string SettingFields_StatusOverWorkTime6;

        public string SettingFields_StatusOverLeaveTime1;
        public string SettingFields_StatusOverLeaveTime2;
        public string SettingFields_StatusOverLeaveTime3;
        public string SettingFields_StatusOverLeaveTime4;
        public string SettingFields_StatusOverLeaveTime5;
        public string SettingFields_StatusOverLeaveTime6;

        public string SettingFields_StatusOverHoldTime1;
        public string SettingFields_StatusOverHoldTime2;
        public string SettingFields_StatusOverHoldTime3;
        public string SettingFields_StatusOverHoldTime4;
        public string SettingFields_StatusOverHoldTime5;
        public string SettingFields_StatusOverHoldTime6;

        public string SettingFields_StatusOverQuecallTime1;
        public string SettingFields_StatusOverQuecallTime2;
        public string SettingFields_StatusOverQuecallTime3;
        public string SettingFields_StatusOverQuecallTime4;
        public string SettingFields_StatusOverQuecallTime5;
        public string SettingFields_StatusOverQuecallTime6;

        public string PopupWorktime;
        public string PopupIdle;
        public string PopupHold;
        public string PopupLeave;
        public string PopupTalk;

        public string SettingFields_OptionName1 = "拠点";
        public string SettingFields_OptionName2 = "種別";
        public string SettingFields_OptionName3 = "";
        public string SettingFields_OptionName4 = "";
        public string SettingFields_OptionName5 = "";

        public string SettingFields_AgentListViewShowCol = "100000111111111";
        public string SettingFields_QuickAnswerMinutes = "0";
        public string SettingFields_QuePeriod1 = "";
        public string SettingFields_QuePeriodVoice1 = "";
        public string SettingFields_QuePeriod2 = "";
        public string SettingFields_QuePeriodVoice2 = "";
        public string SettingFields_QuePeriod3 = "";
        public string SettingFields_QuePeriodVoice3 = "";
        public string SettingFields_IdlePeriodLongString = "";
        public string SettingFields_IdlePeriodVoiceLongString = "";

        public int SettingFields_RefreshTime = 0;
        private string SettingFields_ShowBackColorForCol9 = ""; //1 即答列 背景ある、
        private string SettingFields_MonitorGroupList = ""; //1 即答列 背景ある、
        public string SettingFields_ShowAgentF = "";//AgentList Rule 5桁 0全部
        //public string ShowCallTagF = "0";//1:回線Tag非表示        
        //public string ShowChatF = "0";//0:show,1chat　不可
        //public string ShowMonitorF = "0";//0:show,1:MonitorTag非表示 
        //private int KeepAliveTime = 15;

        //public string SVPhone = "";
        public string SettingFields_LineCutShow = "";
        public string SettingFields_KyoKuGroupShow = "";
        public string SettingFields_MonitorTabShow = "";
        public string SettingFields_MessagePop = "";
        public string SettingFields_AgentGraphShow = "";
        public string SettingFields_WebServer = "";
        public string SettingFields_AgentListView_Width = "";
        public string SettingFields_CallListView_Width = "";
        public string SettingFields_QueueListView_Width = "";
        public string SettingFields_TotalListView_Width = "";
        public string SettingFields_MonitorGridView_Width = "";
        public string SettingFields_SplitContainer_Width = "";

        public string SettingFields_GroupSumColumnShow = "";

        public string SettingFields_AgentListViewSort = "";
        public string SettingFields_CallListViewSort = "";
        public string SettingFields_QueueListViewSort = "";
        public string SettingFields_TotalListViewSort = "";
        public string SettingFields_MonitorGridViewSort = "";
        
        public float SettingFields_ListFontSize = 1;
        public Dictionary<string, string> Dic_SettingFields_SkillQuecall = new Dictionary<string, string>();


        #region GetIniSettingValue

        private void GetIniSettingValueInLoadEvent()
        {
            //IniProfile = new TksProfileAcxLib.TksProfileClass();
            // IniProfile.Load(MyTool.GetModuleIniPath());
            SettingFields_MessagePop = IniProfile.GetStringDefault(ConstEntity.MESSAGEPOP, "");
            SettingFields_AgentGraphShow = IniProfile.GetStringDefault(ConstEntity.AGENTGRAPH, "");
            SettingFields_WebServer = IniProfile.GetStringDefault(ConstEntity.WEBSERVER, "");
            SettingFields_ListFontSize = float.Parse(IniProfile.GetStringDefault(ConstEntity.LISTFONTSIZE, "1"));
            SettingFields_AgentListView_Width = IniProfile.GetStringDefault(ConstEntity.AGENT_LIST_VIEW_WIDTH, "");
            SettingFields_CallListView_Width = IniProfile.GetStringDefault(ConstEntity.CALL_LIST_VIEW_WIDTH, "");
            SettingFields_QueueListView_Width = IniProfile.GetStringDefault(ConstEntity.QUEUE_LIST_VIEW_WIDTH, "");
            SettingFields_MonitorGridView_Width = IniProfile.GetStringDefault(ConstEntity.MONITOR_GRID_VIEW_WIDTH, "");
            SettingFields_TotalListView_Width = IniProfile.GetStringDefault(ConstEntity.TOTAL_LIST_VIEW_WIDTH, "");
            SettingFields_GroupSumColumnShow = IniProfile.GetStringDefault(ConstEntity.GROUP_SUM_COLUMN_SHOW, "111111");

            SettingFields_AgentListViewSort = IniProfile.GetStringDefault(ConstEntity.AGENT_LIST_VIEW_SORt, "");
            SettingFields_CallListViewSort = IniProfile.GetStringDefault(ConstEntity.CALL_LIST_VIEW_SORT, "");
            SettingFields_QueueListViewSort = IniProfile.GetStringDefault(ConstEntity.QUEUE_LIST_VIEW_SORT, "");
            SettingFields_TotalListViewSort = IniProfile.GetStringDefault(ConstEntity.TOTAL_LIST_VIEW_SORT, "");
            SettingFields_MonitorGridViewSort = IniProfile.GetStringDefault(ConstEntity.MONITOR_GRID_VIEW_SORT, "");
            SettingFields_SplitContainer_Width = IniProfile.GetStringDefault(ConstEntity.SPLITCONTAINER_PANEL_WIDTH, "");

            //IniProfile.SelectSection("Alert");
            //SettingFields_AlertTotal = IniProfile.GetLongDefault("nTotal", 100);
            //SettingFields_AlertGroup = IniProfile.GetLongDefault("nGroup", 100);

            //IniProfile.SelectSection("StatusMonitor");
            //SettingFields_CpfmsgsvrAddr = IniProfile.GetStringDefault("sCpfmsgsvrAddr", "");
            //SettingFields_CpfmsgsvrPort = IniProfile.GetLongDefault("nCpfmsgsvrPort", 0);

            //IniProfile.SelectSection("SessMain");
            //SettingFields_Server = IniProfile.GetStringDefault("sServer","");
            //SettingFields_Media = IniProfile.GetStringDefault("sMediaSet", "");
            //if (string.IsNullOrEmpty(SettingFields_Media) || SettingFields_Media == "null")
            //{
            //    SettingFields_Media = IniProfile.GetStringDefault("sMedia","") + "S";
            //}
            //SettingFields_Name = IniProfile.GetString("sName");
            //SettingFields_Appl = IniProfile.GetString("sAppl");
            //SettingFields_Phase = IniProfile.GetStringDefault("sPhase", "Phase");
            //SettingFields_Logon = IniProfile.GetStringDefault("sLogon", "MAIN");




            //IniProfile.SelectSection("SVSet");
            //SettingFields_MonitorGroupList = IniProfile.GetStringDefault("MonitorGroupList", "");
            //string strShowCol = IniProfile.GetStringDefault("ShowCol", "");
            //if (!string.IsNullOrEmpty(strShowCol))
            //{
            //    if (strShowCol.Length >= 15)
            //        SettingFields_AgentListViewShowCol = strShowCol;
            //}
            //string keepAlive = IniProfile.GetStringDefault("KeepAlive", "");
            //if (string.IsNullOrEmpty(keepAlive))
            //    keepAlive = "15";
            //string strRefreshTime = IniProfile.GetStringDefault("Refresh", "10");
            //if (string.IsNullOrEmpty(keepAlive))
            //    strRefreshTime = "10";
            //SettingFields_RefreshTime = int.Parse(strRefreshTime);
            //KeepAliveTime = int.Parse(keepAlive);

            //SettingFields_QuickAnswerMinutes = IniProfile.GetStringDefault("QuickAnswerMinutes", "0");
            //SettingFields_QuePeriod1 = IniProfile.GetStringDefault("QuePeriod1", "");
            //SettingFields_QuePeriodVoice1 = IniProfile.GetStringDefault("QuePeriodVoice1", "");
            //SettingFields_QuePeriod2 = IniProfile.GetStringDefault("QuePeriod2", "");
            //SettingFields_QuePeriodVoice2 = IniProfile.GetStringDefault("QuePeriodVoice2", "");
            //SettingFields_QuePeriod3 = IniProfile.GetStringDefault("QuePeriod3", "");
            //SettingFields_QuePeriodVoice3 = IniProfile.GetStringDefault("QuePeriodVoice3", "");
            //SettingFields_IdlePeriodLongString = IniProfile.GetStringDefault("SkillIdlePeriod", "");
            //SettingFields_IdlePeriodVoiceLongString = IniProfile.GetStringDefault("SkillIdlePeriodVoice", "");

            //SVPhone = IniProfile.GetStringDefault("Phone", "");
            //SettingFields_ShowBackColorForCol9 = IniProfile.GetStringDefault("ShowBackColorForCol9", "");

            //GetStatusOverTime();
            //GetOptionName();
            //GetSpecialFlag();


        }

        private void GetSpecialFlag()
        {
            string strSetExtend = IniProfile.GetStringDefault("sSetExtend", "");
            if (strSetExtend.Length > 0)
            {
                SettingFields_ShowAgentF = strSetExtend.Substring(0, 1);
                if (SettingFields_ShowAgentF == "0")
                    SettingFields_ShowAgentF = "";
                if (strSetExtend.Length > 1)
                {
                    ShowCallTagF = strSetExtend.Substring(1, 1);
                    if (ShowCallTagF == "1")
                    {
                        //to do
                        // statusTabCtrl.TabPages.Remove(tabLineStatusPage);
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
                            if (string.IsNullOrEmpty(SettingFields_MonitorGroupList) && strSetExtend.Length > 10)
                            {
                                SettingFields_MonitorGroupList = strSetExtend.Substring(10);
                            }
                        }
                    }
                }
            }
        }
        private void GetStatusOverTime()
        {
            IniProfile.SelectSection("SVSet");

            SettingFields_StatusOverIdelTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverIdelTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverIdelTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverIdelTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverIdelTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverIdelTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEIDLE6, "0000").PadLeft(4, '0');

            SettingFields_StatusOverTalkTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverTalkTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverTalkTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverTalkTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverTalkTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverTalkTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMETALK6, "0000").PadLeft(4, '0');

            SettingFields_StatusOverWorkTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverWorkTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverWorkTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverWorkTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverWorkTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverWorkTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEWORKTIME6, "0000").PadLeft(4, '0');

            SettingFields_StatusOverLeaveTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverLeaveTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverLeaveTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverLeaveTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverLeaveTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverLeaveTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMELEAVE6, "0000").PadLeft(4, '0');

            SettingFields_StatusOverHoldTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverHoldTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverHoldTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverHoldTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverHoldTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverHoldTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEHOLD6, "0000").PadLeft(4, '0');

            SettingFields_StatusOverQuecallTime1 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL1, "0000").PadLeft(4, '0');
            SettingFields_StatusOverQuecallTime2 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL2, "0000").PadLeft(4, '0');
            SettingFields_StatusOverQuecallTime3 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL3, "0000").PadLeft(4, '0');
            SettingFields_StatusOverQuecallTime4 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL4, "0000").PadLeft(4, '0');
            SettingFields_StatusOverQuecallTime5 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL5, "0000").PadLeft(4, '0');
            SettingFields_StatusOverQuecallTime6 = IniProfile.GetStringDefault(ConstEntity.STATUSTIMEQUECALL6, "0000").PadLeft(4, '0');

        }

        private void GetOptionName()
        {
            IniProfile.SelectSection("SVSet");
            SettingFields_OptionName1 = IniProfile.GetStringDefault("Option1", "拠点");
            SettingFields_OptionName2 = IniProfile.GetStringDefault("Option2", "種別");
            SettingFields_OptionName3 = IniProfile.GetStringDefault("Option3", "Option3");
            SettingFields_OptionName4 = IniProfile.GetStringDefault("Option4", "Option4");
            SettingFields_OptionName5 = IniProfile.GetStringDefault("Option5", "Option5");
        }

        private void GetSkillQuecallSetting(DataTable dtSkillGroup)
        {
            IniProfile.SelectSection("SVSet");
            if (Dic_SettingFields_SkillQuecall == null)
                Dic_SettingFields_SkillQuecall = new Dictionary<string, string>();
            Dic_SettingFields_SkillQuecall.Clear();
            string periodAndVoiceTemplate = "{0},{1}";
            foreach (DataRow dr in dtSkillGroup.Rows)
            {
                if (dr["groupId"].ToString() == "-1") continue;
                string skillGroupID = dr["groupId"].ToString();
                string period1 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate1, skillGroupID), string.Empty);
                string voice1 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate1, skillGroupID), string.Empty);
                string period2 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate2, skillGroupID), string.Empty);
                string voice2 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate2, skillGroupID), string.Empty);
                string period3 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallPeriodTemplate3, skillGroupID), string.Empty);
                string voice3 = IniProfile.GetStringDefault(string.Format(ConstEntity.SkillQueCallVoiceTemplate3, skillGroupID), string.Empty);
                if (string.IsNullOrEmpty(period1))
                {
                    continue;
                }
                string value = period1 + "," + voice1 + "|" + period2 + "," + voice2 + "|" + period3 + "," + voice3;
                Dic_SettingFields_SkillQuecall.Add(skillGroupID, value);
                SkillQueCallSoundPlayerManager.ConfigSoundPlayers(skillGroupID);
            }

        }
        #endregion

        #region SetIniSettingValue no use
        //private void SetIniSettingValue()
        //{

        //}

        //public void SetAgentListViewShowCol(string strShowCol)
        //{
        //    try
        //    {
        //        this.SettingFields_AgentListViewShowCol = strShowCol;
        //        // to use the tab in furture
        //        //(this.DicTabForms[this.AGENTTAB] as AgentStatusForm).SetAgentOptionShow();
        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString("ShowCol", SettingFields_AgentListViewShowCol);
        //        IniProfile.Save(MyTool.GetModuleIniPath());
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("setShowCol:" + ex.Message);
        //    }
        //}


        //public void setOptionName(string strOption1, string strOption2, string strOption3, string strOption4, string strOption5)
        //{
        //    try
        //    {

        //        SettingFields_OptionName1 = strOption1;
        //        SettingFields_OptionName2 = strOption2;
        //        SettingFields_OptionName3 = strOption3;
        //        SettingFields_OptionName4 = strOption4;
        //        SettingFields_OptionName5 = strOption5;
        //        // to use the tab in furture
        //        //(this.DicTabForms[this.AGENTTAB] as AgentStatusForm).SetOptionNameView(strOption1, strOption2, strOption3, strOption4, strOption5);
        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString("Option1", strOption1);
        //        IniProfile.SetString("Option2", strOption2);
        //        IniProfile.SetString("Option3", strOption3);
        //        IniProfile.SetString("Option4", strOption4);
        //        IniProfile.SetString("Option5", strOption5);
        //        IniProfile.Save(MyTool.GetModuleIniPath());


        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("setOptionName:" + ex.Message);
        //    }
        //}

        //public void SetQuickAnswerMinutes(string inTime)
        //{
        //    try
        //    {
        //        SettingFields_QuickAnswerMinutes = inTime;


        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString("QuickAnswerMinutes", inTime);

        //        IniProfile.Save(MyTool.GetModuleIniPath());
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("setQuickAnswerMinutes System Error:" + ex.Message);
        //    }

        //}

        //public void setPhone(string phone)
        //{
        //    try
        //    {
        //        SVPhone = phone;
        //        setFrmText(MonitorStatus);
        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString("Phone", phone);
        //        IniProfile.Save(MyTool.GetModuleIniPath());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public void SetRefresh(string strRefresh)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(strRefresh))
        //        {
        //            return;
        //        }
        //        int intRefresh = 0;
        //        intRefresh = int.Parse(strRefresh);
        //        if (intRefresh < 1 || intRefresh > 999)
        //        {
        //            return;
        //        }
        //        SettingFields_RefreshTime = int.Parse(strRefresh);
        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString("Refresh", strRefresh);
        //        IniProfile.Save(MyTool.GetModuleIniPath());
        //        this.UpdateContinueTimer.Interval = this.SettingFields_RefreshTime * 1000;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("setRefresh:" + ex.Message);
        //    }
        //}

        //public void SetStatusOverTimes(string strIdleValue, string strWorkTimeValue, string strTalkValue, string strHoldValue, string strLeaveValue, string strQuecallValue)
        //{
        //    try
        //    {
        //        string[] arrIdleValue = strIdleValue.Split(',');
        //        string[] arrWorkTimeValue = strWorkTimeValue.Split(',');
        //        string[] arrTalkValue = strTalkValue.Split(',');
        //        string[] arrHoldValue = strHoldValue.Split(',');
        //        string[] arrLeaveValue = strLeaveValue.Split(',');
        //        string[] arrQuecallValue = strQuecallValue.Split(',');

        //        SettingFields_StatusOverIdelTime1 = arrIdleValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverIdelTime2 = arrIdleValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverIdelTime3 = arrIdleValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverIdelTime4 = arrIdleValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverIdelTime5 = arrIdleValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverIdelTime6 = arrIdleValue[5].PadLeft(4, '0');

        //        SettingFields_StatusOverTalkTime1 = arrTalkValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverTalkTime2 = arrTalkValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverTalkTime3 = arrTalkValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverTalkTime4 = arrTalkValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverTalkTime5 = arrTalkValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverTalkTime6 = arrTalkValue[5].PadLeft(4, '0');

        //        SettingFields_StatusOverWorkTime1 = arrWorkTimeValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverWorkTime2 = arrWorkTimeValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverWorkTime3 = arrWorkTimeValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverWorkTime4 = arrWorkTimeValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverWorkTime5 = arrWorkTimeValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverWorkTime6 = arrWorkTimeValue[5].PadLeft(4, '0');

        //        SettingFields_StatusOverLeaveTime1 = arrLeaveValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverLeaveTime2 = arrLeaveValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverLeaveTime3 = arrLeaveValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverLeaveTime4 = arrLeaveValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverLeaveTime5 = arrLeaveValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverLeaveTime6 = arrLeaveValue[5].PadLeft(4, '0');

        //        SettingFields_StatusOverHoldTime1 = arrHoldValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverHoldTime2 = arrHoldValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverHoldTime3 = arrHoldValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverHoldTime4 = arrHoldValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverHoldTime5 = arrHoldValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverHoldTime6 = arrHoldValue[5].PadLeft(4, '0');

        //        SettingFields_StatusOverQuecallTime1 = arrQuecallValue[0].PadLeft(4, '0');
        //        SettingFields_StatusOverQuecallTime2 = arrQuecallValue[1].PadLeft(4, '0');
        //        SettingFields_StatusOverQuecallTime3 = arrQuecallValue[2].PadLeft(4, '0');
        //        SettingFields_StatusOverQuecallTime4 = arrQuecallValue[3].PadLeft(4, '0');
        //        SettingFields_StatusOverQuecallTime5 = arrQuecallValue[4].PadLeft(4, '0');
        //        SettingFields_StatusOverQuecallTime6 = arrQuecallValue[5].PadLeft(4, '0');

        //        IniProfile.SelectSection("SVSet");
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE1, arrIdleValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE2, arrIdleValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE3, arrIdleValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE4, arrIdleValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE5, arrIdleValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEIDLE6, arrIdleValue[5]);

        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME1, arrWorkTimeValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME2, arrWorkTimeValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME3, arrWorkTimeValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME4, arrWorkTimeValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME5, arrWorkTimeValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEWORKTIME6, arrWorkTimeValue[5]);

        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK1, arrTalkValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK2, arrTalkValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK3, arrTalkValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK4, arrTalkValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK5, arrTalkValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMETALK6, arrTalkValue[5]);

        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD1, arrHoldValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD2, arrHoldValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD3, arrHoldValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD4, arrHoldValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD5, arrHoldValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEHOLD6, arrHoldValue[5]);

        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE1, arrLeaveValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE2, arrLeaveValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE3, arrLeaveValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE4, arrLeaveValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE5, arrLeaveValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMELEAVE6, arrLeaveValue[5]);

        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL1, arrQuecallValue[0]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL2, arrQuecallValue[1]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL3, arrQuecallValue[2]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL4, arrQuecallValue[3]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL5, arrQuecallValue[4]);
        //        IniProfile.SetString(ConstEntity.STATUSTIMEQUECALL6, arrQuecallValue[5]);




        //        IniProfile.Save(MyTool.GetModuleIniPath());
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("setStatusOverTimes System Error:" + ex.Message);
        //    }

        //}

        #endregion
    }
}
