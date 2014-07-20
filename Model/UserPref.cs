using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.Model
{
    using Properties;
    using AreGamersStreaming.Model;

    public class UserPref : IUserPref
    {
        private bool _IsStartAtBoot = Settings.Default.Bootatstartup;
        private bool _IsMinamizedAtStart = Settings.Default.Minamizeatstart;

        public bool IsStartAtBoot
        {
            get { return _IsStartAtBoot; }
            set
            {
                if (_IsStartAtBoot != value)
                {
                    _IsStartAtBoot = value; 
                    Settings.Default.Bootatstartup = value;
                    Settings.Default.Save();
                }
            }
        }

        public bool IsMinamizeAtStart
        {
            get { return _IsMinamizedAtStart; }
            set
            {
                if (_IsMinamizedAtStart != value)
                {
                    _IsMinamizedAtStart = value;
                    Settings.Default.Minamizeatstart = value;
                    Settings.Default.Save();
                }
            }
        }

        public List<string> UserStreamList
        {
            get;
            set;
        }
    }
}
