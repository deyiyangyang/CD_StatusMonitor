using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Model
{
    public class LineStatusEnum
    {
        public int Status;
        public string StatusName;
        public string Image;

        public LineStatusEnum(int status, string statusName, string image)
        {
            Status = status;
            StatusName = statusName;
            Image = image;
        }

        public static int ConvStatusID(int status)
        {
            // Check Range
            if ((status < 0) || (4 < status)) return 0;
            return status;
        }
    }
}
