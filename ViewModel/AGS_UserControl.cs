using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.ViewModel
{
    using Smiles.MvvM.Lib;
    using Properties;
    using AreGamersStreaming.Model;

    public class AGS_UserControl : CommonBase
    {
        private IUserPref _Preference = new UserPref();

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
            get { return _Preference.IsStartAtBoot; }
            set
            {
                if (_Preference.IsStartAtBoot != value)
                {
                    _Preference.IsStartAtBoot = value;
                    RaisePropertyChanged("IsStartBoot");
                }
            }
        }

        public bool IsMinStart
        {
            get {return _Preference.IsMinamizeAtStart; }
            set
            {
                if(_Preference.IsMinamizeAtStart != value)
                {
                    _Preference.IsMinamizeAtStart = value;
                    RaisePropertyChanged("IsMinStart");
                }
            }
        }
    }
}
