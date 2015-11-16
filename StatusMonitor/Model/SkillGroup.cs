using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Model
{
    public class SkillGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public int IdleAlarm { get; set; }
    }
}
