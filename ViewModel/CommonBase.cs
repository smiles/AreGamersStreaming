using System.ComponentModel;

namespace Smiles.MvvM.Lib
{
    public class CommonBase : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if(handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }
}
