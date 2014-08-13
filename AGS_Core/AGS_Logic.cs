using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace AreGamersStreaming.AGS_Core
{
    using Twitch;
    using ViewModel;

    public class AGS_Logic 
    {
        private ITwitchStreamLogic _StreamLogic = new TwitchStreamLogic();
        private List<TwitchStreamInfo> _ListOfStreamers = new List<TwitchStreamInfo>();
        private int _BalloonTimeVisiable = 7000;
        private AGS_UserControl _UserControl = new AGS_UserControl();
        private NotifyIcon _NotifyIC;
        private string _AGSTitle = "Are Gamers Streaming?";
        private string _AGSHoverOver = "Are Gamers Streaming?";

        public event EventHandler IconDoubleClick;

        #region Construct

        public AGS_Logic()
        {
            NetworkWatch();
            TwitchStreamLogicSetup();
            TaskBarSetup();
            _UserControl.ListHasBeenUpdatedEvent += ListOfStreamersChanged;
            _UserControl.HowOftenToCheckUpdatedEvent += _HowOftenToCheckUpdatedEvent;
            _NotifyIC.DoubleClick += OnIconDoubleClick;
        }

        void _HowOftenToCheckUpdatedEvent(object sender, EventArgs e)
        {
            _StreamLogic.UpdateHowOftenToCheck();
        }


        #endregion

        public bool StartMinimize()
        {
            return _UserControl.IsMinStart;
        }

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
            _NotifyIC.Icon = Properties.Resources.ICOSomeoneStreaming;
            _NotifyIC.ShowBalloonTip(balloonTime, tipTitle, tipText, ToolTipIcon.Info);
            
        }

        private void NoOneIsstreamingICO()
        {
            _NotifyIC.Icon = Properties.Resources.ICOTaskBar;
        }
        
        #endregion


        #region Private Method

        private void TwitchStreamLogicSetup()
        {
            _StreamLogic.UpdateListFromDB();
            _StreamLogic.StartCheckingForStreams();
            _StreamLogic.SomeoneIsStreamingEvent += SomeoneStreaming;
            _StreamLogic.SomeoneHasStopStreamingEvent += SomeoneStopStreaming;
        }

        private void SomeoneStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.Add(e);
            SomeoneIsStreamingAlertICO(_BalloonTimeVisiable, e.BaseStreamName, e.URL);
        }

        private void SomeoneStopStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.RemoveAll(x => x.URL == e.URL);
            if(_ListOfStreamers.Count == 0)
            {
                NoOneIsstreamingICO();
            }
        }

        private void ListOfStreamersChanged(object sender, EventArgs e)
        {
            _StreamLogic.UpdateListFromDB();
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
    }
}
