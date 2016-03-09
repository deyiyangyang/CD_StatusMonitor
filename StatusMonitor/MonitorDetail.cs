using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace StatusMonitor
{
    public partial class MonitorDetail : Form
    {
        public MonitorDetail()
        {
            InitializeComponent();
        }
        private DataSet dsData;
        public StatusMonitor.MainForm mainF;
        public string iGroup = "";
        public string sGroupName = "";
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MonitorDetail_Load(object sender, EventArgs e)
        {
            try
            {
                iniDV();

                //deleted by zhu 2014/04/14
                //if (string.IsNullOrEmpty(iGroup)) return;
                //if (iGroup == "-1") return;
                //end deleted

                //added by zhu 2014/04/14
                if (iGroup == "-1") iGroup = string.Empty;
                //end added
                setData();
            }
            catch (Exception ex)
            {

            }

        }


        private void iniDV()
        {
            try
            {

                dvData.Columns.Clear();
                dvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;


                dvData.RowHeadersVisible = false;

                DataGridViewColumn column;
                //column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol1;// "スキルグループ";
                //column.Name = "groupName";
                //column.SortMode = DataGridViewColumnSortMode.Automatic;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //column.Width = 100;
                //column.DataPropertyName = "groupName";
                //dvData.Columns.Add(column);





                column = new DataGridViewTextBoxColumn();

                column.HeaderText = "時間帯"; //時間帯
                column.Name = "timePeriod";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 50;
                column.DataPropertyName = "timePeriod";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol5; //"OP呼出数";　　//通話数->着信数->OP呼出数
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[5].DisplayName;
                column.Name = "acdCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 80;
                column.DataPropertyName = "acdCnt";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol6;// "OP応答数";//応答数
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[6].DisplayName;
                column.Name = "answerCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 80;
                column.DataPropertyName = "answerCnt";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol7;// "応答率";
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[7].DisplayName;
                column.Name = "answerPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "answerPer";
                dvData.Columns.Add(column);

                //add,S
                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol8;// "即答数";
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[8].DisplayName;
                column.Name = "answerNowCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "answerNowCnt";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol9;// "即答率";
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[9].DisplayName;
                column.Name = "answerNowPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 60;

                column.DataPropertyName = "answerNowPer";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol12;// "放棄数";
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[12].DisplayName;
                column.Name = "failCnt";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "failCnt";
                dvData.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                //column.HeaderText = mainF.MonitorCol13;// "放棄率";
                column.HeaderText = mainF._MonitorItemManager.MonitorItems[13].DisplayName;
                column.Name = "failPer";
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //column.DefaultCellStyle.BackColor = Color.LightGray;
                column.Width = 60;
                column.DataPropertyName = "failPer";
                dvData.Columns.Add(column);

                dvData.ClearSelection();

                dsData = new DataSet();
                DataTable dtData = new DataTable("dtData");
                dtData.Columns.Add("timePeriod");
                dtData.Columns.Add("acdCnt");
                dtData.Columns.Add("answerCnt");
                dtData.Columns.Add("answerPer");
                dtData.Columns.Add("answerNowCnt");
                dtData.Columns.Add("answerNowPer");
                dtData.Columns.Add("failCnt");
                dtData.Columns.Add("failPer");
                dsData.Tables.Add(dtData);
                dvData.DataSource = dsData.Tables["dtData"];
            }
            catch (Exception ex)
            {

            }

        }
        //modified by zhu 2014/06/18
        //private void setData()
        //{
        //    ArrayList rs = new ArrayList();
        //    ArrayList rsPre = new ArrayList();
        //    try
        //    {
        //        int acdCount = 0;
        //        int answerCnt = 0;
        //        int failCnt = 0;
        //        int answerNowCnt = 0;
        //        int answerNowPer = 0;
        //        bool hasSamePeriod = false;
        //        rs = getCallInfo(iGroup, "");
        //        rsPre = getCallInfoPre(iGroup, "");
        //        if (rsPre.Count < 1 && rs.Count < 1) return;

        //        DataRow csDataRow1;//= dsData.Tables["dtData"].NewRow();
        //        //string[] rows = "1,2,3,4,5;0,0,0,0,0".Split(';');
        //        ArrayList fsPreEnd = new ArrayList();
        //        ArrayList fsFirst = new ArrayList();
        //        if (rsPre.Count > 0)
        //        {
        //            fsPreEnd = (ArrayList)rsPre[rsPre.Count - 1];
        //        }
        //        if (rs.Count > 0)
        //        {
        //            //modified by zhu 2014/06/03
        //            //fsFirst = (ArrayList)rs[rs.Count - 1];
        //            fsFirst = (ArrayList)rs[0];
        //            //end modified
        //        }

        //        //if (fsFirst[0].ToString() == fsPreEnd[0].ToString())
        //        if (fsFirst.Count > 0 && fsPreEnd.Count > 0)
        //        {
        //            if (fsFirst[0].ToString() == fsPreEnd[0].ToString())
        //                hasSamePeriod = true;
        //        }
        //        for (int i = 0; i < rsPre.Count; i++)
        //        {
        //            acdCount = 0;
        //            answerCnt = 0;
        //            failCnt = 0;
        //            answerNowCnt = 0;
        //            answerNowPer = 0;



        //            ArrayList fs = (ArrayList)rsPre[i];
        //            csDataRow1 = dsData.Tables["dtData"].NewRow();

        //            if (string.IsNullOrEmpty(fs[1].ToString()))
        //                acdCount = 0;
        //            else
        //                acdCount = int.Parse(fs[1].ToString());

        //            if (string.IsNullOrEmpty(fs[2].ToString()))
        //                answerCnt = 0;
        //            else
        //                answerCnt = int.Parse(fs[2].ToString());


        //            if (string.IsNullOrEmpty(fs[3].ToString()))
        //                answerNowCnt = 0;
        //            else
        //                answerNowCnt = int.Parse(fs[3].ToString());

        //            failCnt = acdCount - answerCnt;

        //            if ((i == rsPre.Count - 1) && hasSamePeriod == true)
        //            {
        //                if (!string.IsNullOrEmpty(fsFirst[1].ToString()))
        //                {
        //                    //modified by zhu 2014/06/03
        //                    //acdCount = acdCount + int.Parse(fs[1].ToString());
        //                    acdCount = acdCount + int.Parse(fsFirst[1].ToString());
        //                    //end modified
        //                }

        //                if (!string.IsNullOrEmpty(fsFirst[2].ToString()))
        //                {
        //                    //modified by zhu 2014/06/03
        //                    //answerCnt = answerCnt + int.Parse(fs[2].ToString());
        //                    answerCnt = answerCnt + int.Parse(fsFirst[2].ToString());
        //                    //end modified
        //                }
        //                if (!string.IsNullOrEmpty(fsFirst[3].ToString()))
        //                {
        //                    //modified by zhu 2014/06/03
        //                    //answerNowCnt = answerNowCnt + int.Parse(fs[3].ToString());
        //                    answerNowCnt = answerNowCnt + int.Parse(fsFirst[3].ToString());
        //                    //end modified
        //                }

        //            }

        //            csDataRow1[0] = fs[0].ToString();
        //            csDataRow1[1] = acdCount.ToString();
        //            csDataRow1[2] = answerCnt.ToString();
        //            if (acdCount > 0)
        //            {
        //                csDataRow1[3] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[3] = "0.0%";

        //            }
        //            csDataRow1[4] = answerNowCnt.ToString();
        //            if (answerCnt > 0)
        //            {
        //                csDataRow1[5] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[5] = "0.0%";

        //            }
        //            csDataRow1[6] = failCnt.ToString();
        //            if (acdCount > 0)
        //            {
        //                csDataRow1[7] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[7] = "0.0%";
        //            }
        //            //csDataRow1[3]=fs[3].ToString();
        //            //csDataRow1[4]=fs[0];
        //            dsData.Tables["dtData"].Rows.Add(csDataRow1);
        //        }

        //        int rsCount = 0;
        //        if (hasSamePeriod == true)
        //        {
        //            rsCount = 1;
        //        }
        //        for (int i = rsCount; i < rs.Count; i++)
        //        {
        //            acdCount = 0;
        //            answerCnt = 0;
        //            failCnt = 0;
        //            answerNowCnt = 0;
        //            answerNowPer = 0;


        //            ArrayList fs = (ArrayList)rs[i];
        //            csDataRow1 = dsData.Tables["dtData"].NewRow();

        //            if (string.IsNullOrEmpty(fs[1].ToString()))
        //                acdCount = 0;
        //            else
        //                acdCount = int.Parse(fs[1].ToString());

        //            if (string.IsNullOrEmpty(fs[2].ToString()))
        //                answerCnt = 0;
        //            else
        //                answerCnt = int.Parse(fs[2].ToString());


        //            if (string.IsNullOrEmpty(fs[3].ToString()))
        //                answerNowCnt = 0;
        //            else
        //                answerNowCnt = int.Parse(fs[3].ToString());

        //            failCnt = acdCount - answerCnt;
        //            csDataRow1[0] = fs[0].ToString();
        //            csDataRow1[1] = acdCount.ToString();
        //            csDataRow1[2] = answerCnt.ToString();
        //            if (acdCount > 0)
        //            {
        //                csDataRow1[3] = Math.Round((answerCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[3] = "0.0%";

        //            }
        //            csDataRow1[4] = answerNowCnt.ToString();
        //            if (answerCnt > 0)
        //            {
        //                csDataRow1[5] = Math.Round((answerNowCnt * 1.0 * 100 / answerCnt), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[5] = "0.0%";

        //            }
        //            csDataRow1[6] = failCnt.ToString();
        //            if (acdCount > 0)
        //            {
        //                csDataRow1[7] = Math.Round((failCnt * 1.0 * 100 / acdCount), 1).ToString("F1") + "%";
        //            }
        //            else
        //            {
        //                csDataRow1[7] = "0.0%";
        //            }
        //            //csDataRow1[3]=fs[3].ToString();
        //            //csDataRow1[4]=fs[0];
        //            dsData.Tables["dtData"].Rows.Add(csDataRow1);
        //        }

        //        dvData.DataSource = dsData.Tables["dtData"];
        //        dvData.ClearSelection();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}
        //
        private void setData()
        {
            ArrayList rs = new ArrayList();
            ArrayList rsPre = new ArrayList();
            try
            {
                int acdCount = 0;
                int answerCnt = 0;
                int failCnt = 0;
                int answerNowCnt = 0;
                int answerNowPer = 0;
                bool hasSamePeriod = false;
                rs = getCallInfo(iGroup, "");
                rsPre = getCallInfoPre(iGroup, "");
                if (rsPre.Count < 1 && rs.Count < 1) return;

                DataRow csDataRow1;


                for (int i = 0; i < 24; i++)
                {
                    csDataRow1 = dsData.Tables["dtData"].NewRow();
                    csDataRow1[0] = i.ToString();
                    csDataRow1[1] = "0";
                    csDataRow1[2] = "0";
                    csDataRow1[3] = "0%";
                    csDataRow1[4] = "0";
                    csDataRow1[5] = "0%";
                    csDataRow1[6] = "0";
                    csDataRow1[7] = "0%";
                    dsData.Tables["dtData"].Rows.Add(csDataRow1);
                }
                AddDataToTable(rs);
                AddDataToTable(rsPre);


                DataView viewTemp = new DataView();
                viewTemp.Table = dsData.Tables["dtData"];
                viewTemp.RowFilter = "acdCnt<>'0'";

                DataTable tblTemp = viewTemp.ToTable();
                dvData.DataSource = tblTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddDataToTable(ArrayList rs)
        {
            int acdCount = 0;
            int answerCnt = 0;
            int failCnt = 0;
            int answerNowCnt = 0;
            int answerNowPer = 0;


            for (int i = 0; i < rs.Count; i++)
            {
                ArrayList fs = (ArrayList)rs[i];
                int rowIndex = int.Parse(fs[0].ToString());
                acdCount = 0;
                answerCnt = 0;
                failCnt = 0;
                answerNowCnt = 0;
                answerNowPer = 0;


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

                failCnt = acdCount - answerCnt;

                dsData.Tables["dtData"].Rows[rowIndex]["acdCnt"] = (int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["acdCnt"].ToString()) + acdCount).ToString();
                dsData.Tables["dtData"].Rows[rowIndex]["answerCnt"] = (int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["answerCnt"].ToString()) + answerCnt).ToString();

                int totalanswerCnt = int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["answerCnt"].ToString());
                int totalacdCnt = int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["acdCnt"].ToString());
                if (totalanswerCnt > 0)
                {
                    dsData.Tables["dtData"].Rows[rowIndex][3] = Math.Round((totalanswerCnt * 1.0 * 100 / totalacdCnt), 1).ToString("F1") + "%";
                }
                else
                {
                    dsData.Tables["dtData"].Rows[rowIndex][3] = "0.0%";

                }
                dsData.Tables["dtData"].Rows[rowIndex]["answerNowCnt"] = (int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["answerNowCnt"].ToString()) + answerNowCnt).ToString();
                int totalanswerNowCnt = int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["answerNowCnt"].ToString());
                if (totalanswerCnt > 0)
                {
                    dsData.Tables["dtData"].Rows[rowIndex][5] = Math.Round((totalanswerNowCnt * 1.0 * 100 / totalanswerCnt), 1).ToString("F1") + "%";
                }
                else
                {
                    dsData.Tables["dtData"].Rows[rowIndex][5] = "0.0%";

                }
                dsData.Tables["dtData"].Rows[rowIndex]["failCnt"] = (int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["failCnt"].ToString()) + failCnt).ToString();
                int totalfailCnt = int.Parse(dsData.Tables["dtData"].Rows[rowIndex]["failCnt"].ToString());
                if (totalacdCnt > 0)
                {
                    dsData.Tables["dtData"].Rows[rowIndex][7] = Math.Round((totalfailCnt * 1.0 * 100 / totalacdCnt), 1).ToString("F1") + "%";
                }
                else
                {
                    dsData.Tables["dtData"].Rows[rowIndex][7] = "0.0%";
                }

            }
        }
        private ArrayList getCallInfoPre(string iSkillGroupID, string iSkillID)
        {
            Database db = new Database();
            ArrayList rs = new ArrayList();
            try
            {

                if (!db.openDB1(Application.StartupPath, "")) return rs;
                //modified by zhu 2014-06-02
                //string sql = "SELECT DatePart('h',dtAcdCall) as timePeriod ,COUNT(iSessionProfileid) as acdCallCount ";
                //sql = sql + " ,SUM(Switch(dtDiff>0,1,dtDiff<=0, 0)) as completeCallCount ,SUM(Switch(dtDiff<=" + mainF.QuickAnswerMinutes + ",1,dtDiff>" + mainF.QuickAnswerMinutes + ", 0)) as notOverCall ";
                //sql = sql + " FROM (SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',b.dtAcdCall1,b.dtstatus1) as dtDiff ,dtAcdCall";
                //sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid,dtAcdCall   FROM callInfoPre";
                ////added by Zhu 2014/04/14
                //if (string.IsNullOrEmpty(iSkillGroupID))
                //{ 

                //}
                //else
                //{
                //    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                //}
                ////end added
                //if (!string.IsNullOrEmpty(iSkillID))
                //    sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //sql = sql + "  )a LEFT JOIN (  ";
                ////sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1,max(dtAcdCall) as dtAcdCall1  FROM callInfoPre ";
                //sql = sql + " SELECT iSessionProfileid,dtstatus as dtstatus1,dtAcdCall as dtAcdCall1 FROM callInfoPre ";

                ////added by zhu 2014/04/14
                //if (string.IsNullOrEmpty(iSkillGroupID))
                //{

                //}
                //else
                //{
                //    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                //    if (!string.IsNullOrEmpty(iSkillID))
                //        sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //}
                ////end added
                ////sql = sql + "  GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                //sql = sql + "  ) b on a.iSessionProfileid=b.iSessionProfileid ";
                //sql = sql + "  ) a GROUP BY  DatePart('h',dtAcdCall)";

                //string sql = "SELECT DatePart('h',dtAcdCall) as timePeriod  ,COUNT(iSessionProfileid) as acdCallCount ";
                //sql = sql + " ,SUM(Switch(dtDiff>0,1,dtDiff<=0, 0)) as completeCallCount ,SUM(Switch(dtDiff<=" + mainF.QuickAnswerMinutes + ",1,dtDiff>" + mainF.QuickAnswerMinutes + ", 0)) as notOverCall ";
                //sql = sql + " FROM (SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',b.dtAcdCall1,b.dtstatus1) as dtDiff,dtAcdCall ";
                //sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid,dtAcdCall   FROM callInfoPre";

                string sql = "SELECT DatePart('h',dtAcdCall) as timePeriod  ,COUNT(iSessionProfileid) as acdCallCount ";
                sql = sql + " ,SUM(Switch(dtDiff>0,1,dtDiff<=0, 0)) as completeCallCount ,SUM(Switch(dtDiff<=" + mainF.QuickAnswerMinutes + ",1,dtDiff>" + mainF.QuickAnswerMinutes + ", 0)) as notOverCall ";
                sql = sql + " FROM (SELECT DISTINCT a.iStatus,a.iSessionProfileid,DateDiff('s',b.dtAcdCall1,b.dtstatus1) as dtDiff,dtAcdCall ";
                sql = sql + " FROM( SELECT DISTINCT iStatus,iSessionProfileid,dtAcdCall   FROM callInfoPre";
                if (!string.IsNullOrEmpty(iSkillGroupID))
                    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                else
                {
                    if (string.IsNullOrEmpty(mainF._ShowSkillGroupIDs))
                        sql = sql + " WHERE  iSkillGroupID in (" + mainF._DefaultShowSkillGroupIDs + ")";
                    else
                        sql = sql + " WHERE  iSkillGroupID in (" + mainF._ShowSkillGroupIDs + ")";
                }
                sql = sql + "  )a LEFT JOIN (  ";
                //sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1,max(dtAcdCall) as dtAcdCall1  FROM callInfoPre ";
                sql = sql + " SELECT iSessionProfileid,dtstatus as dtstatus1,dtAcdCall as dtAcdCall1  FROM callInfoPre ";
                if (!string.IsNullOrEmpty(iSkillGroupID))
                    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                else
                {
                    if (string.IsNullOrEmpty(mainF._ShowSkillGroupIDs))
                        sql = sql + " WHERE  iSkillGroupID in (" + mainF._DefaultShowSkillGroupIDs + ")";
                    else
                        sql = sql + " WHERE  iSkillGroupID in (" + mainF._ShowSkillGroupIDs + ")";
                }
                //sql = sql + "  GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                sql = sql + "  ) b on a.iSessionProfileid=b.iSessionProfileid ";
                sql = sql + "  ) a GROUP BY DatePart('h',dtAcdCall)";
                //end modified
                if (false == db.readDB(sql, rs))
                {
                    db.closeDB();
                    return rs;
                }

            }
            catch (Exception ex)
            {
                //writeLog("getCallInfo:" + ex.Message);
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

                //modified by zhu 2014-06-02
                ////updated by zhu 2014/06/02 change 3 to 4
                //string sql = "SELECT DatePart('h',dtAcdCall) as timePeriod ,SUM(Switch(iStatus='0',1,iStatus<>'0', 0)) as acdCallCount ";
                //sql = sql + "  ,SUM(Switch(iStatus='4',1,iStatus<>'4', 0)) as completeCallCount";
                //sql = sql + " ,SUM(Switch(iStatus='4',Switch(dtDiff<=" + mainF.QuickAnswerMinutes + ",1,dtDiff>" + mainF.QuickAnswerMinutes + ", 0),iStatus<>'4',0)) as notOverCall";
                //sql = sql + " FROM (";
                //sql = sql + " SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',a.dtAcdCall,b.dtstatus1),dtAcdCall as dtDiff,dtAcdCall";
                //sql = sql + " FROM( SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid,dtAcdCall ";
                //sql = sql + "  FROM callInfo ";
                ////added by zhu 2014/04/14
                //if (string.IsNullOrEmpty(iSkillGroupID))
                //{
                //    sql = sql + "  WHERE   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
                //}
                //else
                //{
                //    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";
                //    if (!string.IsNullOrEmpty(iSkillID))
                //        sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //    sql = sql + " AND   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')";
                //}
                //sql = sql + " )a LEFT JOIN ( ";
                //sql = sql + " SELECT iSessionProfileid,Min(dtstatus) as dtstatus1 ";
                //sql = sql + " FROM callInfo  ";
                ////added by zhu 2014/04/14
                //if (string.IsNullOrEmpty(iSkillGroupID))
                //{

                //}
                //else
                //{
                //    sql = sql + " WHERE   iSkillGroupID='" + iSkillGroupID + "'";

                //    if (!string.IsNullOrEmpty(iSkillID))
                //        sql = sql + " AND   iSkillID='" + iSkillID + "'";
                //}
                //end added
                //sql = sql + " GROUP BY iSessionProfileid ) b on a.iSessionProfileid=b.iSessionProfileid ";
                //sql = sql + "  ) a GROUP BY DatePart('h',dtAcdCall)";

                string sql = "SELECT a2.timePeriod,b2.acdCallCount1 ,a2.completeCallCount,a2.notOverCall FROM(";
                sql = sql + " SELECT DatePart('h',dtAcdCall2) as timePeriod ,SUM(Switch(iStatus='4',1,iStatus<>'4', 0)) as completeCallCount ,";
                sql = sql + " SUM(Switch(iStatus='4',Switch(dtDiff<=0,1,dtDiff>0, 0),iStatus<>'4',0)) as notOverCall ";
                sql = sql + " FROM ( SELECT distinct a.* FROM (SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,";
                sql = sql + " DateDiff('s',b.dtAcdCall2,b.dtstatus1) as dtDiff,dtAcdCall2 FROM(";
                sql = sql + " SELECT DISTINCT iSkillGroupID,iStatus,iSessionProfileid";
                sql = sql + " FROM callInfo WHERE ";
                if (!string.IsNullOrEmpty(iSkillGroupID))
                    sql = sql + "   iSkillGroupID='" + iSkillGroupID + "' And";
                else
                {
                    if (string.IsNullOrEmpty(mainF._ShowSkillGroupIDs))
                        sql = sql + "  iSkillGroupID in (" + mainF._DefaultShowSkillGroupIDs + ") AND ";
                    else
                        sql = sql + "  iSkillGroupID in (" + mainF._ShowSkillGroupIDs + ") AND ";
                }
                sql = sql + " iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0') )a";
                sql = sql + " LEFT JOIN (  SELECT iSessionProfileid,iSkillGroupID,Min(dtstatus) as dtstatus1,MIN(dtAcdCall) as dtAcdCall2 FROM callInfo  ";
                if (!string.IsNullOrEmpty(iSkillGroupID))
                    sql = sql + " Where iSkillGroupID='" + iSkillGroupID + "'";
                sql = sql + " GROUP BY iSessionProfileid,iSkillGroupID ) b on a.iSessionProfileid=b.iSessionProfileid ) a ) a GROUP BY DatePart('h',dtAcdCall2))a2";
                //sql = sql + " LEFT JOIN ( SELECT iSkillGroupID,DatePart('h',dtAcdCall) as timePeriod,COUNT(iSessionProfileid ) as acdCallCount1";
                sql = sql + " LEFT JOIN ( SELECT DatePart('h',dtAcdCall1) as timePeriod,COUNT(iSessionProfileid ) as acdCallCount1";
                sql = sql + " FROM (  SELECT DISTINCT iSkillGroupID,iSessionProfileid,MIN(dtAcdCall) as dtAcdCall1  FROM callInfo   WHERE   ";

                if (!string.IsNullOrEmpty(iSkillGroupID))
                    sql = sql + " iSkillGroupID='" + iSkillGroupID + "' AND ";
                else
                    if (string.IsNullOrEmpty(mainF._ShowSkillGroupIDs))
                        sql = sql + "  iSkillGroupID in (" + mainF._DefaultShowSkillGroupIDs + ") AND ";
                    else
                        sql = sql + "  iSkillGroupID in (" + mainF._ShowSkillGroupIDs + ") AND ";

                sql = sql + " iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0') GROUP BY iSkillGroupID,iSessionProfileid) a1  GROUP BY ";
                //sql = sql + "iSkillGroupID,DatePart('h',dtAcdCall) )  b2 ON a2.timePeriod=b2.timePeriod";
                sql = sql + " DatePart('h',dtAcdCall1) )  b2 ON a2.timePeriod=b2.timePeriod";

                //string sql = "SELECT DatePart('h',dtAcdCall) as timePeriod ,Min(acdCallCount1) as acdCallCount "; //update,2014/04/08
                //sql = sql + "  ,SUM(Switch(iStatus='4',1,iStatus<>'4', 0)) as completeCallCount";
                //sql = sql + " ,SUM(Switch(iStatus='4',Switch(dtDiff<=" + mainF.QuickAnswerMinutes + ",1,dtDiff>" + mainF.QuickAnswerMinutes + ", 0),iStatus<>'4',0)) as notOverCall";
                //sql = sql + " FROM (";
                //sql = sql + " SELECT distinct a.*,b.acdCallCount1 FROM ( "; //add,2014/04/08
                //sql = sql + " SELECT DISTINCT a.iSkillGroupID,a.iStatus,a.iSessionProfileid,DateDiff('s',a.dtAcdCall,b.dtstatus1) as dtDiff,dtAcdCall";
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
                //sql = sql + "  SELECT DISTINCT iSkillGroupID,iSessionProfileid    FROM callInfo  ";
                //sql = sql + " WHERE   iSessionProfileid  IN (SELECT iSessionProfileid FROM callInfo WHERE  iStatus='0')) a1  GROUP BY iSkillGroupID )  b";
                //sql = sql + " ON a.iSkillGroupID=b.iSkillGroupID ";
                ////add,2014/04/08,E
                //sql = sql + "  ) a GROUP BY DatePart('h',dtAcdCall)";
                ////end modified
                if (false == db.readDB(sql, rs))
                {
                    db.closeDB();
                    return rs;
                }

            }
            catch (Exception ex)
            {
                //writeLog("getCallInfo:" + ex.Message);
                //}
            }
            db.closeDB();
            return rs;
        }
    }
}
