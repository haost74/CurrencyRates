using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using CurrencyRates.Types;

namespace CurrencyRates.ViewModel
{
    public class DataModel : INotifyPropertyChanged
    {

        private ObservableCollection<Types.ValCursValute> getRarters =
            new ObservableCollection<Types.ValCursValute>();
        /// <summary>
        /// содержит значения курсовых котировок 
        /// различный пар валют на последний момент времени
        /// </summary>
        public ObservableCollection<Types.ValCursValute> GetRarters
        {
            get { return getRarters; }
            set { getRarters = value; OnPropertyChanged(); }
        }

        private Types.ValCursValute rateUSD = new Types.ValCursValute();
        /// <summary>
        /// последнее значение по паре 
        /// долар рубль
        /// </summary>
        public Types.ValCursValute RateUSD
        {
            get { return rateUSD; }
            set
            {
                rateUSD = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CurrencyName> currencyNames =
            new ObservableCollection<CurrencyName>();
        /// <summary>
        /// содержит список вылют
        /// </summary>
        public ObservableCollection<CurrencyName> CurrencyName
        {
            get { return currencyNames; }
            set
            {
                currencyNames = value;
                OnPropertyChanged();
            }
        }

        private CurrencyName selectCurrencyName = null;
        /// <summary>
        /// выбранное имя валюты
        /// </summary>
        public CurrencyName SelectCurrencyName
        {
            get { return selectCurrencyName; }
            set { selectCurrencyName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> listMonths =
            new ObservableCollection<string>() {"Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        public ObservableCollection<string> ListMonths
        {
            get { return listMonths; }
        }

        private ObservableCollection<string> listMonthsTo =
           new ObservableCollection<string>(); //{ "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        public ObservableCollection<string> ListMonthsTo
        {
            get { return listMonthsTo; }
            set
            {
                listMonthsTo = value;
                OnPropertyChanged();
            }
        }

        private string fromMonth = "";
        /// <summary>
        /// выбранный месяц от 
        /// </summary>
        public string FromMonth
        {
            get { return fromMonth; }
            set { fromMonth = value; OnPropertyChanged(); }
        }

        private string toMonth = "";
        /// <summary>
        ///выбранный месяц до
        /// </summary>
        public string ToMonth
        {
            get { return toMonth; }
            set { toMonth = value; OnPropertyChanged(); }
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
