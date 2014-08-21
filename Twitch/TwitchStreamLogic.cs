using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AreGamersStreaming.Twitch
{
    using Model;

    public class TwitchStreamLogic : AreGamersStreaming.Twitch.ITwitchStreamLogic
    {
        #region Private Variables

        private List<TwitchStream> _AllStreams = new List<TwitchStream>();
        private List<string> _StreamList = new List<string>();

        private string _TwitchStreamAPI;
        private ITwitchPref _TwitchInfo = new TwitchPref();
        private bool _IsCheckingForStreams;

        private Timer _CheckEvery = new Timer();
        private TwitchEngine _TwitchRequest = new TwitchEngine();

        #endregion

        #region Events

        public event EventHandler<TwitchStreamInfo> SomeoneIsStreamingEvent;
        public event EventHandler<TwitchStreamInfo> SomeoneHasStopStreamingEvent;

        #endregion

        public bool IsChecking
        {
            get { return _IsCheckingForStreams; }
            set
            {
                _IsCheckingForStreams = value;
            }
        }

        public TwitchStreamLogic()
        {
            _TwitchStreamAPI = _TwitchInfo.TwitchAPIStream;
        }

        public void StartCheckingForStreams(int minutes)
        {
            this.IsChecking = true;
            _CheckEvery.Interval = 60000 * minutes;
            _CheckEvery.Tick += CheckStreams;
            _CheckEvery.Start();
        }

        public void StopCheckingForStreams()
        {
            this.IsChecking = false;
            _CheckEvery.Stop();
        }

        public void RestartCheck()
        {
            if (!this.IsChecking)
            {
                _CheckEvery.Start();
            }
        }

        public void ChangeCheck(int minutes)
        {
            StopCheckingForStreams();
            StartCheckingForStreams(minutes);
        }


        #region Private Method

        public void NewStreamList(List<string> newStreamList)
        {

            _AllStreams.Clear();

            foreach(string streamList in newStreamList)
            {
                _AllStreams.Add(new TwitchStream(streamList, ConvertStreamURLToAPIAddress(streamList)));
            }

        }

        private string ConvertStreamURLToAPIAddress(string URL)
        {
            string streamName = URL.Substring(14, URL.Length - 14);
            return _TwitchStreamAPI + streamName;
        }

        private void CheckStreams(object sender, EventArgs e)
        {
            _TwitchRequest.GetAllTwitchStream(_AllStreams);
            CheckStreamStatus();
        }

        private void CheckStreamStatus()
        {
            if (_AllStreams.Count != 0)
            {
                foreach (TwitchStream Stream in _AllStreams)
                {
                    if(Stream.StreamJSON.stream != null)
                    {
                        if (!Stream.IsStreaming)
                        {
                            Stream.IsStreaming = true;
                            OnSomeoneIsStreamingEvent(new TwitchStreamInfo(Stream.StreamURL));
                        }
                    }
                    else
                    {
                        if(Stream.IsStreaming)
                        {
                            Stream.IsStreaming = false;
                            OnSomeoneHasStopStreamingEvent(new TwitchStreamInfo(Stream.StreamURL));
                        }
                    }
                }
            }
        }

        

        #endregion

        #region Protected Handler

        protected virtual void OnSomeoneIsStreamingEvent(TwitchStreamInfo e)
        {
            EventHandler<TwitchStreamInfo> handler = SomeoneIsStreamingEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSomeoneHasStopStreamingEvent(TwitchStreamInfo e)
        {
            EventHandler<TwitchStreamInfo> handler = SomeoneHasStopStreamingEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
       

        #endregion
    }


}
