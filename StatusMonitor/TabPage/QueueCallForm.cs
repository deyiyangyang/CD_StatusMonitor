using Cpfmsgacxa;
using StatusMonitor.Helper;
using StatusMonitor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StatusMonitor.TabPage
{
    public partial class QueueCallForm : Form
    {
        private MainForm _MainForm;
        private LanguageResourceManager _resourceManager;
        private bool[] lineStatusListViewOrder = new bool[8];
        private Dictionary<string, int> DicListViewColumnWidth = new Dictionary<string, int>();
        public QueueCallForm(Form frm)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this._MainForm = (frm as MainForm);
            this._resourceManager = new LanguageResourceManager("JP");
            InitParentGroup();
            InitListView();
            AutoCtlSize();
        }

        private void InitListView()
        {
            //this.lineStatusListView.LargeImageList = _MainForm.largeImageList;
            this.quecallStatusListView.SmallImageList = _MainForm.smallImageList;
            quecallStatusListView.View = View.Details;
            quecallStatusListView.TileSize = new Size(130, 110);
            quecallStatusListView.MultiSelect = false;
            quecallStatusListView.FullRowSelect = true;
            quecallStatusListView.Columns.Add("GroupName", _resourceManager.GetString("SM0020026"), 100, HorizontalAlignment.Left, -1);
            quecallStatusListView.Columns.Add("Caller", _resourceManager.GetString("SM0020027"), 100, HorizontalAlignment.Center, -1);
            quecallStatusListView.Columns.Add("Callee", _resourceManager.GetString("SM0020028"), 100, HorizontalAlignment.Center, -1);
            quecallStatusListView.Columns.Add("ConnectedTime", _resourceManager.GetString("SM0020029"), 80, HorizontalAlignment.Center, -1);
            //lineStatusListView.Columns.Add("Status", _resourceManager.GetString("SM0020030"), 60, HorizontalAlignment.Center, -1);
            quecallStatusListView.Columns.Add("StatusTime", _resourceManager.GetString("SM0020031"), 80, HorizontalAlignment.Center, -1);
            quecallStatusListView.Columns.Add("StatusContinueTime", _resourceManager.GetString("SM0020061"), 80, HorizontalAlignment.Center, -1);
            //lineStatusListView.Columns.Add("Service", _resourceManager.GetString("SM0020032"), 100, HorizontalAlignment.Center, -1);

            lineStatusListViewOrder = new bool[quecallStatusListView.Columns.Count];
            quecallStatusListView.ListViewItemSorter = new StatusListViewItemComparer(0, true);
            FormHelper.SetDoubleBuffered(quecallStatusListView, true);
            InitListViewWidth();
        }

        private void InitParentGroup()
        {
            this.quecallDDLParentGroup.Items.Clear();

            GroupInfo groupInfo1 = new GroupInfo(-1, _resourceManager.GetString("SM0020040"));
            this.quecallDDLParentGroup.Items.Add(groupInfo1);
            this.quecallDDLParentGroup.SelectedIndex = 0;
            //foreach (var item in _MainForm.DicParentGroup)
            //{
            //    string[] values = item.Key.Split(',');
            //    GroupInfo gp = new GroupInfo(int.Parse(values[0]), values[1]);
            //    this.quecallDDLParentGroup.Items.Add(gp);
            //}

        }

        private void InitListViewWidth()
        {
            foreach (ColumnHeader col in quecallStatusListView.Columns)
            {
                DicListViewColumnWidth.Add(col.Name, col.Width);
            }
        }

        private void AutoCtlSize()
        {
            Size deskTopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            Control inObj = quecallStatusListView;
            //Single fontSize = inObj.Font.Size * deskTopSize.Height / 768;
            inObj.Size = new Size((int)(inObj.Size.Width * deskTopSize.Width / 1024), (int)(inObj.Size.Height * deskTopSize.Height / 768));
            inObj.Location = new Point((int)(inObj.Location.X * deskTopSize.Width / 1024), (int)(inObj.Location.Y * deskTopSize.Height / 768));
        }

        public void DisplayLine()
        {
            try
            {
                return;
                List<LineStatus> lineStatusList = new List<LineStatus>();
                lineStatusList = _MainForm.lineStatusList.FindAll(p => p.Service == "QUECALL");

                int i = 0;
                int j = 0;
                //add,2013/12/10,S
                int intCurrTopRow = 0;
                int loop = 0;
                string strSelectItem = "";
                if (quecallStatusListView.Items.Count > 0)
                {
                    if (quecallStatusListView.View == View.Details)
                        intCurrTopRow = quecallStatusListView.TopItem.Index;

                }

                if (quecallStatusListView.SelectedItems.Count > 0)
                {
                    strSelectItem = quecallStatusListView.SelectedItems[0].SubItems["ISessionprofileID"].Text; //index is 6
                }
                //add,2013/12/10,E
                for (j = 0; j < lineStatusList.Count; j++)
                {
                    LineStatus obj1 = lineStatusList[j];
                    string strKey = obj1.iSessionProfileID;

                    LineStatusEnum strStatus = CTe1Helper.GetLineStatusEnum(obj1.Status);
                    for (i = 0; i < quecallStatusListView.Items.Count; i++)
                    {
                        ListViewItem item = quecallStatusListView.Items[i];
                        if (strKey == item.SubItems["ISessionprofileID"].Text)//index is 6
                        {
                            obj1.StatusContinueTime = item.SubItems["StatusContinueTime"].Text; //index is 5
                            break;
                        }
                    }

                }

                // Update lineStatusListView
                quecallStatusListView.BeginUpdate();
                quecallStatusListView.Items.Clear();
                foreach (LineStatus obj in lineStatusList)
                {
                    // Select Group
                    if ((_MainForm.displayGroup != -1) && (_MainForm.displayGroup != obj.Group)) continue;
                    // Get StatusEnum
                    LineStatusEnum status = CTe1Helper.GetLineStatusEnum(obj.Status);

                    ListViewItem item = LoadDataForListView(obj);
                    if (item == null) continue;

                    //add,xzg,2013/12/10,S
                    if (!string.IsNullOrEmpty(strSelectItem) && strSelectItem == obj.iSessionProfileID)
                        item.Selected = true;
                    //add,xzg,2013/12/10,E


                    quecallStatusListView.Items.Add(item);

                    //add,xzg,2013/12/10,S
                    if (quecallStatusListView.View == View.Details)
                    {
                        quecallStatusListView.EnsureVisible(loop);
                        if (intCurrTopRow > quecallStatusListView.TopItem.Index)
                            loop++;
                    }
                    //add,xzg,2013/12/10,E
                }
                quecallStatusListView.EndUpdate();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DisplayLine System Error:" + ex.Message + ex.StackTrace);
            }
        }

        private ListViewItem LoadDataForListView(LineStatus queCallInfo)
        {
            if (quecallDDLParentGroup.SelectedIndex > 0)
            {
                bool findSkill = false;
                string key = (quecallDDLParentGroup.SelectedItem as GroupInfo).Group.ToString() + "," + (quecallDDLParentGroup.SelectedItem as GroupInfo).GroupName.ToString();
                foreach (var skillid in _MainForm.DicParentGroup[key].Split(','))
                {
                    if (queCallInfo.iSkillGroupID.ToString() == skillid || queCallInfo.Group.ToString()==skillid)
                    {
                        findSkill = true;
                        break;
                    }
                }
                if (!findSkill) return null;

            }
            // Add Item
            string skillGroupName = GetGroupNameBySkillGroupId(queCallInfo.iSkillGroupID.ToString());
            if (string.IsNullOrEmpty(skillGroupName))
                return null;

            LineStatusEnum status = CTe1Helper.GetLineStatusEnum(queCallInfo.Status);
            ListViewItem item = new ListViewItem();
            item.Text = skillGroupName;
            item.Name = "GroupName";
            item.ImageKey = status.Image;
            item.BackColor = Color.White;


            ListViewItem.ListViewSubItem subCaller = new ListViewItem.ListViewSubItem(item, queCallInfo.Caller);
            subCaller.Name = "Caller";
            item.SubItems.Add(subCaller);

            ListViewItem.ListViewSubItem subCallee = new ListViewItem.ListViewSubItem(item, queCallInfo.Callee);
            subCallee.Name = "Callee";
            item.SubItems.Add(subCallee);

            ListViewItem.ListViewSubItem subConnectedTime = new ListViewItem.ListViewSubItem(item, queCallInfo.ConnectedTime.ToString("HH:mm:ss"));
            subConnectedTime.Name = "ConnectedTime";
            item.SubItems.Add(subConnectedTime);

            ListViewItem.ListViewSubItem subStatusTime = new ListViewItem.ListViewSubItem(item, queCallInfo.StatusTime.ToString("HH:mm:ss"));
            subStatusTime.Name = "StatusTime";
            item.SubItems.Add(subStatusTime);

            ListViewItem.ListViewSubItem subStatusContinueTime = new ListViewItem.ListViewSubItem(item, "");
            subStatusContinueTime.Name = "StatusContinueTime";

            if (string.IsNullOrEmpty(queCallInfo.StatusContinueTime))
            {
                subStatusContinueTime.Text = "00:00:00";
            }
            else
            {
                try
                {
                    if (DateTime.Compare(DateTime.Now, queCallInfo.StatusTime) <= 0)
                    {
                        subStatusContinueTime.Text = "00:00:00";
                    }
                    else
                    {
                        if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.Subtract(queCallInfo.StatusTime).ToString()), DateTime.Parse(queCallInfo.StatusContinueTime)) < 0)
                        {
                            subStatusContinueTime.Text = Convert.ToDateTime(DateTime.Now.Subtract(queCallInfo.StatusTime).ToString()).ToString("HH:mm:ss");
                        }
                        else
                        {
                            subStatusContinueTime.Text = queCallInfo.StatusContinueTime;
                        }
                    }
                }
                catch (Exception ee1)
                {
                    subStatusContinueTime.Text = "00:00:00";
                }
            }
            item.SubItems.Add(subStatusContinueTime);

            SetRowBackColor(subStatusContinueTime.Text, item);

            ListViewItem.ListViewSubItem subISessionprofileID = new ListViewItem.ListViewSubItem(item, queCallInfo.iSessionProfileID);
            subISessionprofileID.Name = "ISessionprofileID";
            item.SubItems.Add(subISessionprofileID);

            return item;
        }


        private void lineStatusListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool order = !lineStatusListViewOrder[e.Column];
            lineStatusListViewOrder[e.Column] = order;
            ((ListView)sender).ListViewItemSorter = new StatusListViewItemComparer(e.Column, order);
        }

        private void NoSelectListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count != 0)
            {
                //lv.SelectedItems[0].Focused = false;
                //lv.SelectedItems[0].Selected = false;
            }
        }
        public void DoContinueLineStatus()
        {
            try
            {
                quecallStatusListView.BeginUpdate();
                int listCount = quecallStatusListView.Items.Count;
                int i = 0;
                string strTime = "00:00:00";
                for (i = 0; i < listCount; i++)
                {
                    ListViewItem item = quecallStatusListView.Items[i];
                    //modified by zhu 2014/09/01

                    try
                    {
                        if (item.SubItems["StatusTime"] != null && !string.IsNullOrEmpty(item.SubItems["StatusTime"].Text)) //index is 4
                        {
                            DateTime statusTime = DateTime.Parse(item.SubItems["StatusTime"].Text);//index is 4
                            if (DateTime.Compare(DateTime.Now, statusTime) <= 0)
                            {
                                quecallStatusListView.Items[i].SubItems["StatusContinueTime"].Text = "00:00:00"; //index is 5
                            }
                            else
                            {
                                strTime = Convert.ToDateTime(DateTime.Now.Subtract(statusTime).ToString()).ToString("HH:mm:ss");
                                quecallStatusListView.Items[i].SubItems["StatusContinueTime"].Text = strTime;//index is 5
                            }
                        }
                        else
                        {
                            quecallStatusListView.Items[i].SubItems["StatusContinueTime"].Text = "00:00:00"; // index is 5
                        }
                    }
                    catch (Exception ee1)
                    {
                        quecallStatusListView.Items[i].SubItems["StatusContinueTime"].Text = "00:00:00"; // index is 5
                    }

                    //added by zhu 2014/09/11
                    SetRowBackColor(strTime, quecallStatusListView.Items[i]);
                    //end added

                }
                quecallStatusListView.EndUpdate();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DoContinueLineStatus System error:" + ex.Message);
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

                iTime = int.Parse(sumSS) + this._MainForm.refreshTime;
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

        private void lineStatusListView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseButtons.Right == e.Button)
                {
                    //System.Diagnostics.Trace.WriteLine("lineStatusListView_MouseClick");
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
                    if (strPickUpID.Length < 1)
                    {
                        return;
                    }
                    //this._MainForm.LineID = strPickUpID;
                    //this._MainForm.LineRightMenu.Show(this.lineStatusListView, e.X, e.Y);

                    System.Diagnostics.Trace.WriteLine(strPickUpID);
                    List<AgentStatus> agentStatusList = new List<AgentStatus>();
                    agentStatusList = _MainForm.agentStatusList.FindAll(p => p.Status == 0 || p.Status == 5);
                    agentStatusList.Sort(delegate (AgentStatus a, AgentStatus b) { return a.Agent.CompareTo(b.Agent); });
                    quecallRightMenu.Items.Clear();
                    foreach (var agent in agentStatusList)
                    {
                        if (agent.Status == 0 || agent.Status == 5)
                        {
                            ToolStripMenuItem subMenu = new ToolStripMenuItem();
                            subMenu.Text = agent.Agent + "  " + agent.AgentName;
                            subMenu.Name = "subMenu" + strPickUpID;
                            subMenu.Click += subMenu_Click;
                            quecallRightMenu.Items.Add(subMenu);
                        }
                    }
                    quecallRightMenu.Show(this.quecallStatusListView, e.X, e.Y);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("lineStatusListView_MouseClick system error:" + ex.Message);
            }
        }

        void subMenu_Click(object sender, EventArgs e)
        {
            try
            {
                string iSessionProfileID = (sender as ToolStripMenuItem).Name.Replace("subMenu", "");
                string vAgentID = (sender as ToolStripMenuItem).Text.Substring(0, 10).Trim();
                //MessageBox.Show(iSessionProfileID + " " + vAgentID);
                //System.Diagnostics.Trace.WriteLine("iSessionProfileID:" + iSessionProfileID);
                CpfParams cpfParam = new CpfParams();
                cpfParam.AddString("iSessionProfileID", iSessionProfileID);
                cpfParam.AddString("vAgentID", vAgentID);
                this._MainForm.SendEvent("UM_POPQUE", cpfParam);
                LogManager.WriteLog("quecall subMenu_Click:UM_POPQUE" + cpfParam.GetParams());
                System.Threading.Thread.Sleep(50);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("subMenu_Click system error :" + ex.Message);
            }
        }

        private string GetGroupNameBySkillGroupId(string id)
        {
            try
            {
                DataTable dtGroupPersonal = _MainForm.dsMontor.Tables["dtGroupPersonal"].DefaultView.ToTable(true, new string[] { "groupId", "groupName" });
                int count = 0;
                while (dtGroupPersonal.Rows.Count <= 1 && count < 5)
                {
                    count++;
                    Thread.Sleep(10);
                    dtGroupPersonal = _MainForm.dsMontor.Tables["dtGroupPersonal"].DefaultView.ToTable(true, new string[] { "groupId", "groupName" });
                }

                if (!string.IsNullOrEmpty(_MainForm._ShowSkillGroupIDs) && !_MainForm._ShowSkillGroupIDs.Contains(id)) return "";


                DataRow[] rows = dtGroupPersonal.Select("groupId = " + id);
                if (rows.Length > 0)
                    return rows[0]["groupName"].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetGroupNameBySkillGroupId System Error:" + ex.Message);
                return "";
            }
        }

        private void SetRowBackColor(string strTime, ListViewItem item)
        {
            try
            {
                //added by zhu 2014/09/11
                if (_MainForm.SettingFields_StatusOverQuecallTime1 != _MainForm.DefaultOverTime)
                {
                    if (strTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(_MainForm.SettingFields_StatusOverQuecallTime1))) >= 0)
                    {
                        item.BackColor = Color.LightYellow;
                    }
                }
                if (_MainForm.SettingFields_StatusOverQuecallTime2 != _MainForm.DefaultOverTime)
                {
                    if (strTime.CompareTo(UtilityHelper.ConvertTimeHHMMSSFromSecond(int.Parse(_MainForm.SettingFields_StatusOverQuecallTime2))) >= 0)
                    {
                        item.BackColor = Color.LightPink;
                    }
                }
                //end added
            }
            catch (Exception ex)
            {
                _MainForm.writeLog("SetRowBackColor system error:" + ex.Message + ex.StackTrace);
                item.BackColor = Color.White;
            }
        }


        public void AjustListFontSize()
        {
            float size = 9f;
            quecallStatusListView.Font = new Font(this.quecallStatusListView.Font.FontFamily, size * this._MainForm.SettingFields_ListFontSize);
            foreach (ColumnHeader col in this.quecallStatusListView.Columns)
            {
                col.Width = Convert.ToInt32(float.Parse(DicListViewColumnWidth[col.Name].ToString()) * this._MainForm.SettingFields_ListFontSize);
            }
        }

        private void QueueCallForm_Load(object sender, EventArgs e)
        {

        }

        private void quecallDDLParentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayLine();
        }
    }
}
