using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Model
{
    public class AgentStatusEnum
    {
        public int Status;
        public string StatusName;
        public string Image;
        public string Key;

        public AgentStatusEnum(int status, string statusName, string image, string key)
        {
            Status = status;
            StatusName = statusName;
            Image = image;
            Key = key;
        }

        public static int ConvStatusID(int status)
        {
            // Ope
            if (status == 0) return 1; // 待機中
            if (status == 4) return 10; // 通話中
            if ((status == 1) || (status == 8) || (status == 9)) return 0; // Logoff
            // Tel
            if (status > 10) status = (status / 10) * 10 + 10;
            if ((status < 0) || (70 < status)) return 0;
            return status;
        }
    }
}
