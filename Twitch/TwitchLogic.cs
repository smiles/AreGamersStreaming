using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.Twitch
{
    using Properties;
    using Model;

    public class TwitchLogic
    {
        #region Private Variables

        private int _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        private List<TwitchStream> _AllStreams = new List<TwitchStream>();
        private List<string> _StreamList = new List<string>();
        private string _TwitchStreamAPI = Settings.Default.TwitchAPIStream;
        private UserPref _Preference = new UserPref();

        #endregion

        public TwitchLogic()
        {
            ConvertListToTwitchStreams();
            _HowOftenToCheck = _Preference.HowOftenToCheck;
        }

       


        #region Private Methods

        private void ConvertListToTwitchStreams()
        {
            _StreamList = _Preference.AllStreamList;

            foreach(string streamList in _StreamList)
            {
                _AllStreams.Add(new TwitchStream(ConvertStreamURLToAPIAddress(streamList)));
            }

        }

        private string ConvertStreamURLToAPIAddress(string URL)
        {
            string streamName = URL.Substring(14, URL.Length - 14);
            return _TwitchStreamAPI += streamName;
        }

        #endregion
    }
}
