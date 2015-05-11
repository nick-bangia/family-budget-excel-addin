using System.ComponentModel;

namespace FamilyBudget.Data.Domain
{
    public class ManagedDataObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                // if the PropertyChanged event is subscribed to, then fire the event
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
