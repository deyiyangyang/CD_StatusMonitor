using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor
{
    public partial class AgentState : Form
    {
        public StatusMonitor.MainForm mainF;
        public string AgentName = "";
        public string AgentID = "";
        public string CurAgentState = "";
        public AgentState()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string agentState = "";
                this.label1.Text = AgentName + "(" + AgentID + ")";
                if (this.ra1.Checked == true)
                {
                    agentState = "0";
                }

                else if (this.ra2.Checked == true)
                {
                    agentState = "5";
                }

                else if (this.ra3.Checked == true)
                {
                    agentState = "6";
                }
                mainF.SetAgetState(agentState);
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AgentState_Load(object sender, EventArgs e)
        {
            try
            {
                this.label1.Text = AgentName + "(" + AgentID + ")";

                if (CurAgentState == "1")
                {
                    this.ra1.Checked = true;
                }
                else if (CurAgentState == "5")
                {
                    this.ra2.Checked = true;
                }

                else if (CurAgentState == "6")
                {
                    this.ra3.Checked = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
