using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using IWshRuntimeLibrary;
using System.IO;

namespace AreGamersStreaming.ViewModel
{
    using Smiles.MvvM.Lib;
    using AreGamersStreaming.Model;
    using AreGamersStreaming.Twitch;
    using AGS_Core;

    public class AGS_UserControl : CommonBase
    {
        #region Private Variables

        private string _AddStreamInput;
        private List<string> _StreamList = new List<string>();
        private ObservableCollection<string> _ComboBoxList = new ObservableCollection<string>();
        private bool _MinToStart;
        private int _HowOftenToCheck;
        private string shortCutPathName = Environment.SpecialFolder.Startup.ToString() + "AreGamersStreaming.lnk";
        private AGS_Logic _AGSLogic = new AGS_Logic();

        #endregion

        #region Construct

        public AGS_UserControl()
        {
            _AGSLogic.LoadPersistentData();
            if (_AGSLogic.StreamList != null)
            {
                _StreamList = _AGSLogic.StreamList;
            }
            _MinToStart = _AGSLogic.IsMinAtStart;
            _HowOftenToCheck = _AGSLogic.HowOftenToCheck;
            _PopulateComboBox();
        }

        #endregion

        #region DataBindings

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
            get { return _AGSLogic.IsStartAtBoot; }
            set
            {
                if (_AGSLogic.IsStartAtBoot != value)
                {
                    _AGSLogic.IsStartAtBoot = value;
                    RaisePropertyChanged("IsStartBoot");
                }
            }
        }

        public bool IsMinStart
        {
            get { return _MinToStart; }
            set
            {
                if(_MinToStart != value)
                {
                    _MinToStart = value;
                    _AGSLogic.IsMinAtStart = value;
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


        public int HowOftenToCheck
        {
            get { return _HowOftenToCheck; }
            set
            {
                if(_HowOftenToCheck != value)
                {
                    _AGSLogic.HowOftenToCheck = value;
                    _HowOftenToCheck = value;
                }
            }
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

        public ICommand CloseApp
        {
            get { return new DelegateCast(_ExitApp); }
        }

        public ICommand ConfigWindow
        {
            get { return new DelegateCast(_ConfigWindow); }
        }

        public ICommand AddOrRemoveStartup
        {
            get { return new DelegateCast(_StartupControl); }
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
                }
                else
                {
                    MessageBox.Show("fail");
                }
            }
        }

        private void _ExitApp()
        {
            Environment.Exit(0);
        }

        private void _ConfigWindow()
        {
            View.ConfigWin config = new View.ConfigWin();
            config.HowOftenCheck.Value = this.HowOftenToCheck;
            config.HowOftenCheck.ValueChanged += HowOftenCheck_ValueChanged;
            config.Show();
        }

        void HowOftenCheck_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.HowOftenToCheck = (int)Math.Round(e.NewValue);
        }


        private void _AddToStreamList()
        {
            if (this.AddStreamInput.StartsWith("http://"))
            {
                this.AddStreamInput = this.AddStreamInput.Substring(7, this.AddStreamInput.Length - 7);
            }
            _ComboBoxList.Add(this.AddStreamInput);
            _StreamList.Add(this.AddStreamInput);
            _AGSLogic.StreamList = _StreamList;
            this.AddStreamInput = string.Empty;
        }

        private void _TheDeleteButton()
        {
            if(this.SelectedComboBoxItem != null)
            {
                _RemoveFromStreamList();
            }
        }

        private void _RemoveFromStreamList()
        {
            _StreamList.Remove(this.SelectedComboBoxItem);
            _ComboBoxList.Remove(this.SelectedComboBoxItem);
            _AGSLogic.StreamList = _StreamList;
        }

        private void _PopulateComboBox()
        {
            if(_StreamList != null)
            {
                _ComboBoxList.Clear();

                foreach(string AllStreamURL in _StreamList)
                {
                    _ComboBoxList.Add(AllStreamURL);
                }
            }
        }

        private void _StartupControl()
        {
            if(this.IsStartBoot)
            {
                _AddShortcutToStartup();
            }
            else
            {
                _RemoveShortcutFromStartup();
            }
        }

        private void _AddShortcutToStartup()
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortCutPathName);

            shortcut.Description = "Are Gamers Streaming Shortcut";
            shortcut.TargetPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            shortcut.Save();
        }

        private void _RemoveShortcutFromStartup()
        {
            if(System.IO.File.Exists(shortCutPathName))
            {
                System.IO.File.Delete(shortCutPathName);
            }
        }

        #endregion

    }
}
