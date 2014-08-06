using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AreGamersStreaming.Twitch
{
    public class TwitchStreamInfo : EventArgs
    {
        private string _URL;
        private Image _StreamImage;
        private string _BaseStreamName;

        public TwitchStreamInfo(string URL, Image streamImage)
        {
            _URL = URL;
            _StreamImage = streamImage;
            _BaseStreamName = URL.Substring(22, URL.Length - 22);
        }

        public TwitchStreamInfo(string URL)
        {
            _URL = URL;
            _BaseStreamName = URL.Substring(22, URL.Length - 22);
        }

        public string URL
        {
            get { return _URL; }
        }

        public Image StreamImage
        {
            get { return _StreamImage; }
        }

        public string BaseStreamName
        {
            get { return _BaseStreamName; }
        }
    }
}
