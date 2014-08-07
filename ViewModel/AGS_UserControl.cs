using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace AreGamersStreaming.ViewModel
{
    using Smiles.MvvM.Lib;
    using AreGamersStreaming.Model;
    using AreGamersStreaming.Twitch;

    public class AGS_UserControl : CommonBase
    {
        #region Private Variables

        private IUserPref _Preference = new UserPref();
        private string _AddStreamInput;
        private List<string> _StreamList = new List<string>();
        private ObservableCollection<string> _ComboBoxList = new ObservableCollection<string>();

        #endregion

        public static event EventHandler ListHasBeenUpdatedEvent;
        public static event EventHandler HowOftenToCheckUpdatedEvent;

        #region DataBindings

        public AGS_UserControl()
        {
            _StreamList = _Preference.AllStreamList;
            _PopulateComboBox();
        }

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

        public ObservableCollection<string> ComboBoxList
        {
            get { return _ComboBoxList; }
        }

        public string SelectedComboBoxItem
        {
            get;
            set;
        }

        #endregion

        #region UI Click Events

        public ICommand AddButton
        {
            get { return new DelegateCast(_TheAddButton); }
        }

        public ICommand DelButton
        {
            get { return new DelegateCast(_TheDeleteButton); }
        }

        #endregion

        #region Private Methods

        private void _TheAddButton()
        {
            if(AddStreamInput != null)
            {
                if (TwitchValidation.IsValidStream(this.AddStreamInput)) 
                {
                    _AddToStreamList();
                    _OnListHasBeenUpdatedEvent();
                }
                else
                {
                    MessageBox.Show("fail");
                }
            }
        }

        private void _AddToStreamList()
        {
            if (this.AddStreamInput.StartsWith("http://"))
            {
                this.AddStreamInput = this.AddStreamInput.Substring(7, this.AddStreamInput.Length - 7);
            }
            _ComboBoxList.Add(this.AddStreamInput);
            _StreamList.Add(this.AddStreamInput);
            _Preference.AllStreamList = _StreamList;
            this.AddStreamInput = string.Empty;
        }

        private void _TheDeleteButton()
        {
            if(this.SelectedComboBoxItem != null)
            {
                _RemoveFromStreamList();
                _OnListHasBeenUpdatedEvent();
            }
        }

        private void _RemoveFromStreamList()
        {
            _StreamList.Remove(this.SelectedComboBoxItem);
            _ComboBoxList.Remove(this.SelectedComboBoxItem);
            _Preference.AllStreamList = _StreamList;
        }

        private void _PopulateComboBox()
        {
            if(_StreamList != null)
            {
                foreach(string AllStreamURL in _StreamList)
                {
                    _ComboBoxList.Add(AllStreamURL);
                }
            }
        }

        private void _OnListHasBeenUpdatedEvent()
        {
            EventHandler handler = ListHasBeenUpdatedEvent;
            
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void _OnHowOftenToCheckUpdatedEvent()
        {
            EventHandler handler = HowOftenToCheckUpdatedEvent;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion

    }
}
