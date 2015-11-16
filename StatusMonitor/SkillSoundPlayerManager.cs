/*************************************************************************************
Author:Zhu liangjun
Create Date:2014/03/26
Descrition: create a media player list for skill groups
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Threading;

namespace StatusMonitor
{  
    public class SkillSoundPlayerManager
    {
        public static Dictionary<int, WMPLib.WindowsMediaPlayer> Players;

        static SkillSoundPlayerManager()
        {
            Players = new Dictionary<int, WMPLib.WindowsMediaPlayer>();
        }

        public static void GetDefaultPlayersFromIni(string IdlePeriodLongString)
        {
            try
            {
                if (!string.IsNullOrEmpty(IdlePeriodLongString))
                {
                    if (IdlePeriodLongString.Contains("|"))
                    {
                        string[] arrGrp = IdlePeriodLongString.Split('|');
                        for (int i = 0; i < arrGrp.Length; i++)
                        {
                            string grpid = arrGrp[i].Split(':')[0];
                            var player = new WMPLib.WindowsMediaPlayer();
                            player.settings.setMode("loop", true);
                            Players.Add(int.Parse(grpid), player);
                        }
                    }
                    else
                    {
                        string grpid = IdlePeriodLongString.Split(':')[0];
                        var player = new WMPLib.WindowsMediaPlayer();
                        player.settings.setMode("loop", true);
                        Players.Add(int.Parse(grpid), player);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
