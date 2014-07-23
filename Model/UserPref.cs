using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.Model
{
    using Properties;
    using AreGamersStreaming.Model;
    using System.Collections.Specialized;

    public class UserPref : IUserPref
    {
        #region Private Variables

        private bool _IsStartAtBoot = Settings.Default.Bootatstartup;
        private bool _IsMinamizedAtStart = Settings.Default.Minamizeatstart;
        private int _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        private StringCollection _StreamCollection = Settings.Default.StreamList;
        private List<string> _StreamList = new List<string>();

        #endregion

        #region Properties

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

        public int HowOftenToCheck
        {
            get { return _HowOftenToCheck; }
            set
            {
                if(_HowOftenToCheck != value)
                {
                    _HowOftenToCheck = value;
                    Settings.Default.HowOftenToCheck = value;
                    Settings.Default.Save();
                }
            }
        }

        #endregion
    }
}
