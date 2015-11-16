using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StatusMonitor.Helper
{
    public class UtilityHelper
    {

        public static string DoContinueTime(string iTimeSS, int refreshTime)
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

                iTime = int.Parse(sumSS) + refreshTime;
                string strTime = ConvertTimeHHMMSSFromSecond(iTime);


                return strTime;
            }
            catch (Exception ex)
            {
                return "00:00:00";
            }

        }
        private static string ConvertTimeHHMMSSFromMinute(string iTimeMinute)
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
                if (string.IsNullOrEmpty(iTimeMinute))
                    iTimeMinute = "000";
                iTime = int.Parse(iTimeMinute) * 60; //単位分ので、＊60
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
        public static string ConvertTimeHHMMSSFromSecond(int iTimeSS)
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
        public static string ConvertToHHMMSSFromSecond(int iTimeSS)
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

        public static bool IsNumeric(string inData)
        {
            try
            {
                Regex r = new Regex("^[0-9]+$");
                bool m = r.IsMatch(inData);


                return m;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// get continue time from statustime
        /// </summary>
        /// <param name="dtStatusTime"></param>
        /// <returns></returns>
        public static string GetContinueTime(DateTime dtStatusTime)
        {
            string strTime = "00:00:00";
            try
            {
                if (DateTime.Compare(DateTime.Now, dtStatusTime) <= 0)
                {
                    strTime = "00:00:00";
                }
                else
                {
                    strTime = Convert.ToDateTime(DateTime.Now.Subtract(dtStatusTime).ToString()).ToString("HH:mm:ss");
                }
            }
            catch
            {
                strTime = "00:00:00";
            }
            return strTime;
        }
    }
}
