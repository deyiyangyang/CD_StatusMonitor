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

        /// <summary>
        /// find skillid from existingValue
        /// </summary>
        /// <param name="existingValue">like "123:xxx|34:xx"</param>
        /// <param name="skillID"></param>
        /// <returns></returns>
        public static bool CheckSkillIdExists(string existingValue,string skillID)
        {
            if (string.IsNullOrEmpty(existingValue)) return false;
            string[] arr = existingValue.Split('|');
            foreach(var item in arr)
            {
                string[] vs = item.Split(':');
                foreach(var v in vs)
                {
                    if (v == skillID)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// get skillids from ParentGroup
        /// </summary>
        /// <param name="parentGroupID"></param>
        /// <returns></returns>
        public static string GetSkillIdsByParentGroup(string parentGroupID, Dictionary<string, string> DicParentGroup)
        {
            string result = string.Empty;
            foreach (string key in DicParentGroup.Keys)
            {
                var arr = key.Split(',');
                if (arr.Length == 2)
                {
                    if (arr[0] == parentGroupID)
                    {
                        result = DicParentGroup[key];
                        break;
                    }
                }
            }
            return result;
        }

        public static List<int> GetSkillIdListByParentGroup(string parentGroupID, Dictionary<string, string> DicParentGroup)
        {
            List<int> result = new List<int>();
            foreach (string key in DicParentGroup.Keys)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var arr = key.Split(',');
                    if (arr.Length == 2)
                    {
                        if (arr[0] == parentGroupID)
                        {
                            if(!string.IsNullOrEmpty(DicParentGroup[key]))
                            {
                                foreach (var skillGroupIds in DicParentGroup[key].Split(','))
                                {
                                    result.Add(int.Parse(skillGroupIds));
                                }
                            }
                                                     
                            break;
                        }
                    }
                }

            }
            return result;
        }
    }
}
