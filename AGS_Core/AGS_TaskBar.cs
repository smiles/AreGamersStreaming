using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AreGamersStreaming.AGS_Core
{
    public class AGS_TaskBar
    {

        private NotifyIcon _NotifyIC = new NotifyIcon();
        private string _AGSTitle = "Are Gamers Streaming?";
        private string _AGSHoverOver = "Are Gamers Streaming?";

        public AGS_TaskBar()
        {
            _NotifyIC.BalloonTipTitle = _AGSTitle;
            _NotifyIC.Text = _AGSHoverOver;
        }

        public void DisconnectedNetworkAlertICO()
        {
            _NotifyIC.Icon = Properties.Resources.ICONetworkError;
        }

        public void SomeoneIsStreamingAlertICO(int balloonTime, string tipTitle, string tipText)
        {
            _NotifyIC.Icon = Properties.Resources.ICOSomeoneStreaming;
            _NotifyIC.ShowBalloonTip(balloonTime, tipTitle, tipText, ToolTipIcon.Info);
        }

        public void NoOneIsstreamingICO()
        {
            _NotifyIC.Icon = Properties.Resources.ICOTaskBar;
        }


    }
}
