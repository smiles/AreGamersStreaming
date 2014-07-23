using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.Twitch
{
    using Properties;

    public class TwitchLogic
    {
        #region Private Variables

        private int _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        private List<TwitchStream> _AllStreams = new List<TwitchStream>();
        private List<string> _StreamList = new List<string>();
        private string _TwitchStreamAPI = Settings.Default.TwitchAPIStream;

        #endregion

        public TwitchLogic()
        {
            ConvertListToTwitchStreams();
        }

        public void UpdateHowOftenToCheck()
        {
            _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        }


        #region Private Methods

        private void ConvertListToTwitchStreams()
        {
            _StreamList = Settings.Default.StreamList.Cast<string>().ToList();

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
