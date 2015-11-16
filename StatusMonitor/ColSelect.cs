using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class ColSelect : Form
    {
        public StatusMonitor.MainForm mainF;
        public string ShowCol="0000000000000000";
        public string Option1 = "";
        public string Option2 = "";
        public string Option3 = "";
        public string Option4 = "";
        public string Option5 = "";
        private List<string> ColumnList;

        public ColSelect()
        {
            InitializeComponent();
            InitColumnList();
        }

        private void InitColumnList()
        {
            ColumnList = new List<string>();
            ColumnList.Add("AgentName"); 
            ColumnList.Add("Option1"); 
            ColumnList.Add("Option2");
            ColumnList.Add("Option3");
            ColumnList.Add("Option4");
            ColumnList.Add("Option5");
            ColumnList.Add("内線番号");
            ColumnList.Add("スキルグループ");
            ColumnList.Add("Status");
            ColumnList.Add("状態時間");
            ColumnList.Add("継続時間");
            ColumnList.Add("発着信");
            ColumnList.Add("発信者番号");
            ColumnList.Add("ヘルプ");
            ColumnList.Add("ログイン時間");
            ColumnList.Add("コメント");
        }
        private void ColSelect_Load(object sender, EventArgs e)
        {
            try
            {
                chkOption1.Text = Option1;
                chkOption2.Text = Option2;
                chkOption3.Text = Option3;
                chkOption4.Text = Option4;
                chkOption5.Text = Option5;

                //Skill
                //if (ShowCol.Substring(0, 1) == "1")
                if (ShowCol.Substring(ColumnList.IndexOf("スキルグループ"), 1) == "1")//6=>7 modified by zhu 2014/05/07
                    chkSkillGroup.Checked = true;
                else
                    chkSkillGroup.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("Option1"), 1) == "1")
                    chkOption1.Checked = true;
                else
                    chkOption1.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("Option2"), 1) == "1")
                    chkOption2.Checked = true;
                else
                    chkOption2.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("Option3"), 1) == "1")
                    chkOption3.Checked = true;
                else
                    chkOption3.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("Option4"), 1) == "1")
                    chkOption4.Checked = true;
                else
                    chkOption4.Checked = false;


                if (ShowCol.Substring(ColumnList.IndexOf("Option5"), 1) == "1")
                    chkOption5.Checked = true;
                else
                    chkOption5.Checked = false;

                //added by zhu 2014/05/07
                if (ShowCol.Substring(ColumnList.IndexOf("内線番号"), 1) == "1")
                    chkExtension.Checked = true;
                else
                    chkExtension.Checked = false;
                //end added

                if (ShowCol.Substring(ColumnList.IndexOf("状態時間"), 1) == "1")
                    chkStatusTime.Checked = true;
                else
                    chkStatusTime.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("継続時間"), 1) == "1")
                    chkContinueTime.Checked = true;
                else
                    chkContinueTime.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("発着信"), 1) == "1")
                    this.chkInOrOutCall.Checked = true;
                else
                    chkInOrOutCall.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("発信者番号"), 1) == "1")
                    chkInOrOutCallNum.Checked = true;
                else
                    chkInOrOutCallNum.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("ヘルプ"), 1) == "1")
                    chkHelp.Checked = true;
                else
                    chkHelp.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("ログイン時間"), 1) == "1")
                    chkLoginTime.Checked = true;
                else
                    chkLoginTime.Checked = false;

                if (ShowCol.Substring(ColumnList.IndexOf("コメント"), 1) == "1")
                    chkComment.Checked = true;
                else
                    chkComment.Checked = false;
            }
            catch (Exception ex)
            {

            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strShowCol = "";
                //if (checkBox1.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                foreach(string agentItem in ColumnList)
                {
                    if(agentItem=="Option1")
                    {
                        if (chkOption1.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "Option2")
                    {
                        if (chkOption2.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "Option3")
                    {
                        if (chkOption3.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "Option4")
                    {
                        if (chkOption4.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "Option5")
                    {
                        if (chkOption5.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "スキルグループ")
                    {
                        if (this.chkSkillGroup.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "内線番号")
                    {
                        if (this.chkExtension.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "状態時間")
                    {
                        if (this.chkStatusTime.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "継続時間")
                    {
                        if (this.chkContinueTime.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "発着信")
                    {
                        if (this.chkInOrOutCall.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "発信者番号")
                    {
                        if (this.chkInOrOutCallNum.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "ヘルプ")
                    {
                        if (this.chkHelp.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";
                    }
                    else if (agentItem == "ログイン時間")
                    {
                        if (this.chkLoginTime.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";

                    }
                    else if (agentItem == "コメント")
                    {
                        if (this.chkComment.Checked)
                            strShowCol = strShowCol + "1";
                        else
                            strShowCol = strShowCol + "0";

                    }
                    else
                    {
                        strShowCol = strShowCol + "1";
                    }
                }



                //if (chkOption1.Checked == true)
                //    strShowCol =strShowCol +  "1";
                //else
                //    strShowCol = strShowCol +"0";

                //if (chkOption2.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkOption3.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkOption4.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkOption5.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                ////added by zhu 2014/05/07
                //if (chkExtension.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";
                ////end added
                ////skill
                //if (chkSkillGroup.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                ////strShowCol = strShowCol + "11";
                //strShowCol = strShowCol + "1";

                //if (chkStatusTime.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkContinueTime.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkInOrOutCall.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkInOrOutCallNum.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";

                //if (chkHelp.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";
                //if (chkLoginTime.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";
                //if (chkComment.Checked == true)
                //    strShowCol = strShowCol + "1";
                //else
                //    strShowCol = strShowCol + "0";
                mainF.setShowCol(strShowCol);
                this.Dispose();
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.Message );
                MessageBox.Show(ex.Message );
            }
            
        }
    }
}
