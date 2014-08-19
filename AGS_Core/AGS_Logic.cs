using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Diagnostics;


namespace AreGamersStreaming.AGS_Core
{
    using Twitch;
    using ViewModel;
    using Properties;
    using Model;

    public class AGS_Logic 
    {
        private ITwitchStreamLogic _StreamLogic = new TwitchStreamLogic();
        private List<TwitchStreamInfo> _ListOfStreamers = new List<TwitchStreamInfo>();
        private int _BalloonTimeVisiable = 7000;
        private NotifyIcon _NotifyIC;
        private string _AGSTitle = "Are Gamers Streaming?";
        private string _AGSHoverOver = "Are Gamers Streaming?";
        private string _URLinBalloon;

        #region User Preference Private Variables

        private IUserPref _Preference = new UserPref();
        private List<string> _StreamList;
        private int _HowOFtenToCheck;
        private bool _MinAtStart;
        private bool _StartAtBoot;

        #endregion

        public event EventHandler IconDoubleClick;

        #region Construct

        public AGS_Logic()
        {
            NetworkWatch();
            TwitchStreamLogicSetup();
            TaskBarSetup();
            _NotifyIC.DoubleClick += OnIconDoubleClick;
            _NotifyIC.BalloonTipClicked += _NotifyIC_BalloonTipClicked;
            
        }


        #endregion

        public bool StartMinimize()
        {
            return _Preference.IsMinamizeAtStart;
        }

        #region UserPref 

        public List<string> StreamList
        {
            get { return _StreamList; }
            set
            {
                if(value != null)
                {
                    _Preference.AllStreamList = value;
                    _StreamList = value;
                    _StreamLogic.NewStreamList(value);
                    
                }
            }
        }

        public int HowOftenToCheck
        {
            get { return _HowOFtenToCheck; }
            set
            {
                if(_HowOFtenToCheck != value)
                {
                    _Preference.HowOftenToCheck = value;
                    _HowOFtenToCheck = value;
                }
            }
        }

        public bool IsMinAtStart
        {
            get { return _MinAtStart; }
            set
            {
                if(_MinAtStart != value)
                {
                    _Preference.IsMinamizeAtStart = value;
                    _MinAtStart = value;
                }
            }
        }

        public bool IsStartAtBoot
        {
            get { return _StartAtBoot; }
            set
            {
                if(_StartAtBoot != value)
                {
                    _Preference.IsStartAtBoot = value;
                    _StartAtBoot = value;
                }
            }
        }

        public void LoadPersistentData()
        {
            if (_Preference.AllStreamList.Count != 0)
            {
                this.StreamList = _Preference.AllStreamList;
            }

            this.HowOftenToCheck = _Preference.HowOftenToCheck;
            this.IsMinAtStart = _Preference.IsMinamizeAtStart;
            this.IsStartAtBoot = _Preference.IsStartAtBoot;
        }

#endregion

        #region Taskbar

        private void TaskBarSetup()
        {
            _NotifyIC = new NotifyIcon();
            _NotifyIC.BalloonTipTitle = _AGSTitle;
            _NotifyIC.Text = _AGSHoverOver;
            _NotifyIC.Visible = true;

            NoOneIsstreamingICO();

        }

        private void DisconnectedNetworkAlertICO()
        {
            //_NotifyIC.Icon = Properties.Resources.ICONetworkError;
        }

        private void SomeoneIsStreamingAlertICO(int balloonTime, string tipTitle, string tipText)
        {
            _NotifyIC.Icon = Properties.Resources.SomeoneStreamingSmall;
            _NotifyIC.ShowBalloonTip(balloonTime, tipTitle, tipText, ToolTipIcon.None);
            
        }

        private void NoOneIsstreamingICO()
        {
            _NotifyIC.Icon = Properties.Resources.BaseLineAGSSmall;
        }
        
        #endregion


        #region Private Method

        private void TwitchStreamLogicSetup()
        {
            if (this.StreamList != null)
            {
                _StreamLogic.NewStreamList(this.StreamList);
            }

            if (this.HowOftenToCheck != 0)
            {
                _StreamLogic.StartCheckingForStreams(this.HowOftenToCheck);
            }
            _StreamLogic.SomeoneIsStreamingEvent += SomeoneStreaming;
            _StreamLogic.SomeoneHasStopStreamingEvent += SomeoneStopStreaming;
        }

        private void SomeoneStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.Add(e);
            SomeoneIsStreamingAlertICO(_BalloonTimeVisiable, e.BaseStreamName, e.URL);
            _URLinBalloon = e.URL;
        }

        private void SomeoneStopStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.RemoveAll(x => x.URL == e.URL);
            if(_ListOfStreamers.Count == 0)
            {
                NoOneIsstreamingICO();
            }
        }


        private void NetworkWatch()
        {
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkHasChanged);
        }

        private void NetworkHasChanged(object sender, EventArgs e)
        {
            if(NetworkInterface.GetIsNetworkAvailable())
            {
                if(!_StreamLogic.IsChecking)
                {
                    _StreamLogic.RestartCheck();
                }

                NoOneIsstreamingICO();
            }
            else
            {
                _StreamLogic.StopCheckingForStreams();
                DisconnectedNetworkAlertICO();
            }

        }

        private void OnIconDoubleClick(object Sender, EventArgs e)
        {
            EventHandler handler = IconDoubleClick;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Event

        void _NotifyIC_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start(_URLinBalloon);
        }


        #endregion
    }
}
