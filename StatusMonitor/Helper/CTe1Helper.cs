using StatusMonitor.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StatusMonitor.Helper
{
    public class CTe1Helper
    {

        public static Color AgentStatusOfIdleColor = Color.FromArgb(130, 130, 255);
        public static Color AgentStatusOfWorktimeColor = Color.FromArgb(130, 255, 255);
        public static Color AgentStatusOfConnectColor = Color.FromArgb(130, 255, 130);
        public static Color AgentStatusOfHoldColor = Color.FromArgb(255, 190, 130);
        public static Color AgentStatusOfOfferringColor = Color.FromArgb(255, 129, 129);
        public static Color AgentStatusOfSeatOffColor = Color.FromArgb(209, 209, 209);
        public static Color AgentStatusOfCallingColor = Color.FromArgb(255, 129, 129);
        public static Color AgentStatusOfTransferColor = Color.FromArgb(255, 132, 255);

        public static Color AgentHelpColor = Color.LightGreen;

        public static AgentStatusEnum[] AgentStatusEnums =
		{
			new AgentStatusEnum( 0, "SM0020046", "IconOpeIdle","Idle"),
			new AgentStatusEnum( 1, "SM0020047", "IconOpeWait","Wait"),
            //deleted by zhu 2014/05/29
			//new AgentStatusEnum( 2, "SM0020048", "IconOpePreparing"),
            //end deleted
			new AgentStatusEnum( 3, "SM0020049", "IconOpeOffering","Offering"),
			new AgentStatusEnum( 5, "SM0020050", "IconOpeWorktime","Worktime"),
			new AgentStatusEnum( 6, "SM0020051", "IconOpeSeatoff","SeatOff"),
            //deleted by zhu 2014/05/29
			//new AgentStatusEnum( 7, "SM0020052", "IconOpeTelephone"),
            //end deleted
			new AgentStatusEnum(10, "SM0020053", "IconTelConnect","Connect"),
			new AgentStatusEnum(20, "SM0020054", "IconTelCalling","Calling"),
			new AgentStatusEnum(30, "SM0020055", "IconTelHold","Hold"),
			new AgentStatusEnum(40, "SM0020056", "IconTelTransfer","Transfer"),
            //deleted by zhu 2014/05/29
			//new AgentStatusEnum(50, "SM0020057", "IconTelConf"), 
			//new AgentStatusEnum(60, "SM0020058", "IconTelMonitor"),
            //end deleted
            //del,xzg,2008/12/09,S---
			//new AgentStatusEnum(70, "通話録音中", "IconTelRecord"),
            //del,xzg,2008/12/09,E---
		};

        public static LineStatusEnum[] LineStatusEnums =
		{
			new LineStatusEnum(0, "SM0020041", "IconCallIdle"),
			new LineStatusEnum(1, "SM0020042", "IconCallCalling"),
			new LineStatusEnum(2, "SM0020043", "IconCallIvr"),
			new LineStatusEnum(3, "SM0020044", "IconCallPreparing"),
			new LineStatusEnum(4, "SM0020045", "IconCallOperator"),
		};

        public static AgentStatusEnum GetAgentStatusEnum(int status)
        {
            for (int i = 0; i < AgentStatusEnums.Length; ++i)
            {
                if (AgentStatusEnums[i].Status == status) return AgentStatusEnums[i];
            }
            return AgentStatusEnums[0];
        }

        public static LineStatusEnum GetLineStatusEnum(int status)
        {
            for (int i = 0; i < LineStatusEnums.Length; ++i)
            {
                if (LineStatusEnums[i].Status == status) return LineStatusEnums[i];
            }
            return LineStatusEnums[0];
        }

        public static string GetConnType(string Conntype)
        {
            if (Conntype == "0")
            {
                return "着信";
            }
            else if (Conntype == "1")
            {
                return "発信";
            }
            return "";
        }
    }
}
