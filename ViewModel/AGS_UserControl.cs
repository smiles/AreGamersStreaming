using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace AreGamersStreaming.ViewModel
{
    using Smiles.MvvM.Lib;
    using Properties;
    using AreGamersStreaming.Model;
    using AreGamersStreaming.Twitch;
    using View;

    public class AGS_UserControl : CommonBase
    {
        private IUserPref _Preference = new UserPref();
        private string _AddStreamInput;

        public string AddStreamInput
        {
            get{return _AddStreamInput;}
            set
            {
                if (_AddStreamInput != value)
                {
                    _AddStreamInput = value;
                    RaisePropertyChanged("AddStreamInput");
                }
            }
        }

        public bool IsStartBoot
        {
            get { return _Preference.IsStartAtBoot; }
            set
            {
                if (_Preference.IsStartAtBoot != value)
                {
                    _Preference.IsStartAtBoot = value;
                    RaisePropertyChanged("IsStartBoot");
                }
            }
        }

        public bool IsMinStart
        {
            get {return _Preference.IsMinamizeAtStart; }
            set
            {
                if(_Preference.IsMinamizeAtStart != value)
                {
                    _Preference.IsMinamizeAtStart = value;
                    RaisePropertyChanged("IsMinStart");
                }
            }
        }

        public ICommand AddButton
        {
            get { return new DelegateCast(_TheAddButton); }
        }

        private void _TheAddButton()
        {
            if(AddStreamInput != null)
            {
                if (TwitchValidation.IsValidStream(this.AddStreamInput)) 
                {
                    
                    this.AddStreamInput = string.Empty;
                }
                else
                {
                    MessageBox.Show("fail");
                }
            }
        }
    }
}
