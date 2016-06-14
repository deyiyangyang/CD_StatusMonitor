using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.SoundPlayer
{
    public class SinglePlayerManager
    {
        public static Dictionary<string, WMPLib.WindowsMediaPlayer> Players;
        static SinglePlayerManager()
        {
            Players = new Dictionary<string, WMPLib.WindowsMediaPlayer>();
        }

        public static void AddPlayer(string name)
        {
            if(Players != null && !Players.ContainsKey(name))
            {
                var player = new WMPLib.WindowsMediaPlayer();
                player.settings.setMode("loop", true);
                Players.Add(name, player);
            }
        }
        public static void StopPlayer(string name)
        {
            if (Players != null && Players.ContainsKey(name))
            {
                var palyer = Players[name];
                palyer.URL = string.Empty;
                palyer.controls.stop();

            }
        }

        public static void RunPlayer(string name,string fullPathVoice)
        {
            if (Players != null && Players.ContainsKey(name))
            {
                var palyer = Players[name];
                palyer.URL = string.Empty;
                palyer.controls.stop();
                palyer.URL = fullPathVoice;
            }
        }
    }
}
