using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AreGamersStreaming.Twitch
{
    using Properties;
    using Model;
    using Smiles.Common.Lib;

    public class TwitchStreamLogic
    {
        #region Private Variables

        private List<TwitchStream> _AllStreams = new List<TwitchStream>();
        private List<string> _StreamList = new List<string>();

        private string _TwitchStreamAPI;
        private IUserPref _Preference = new UserPref();
        private ITwitchPref _TwitchInfo = new TwitchPref();
        private int _HowOftenToCheck;

        private Timer _CheckEvery = new Timer();
        private TwitchEngine _TwitchRequest = new TwitchEngine();

        #endregion

        #region Events

        public event EventHandler<TwitchStreamInfo> SomeoneIsStreamingEvent;
        public event EventHandler NetworkNotConnectedEvent;

        #endregion

        public TwitchStreamLogic()
        {
            
        }

        public void StartCheckingForStreams()
        {
            StartCheckStream();
        }



        public void UpdateListFromDB()
        {
            ConvertListToTwitchStreams();
            _TwitchStreamAPI = _TwitchInfo.TwitchAPI;
        }

        public void UpdateHowOftenToCheck()
        {
            _HowOftenToCheck = _Preference.HowOftenToCheck;
        }


        #region Private Method

        private void ConvertListToTwitchStreams()
        {

            _StreamList = _Preference.AllStreamList;

            _AllStreams.Clear();

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

        private void CheckStreams(object sender, EventArgs e)
        {
            while(!GeneralNetwork.IsNetworkConnected())
            {
                OnNetworkNotConnectedEvent();
            }
            _TwitchRequest.GetAllTwitchStream(_AllStreams);
            CheckIfAnyStreaming();
        }

        private void CheckIfAnyStreaming()
        {
            if (_AllStreams.Count != 0)
            {
                foreach (TwitchStream Stream in _AllStreams)
                {
                    if(Stream.StreamJSON.stream != null)
                    {
                        OnSomeoneIsStreamingEvent(new TwitchStreamInfo(Stream.StreamURL));
                    }
                }
            }
        }

        private void StartCheckStream()
        {
            _CheckEvery.Interval = 6000 * _HowOftenToCheck;
            _CheckEvery.Tick += CheckStreams;
            _CheckEvery.Start();
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

        protected virtual void OnNetworkNotConnectedEvent()
        {
            EventHandler handler = NetworkNotConnectedEvent;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }


}
