using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
// add
using MyTools;
using Cpfmsgacxa;
using TksProfileAcxLib;
using System.Threading;
using StatusMonitor.Helper;
using StatusMonitor.Model;

namespace StatusMonitor
{
    public struct LineUseage
    {
        public int LineNumber;
        public int BusyCount;
    }

    public class GroupInfo
    {
        public int Group;
        public string GroupName;

        public GroupInfo(int group, string groupName)
        {
            Group = group;
            GroupName = groupName;
        }

        public override string ToString()
        { return GroupName; }
    }



    public class TotalStatus
    {
        public int Group = 0;
        public string GroupName = "";
        public int WaitCount = 0;
        public int ConnectCount = 0;
        public int OtherCount = 0;
        public int QueueCount = 0;
        public string MaxQueCallContinueTime = "";
        public DateTime QueCallStatusDateTime;


    }

    public class QueueStatus
    {
        public int Group;
        public string GroupName;
        public int QueueCount;
    }

    public class AgentStatus
    {
        public int Group;
        public string GroupName;
        public string Agent;
        public string AgentName;
        public string Extension;
        public DateTime LoginTime;
        public int Status;
        public DateTime StatusTime;
        public string StatusContinueTime;
        public bool Help;
        public int Session;
        public string Caller;
        public int Elapsed;
        //add,xzg,2011/07/04,S
        public int iStatus;
        public string iSkillName;
        public string vReason;
        public string vMemo;
        //add,xzg,2011/07/04,E

        public string Option1;
        public string Option2;
        public string Option3;
        public string Option4;
        public string Option5;

        //added by zhu 2015/05/27
        public int iSkillGroupID;
        // end added
        public string Conntype;
    }

    public class LineStatus
    {
        public int Group;
        public string GroupName;
        public string Device;
        public int Session;
        public string Caller;
        public string Callee;
        public DateTime ConnectedTime;
        public int Status;
        public DateTime StatusTime;
        public string Service;
        public string Extension;
        public string StatusContinueTime;
        public string iSessionProfileID;
        public int iSkillGroupID; //SkillID
        public DateTime dtAcdcall; //dtAcdcall
        public string Conntype;
        public string vAgentID;
        public string vAgentName;
    }

    //add,xzg,2012/10/27,S
    public class LineMaxBusy
    {
        public string iGroupID;
        public string TimeS;
        public string TimeEnd;
        public int MaxBusy;
    }
    public class CallReport
    {

        public string dtSum;
        public String iGroupID;
        public string iInCall;
        public string iRefuse;
        public string iInComp;

        //public string iInCall;
        //public string iRefuse;
        //public string iInComp;


        public string iOutCall;
        public string iOutComp;

        public string iMaxCount;
        public string sMsgFlag;
    }
    //add,xzg,2012/10/27,E

    public partial class MainForm : Form
    {
        // Enum
        //public LineStatusEnum[] lineStatusEnum =
        //{
        //    new LineStatusEnum(0, "SM0020041", "IconCallIdle"),
        //    new LineStatusEnum(1, "SM0020042", "IconCallCalling"),
        //    new LineStatusEnum(2, "SM0020043", "IconCallIvr"),
        //    new LineStatusEnum(3, "SM0020044", "IconCallPreparing"),
        //    new LineStatusEnum(4, "SM0020045", "IconCallOperator"),
        //};

        //public AgentStatusEnum[] agentStatusEnum =
        //{
        //    new AgentStatusEnum( 0, "SM0020046", "IconOpeIdle","Idle"),
        //    new AgentStatusEnum( 1, "SM0020047", "IconOpeWait","Wait"),
        //    //deleted by zhu 2014/05/29
        //    //new AgentStatusEnum( 2, "SM0020048", "IconOpePreparing"),
        //    //end deleted
        //    new AgentStatusEnum( 3, "SM0020049", "IconOpeOffering","Offering"),
        //    new AgentStatusEnum( 5, "SM0020050", "IconOpeWorktime","Worktime"),
        //    new AgentStatusEnum( 6, "SM0020051", "IconOpeSeatoff","SeatOff"),
        //    //deleted by zhu 2014/05/29
        //    //new AgentStatusEnum( 7, "SM0020052", "IconOpeTelephone"),
        //    //end deleted
        //    new AgentStatusEnum(10, "SM0020053", "IconTelConnect","Connect"),
        //    new AgentStatusEnum(20, "SM0020054", "IconTelCalling","Calling"),
        //    new AgentStatusEnum(30, "SM0020055", "IconTelHold","Hold"),
        //    new AgentStatusEnum(40, "SM0020056", "IconTelTransfer","Transfer"),
        //    //deleted by zhu 2014/05/29
        //    //new AgentStatusEnum(50, "SM0020057", "IconTelConf"), 
        //    //new AgentStatusEnum(60, "SM0020058", "IconTelMonitor"),
        //    //end deleted
        //    //del,xzg,2008/12/09,S---
        //    //new AgentStatusEnum(70, "通話録音中", "IconTelRecord"),
        //    //del,xzg,2008/12/09,E---
        //};

        // Prog Info
        public bool formClosing = false;
        public TksProfileClass IniProfile = new TksProfileClass();
        public LineUseage lineUseage;
        public List<GroupInfo> groupInfoList = new List<GroupInfo>();
        public List<QueueStatus> queueStatusList = new List<QueueStatus>();
        public List<LineStatus> lineStatusList = new List<LineStatus>();
        public List<AgentStatus> agentStatusList = new List<AgentStatus>();
        public int displayGroup = -1;
        public int alertTotal = 100;
        public int alertGroup = 100;

        public List<GroupInfo> option1InfoList = new List<GroupInfo>();
        public List<GroupInfo> option2InfoList = new List<GroupInfo>();
        public List<GroupInfo> option3InfoList = new List<GroupInfo>();
        public List<GroupInfo> option4InfoList = new List<GroupInfo>();
        public List<GroupInfo> option5InfoList = new List<GroupInfo>();
        public List<GroupInfo> option6InfoList = new List<GroupInfo>();
        public List<GroupInfo> option7InfoList = new List<GroupInfo>();


        public List<LineMaxBusy> lineMaxBusy = new List<LineMaxBusy>();
        private List<CallReport> ReportList = new List<CallReport>();
        //added bu zhu lock object  2014/06/18
        private static Mutex muxConsole = new Mutex();
        private Thread CommandThread;
        private List<AgentStatus> ListAgentCommand = new List<AgentStatus>();
        private List<LineStatus> ListCallCommand = new List<LineStatus>();
        private List<QueueStatus> ListQueueCommand = new List<QueueStatus>();

        public void OnRecvCommand(string command, CpfParams recvParams)
        {
            //DebugPrint(String.Format("Recv [{0}] {1}", command, recvParams.GetParams()));
            // Command
            //Console.WriteLine(command);
            //Console.WriteLine(recvParams.GetParams());
            //Debug.WriteLine(recvParams.GetParams());
            //try
            //{

            //added by Zhu 2014/04/10
            //Application.DoEvents();
            //end added

            try
            {
                switch (command.ToUpper())
                {
                    case "LOGOFF":
                        {
                            // Close Cpfmagacx
                            axCpfMsg.Close();
                        }
                        break;

                    //case "VOICEMAIL":
                    //    {

                    //    }
                    //    break;
                    case "QUECALL":
                        {
                            // Make queueStatus
                            QueueStatus queueStatus = new QueueStatus();
                            queueStatus.Group = recvParams.GetLongDefault("iSkillID", 0);
                            queueStatus.GroupName = recvParams.GetStringDefault("vSkillName", "");
                            queueStatus.QueueCount = recvParams.GetLongDefault("iNumberOfQue", 0);
                            // AddGroup
                            AddGroup(queueStatus.Group, queueStatus.GroupName);
                            // Add queueStatusList
                            queueStatusList.RemoveAll(delegate (QueueStatus obj)
                                { return obj.Group == queueStatus.Group; });
                            queueStatusList.Add(queueStatus);




                            //add,xzg,2009/02/23,S--------
                            int iMsgDone = recvParams.GetLongDefault("iMsgDone", 0);
                            if (1 == iMsgDone)
                            {
                                int curQUES = QUECALLTimes * GetTimes;
                                if (QUECALLCount > curQUES)
                                {
                                    CpfParams cpfParam = new CpfParams();
                                    cpfParam.AddLong("iStart", curQUES);
                                    cpfParam.AddLong("iEnd", curQUES + GetTimes);
                                    axCpfMsg.SendEvent("", "", "AM_GET_QUECALL_LIST", cpfParam);
                                    QUECALLTimes = QUECALLTimes + 1;
                                }
                            }
                            //if (iMsgDone == 0)
                            //    updateQueStatus = true;
                            //else
                            //    updateQueStatus = false;
                            //if (QUECALLTimes * GetTimes >= QUECALLCount)//2014/03/10
                            updateQueStatus = true;
                            //add,xzg,2009/02/23,E--------

                            //update,2014/03,S
                            ////add,xzg,2013/11/14,S
                            //setMonitorQue(queueStatus);
                            ////add,xzg,2013/11/14,E
                            if (ShowMonitorF != "1")
                            {
                                //setMonitorQue(queueStatus);


                                //modified by zhu 2014/06/27
                                setMonitorQue(queueStatus);
                                //this.ListQueueCommand.Add(queueStatus);
                                //end modified
                            }
                            //update,2014/03,E
                        }
                        break;
                    case "CALLLEG":
                        {
                            // Make lineStatus
                            LineStatus lineStatus = new LineStatus();
                            lineStatus.Group = recvParams.GetLongDefault("iSkillID", 0);
                            lineStatus.GroupName = recvParams.GetStringDefault("vSkillName", "");
                            lineStatus.Device = recvParams.GetStringDefault("vDeviceName", "");

                            //update,xzg,2012/02/08,S
                            //lineStatus.Session = recvParams.GetLongDefault("iSessionProfileID", 0);
                            int iSession = recvParams.GetLongDefault("iSessionProfileID", 0);
                            if (iSession == 0) return;
                            lineStatus.Session = iSession;
                            //update,xzg,2012/02/08,E
                            lineStatus.Caller = recvParams.GetStringDefault("vCaller", "");
                            lineStatus.Callee = recvParams.GetStringDefault("vCallee", "");
                            //lineStatus.ConnectedTime= MyTool.dtConnected(recvParams.GetStringDefault("dtConnected", ""));
                            if (recvParams.GetStringDefault("dtConnected", "") != "0" && recvParams.GetStringDefault("dtConnected", "").Length > 0)
                            {
                                lineStatus.ConnectedTime = DateTime.Parse(recvParams.GetStringDefault("dtConnected", ""));
                            }
                            lineStatus.Status = LineStatusEnum.ConvStatusID(recvParams.GetLongDefault("iStatus", 0));
                            //lineStatus.StatusTime	= MyTool.ParseDateTime(recvParams.GetStringDefault("dtStatus", ""));
                            if (recvParams.GetStringDefault("dtStatus", "") != "0" && recvParams.GetStringDefault("dtStatus", "") != "NONE")
                            {
                                lineStatus.StatusTime = DateTime.Parse(recvParams.GetStringDefault("dtStatus", ""));
                            }
                            lineStatus.Service = recvParams.GetStringDefault("vService", "");
                            lineStatus.Extension = recvParams.GetStringDefault("vExtension", "");
                            lineStatus.Conntype = recvParams.GetStringDefault("iCallType", "");
                            lineStatus.iSessionProfileID = recvParams.GetStringDefault("iSessionProfileID", "0");
                            lineStatus.vAgentID = recvParams.GetStringDefault("vAgentID", "");

                            //add,xzg,2013/11/19,S
                            lineStatus.iSkillGroupID = recvParams.GetLongDefault("iSkillGroupID", 0);
                            if (recvParams.GetStringDefault("dtAcdcall", "") != "0" && !string.IsNullOrEmpty(recvParams.GetStringDefault("dtAcdcall", "")) && recvParams.GetStringDefault("dtAcdcall", "") != "NONE")
                                lineStatus.dtAcdcall = DateTime.Parse(recvParams.GetStringDefault("dtAcdcall", ""));

                            //add,xzg,2013/11/19,E

                            string iElapsedTime = recvParams.GetStringDefault("iElapsedTime", "0");
                            if (iElapsedTime != "0")
                            {
                                int iTime;

                                string strTime = "";



                                iTime = int.Parse(iElapsedTime);

                                strTime = ConvertTimeHHMMSS(iTime);
                                lineStatus.StatusContinueTime = strTime;
                            }
                            else
                            {
                                //added by zhu 2014/05/13 set the default value
                                lineStatus.StatusContinueTime = "00:00:00";
                                //end added
                            }
                            // AddGroup
                            AddGroup(lineStatus.Group, lineStatus.GroupName);
                            // Add lineStatusList
                            lineStatusList.RemoveAll(delegate (LineStatus obj)
                                { return obj.Device == lineStatus.Device; });
                            if (lineStatus.Status > 0) lineStatusList.Add(lineStatus);
                            // LineUseage
                            lineUseage.LineNumber = recvParams.GetLongDefault("iNumberOfChannel", 0);
                            lineUseage.BusyCount = lineStatusList.Count;

                            //add,xzg,2011/09/16,S
                            updateLineStatus = true;
                            //add,xzg,2011/09/16,E                            
                            //Console.WriteLine(lineStatus.dtAcdcall.ToString());
                            //update,2014/03,S
                            //add,xzg,2013/11/14,S
                            //if (lineStatus.dtAcdcall.Year > 2012 && (lineStatus.Status == 0 || lineStatus.Status == 3)) //&& lineStatus.iSkillGroupID!=0
                            //    setMonitorCall(lineStatus);
                            //add,xzg,2013/11/14,E
                            if (ShowMonitorF != "1")
                            {

                                //lineStatus.dtAcdcall.Year> 2012
                                //modified by zhu 2014/5/28 change 3 to 4
                                //if (lineStatus.dtAcdcall.Year> 2012 && (lineStatus.Status == 0 || lineStatus.Status==3)) //&& lineStatus.iSkillGroupID!=0
                                //    setMonitorCall(lineStatus);
                                try
                                {
                                    if (lineStatus.dtAcdcall.Year > 2012 && (lineStatus.Status == 0 || lineStatus.Status == 4)) //&& lineStatus.iSkillGroupID!=0
                                    {

                                        //modified by zhu 2014/06/27
                                        setMonitorCall(lineStatus);
                                        //ListCallCommand.Add(lineStatus);
                                        //end modified
                                    }
                                }
                                catch (Exception ex)
                                {
                                    writeLog("setMonitorCall System Error " + ex.Message + ex.StackTrace);
                                }
                                //end modified
                            }
                            //update,2014/03,E

                        }
                        break;
                    //add,xzg,2008/04/25,S--------
                    //receive message
                    case "SM_MESSAGE":
                        {
                            //add,2014/03,S
                            if (ShowChatF == "1")
                            {
                                return;
                            }
                            //add,2014/03,E
                            string vSkillName = recvParams.GetStringDefault("vSkillName", "");
                            string vMessage = recvParams.GetStringDefault("vMessage", "");
                            string vAgentID = recvParams.GetStringDefault("vAgentID", "");
                            string vAgentName = recvParams.GetStringDefault("vAgentName", "");

                            //int iStatus = AgentStatusEnum.ConvStatusID(recvParams.GetLongDefault("iStatus", 0));
                            //AgentStatusEnum status = CTe1Helper.GetAgentStatusEnum(iStatus);
                            //string vStatus = res.GetString(status.StatusName);

                            int iStatus = 1;
                            string vStatus = "";

                            //added by zhu 2015/09/28
                            if (agentStatusList.Count > 0)
                            {
                                var tempAgent = agentStatusList.Find(p => p.Agent == vAgentID);
                                iStatus = tempAgent.iStatus;
                                //added by zhu 2015/11/10
                                iStatus = tempAgent.Status;
                                //end added
                                if (iStatus == 6)
                                {
                                    vStatus = tempAgent.vReason;
                                    if (string.IsNullOrEmpty(vStatus))
                                        vStatus = "離席";
                                }
                                else
                                {
                                    AgentStatusEnum status = CTe1Helper.GetAgentStatusEnum(iStatus);
                                    vStatus = res.GetString(status.StatusName);
                                }
                                //2015/11/18 from xie-san's request
                                vSkillName = tempAgent.GroupName;
                            }
                            else
                            {
                                iStatus = AgentStatusEnum.ConvStatusID(recvParams.GetLongDefault("iStatus", 0));
                                AgentStatusEnum status = CTe1Helper.GetAgentStatusEnum(iStatus);
                                vStatus = res.GetString(status.StatusName);
                            }

                            //end added


                            int msgIndex;
                            if (vAgentID.Length < 1)
                            {
                                return;
                            }
                            //added by zhu 2014/04/17
                            if (!string.IsNullOrEmpty(SpecialNameFlag))
                            {
                                if (SpecialNameFlag.ToLower() != "a")
                                {
                                    if (vAgentID.Substring(5, 1) != SpecialNameFlag)
                                        return;
                                }
                            }
                            else
                            {
                                if (vAgentID.Substring(5, 1) != "0")
                                    return;
                            }
                            //end added
                            msgIndex = -1;
                            try
                            {
                                if (msgFromID.Count > 0)
                                {
                                    msgIndex = msgFromID.IndexOf(vAgentID);
                                }
                                if (msgIndex >= 0)
                                {
                                    MessageForm msgFrom;//= new MessageForm(); //コメント
                                    msgFrom = (MessageForm)msgFromList[msgIndex];
                                    //msgFrom.MessageInfo = vMessage;
                                    msgFrom.setMsg(vMessage);
                                    msgFrom.Activate();
                                }
                                else
                                {
                                    if (SettingFields_MessagePop == "0")
                                    {
                                        break;
                                    }
                                    MessageForm msgFrom = new MessageForm();
                                    msgFrom.axCpfMsg1 = this.axCpfMsg;
                                    msgFrom.mainF = this;
                                    msgFrom.SkillID = vSkillName;
                                    msgFrom.AgentID = vAgentID;
                                    msgFrom.AgentName = vAgentName;
                                    msgFrom.Status = vStatus;
                                    msgFrom.MessageInfo = vMessage;
                                    msgFromList.Add(msgFrom);
                                    msgFromID.Add(vAgentID);
                                    msgFrom.Show();
                                    msgFrom.Activate();
                                }
                            }
                            catch (Exception ex)
                            {
                                writeLog("MainProg:OnRecvCommand:PopMsgFrm recv msg " + ex.Message + ex.StackTrace);
                            }
                        }
                        break;
                    //add,xzg,2008/04/25,E--------
                    //add,xzg,2008/05/19,S--------
                    //reBoot
                    case "REBOOTED":
                        {
                            connectTimer.Enabled = true;
                            axCpfMsg.Close();
                            // Read StatusMonitor
                            IniProfile.SelectSection("StatusMonitor");
                            string cpfmsgsvrAddr = IniProfile.GetStringDefault("sCpfmsgsvrAddr", "");
                            int cpfmsgsvrPort = IniProfile.GetLongDefault("nCpfmsgsvrPort", 0);
                            // Read MainSess
                            IniProfile.SelectSection("SessMain");
                            string server = IniProfile.GetString("sServer");
                            //update,xzg,2012/08/23,S
                            //string media = iniProfile.GetString("sMedia");
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
                            axCpfMsg.Open(cpfmsgsvrAddr, cpfmsgsvrPort, server, media, name, appl, phase, logon);

                            // Set Title
                            //this.Text = "NGCP 状態モニタ";                    
                            this.Text = res.GetString(NOTIFYICON_TEXT);
                            mainNotifyIcon.Text = res.GetString(NOTIFYICON_TEXT);
                            // Stop connectTimer
                            connectTimer.Enabled = false;
                        }
                        break;
                    //add,xzg,2008/05/19,E--------

                    //add,xzg,2010/02/23,S----------
                    case "QUECALLCOUNT":
                        {
                            QUECALLCount = recvParams.GetLongDefault("iNumberOfList", 0);
                            QUECALLTimes = 1;
                            //add,xzg,2010/10/06,S
                            //lineStatusList.Clear();
                            //add,xzg,2010/10/06,E

                            //add,xzg,2011/07/01,S
                            if (lineStatusList != null)
                            {
                                if (lineStatusList.Count > 0)
                                    lineStatusList.Clear();
                            }

                            if (queueStatusList != null)
                            {
                                if (queueStatusList.Count > 0)
                                    queueStatusList.Clear();
                            }

                            //add,xzg,2011/07/01,E

                            CpfParams cpfParam = new CpfParams();
                            cpfParam.AddLong("iStart", 0);
                            cpfParam.AddLong("iEnd", GetTimes);
                            axCpfMsg.SendEvent("", "", "AM_GET_QUECALL_LIST", cpfParam);
                        }
                        break;
                    case "AGENTCOUNT":
                        {
                            AGENTCount = recvParams.GetLongDefault("iNumberOfList", 0);
                            //added by zhu 2014/12/09
                            if (!this.ReShowsFlag)
                            {
                                this.ReShowsFlag = true;
                                this.button1.Enabled = true;
                            }
                            //end added
                            AGENTTimes = 1;

                            if (agentStatusList != null)
                            {
                                if (agentStatusList.Count > 0)
                                    agentStatusList.Clear();
                            }
                            if (groupInfoList != null)
                            {
                                if (groupInfoList.Count > 0)
                                    groupInfoList.Clear();
                                if (groupComboBox.Items.Count > 0)
                                {
                                    groupComboBox.Items.Clear();
                                    GroupInfo groupInfo = new GroupInfo(-1, res.GetString("SM0020040"));
                                    groupComboBox.Items.Add(groupInfo);
                                    groupComboBox.SelectedIndex = 0;

                                }
                            }

                            //init option
                            resetOption();

                            CpfParams cpfParam = new CpfParams();
                            cpfParam.AddLong("iStart", 0);
                            cpfParam.AddLong("iEnd", GetTimes);
                            axCpfMsg.SendEvent("", "", "AM_GET_AGENT_LIST", cpfParam);
                        }
                        break;

                    //add,xzg,2010/02/23,E----------
                    case "AGENT":
                        {
                            // Make agentStatus
                            AgentStatus agentStatus = new AgentStatus();
                            agentStatus.Group = recvParams.GetLongDefault("iSkillID", 0);
                            agentStatus.GroupName = recvParams.GetStringDefault("vSkillName", "");

                            //update,xzg,2012/02/08,S
                            //agentStatus.Agent = recvParams.GetStringDefault("vAgentID", "0000");
                            string strAgentID = recvParams.GetStringDefault("vAgentID", "");
                            if (string.IsNullOrEmpty(strAgentID))
                                return;
                            //add,2014/03,S
                            string strAgentType = "";
                            if (strAgentID.Length > 5 && !string.IsNullOrEmpty(ShowAgentF))
                            {
                                strAgentType = strAgentID.Substring(5, 1);
                                if (strAgentType != ShowAgentF)
                                {
                                    //added by zhu 2015/10/30
                                    int iMsgDone1 = recvParams.GetLongDefault("iMsgDone", 0);
                                    if (1 == iMsgDone1)
                                    {
                                        int curAgents = AGENTTimes * GetTimes;
                                        if (AGENTCount > curAgents)
                                        {
                                            CpfParams cpfParam = new CpfParams();
                                            cpfParam.AddLong("iStart", curAgents);
                                            cpfParam.AddLong("iEnd", curAgents + GetTimes);
                                            axCpfMsg.SendEvent("", "", "AM_GET_AGENT_LIST", cpfParam);
                                            AGENTTimes = AGENTTimes + 1;
                                        }
                                    }
                                    //end added

                                    return;
                                }
                            }
                            //add,2014/03,E

                            agentStatus.Agent = strAgentID;
                            //update,xzg,2012/02/08,E
                            agentStatus.AgentName = recvParams.GetStringDefault("vAgentName", "");
                            agentStatus.Extension = recvParams.GetStringDefault("vExtension", "");
                            string iElapsedTime = recvParams.GetStringDefault("iElapsedTime", "0");
                            if (iElapsedTime != "0")
                            {
                                int iTime;
                                string strTime = "";
                                iTime = int.Parse(iElapsedTime);
                                strTime = ConvertTimeHHMMSS(iTime);
                                agentStatus.StatusContinueTime = strTime;
                            }

                            //agentStatus.LoginTime = MyTool.ParseDateTime(recvParams.GetStringDefault("dtLogin", ""));
                            if (recvParams.GetStringDefault("dtLogin", "") != "0" && recvParams.GetStringDefault("dtLogin", "") != "" && recvParams.GetStringDefault("dtLogin", "") != "NONE")
                            {
                                agentStatus.LoginTime = DateTime.Parse(recvParams.GetStringDefault("dtLogin", ""));
                            }
                            agentStatus.Status = AgentStatusEnum.ConvStatusID(recvParams.GetLongDefault("iStatus", 0));
                            //agentStatus.StatusTime = MyTool.ParseDateTime(recvParams.GetStringDefault("dtStatus", ""));
                            if (recvParams.GetStringDefault("dtStatus", "") != "0" && recvParams.GetStringDefault("dtStatus", "") != "" && recvParams.GetStringDefault("dtStatus", "") != "NONE")
                            {
                                agentStatus.StatusTime = DateTime.Parse(recvParams.GetStringDefault("dtStatus", ""));
                            }
                            agentStatus.Help = recvParams.GetLongDefault("iHelp", 0) != 0;

                            //add,xzg,2013/09/11,S
                            string strHelp = "通常";
                            if (agentStatus.Help == true)
                            {
                                strHelp = "ヘルプ中";
                                if (!string.IsNullOrEmpty(agentStatus.AgentName))
                                    ShowBalloonTipHelp("ヘルプ", agentStatus.AgentName);
                            }

                            //add,xzg,2013/09/11,E

                            agentStatus.Session = recvParams.GetLongDefault("iSessionProfileID", 0);
                            agentStatus.Caller = recvParams.GetStringDefault("vCaller", "");
                            agentStatus.Elapsed = recvParams.GetLongDefault("iElapsedTime", 0);
                            //add,xzg,2011/07/04,S
                            agentStatus.iStatus = recvParams.GetLongDefault("iStatus", 0);
                            agentStatus.iSkillName = recvParams.GetStringDefault("iSkillName", "");
                            agentStatus.vReason = recvParams.GetStringDefault("vReason", "");
                            agentStatus.vMemo = recvParams.GetStringDefault("vMemo", "");
                            //add,xzg,2011/07/04,E

                            agentStatus.Option1 = recvParams.GetStringDefault("vOption1", "");
                            agentStatus.Option2 = recvParams.GetStringDefault("vOption2", "");
                            agentStatus.Option3 = recvParams.GetStringDefault("vOption3", "");
                            agentStatus.Option4 = recvParams.GetStringDefault("vOption4", "");
                            agentStatus.Option5 = recvParams.GetStringDefault("vOption5", "");
                            //added by zhu 2015/05/27
                            agentStatus.iSkillGroupID = recvParams.GetLongDefault("iSkillGroupID", 0);
                            agentStatus.Conntype = recvParams.GetStringDefault("iCallType", "");
                            //DataRow[] foundRows1 = dsMontor.Tables["dtGroupPersonal"].Select("groupId=" + agentStatus.iSkillGroupID);
                            //if(foundRows1.Length>0)
                            //{
                            //    if(!string.IsNullOrEmpty(foundRows1[0][1].ToString()))
                            //    {
                            //        agentStatus.GroupName = foundRows1[0][1].ToString();
                            //    }
                            //}
                            GroupInfo groupFind = groupInfoList.Find(delegate (GroupInfo obj)
                            { return obj.Group == agentStatus.iSkillGroupID; });

                            if (groupFind != null && !string.IsNullOrEmpty(groupFind.GroupName))
                            {
                                agentStatus.GroupName = groupFind.GroupName;
                                agentStatus.Group = agentStatus.iSkillGroupID;
                            }
                            else
                            {
                                AgentStatus agentStatusFind = agentStatusList.Find(delegate (AgentStatus obj)
                                { return obj.Agent == agentStatus.Agent; });
                                if (agentStatusFind != null)
                                {
                                    agentStatus.GroupName = agentStatusFind.GroupName;
                                    agentStatus.Group = agentStatusFind.Group;
                                }
                            }

                            //end added

                            //add,xzg,2008/05/01,S------------
                            //set status
                            int iStatus = AgentStatusEnum.ConvStatusID(recvParams.GetLongDefault("iStatus", 0));

                            //Add,2014/03,S ShowMonitorF
                            if (ShowMonitorF != "1")
                            {
                                if (SettingFields_MonitorTabShow != "0")
                                {
                                    try
                                    {
                                        DataRow[] foundRows = dsMontor.Tables["dtGroupPersonal"].Select("agentID='" + strAgentID + "'");
                                        for (int i = 0; i < foundRows.Length; i++)
                                        {
                                            //Application.DoEvents();
                                            foundRows[i][3] = iStatus;
                                        };
                                    }
                                    catch (Exception ex)
                                    {
                                        writeLog("update dtGroupPersonal Error " + ex.Message + ex.StackTrace);
                                    }
                                }

                            }

                            AgentStatusEnum status = CTe1Helper.GetAgentStatusEnum(iStatus);
                            string vStatus = res.GetString(status.StatusName);

                            int msgIndex;
                            msgIndex = -1;
                            try
                            {
                                if (msgFromID.Count > 0)
                                {
                                    msgIndex = msgFromID.IndexOf(agentStatus.Agent);
                                }
                                if (msgIndex >= 0)
                                {
                                    MessageForm msgFrom;//= new MessageForm(); //コメント
                                    msgFrom = (MessageForm)msgFromList[msgIndex];
                                    msgFrom.setStatus(vStatus);
                                    //added by zhu 2015/09/28
                                    if (iStatus == 6)
                                    {
                                        msgFrom.setStatus(agentStatus.vReason);
                                    }
                                    //end added
                                    msgFrom.Activate();
                                }
                            }
                            catch (Exception ex)
                            {
                                writeLog("MainProg:OnRecvCommand:PopMsgFrm" + ex.Message + ex.StackTrace);
                            }
                            //add,xzg,2008/05/01,E------------
                            // AddGroup
                            AddGroup(agentStatus.Group, agentStatus.GroupName);

                            AddOption(1, comboBox1.Items.Count, agentStatus.Option1);
                            AddOption(2, comboBox2.Items.Count, agentStatus.Option2);
                            AddOption(3, comboBox3.Items.Count, agentStatus.Option3);
                            AddOption(4, comboBox4.Items.Count, agentStatus.Option4);
                            AddOption(5, comboBox5.Items.Count, agentStatus.Option5);

                            AddOption(6, comboBox6.Items.Count, vStatus);
                            //AddOption(7, comboBox7.Items.Count, strHelp);


                            // Add agentStatusList
                            agentStatusList.RemoveAll(delegate (AgentStatus obj)
                                { return obj.Agent == agentStatus.Agent; });
                            if (agentStatus.Status > 0) agentStatusList.Add(agentStatus);
                            //add,xzg,2009/02/23,S--------
                            int iMsgDone = recvParams.GetLongDefault("iMsgDone", 0);
                            if (1 == iMsgDone)
                            {
                                int curAgents = AGENTTimes * GetTimes;
                                if (AGENTCount > curAgents)
                                {
                                    CpfParams cpfParam = new CpfParams();
                                    cpfParam.AddLong("iStart", curAgents);
                                    cpfParam.AddLong("iEnd", curAgents + GetTimes);
                                    axCpfMsg.SendEvent("", "", "AM_GET_AGENT_LIST", cpfParam);
                                    AGENTTimes = AGENTTimes + 1;

                                    int lastAgents = AGENTTimes * GetTimes;
                                    if (AGENTCount < lastAgents)
                                    {
                                        if (!this.ReShowsFlag)
                                        {
                                            this.ReShowsFlag = true;
                                            this.button1.Enabled = true;
                                        }
                                    }
                                }
                                else
                                {
                                    //added by zhu 2014/12/09
                                    if (!this.ReShowsFlag)
                                    {
                                        this.ReShowsFlag = true;
                                        this.button1.Enabled = true;
                                    }
                                    //end added
                                }
                            }

                            //added by zhu 2014/12/09
                            if (AGENTCount == 1)
                            {
                                if (!this.ReShowsFlag)
                                {
                                    this.ReShowsFlag = true;
                                    this.button1.Enabled = true;
                                }
                            }
                            //add,xzg,2009/02/23,E--------

                            //add,xzg,2011/09/16,S
                            //if (iMsgDone == 0)
                            //    updateAgentStatus = true;
                            //else
                            //    updateAgentStatus = false;

                            //if (AGENTTimes * GetTimes >= AGENTCount)//2014/03/10
                            updateAgentStatus = true;
                            //add,xzg,2011/09/16,E

                            //update,2014/03,S
                            ////add,xzg,2013/11/14,S
                            //listMonitorShow();
                            ////add,xzg,2013/11/14,E

                            //update,2014/03,E

                        }
                        break;
                    //add,xzg,2009/02/04,S--------
                    case "SM_STATUS":
                        {
                            int iMonitorStatus = 0;
                            string sResult = "";
                            string sReason = "";
                            iMonitorStatus = recvParams.GetLongDefault("iStatus", 0);
                            sResult = recvParams.GetStringDefault("vResult", "");
                            sReason = recvParams.GetStringDefault("vReason", "");
                            if (iMonitorStatus == 1)
                            {
                                MonitorStatus = MONITOR_STATUS_IDLE;
                            }
                            else if (iMonitorStatus == 2)
                            {
                                MonitorStatus = MONITOR_STATUS_CALLING;
                            }
                            else if (iMonitorStatus == 3)
                            {
                                if (sReason == MONITOR_TYPE_MONITOR)
                                    MonitorStatus = MONITOR_TYPE_MONITOR;
                                else if (sReason == MONITOR_TYPE_COACH)
                                    MonitorStatus = MONITOR_TYPE_COACH;
                                else if (sReason == MONITOR_TYPE_MEETING)
                                    MonitorStatus = MONITOR_TYPE_MEETING;
                            }
                            setFrmText(MonitorStatus);
                            int msgCount = 0;
                            int loop = 0;
                            msgCount = msgFromList.Count;
                            for (loop = 0; loop < msgCount; loop++)
                            {
                                MessageForm msgFrom = (MessageForm)msgFromList[loop];
                                msgFrom.setMonitorStatus(MonitorStatus);
                            }
                        }
                        break;
                    //add,xzg,2009/02/04,E--------
                    case "SM_RESET":
                        {

                            RetSetCallInfo = true;
                        }
                        break;
                    default:
                        break;
                }

                // Update Display
                //Del,xzg,2011/09/16,S
                //DisplayLine();
                //DisplayAgent();
                //DisplayTotal();
                //Del,xzg,2011/09/16,E
                //}
                //catch ( Exception ex)
                //   {
                //        Debug.WriteLine(ex.Message);
                //    }
            }
            catch (Exception ex)
            {
                writeLog("MainProg:OnRecvCommand:" + command + " " + ex.Message + ex.StackTrace);
                throw ex;
            }

        }

        public void AddGroup(int group, string groupName)
        {
            try
            {
                GroupInfo groupInfo = new GroupInfo(group, groupName);
                // Add groupInfoList
                GroupInfo groupFind = groupInfoList.Find(delegate (GroupInfo obj)
                    { return obj.Group == groupInfo.Group; });
                if (groupFind == null)
                {
                    groupInfoList.Add(groupInfo);
                    groupComboBox.Items.Add(groupInfo);
                }
            }
            catch (Exception ex)
            {
                writeLog("AddGroup:" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void AddOption(int no, int group, string groupName)
        {
            try
            {
                GroupInfo groupInfo = new GroupInfo(group, groupName);
                // Add groupInfoList

                if (no == 1)
                {
                    GroupInfo groupFind = option1InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option1InfoList.Add(groupInfo);
                        this.comboBox1.Items.Add(groupInfo);
                    }
                }
                else if (no == 2)
                {
                    GroupInfo groupFind = option2InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option2InfoList.Add(groupInfo);
                        this.comboBox2.Items.Add(groupInfo);
                    }
                }
                else if (no == 3)
                {
                    GroupInfo groupFind = option3InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option3InfoList.Add(groupInfo);
                        this.comboBox3.Items.Add(groupInfo);
                    }
                }
                else if (no == 4)
                {
                    GroupInfo groupFind = option4InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option4InfoList.Add(groupInfo);
                        this.comboBox4.Items.Add(groupInfo);
                    }
                }
                else if (no == 5)
                {
                    GroupInfo groupFind = option5InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option5InfoList.Add(groupInfo);
                        this.comboBox5.Items.Add(groupInfo);
                    }
                }
                else if (no == 6)
                {
                    GroupInfo groupFind = option6InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option6InfoList.Add(groupInfo);
                        this.comboBox6.Items.Add(groupInfo);
                    }
                }
                else if (no == 7)
                {
                    GroupInfo groupFind = option7InfoList.Find(delegate (GroupInfo obj)
                    { return obj.GroupName == groupInfo.GroupName; });
                    if (groupFind == null)
                    {
                        option7InfoList.Add(groupInfo);
                        this.comboBox7.Items.Add(groupInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                writeLog("AddOption:" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        // Display
        public void DisplayTotal()
        {
            try
            {
                // LineUseage
                if (lineUseage.LineNumber > 0)
                {
                    int useage = (int)(((double)lineUseage.BusyCount / lineUseage.LineNumber) * 100.0);
                    // Update lineUseageProgressLabel
                    lineUseageProgressLabel.Tag = useage;
                    lineUseageProgressLabel.Invalidate();
                    // useageLabel
                    useageLabel.Text = String.Format("{0:d} / {1:d}", lineUseage.BusyCount, lineUseage.LineNumber);
                }

                //add,2014/03/10,S
                int intCurrTopRow = 0;
                int loop = 0;
                string strSelectItem = "";
                if (totalListView.Items.Count > 0)
                {
                    intCurrTopRow = totalListView.TopItem.Index;
                }
                if (totalListView.SelectedItems.Count > 0)
                {
                    strSelectItem = totalListView.SelectedItems[0].SubItems["Group"].Text;
                }
                //add,2014/03/10,E
                // Update totalListView

                //added by zhu 2015/10/13
                Dictionary<int, string> dicGroupContinueTime = new Dictionary<int, string>();
                for (int i = 0; i < this.totalListView.Items.Count; i++)
                {
                    ListViewItem item = totalListView.Items[i];
                    if (item.SubItems["MaxQueCallContinueTime"] != null && !string.IsNullOrEmpty(item.SubItems["MaxQueCallContinueTime"].Text) && item.SubItems["MaxQueCallContinueTime"].Text != "00:00:00")
                    {
                        if (item.SubItems["Group"] != null)
                            dicGroupContinueTime.Add(int.Parse(item.SubItems["Group"].Text), item.SubItems["MaxQueCallContinueTime"].Text);
                    }
                }
                //end added


                totalListView.BeginUpdate();
                totalListView.Items.Clear();
                // Make totalStatusList
                List<TotalStatus> totalStatusList = new List<TotalStatus>();
                foreach (GroupInfo group in groupInfoList)
                {
                    if (!IsShowCurrentGroup(group))
                    {
                        continue;
                    }
                    TotalStatus total = new TotalStatus();
                    total.Group = group.Group;
                    total.GroupName = group.GroupName;

                    if (GetQueueCoutFromCALLLEG(group.Group) == 0)
                    {
                        total.QueueCount = 0;
                    }
                    else
                    {
                        foreach (QueueStatus queue in queueStatusList)
                        {
                            if (queue.Group != total.Group) continue;
                            total.QueueCount = queue.QueueCount;

                            //addde by zhu 2015/10/08
                            List<LineStatus> templineStatusList = new List<LineStatus>();
                            templineStatusList = lineStatusList.FindAll(p => p.iSkillGroupID == total.Group && p.Service == "QUECALL");
                            if (templineStatusList.Count > 0)
                            {
                                if (dicGroupContinueTime.ContainsKey(total.Group))
                                {
                                    total.MaxQueCallContinueTime = dicGroupContinueTime[total.Group];
                                }
                                else
                                {
                                    total.MaxQueCallContinueTime = UtilityHelper.GetContinueTime(templineStatusList[0].StatusTime);
                                }
                                total.QueCallStatusDateTime = templineStatusList[0].StatusTime;
                            }
                            //end added 
                        }
                    }
                    foreach (AgentStatus agent in agentStatusList)
                    {
                        if (agent.Group != total.Group) continue;
                        if (agent.Status == 1) total.WaitCount++;
                        else if (agent.Status >= 10) total.ConnectCount++;
                        else total.OtherCount++;
                    }


                    // Add totalStatusList
                    totalStatusList.Add(total);

                }
                // Total
                TotalStatus totalAll = new TotalStatus();
                foreach (TotalStatus total in totalStatusList)
                {
                    totalAll.WaitCount += total.WaitCount;
                    totalAll.ConnectCount += total.ConnectCount;
                    totalAll.OtherCount += total.OtherCount;
                    totalAll.QueueCount += total.QueueCount;
                }

                //add,xzg,2014/03/10,S
                QueueCountAll = totalAll.QueueCount;
                //add,xzg,2014/03/10,E



                //modified by zhu 2015/10/09 use key for subitems
                //ListViewItem itema = new ListViewItem();
                //itema.Text = res.GetString("SM0070001");
                //itema.BackColor = Color.LightCyan;
                //if (totalAll.QueueCount > alertTotal) itema.BackColor = Color.Magenta;
                //itema.SubItems.Add(totalAll.WaitCount.ToString());
                //itema.SubItems.Add(totalAll.ConnectCount.ToString());
                //itema.SubItems.Add(totalAll.OtherCount.ToString());
                //itema.SubItems.Add(totalAll.QueueCount.ToString());
                ////added by zhu 2015/10/08
                //itema.SubItems.Add(totalAll.MaxQueCallContinueTime);
                ////end added
                ////add,2014/03/10,S
                //itema.SubItems.Add("-1");

                totalAll.Group = -1;
                LineStatus minDateQueCall = lineStatusList.Find(p => p.Service == "QUECALL");
                if (minDateQueCall != null)
                {
                    totalAll.QueCallStatusDateTime = minDateQueCall.StatusTime;
                }
                else
                {
                    totalAll.QueCallStatusDateTime = DateTime.Now;
                }
                if (dicGroupContinueTime.ContainsKey(-1))
                {
                    totalAll.MaxQueCallContinueTime = dicGroupContinueTime[-1];
                }
                else
                {
                    totalAll.MaxQueCallContinueTime = UtilityHelper.GetContinueTime(totalAll.QueCallStatusDateTime);
                }
                ListViewItem itema = LoadTotalListItem(totalAll);
                itema.Text = res.GetString("SM0070001");
                if (itema.BackColor != Color.Magenta)
                    itema.BackColor = Color.LightCyan;

                //end modified


                if (!string.IsNullOrEmpty(strSelectItem) && strSelectItem == "-1")
                    itema.Selected = true;
                //add,2014/03/10,E
                totalListView.Items.Add(itema);
                //add,xzg,2013/12/10,S
                totalListView.EnsureVisible(loop);
                if (intCurrTopRow > totalListView.TopItem.Index)
                    loop++;
                //add,xzg,2013/12/10,E

                // Each Group
                foreach (TotalStatus total in totalStatusList)
                {
                    //modified by zhu 2015/10/09 use key for subitems
                    //ListViewItem iteme = new ListViewItem();
                    //iteme.Text = total.GroupName;
                    //iteme.BackColor = Color.White;
                    ////Debug.WriteLine("待ち呼:" + total.GroupName + total.QueueCount);
                    ////if(total.QueueCount > alertGroup) iteme.BackColor = Color.Pink;
                    //if (total.QueueCount > 0) iteme.BackColor = Color.Magenta;
                    //iteme.SubItems.Add(total.WaitCount.ToString());
                    //iteme.SubItems.Add(total.ConnectCount.ToString());
                    //iteme.SubItems.Add(total.OtherCount.ToString());
                    //iteme.SubItems.Add(total.QueueCount.ToString());
                    ////added by zhu 2015/10/08
                    //iteme.SubItems.Add(total.MaxQueCallContinueTime);
                    ////end added
                    //iteme.SubItems.Add(total.Group.ToString());

                    ListViewItem iteme = LoadTotalListItem(total);
                    //end modified
                    if (!string.IsNullOrEmpty(strSelectItem) && strSelectItem == total.Group.ToString())
                        iteme.Selected = true;

                    totalListView.Items.Add(iteme);

                    //add,xzg,2013/12/10,S

                    totalListView.EnsureVisible(loop);
                    if (intCurrTopRow > totalListView.TopItem.Index)
                        loop++;

                    //add,xzg,2013/12/10,E

                }
                totalListView.EndUpdate();

                //add,xzg,2014/03/10,S
                showVoiceMailQueCall();
                //add,xzg,2014/03/10,E
            }
            catch (Exception ex)
            {
                writeLog(" DiaplayTotal System error:" + ex.Message + ex.StackTrace);
            }
        }

        //public LineStatusEnum GetLineStatusEnum(int status)
        //{
        //    for (int i = 0; i < CTe1Helper.LineStatusEnums.Length; ++i)
        //    {
        //        if (CTe1Helper.LineStatusEnums[i].Status == status) return CTe1Helper.LineStatusEnums[i];
        //    }
        //    return CTe1Helper.LineStatusEnums[0];
        //}

        public void DisplayLine()
        {
            try
            {

                int i = 0;
                int j = 0;
                //add,2013/12/10,S
                int intCurrTopRow = 0;
                int loop = 0;
                string strSelectItem = "";
                if (lineStatusListView.Items.Count > 0)
                {
                    if (lineStatusListView.View == View.Details)
                        intCurrTopRow = lineStatusListView.TopItem.Index;

                }

                if (lineStatusListView.SelectedItems.Count > 0)
                {
                    strSelectItem = lineStatusListView.SelectedItems[0].SubItems["ISessionprofileID"].Text;
                }
                //add,2013/12/10,E
                for (j = 0; j < lineStatusList.Count; j++)
                {
                    LineStatus obj1 = lineStatusList[j];
                    string strKey = obj1.iSessionProfileID;

                    LineStatusEnum strStatus = CTe1Helper.GetLineStatusEnum(obj1.Status);
                    for (i = 0; i < lineStatusListView.Items.Count; i++)
                    {
                        ListViewItem item = lineStatusListView.Items[i];
                        if (strKey == item.SubItems["ISessionprofileID"].Text)
                        {
                            if (res.GetString(strStatus.StatusName) == item.SubItems["Status"].Text)
                            {
                                //obj1.StatusContinueTime = DateTime.Parse(item.SubItems[4].Text);
                                obj1.StatusContinueTime = item.SubItems["StatusContinueTime"].Text;
                            }
                            else
                            {
                                obj1.StatusContinueTime = "00:00:00";
                            }
                            break;
                        }
                    }

                }

                // Update lineStatusListView
                lineStatusListView.BeginUpdate();
                lineStatusListView.Items.Clear();
                foreach (LineStatus obj in lineStatusList)
                {
                    // Select Group
                    if ((displayGroup != -1) && (displayGroup != obj.Group)) continue;
                    // Get StatusEnum
                    // LineStatusEnum status = GetLineStatusEnum(obj.Status);
                    // Add Item
                    //ListViewItem item = new ListViewItem();
                    //item.Text = obj.GroupName;
                    //item.ImageKey = status.Image;
                    //item.BackColor = Color.White;
                    //item.SubItems.Add(obj.Caller);
                    //item.SubItems.Add(obj.Callee);
                    //item.SubItems.Add(obj.ConnectedTime.ToString("HH:mm:ss"));
                    //item.SubItems.Add(res.GetString(status.StatusName));
                    //item.SubItems.Add(obj.StatusTime.ToString("HH:mm:ss"));
                    ////add,xzg,2012/02/09,S
                    //if (string.IsNullOrEmpty(obj.StatusContinueTime))
                    //{
                    //    item.SubItems.Add("00:00:00");
                    //}
                    //else
                    //{
                    //    //modified by zhu 2014/09/02
                    //    // make sure the time is not bigger than current time

                    //    try
                    //    {
                    //        if (DateTime.Compare(DateTime.Now, obj.StatusTime) <= 0)
                    //        {
                    //            item.SubItems.Add("00:00:00");
                    //        }
                    //        else
                    //        {
                    //            if (obj.Service.ToUpper() == "QUECALL" && DateTime.Compare(Convert.ToDateTime(DateTime.Now.Subtract(obj.StatusTime).ToString()), DateTime.Parse(obj.StatusContinueTime)) < 0)
                    //            {
                    //                string strTime = Convert.ToDateTime(DateTime.Now.Subtract(obj.StatusTime).ToString()).ToString("HH:mm:ss");
                    //                item.SubItems.Add(strTime);
                    //            }
                    //            else
                    //            {
                    //                item.SubItems.Add(obj.StatusContinueTime);
                    //            }
                    //        }
                    //    }
                    //    catch (Exception ee1)
                    //    {
                    //        item.SubItems.Add("00:00:00");
                    //    }
                    //}


                    ////add,xzg,2012/02/09,E
                    //if (obj.Status >= 3) item.SubItems.Add(obj.Extension);
                    //else item.SubItems.Add(obj.Service);


                    //item.SubItems.Add(obj.iSessionProfileID);


                    ListViewItem item = LoadDataForLineStatusListView(obj);
                    //add,xzg,2013/12/10,S
                    if (!string.IsNullOrEmpty(strSelectItem) && strSelectItem == obj.iSessionProfileID)
                        item.Selected = true;
                    //add,xzg,2013/12/10,E


                    lineStatusListView.Items.Add(item);

                    //add,xzg,2013/12/10,S
                    if (lineStatusListView.View == View.Details)
                    {
                        lineStatusListView.EnsureVisible(loop);
                        if (intCurrTopRow > lineStatusListView.TopItem.Index)
                            loop++;
                    }
                    //add,xzg,2013/12/10,E
                }
                lineStatusListView.EndUpdate();
            }
            catch (Exception ex)
            {
                writeLog("DisplayLine System Error:" + ex.Message + ex.StackTrace);
            }
        }

        //public AgentStatusEnum GetAgentStatusEnum(int status)
        //{
        //    for (int i = 0; i < agentStatusEnum.Length; ++i)
        //    {
        //        if (agentStatusEnum[i].Status == status) return agentStatusEnum[i];
        //    }
        //    return agentStatusEnum[0];
        //}

        public void DisplayAgent()
        {
            // Update agentStatusListView
            //add,xzg,2009/10/20,S------------
            try
            {
                int i = 0;
                int j = 0;
                int iHelpON = 0;
                int workCount = 0;
                int waitCount = 0;
                int seatLeaveCount = 0;
                int holdCount = 0;
                int telCount = 0;
                int offeringCount = 0;
                int makeCallCount = 0;
                int transCallCount = 0;

                //add,xzg,2013/08/27,S
                int intCurrTopRow = 0;
                int loop = 0;
                string strSelectItem = "";
                if (agentStatusListView.Items.Count > 0)
                {
                    if (agentStatusListView.View == View.Details)
                        intCurrTopRow = agentStatusListView.TopItem.Index;
                    //System.Console.WriteLine(intCurrTopRow);
                }

                if (agentStatusListView.SelectedItems.Count > 0)
                {
                    //modified zhu 2015/09/11
                    //strSelectItem = agentStatusListView.SelectedItems[0].SubItems[15].Text;//modified by zhu 2014/04/17 14->15
                    strSelectItem = agentStatusListView.SelectedItems[0].SubItems["Agent"].Text;
                }
                //add,xzg,2013/08/27,E
                for (j = 0; j < agentStatusList.Count; j++)
                {
                    AgentStatus obj1 = agentStatusList[j];
                    string strAgent = obj1.Agent;
                    AgentStatusEnum strStatus = CTe1Helper.GetAgentStatusEnum(obj1.Status);
                    for (i = 0; i < agentStatusListView.Items.Count; i++)
                    {
                        ListViewItem item = agentStatusListView.Items[i];
                        if (strAgent == item.SubItems["Agent"].Text)
                        {
                            if (strStatus.Status.ToString() == item.SubItems["Status"].Text)
                            {
                                obj1.StatusContinueTime = item.SubItems["StatusContinueTime"].Text;
                            }
                            else
                            {
                                obj1.StatusContinueTime = "00:00:00";
                            }
                            break;
                        }
                    }

                }
                //add,xzg,2009/10/20,E------------
                agentStatusListView.BeginUpdate();
                agentStatusListView.Items.Clear();

                foreach (AgentStatus obj in agentStatusList)
                {

                    // Select Group
                    if ((displayGroup != -1) && (displayGroup != obj.Group)) continue;
                    //select option
                    GroupInfo groupInfo1 = (GroupInfo)this.comboBox1.SelectedItem;
                    if ((groupInfo1.Group != -1) && (groupInfo1.GroupName != obj.Option1)) continue;

                    GroupInfo groupInfo2 = (GroupInfo)this.comboBox2.SelectedItem;
                    if ((groupInfo2.Group != -1) && (groupInfo2.GroupName != obj.Option2)) continue;

                    GroupInfo groupInfo3 = (GroupInfo)this.comboBox3.SelectedItem;
                    if ((groupInfo3.Group != -1) && (groupInfo3.GroupName != obj.Option3)) continue;

                    GroupInfo groupInfo4 = (GroupInfo)this.comboBox4.SelectedItem;
                    if ((groupInfo4.Group != -1) && (groupInfo4.GroupName != obj.Option4)) continue;

                    GroupInfo groupInfo5 = (GroupInfo)this.comboBox5.SelectedItem;
                    if ((groupInfo5.Group != -1) && (groupInfo5.GroupName != obj.Option5)) continue;




                    // Get StatusEnum
                    AgentStatusEnum status = CTe1Helper.GetAgentStatusEnum(obj.Status);
                    // Add Item
                    ListViewItem item = new ListViewItem();
                    //item.Text = obj.GroupName;
                    item.Name = "AgentName";
                    item.Text = obj.AgentName;
                    item.ImageKey = status.Image;
                    item.BackColor = Color.White;
                    if (obj.Help)
                    {
                        //modified by zhu 2015/10/23 change help backcolor to green
                        //item.BackColor = Color.Pink;
                        item.BackColor = CTe1Helper.AgentHelpColor;
                    }


                    //option

                    //item.SubItems.Add(obj.Option1);
                    //item.SubItems.Add(obj.Option2);
                    //item.SubItems.Add(obj.Option3);
                    //item.SubItems.Add(obj.Option4);
                    //item.SubItems.Add(obj.Option5);
                    //item.SubItems.Add(obj.Extension);
                    //item.SubItems.Add(obj.GroupName);

                    ListViewItem.ListViewSubItem subOption1 = new ListViewItem.ListViewSubItem(item, obj.Option1);
                    subOption1.Name = "Option1";
                    item.SubItems.Add(subOption1);

                    ListViewItem.ListViewSubItem subOption2 = new ListViewItem.ListViewSubItem(item, obj.Option2);
                    subOption2.Name = "Option2";
                    item.SubItems.Add(subOption2);

                    ListViewItem.ListViewSubItem subOption3 = new ListViewItem.ListViewSubItem(item, obj.Option3);
                    subOption3.Name = "Option3";
                    item.SubItems.Add(subOption3);

                    ListViewItem.ListViewSubItem subOption4 = new ListViewItem.ListViewSubItem(item, obj.Option4);
                    subOption4.Name = "Option4";
                    item.SubItems.Add(subOption4);

                    ListViewItem.ListViewSubItem subOption5 = new ListViewItem.ListViewSubItem(item, obj.Option5);
                    subOption5.Name = "Option5";
                    item.SubItems.Add(subOption5);

                    ListViewItem.ListViewSubItem subExtension = new ListViewItem.ListViewSubItem(item, obj.Extension);
                    subExtension.Name = "Extension";
                    item.SubItems.Add(subExtension);

                    ListViewItem.ListViewSubItem subGroupName = new ListViewItem.ListViewSubItem(item, obj.GroupName);
                    subGroupName.Name = "GroupName";
                    item.SubItems.Add(subGroupName);




                    //update,xzg,2011/07/04,S
                    //item.SubItems.Add(res.GetString(status.StatusName));
                    //離席理由表示
                    ListViewItem.ListViewSubItem subState = new ListViewItem.ListViewSubItem(item, "");
                    string strState = "";
                    if (!string.IsNullOrEmpty(obj.vReason) && obj.Status == 6)
                        strState = obj.vReason;
                    else
                        strState = res.GetString(status.StatusName);
                    subState.Text = strState;
                    subState.Name = "State";
                    item.SubItems.Add(subState);

                    string strHelp = "";
                    if (obj.Help) strHelp = "ヘルプ中";
                    else strHelp = "通常";


                    GroupInfo groupInfo6 = (GroupInfo)this.comboBox6.SelectedItem;
                    if ((groupInfo6.Group != -1) && (groupInfo6.GroupName != res.GetString(status.StatusName))) continue;


                    GroupInfo groupInfo7 = (GroupInfo)this.comboBox7.SelectedItem;
                    if ((groupInfo7.Group != -1) && (groupInfo7.GroupName != strHelp)) continue;
                    //add,xzg,2013/09/11,E
                    ListViewItem.ListViewSubItem subStatusTime = new ListViewItem.ListViewSubItem(item, obj.StatusTime.ToString("HH:mm:ss"));
                    subStatusTime.Name = "StatusTime";
                    item.SubItems.Add(subStatusTime);

                    ListViewItem.ListViewSubItem subStatusContinueTime = new ListViewItem.ListViewSubItem(item, "");
                    subStatusContinueTime.Name = "StatusContinueTime";
                    if (string.IsNullOrEmpty(obj.StatusContinueTime))
                    {
                        subStatusContinueTime.Text = "00:00:00";
                    }
                    else
                    {
                        subStatusContinueTime.Text = obj.StatusContinueTime;
                        if (!obj.Help)
                        {
                            item.BackColor = GetAgentListItemBackColor(obj.StatusContinueTime, obj.Status);
                        }
                    }
                    item.SubItems.Add(subStatusContinueTime);

                    ListViewItem.ListViewSubItem connType = new ListViewItem.ListViewSubItem(item, CTe1Helper.GetConnType(obj.Conntype));
                    if (obj.Status == 5 || obj.Status == 6 || obj.Status == 1 || obj.Status == 0)
                    {
                        connType.Text = "";
                    }
                    connType.Name = "Conntype";
                    item.SubItems.Add(connType);

                    ListViewItem.ListViewSubItem subCaller = new ListViewItem.ListViewSubItem(item, obj.Caller);
                    subCaller.Name = "Caller";
                    item.SubItems.Add(subCaller);

                    ListViewItem.ListViewSubItem subHelp = new ListViewItem.ListViewSubItem(item, obj.Help ? "○" : "-");
                    subHelp.Name = "Help";
                    item.SubItems.Add(subHelp);

                    //add,xzg,
                    if (obj.Help == true)
                    {
                        iHelpON++;
                    }

                    ListViewItem.ListViewSubItem subLoginTime = new ListViewItem.ListViewSubItem(item, obj.LoginTime.ToString("HH:mm:ss"));
                    subLoginTime.Name = "LoginTime";
                    item.SubItems.Add(subLoginTime);

                    ListViewItem.ListViewSubItem subMemo = new ListViewItem.ListViewSubItem(item, "");
                    subMemo.Name = "Memo";
                    if (!string.IsNullOrEmpty(obj.vMemo))
                        subMemo.Text = obj.vMemo;
                    item.SubItems.Add(subMemo);


                    ListViewItem.ListViewSubItem subAgent = new ListViewItem.ListViewSubItem(item, obj.Agent);
                    subAgent.Name = "Agent";
                    item.SubItems.Add(subAgent);

                    ListViewItem.ListViewSubItem subStatus = new ListViewItem.ListViewSubItem(item, obj.Status.ToString());
                    subStatus.Name = "Status";
                    item.SubItems.Add(subStatus);

                    if (obj.Status == 1) //Wait
                        waitCount = waitCount + 1;
                    else if (obj.Status == 5) //WorkTime
                        workCount = workCount + 1;
                    else if (obj.Status == 6) //SeatOff
                        seatLeaveCount = seatLeaveCount + 1;
                    else if (obj.Status == 30) //Hold
                        holdCount = holdCount + 1;
                    else if (obj.Status == 10) //Tel
                        telCount = telCount + 1;
                    else if (obj.Status == 20) //makecall
                        makeCallCount = makeCallCount + 1;
                    else if (obj.Status == 3) //offering
                        offeringCount = offeringCount + 1;
                    else if (obj.Status == 40) //trancall
                        transCallCount = transCallCount + 1;
                    //add,xzg,2013/08/27,S
                    if (!string.IsNullOrEmpty(strSelectItem) && strSelectItem == obj.Agent)
                        item.Selected = true;
                    //add,xzg,2013/08/27,E
                    agentStatusListView.Items.Add(item);
                    //add,xzg,2013/08/27,S
                    if (agentStatusListView.View == View.Details)
                    {
                        agentStatusListView.EnsureVisible(loop);
                        if (intCurrTopRow > agentStatusListView.TopItem.Index)
                            loop++;
                    }
                    //add,xzg,2013/08/27,E
                }
                this.lblHelpON.Text = iHelpON.ToString();

                agentStatusListView.EndUpdate();

                //add,xzg,2013/08/27,S

                //intCurrTopRow = agentStatusListView.TopItem.Index;
                //agentStatusListView.EnsureVisible( intCurrTopRow);

                //add,xzg,2013/08/27,E
                if (waitCount > 0)
                    agentIconListView.Items["Wait"].Text = res.GetString("SM0020047") + "(" + waitCount.ToString() + ")";
                else
                    agentIconListView.Items["Wait"].Text = res.GetString("SM0020047");

                if (workCount > 0)
                    agentIconListView.Items["Worktime"].Text = res.GetString("SM0020050") + "(" + workCount.ToString() + ")";//modified by zhu 2014/05/29 change index from 3 to 2
                else
                    agentIconListView.Items["Worktime"].Text = res.GetString("SM0020050");//modified by zhu 2014/05/29 change index from 3 to 2

                if (seatLeaveCount > 0)
                    agentIconListView.Items["SeatOff"].Text = res.GetString("SM0020051") + "(" + seatLeaveCount.ToString() + ")";//modified by zhu 2014/05/29 change index from 4 to 3
                else
                    agentIconListView.Items["SeatOff"].Text = res.GetString("SM0020051");//modified by zhu 2014/05/29 change index from 4 to 3

                if (telCount > 0)
                    agentIconListView.Items["Connect"].Text = res.GetString("SM0020053") + "(" + telCount.ToString() + ")";//modified by zhu 2014/05/29 change index from 6 to 4
                else
                    agentIconListView.Items["Connect"].Text = res.GetString("SM0020053");//modified by zhu 2014/05/29 change index from 6 to 4

                if (holdCount > 0)
                    agentIconListView.Items["Hold"].Text = res.GetString("SM0020055") + "(" + holdCount.ToString() + ")";//modified by zhu 2014/05/29 change index from 8 to 6
                else
                    agentIconListView.Items["Hold"].Text = res.GetString("SM0020055");//modified by zhu 2014/05/29 change index from 8 to 6

                if (makeCallCount > 0)
                    agentIconListView.Items["Calling"].Text = res.GetString("SM0020054") + "(" + makeCallCount.ToString() + ")";
                else
                    agentIconListView.Items["Calling"].Text = res.GetString("SM0020054");

                if (offeringCount > 0)
                    agentIconListView.Items["Offering"].Text = res.GetString("SM0020049") + "(" + offeringCount.ToString() + ")";
                else
                    agentIconListView.Items["Offering"].Text = res.GetString("SM0020049");

                if (transCallCount > 0)
                    agentIconListView.Items["Transfer"].Text = res.GetString("SM0020056") + "(" + transCallCount.ToString() + ")";
                else
                    agentIconListView.Items["Transfer"].Text = res.GetString("SM0020056");

                //added by zhu add pie

                CurrentAgentPie = CreateAgentPie(waitCount, workCount, telCount, holdCount, offeringCount, seatLeaveCount, makeCallCount, transCallCount);
                if (agentPie.Visible)
                {
                    agentPie.Image = CurrentAgentPie;
                }

                //end adde 
            }
            catch (Exception ex)
            {
                writeLog("DisplayAgent System error:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callInfo"></param>
        /// <returns></returns>
        private ListViewItem LoadDataForLineStatusListView(LineStatus callInfo)
        {
            // Get StatusEnum
            LineStatusEnum status = CTe1Helper.GetLineStatusEnum(callInfo.Status);
            // Add Item
            ListViewItem item = new ListViewItem();
            item.Text = callInfo.GroupName;
            item.Name = "GroupName";
            item.ImageKey = status.Image;
            item.BackColor = Color.White;

            //着信or発信
            ListViewItem.ListViewSubItem connType = new ListViewItem.ListViewSubItem(item, CTe1Helper.GetConnType(callInfo.Conntype));
            connType.Name = "Conntype";
            item.SubItems.Add(connType);

            ListViewItem.ListViewSubItem subCaller = new ListViewItem.ListViewSubItem(item, callInfo.Caller);
            subCaller.Name = "Caller";
            item.SubItems.Add(subCaller);

            ListViewItem.ListViewSubItem subCallee = new ListViewItem.ListViewSubItem(item, callInfo.Callee);
            subCallee.Name = "Callee";
            item.SubItems.Add(subCallee);

            ListViewItem.ListViewSubItem subConnectedTime = new ListViewItem.ListViewSubItem(item, callInfo.ConnectedTime.ToString("HH:mm:ss"));
            subConnectedTime.Name = "ConnectedTime";
            item.SubItems.Add(subConnectedTime);


            ListViewItem.ListViewSubItem subStatus = new ListViewItem.ListViewSubItem(item, res.GetString(status.StatusName));
            subStatus.Name = "Status";
            item.SubItems.Add(subStatus);

            ListViewItem.ListViewSubItem subStatusTime = new ListViewItem.ListViewSubItem(item, callInfo.StatusTime.ToString("HH:mm:ss"));
            subStatusTime.Name = "StatusTime";
            item.SubItems.Add(subStatusTime);

            ListViewItem.ListViewSubItem subStatusContinueTime = new ListViewItem.ListViewSubItem(item, "");
            subStatusContinueTime.Name = "StatusContinueTime";

            if (string.IsNullOrEmpty(callInfo.StatusContinueTime))
            {
                subStatusContinueTime.Text = "00:00:00";
            }
            else
            {
                try
                {
                    if (DateTime.Compare(DateTime.Now, callInfo.StatusTime) <= 0)
                    {
                        subStatusContinueTime.Text = "00:00:00";
                    }
                    else
                    {
                        if (callInfo.Service.ToUpper() == "QUECALL" && DateTime.Compare(Convert.ToDateTime(DateTime.Now.Subtract(callInfo.StatusTime).ToString()), DateTime.Parse(callInfo.StatusContinueTime)) < 0)
                        {
                            string strTime = Convert.ToDateTime(DateTime.Now.Subtract(callInfo.StatusTime).ToString()).ToString("HH:mm:ss");
                            //item.BackColor = this.MainForm.GetQueCallListItemBackColor(strTime);
                            subStatusContinueTime.Text = strTime;
                        }
                        else if (callInfo.Service.ToUpper() == "QUECALL")
                        {
                            subStatusContinueTime.Text = callInfo.StatusContinueTime;
                            //item.BackColor = this.MainForm.GetQueCallListItemBackColor(callInfo.StatusContinueTime);
                        }
                        else
                        {
                            subStatusContinueTime.Text = callInfo.StatusContinueTime;
                        }
                    }
                }
                catch (Exception ee1)
                {
                    subStatusContinueTime.Text = "00:00:00";
                }
            }
            item.SubItems.Add(subStatusContinueTime);


            ListViewItem.ListViewSubItem subService = new ListViewItem.ListViewSubItem(item, "");
            subService.Name = "Service";
            if (callInfo.Status >= 3)
            {
                if (string.IsNullOrEmpty(callInfo.vAgentID))
                {
                    subService.Text = callInfo.Extension;
                }
                else
                {
                    var agentStatus = agentStatusList.Find(p => p.Agent == callInfo.vAgentID);
                    if (agentStatus != null)
                    {
                        subService.Text = agentStatus.AgentName;
                    }
                    else
                    {
                        subService.Text = callInfo.Extension;
                    }
                }

            }

            else
                subService.Text = callInfo.Service;
            item.SubItems.Add(subService);


            ListViewItem.ListViewSubItem subISessionprofileID = new ListViewItem.ListViewSubItem(item, callInfo.iSessionProfileID);
            subISessionprofileID.Name = "ISessionprofileID";
            item.SubItems.Add(subISessionprofileID);

            return item;
        }


        /// <summary>
        /// get current queue call count for current group
        /// </summary>
        /// <returns></returns>
        private int GetQueueCoutFromCALLLEG(int group)
        {
            int count = 0;
            foreach (LineStatus line in lineStatusList)
            {
                if (line.Group == group)
                {
                    if (line.Service.ToUpper() == "QUECALL")
                    {
                        count++;
                        return count;
                    }
                }
                if (line.iSkillGroupID == group)
                {
                    if (line.Service.ToUpper() == "QUECALL")
                    {
                        count++;
                        return count;
                    }
                }
            }
            return count;
        }

        //added by zhu 2014/06/27
        private void DoCommandUpdate()
        {
            while (true)
            {

                DoCommandAgent();

                DoCommandCallege();

                DoCommandQueue();

            }
        }

        private void DoCommandAgent()
        {
            AgentStatus agentStatus = null;
            //lock (ListAgentCommand)
            //{
            if (ListAgentCommand.Count > 0)
            {
                agentStatus = ListAgentCommand[0];
            }
            //}

            if (agentStatus == null)
            {
                return;
            }

            DataRow[] foundRows = dsMontor.Tables["dtGroupPersonal"].Select("agentID='" + agentStatus.Agent + "'");
            for (int i = 0; i < foundRows.Length; i++)
            {
                foundRows[i][3] = agentStatus.Status;
            }
            listMonitorShow();


            lock (ListAgentCommand)
            {
                ListAgentCommand.RemoveAt(0);
            }

        }

        private void DoCommandCallege()
        {
            LineStatus lineStatus = null;
            //lock (ListCallCommand)
            //{
            if (ListCallCommand.Count > 0)
            {
                lineStatus = ListCallCommand[0];
            }
            //}

            if (lineStatus == null)
            {
                return;
            }

            setMonitorCall(lineStatus);

            lock (ListCallCommand)
            {
                ListCallCommand.RemoveAt(0);
            }
        }

        private void DoCommandQueue()
        {
            QueueStatus queueStatus = null;
            //lock (this.ListQueueCommand)
            //{
            if (ListQueueCommand.Count > 0)
            {
                queueStatus = ListQueueCommand[0];
            }
            //}

            if (queueStatus == null)
            {
                return;
            }
            setMonitorQue(queueStatus);

            lock (ListQueueCommand)
            {
                ListQueueCommand.RemoveAt(0);
            }
        }
        //end added
    }
}