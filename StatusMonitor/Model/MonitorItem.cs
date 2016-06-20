using MyTools;
using StatusMonitor.Helper;
using StatusMonitor.SettingFile;
using System;
using System.Collections.Generic;
using System.Text;
using TksProfileAcxLib;

namespace StatusMonitor.Model
{
    public class MonitorItem
    {
        public int Index { get; set; }
        public bool Visible { get; set; }
        public string DisplayName { get; set; }
    }

    public class MonitorItemManager
    {
        private TksProfileClass _iniProfile;
        private int _columnsCount = ConstEntity.MANAGEMONITORITEMCOUNT;
        private string _columnsDisplayName = "スキルグループ,OP呼出数,OP応答数,応答率,待呼数,放棄呼,放棄率";
        //public string MonitorCol1 = "スキルグループ";
        //public string MonitorCol2 = "ログオン人数";
        //public string MonitorCol3 = "着座OP数";
        //public string MonitorCol4 = "離席数";
        //public string MonitorCol5 = "OP呼出数";
        //public string MonitorCol6 = "OP応答数";
        //public string MonitorCol7 = "応答率";
        //public string MonitorCol8 = "即答数";
        //public string MonitorCol9 = "即答率";
        //public string MonitorCol10 = "待呼数";
        //public string MonitorCol11 = "待ち呼経過時間";
        //public string MonitorCol12 = "受付可数";
        //public string MonitorCol13 = "放棄呼";
        //public string MonitorCol14 = "放棄率";
        private int InsertIndex = 7;
        private string InsertColDisplayName = "経過時間";
        public List<MonitorItem> MonitorItems;
        public event EventHandler MonitorItemChanged;
        public MonitorItemManager(TksProfileClass iniProfile)
        {
            MonitorItems = new List<MonitorItem>();
            _iniProfile = iniProfile;
            Init();
        }

        public void Init()
        {
            try
            {
                _iniProfile.Load(MyTool.GetModuleIniPath());
                //get the value from ini file
                string oldDisplayName = GetOldColDisplayValue();
                
                string newDisplayName = "";
                string[] arrDisplayName;
                bool isNeedRestColName = CheckColDisplayName(oldDisplayName, ref newDisplayName);
                if (isNeedRestColName)
                {
                    _iniProfile.SelectSection("SVSet");
                    for (int i = 1; i < 50; i++)
                    {
                        _iniProfile.Delete("MonitorCol" + i.ToString());
                    }
                    _iniProfile.Delete(ConstEntity.ITEMSHOWKEY);
                    _iniProfile.Delete(ConstEntity.MONITOR_GRID_VIEW_SORT);
                    _iniProfile.Delete(ConstEntity.MONITOR_GRID_VIEW_WIDTH);
                    _iniProfile.Save(MyTool.GetModuleIniPath());
                }

                string oldItemShowValue = _iniProfile.GetStringDefault(ConstEntity.ITEMSHOWKEY, "");

                if (!string.IsNullOrEmpty(newDisplayName))
                    arrDisplayName = newDisplayName.Split(',');
                else
                    arrDisplayName = oldDisplayName.Split(',');
                string[] arrItemShows = GetNewShowFlag(oldItemShowValue);
                int minCount = Math.Min(arrDisplayName.Length, arrItemShows.Length);
                for (int i = 0; i < minCount; i++)
                {
                    MonitorItem item = new MonitorItem();
                    item.DisplayName = arrDisplayName[i];
                    item.Visible = arrItemShows[i] == "1" ? true : false;
                    item.Index = i;
                    MonitorItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Init system error:" + ex.Message);
            }
        }

        private void OnMonitorItemChanged()
        {
            try
            {
                if (MonitorItemChanged != null)
                    MonitorItemChanged(null, null);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("OnMonitorItemChanged system error:" + ex.Message);
            }
        }

        /// <summary>
        /// judge whether need to reset the column display name
        /// </summary>
        /// <param name="oldDisplayName"></param>
        /// <param name="newDisplayName"></param>
        /// <returns>get the right column display name</returns>
        private bool CheckColDisplayName(string oldDisplayName, ref string newDisplayName)
        {
            try
            {
                if (string.IsNullOrEmpty(oldDisplayName))
                {
                    newDisplayName = _columnsDisplayName;
                    return true;
                }
                if(oldDisplayName.Split(',').Length!=ConstEntity.MANAGEMONITORITEMCOUNT)
                {
                    newDisplayName = _columnsDisplayName;
                    return true;
                }
                if (oldDisplayName.Split(',').Length + 1 == _columnsCount)
                {
                    string[] arrNew = new string[_columnsCount];
                    string[] arrOld = oldDisplayName.Split(',');
                    for (int i = 0; i < _columnsCount; i++)
                    {
                        if (i < InsertIndex)
                            arrNew[i] = arrOld[i];
                        else if (i == InsertIndex)
                            arrNew[i] = InsertColDisplayName;
                        else if (i > InsertIndex)
                            arrNew[i] = arrOld[i - 1];
                    }
                    newDisplayName = string.Join(",", arrNew);
                    return true;
                }
                else if (oldDisplayName.Split(',').Length < _columnsCount)
                {
                    newDisplayName = _columnsDisplayName;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckColDisplayName system error:" + ex.Message);
                return true;
            }
        }

        /// <summary>
        /// get new show flag string
        /// </summary>
        /// <param name="oldItemShowValue"></param>
        /// <returns></returns>
        private string[] GetNewShowFlag(string oldItemShowValue)
        {
            string[] arrItemShows = new string[_columnsCount];
            if (string.IsNullOrEmpty(oldItemShowValue))
            {
                for (int i = 0; i < _columnsCount; i++)
                {
                    arrItemShows[i] = "1";
                }
            }
            else
            {
                string[] temp = oldItemShowValue.Split(',');
                if (temp.Length == _columnsCount)
                {
                    arrItemShows = temp;
                }
                else if (temp.Length < _columnsCount)
                {
                    //insert new column show flag into index 10 and the default value is 1
                    int insertIndex = Math.Min(oldItemShowValue.Length, InsertIndex * 2);
                    if (insertIndex == oldItemShowValue.Length)
                        oldItemShowValue = oldItemShowValue.Insert(insertIndex, ",1");
                    else
                        oldItemShowValue = oldItemShowValue.Insert(insertIndex, "1,");
                    arrItemShows = oldItemShowValue.Split(',');
                }
            }
            return arrItemShows;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetOldColDisplayValue()
        {
            try
            {
                _iniProfile.SelectSection("SVSet");
                string oldDisplayName = "";
                for (int i = 1; i < 50; i++)
                {
                    string value = _iniProfile.GetStringDefault("MonitorCol" + i.ToString(), "");
                    if (!string.IsNullOrEmpty(value))
                    {
                        oldDisplayName += "," + value;
                    }
                }
                if (!string.IsNullOrEmpty(oldDisplayName))
                {
                    oldDisplayName = oldDisplayName.Substring(1);
                }
                return oldDisplayName;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetOldValue system error:" + ex.Message);
                return "";
            }
        }

        /// <summary>
        /// save data to ini file
        /// </summary>
        public void SaveData()
        {
            try
            {
                int i = 0;
                string showFlag = string.Empty;
                _iniProfile.SelectSection("SVSet");
                foreach (var item in MonitorItems)
                {
                    i++;
                    _iniProfile.SetString("MonitorCol" + i.ToString(), item.DisplayName);
                    showFlag += "," + (item.Visible ? "1" : "0");
                }
                if (!string.IsNullOrEmpty(showFlag))
                    _iniProfile.SetString(ConstEntity.ITEMSHOWKEY, showFlag.Substring(1));
                _iniProfile.Save(MyTool.GetModuleIniPath());
                OnMonitorItemChanged();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SaveData system error:" + ex.Message);
            }
        }
    }
}
