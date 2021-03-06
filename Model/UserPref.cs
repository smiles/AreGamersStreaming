﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AreGamersStreaming.Model
{
    using Properties;
    using System.Collections.Specialized;

    public class UserPref : IUserPref
    {
        #region Private Variables

        private bool _IsStartAtBoot = Settings.Default.Bootatstartup;
        private bool _IsMinamizedAtStart = Settings.Default.Minamizeatstart;
        private int _HowOftenToCheck = Settings.Default.HowOftenToCheck;
        private StringCollection _StreamCollection = Settings.Default.StreamList;
        private List<string> _StreamList = new List<string>();

        #endregion

        #region Public events

        public event EventHandler HowOftenToCheckEvent;
        public event EventHandler AllStreamListEvent;

        #endregion

        #region Properties

        public UserPref()
        {
            if (_StreamCollection != null)
            {
                _StreamList = _StreamCollection.Cast<string>().ToList();
            }
        }

        public bool IsStartAtBoot
        {
            get { return _IsStartAtBoot; }
            set
            {
                if (_IsStartAtBoot != value)
                {
                    _IsStartAtBoot = value; 
                    Settings.Default.Bootatstartup = value;
                    Settings.Default.Save();
                }
            }
        }

        public bool IsMinamizeAtStart
        {
            get { return _IsMinamizedAtStart; }
            set
            {
                if (_IsMinamizedAtStart != value)
                {
                    _IsMinamizedAtStart = value;
                    Settings.Default.Minamizeatstart = value;
                    Settings.Default.Save();
                }
            }
        }

        public int HowOftenToCheck
        {
            get { return _HowOftenToCheck; }
            set
            {
                if(_HowOftenToCheck != value)
                {
                    _HowOftenToCheck = value;
                    Settings.Default.HowOftenToCheck = value;
                    Settings.Default.Save();
                    OnHowOftenToCheckEvent();
                }
            }
        }

        public List<string> AllStreamList
        {
            get
            {
                return _StreamList;
            }
            set
            {
                if(value != null)
                {
                    _StreamList = value;
                    StringCollection collection = new StringCollection();
                    collection.AddRange(value.ToArray());
                    Settings.Default.StreamList = collection;
                    Settings.Default.Save();
                    OnAllStreamListEvent();
                }
            }
        }

        protected virtual void OnHowOftenToCheckEvent()
        {
            EventHandler handler = HowOftenToCheckEvent;
            
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnAllStreamListEvent()
        {
            EventHandler handler = AllStreamListEvent;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
