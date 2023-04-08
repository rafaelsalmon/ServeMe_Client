using System.ComponentModel;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Base
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        private bool _isEmpty;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                RaisePropertyChanged("IsEmpty");
                RaisePropertyChanged("IsNotEmpty");
            }
        }

        public bool IsNotEmpty => (!_isEmpty);

        private bool _isBusy;
        public bool IsNotBusy => (!_isBusy);

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
                RaisePropertyChanged("IsNotBusy");
            }
        }

        private bool _isError;

        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                RaisePropertyChanged("IsError");
            }
        }

        public void RaisePropertyChanged(string name)
        {
            var pc = PropertyChanged;
            if (pc != null) pc(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
