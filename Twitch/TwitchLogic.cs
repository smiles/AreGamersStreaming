using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreGamersStreaming.Twitch
{
    using Properties;

    public class TwitchLogic
    {
        private int _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        private List<TwitchStream> _AllStreams = new List<TwitchStream>();
        private List<string> _StreamList = new List<string>();
        private string _TwitchStreamAPI = Settings.Default.TwitchAPIStream;

        public TwitchLogic()
        {

        }

        public void UpdateHowOftenToCheck()
        {
            _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        }

        private void ConvertListToStreams()
        {
            _StreamList = Settings.Default.StreamList.Cast<string>().ToList();

        }

        private string ConvertStreamURLToAPIAddress(string URL)
        {
            string streamName = URL.Remove(0,13);
            return _TwitchStreamAPI += streamName;
        }
    }
}
