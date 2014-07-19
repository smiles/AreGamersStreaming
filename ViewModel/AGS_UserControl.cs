using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.ViewModel
{
    using Smiles.MvvM.Lib;
    using Properties;

    public class AGS_UserControl : CommonBase
    {
        private bool _IsStartBoot = Settings.Default.Bootatstartup;
        private bool _IsMinStart = Settings.Default.Minamizeatstart;

        public string AddStream
        {
            get;
            set;
        }

        public string DelStream
        {
            get;
            set;
        }

        public bool IsStartBoot
        {
            get { return _IsStartBoot; }
            set
            {
                if(_IsStartBoot != value)
                {
                    _IsStartBoot = value;
                    Settings.Default.Bootatstartup = value;
                    Settings.Default.Save();
                    RaisePropertyChanged("IsStartBoot");
                }
            }
        }

        public bool IsMinStart
        {
            get {return _IsMinStart; }
            set
            {
                if(_IsMinStart != value)
                {
                    _IsMinStart = value;
                    Settings.Default.Minamizeatstart = value;
                    Settings.Default.Save();
                    RaisePropertyChanged("IsMinStart");
                }
            }
        }
    }
}
