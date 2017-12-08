using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CurrencyRates.Types
{
    [System.Serializable]
    public class CurrencyName : INotifyPropertyChanged
    {
        private string id = "";
        /// <summary>
        /// условное обозначение валюты 
        /// в системе ЦБ
        /// </summary>
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string name = "";
        /// <summary>
        /// имя валюты в системе ЦБ
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        #region - INotifyPropertyChanged -
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
