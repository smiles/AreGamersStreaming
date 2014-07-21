using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreGamersStreaming.Model
{
    using Properties;


    public class TwitchPref : ITwitchPref
    {
        #region Private Variables

        private string _TwitchAPI = Settings.Default.TwitchAPI;
        private string _TwitchAPIStream = Settings.Default.TwitchAPIStream;
        private string _TwitchHeaderv3 = Settings.Default.TwitchJSONHeaderv3;
        private string _TwitchHeaderv2 = Settings.Default.TwitchJSONHeaderv2;

        #endregion

        #region Properties

        public string TwitchAPI
        {
            get { return _TwitchAPI; }
            set
            {
                if(_TwitchAPI != value)
                {
                    _TwitchAPI = value;
                    Settings.Default.TwitchAPI = value;
                    Settings.Default.Save();
                }
            }
        }

        public string TwitchAPIStream
        {
            get { return _TwitchAPIStream; }
            set
            {
                if(_TwitchAPIStream != value)
                {
                    _TwitchAPIStream = value;
                    Settings.Default.TwitchAPIStream = value;
                    Settings.Default.Save();
                }
            }
        }

        public string TwitchHeaderv2
        {
            get { return _TwitchHeaderv2; }
            set
            {
                if(_TwitchHeaderv2 != value)
                {
                    _TwitchHeaderv2 = value;
                    Settings.Default.TwitchJSONHeaderv2 = value;
                    Settings.Default.Save();
                }
            }
        }

        public string TwitchHeaderv3
        {
            get { return _TwitchHeaderv3; }
            set
            {
                if(_TwitchHeaderv3 != value)
                {
                    _TwitchHeaderv3 = value;
                    Settings.Default.TwitchJSONHeaderv3 = value;
                    Settings.Default.Save();
                }
            }
        }

        #endregion
    }
}
