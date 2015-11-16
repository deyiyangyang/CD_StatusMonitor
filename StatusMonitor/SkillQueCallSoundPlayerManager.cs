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
    public class SkillQueCallSoundPlayerManager
    {
        public static Dictionary<string, WMPLib.WindowsMediaPlayer> Players;

        static SkillQueCallSoundPlayerManager()
        {
            Players = new Dictionary<string, WMPLib.WindowsMediaPlayer>();
        }

        /// <summary>
        /// stop play sound for current keyname player
        /// </summary>
        /// <param name="keyName"></param>
        public static void StopSkillGroupPlayer(string skillGroupID)
        {
            if (Players.ContainsKey(skillGroupID + "_period1") && Players.ContainsKey(skillGroupID + "_period2") && Players.ContainsKey(skillGroupID + "_period3"))
            {
                Players[skillGroupID + "_period1"].URL = string.Empty;
                Players[skillGroupID + "_period1"].controls.stop();
                Players[skillGroupID + "_period2"].URL = string.Empty;
                Players[skillGroupID + "_period2"].controls.stop();
                Players[skillGroupID + "_period3"].URL = string.Empty;
                Players[skillGroupID + "_period3"].controls.stop();
            }
        }


        /// <summary>
        /// stop play sound for current keyname player
        /// </summary>
        /// <param name="keyName"></param>
        public static void OnlyRunSkillGroupPeriodPlayer(string skillGroupID, int period, string fullPathVoice)
        {
            if (Players.ContainsKey(skillGroupID + "_period1") && Players.ContainsKey(skillGroupID + "_period2") && Players.ContainsKey(skillGroupID + "_period3"))
            {
                if (period == 1)
                {
                    Players[skillGroupID + "_period2"].URL = string.Empty;
                    Players[skillGroupID + "_period2"].controls.stop();
                    Players[skillGroupID + "_period3"].URL = string.Empty;
                    Players[skillGroupID + "_period3"].controls.stop();

                    if (Players[skillGroupID + "_period1"].URL == string.Empty)
                        Players[skillGroupID + "_period1"].URL = fullPathVoice;
                }
                else if (period == 2)
                {
                    Players[skillGroupID + "_period1"].URL = string.Empty;
                    Players[skillGroupID + "_period1"].controls.stop();
                    Players[skillGroupID + "_period3"].URL = string.Empty;
                    Players[skillGroupID + "_period3"].controls.stop();
                    if (Players[skillGroupID + "_period2"].URL == string.Empty)
                        Players[skillGroupID + "_period2"].URL = fullPathVoice;
                }
                else if (period == 3)
                {
                    Players[skillGroupID + "_period1"].URL = string.Empty;
                    Players[skillGroupID + "_period1"].controls.stop();
                    Players[skillGroupID + "_period2"].URL = string.Empty;
                    Players[skillGroupID + "_period2"].controls.stop();

                    if (Players[skillGroupID + "_period3"].URL == string.Empty)
                        Players[skillGroupID + "_period3"].URL = fullPathVoice;
                }
            }
        }

        /// <summary>
        ///  period1 + "," + voice1 + "|" + period2 + "," + voice2 + "|" + period3 + "," + voice3
        /// </summary>
        /// <param name="queCallPeriodLongString"></param>
        public static void ConfigSoundPlayers(string skillGroupID)
        {
            try
            {
                if (!string.IsNullOrEmpty(skillGroupID))
                {
                    string key1 = skillGroupID + "_period1";
                    string key2 = skillGroupID + "_period2";
                    string key3 = skillGroupID + "_period3";
                    if (!Players.ContainsKey(key1))
                    {
                        var player = new WMPLib.WindowsMediaPlayer();
                        player.settings.setMode("loop", true);
                        Players.Add(key1, player);
                    }
                    if (!Players.ContainsKey(key2))
                    {
                        var player = new WMPLib.WindowsMediaPlayer();
                        player.settings.setMode("loop", true);
                        Players.Add(key2, player);
                    }
                    if (!Players.ContainsKey(key3))
                    {
                        var player = new WMPLib.WindowsMediaPlayer();
                        player.settings.setMode("loop", true);
                        Players.Add(key3, player);
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
