using CurrencyRates.Model;
using CurrencyRates.Types;
using CurrencyRates.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CurrencyRates
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region DATA
        /// <summary>
        /// признак включения кнопок выбора валюты
        /// при доступе к сети интернет true
        /// иначе                       false
        /// </summary>
        private bool isEnabletBtn = false;
        public static MainWindow Customer = null;
        private int oldMinute = -1;
        #endregion
        #region CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();
            Customer = this;
            #region ICON
            System.Drawing.Bitmap bmp = default(System.Drawing.Bitmap);
            bmp = Properties.Resources.CBRF;
            IntPtr nIcon = bmp.GetHicon();
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Icon = bs;
            #endregion

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<CurrencyName> temp =
                new ObservableCollection<CurrencyName>();

            CurrencyName cn1 = new Types.CurrencyName()
            {ID = "R01010", Name = "Австралийский доллар" };
            CurrencyName cn2 = new Types.CurrencyName()
            { ID = "R01035", Name = "Фунт стерлингов" };
            CurrencyName cn3 = new Types.CurrencyName()
            { ID = "R01215", Name = "Датских крон" };
            CurrencyName cn4 = new Types.CurrencyName()
            { ID = "R01235", Name = "Доллар США" };
            CurrencyName cn5 = new Types.CurrencyName()
            { ID = "R01239", Name = "Евро" };
            CurrencyName cn6 = new Types.CurrencyName()
            { ID = "R01350", Name = "Канадский доллар" };

            temp.Add(cn1); temp.Add(cn2); temp.Add(cn3);
            temp.Add(cn4); temp.Add(cn5); temp.Add(cn6);

            Data.CurrencyName = temp;
            isEnabletBtn = true;
           
        }
        #endregion
        #region COMMANDS
        #region Refresh
        /// <summary>
        /// обновляет данные по выбранной валюте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Start(object sender, ExecutedRoutedEventArgs e)
        {
            var task = Model.CurrencyRates.Initialize.GetDataCurrency(Data.FromMonth, Data.ToMonth, Data.SelectCurrencyName.ID);


            IntMonth im = new Model.IntMonth();
            int fromMonth = im.GetIntMonth(Data.FromMonth);
            int toMonth = im.GetIntMonth(Data.ToMonth);
            Chart.Data.axismin = new DateTime(2017, fromMonth, 01);
            Chart.Data.axismax = new DateTime(2017, toMonth, 01);
            Chart.Data.SetUpModel();

            task.ContinueWith(item =>
            {
                if (item.Result.Prices.Count() == 0) { MessageBox.Show("Котировки за данный период недоступны."); return; }
                if (chart.ChartAreas.Count == 0)
                {
                    // Все графики находятся в пределах области построения ChartArea, создадим ее
                    chart.ChartAreas.Add(new ChartArea("Default"));

                    // Добавим линию, и назначим ее в ранее созданную область "Default"
                    chart.Series.Add(new Series("Series1"));
                    chart.Series["Series1"].ChartArea = "Default";
                    chart.Series["Series1"].ChartType = SeriesChartType.Area;//.Area;
                }

                chart.Series["Series1"].Points.DataBindXY(item.Result.DateTime, item.Result.Prices);
                OxyTest.Customer.Start(2.5, item.Result.Prices, item.Result.DateTime);

            }, TaskScheduler.FromCurrentSynchronizationContext());            
        }

        private void CommandBinding_CanExecute_Start(object sender, CanExecuteRoutedEventArgs e)
        {
            #region обновляем курс USD при условии активности пользователя один раз в минуту
            int minute = DateTime.Now.Minute;
            if (oldMinute != minute)
            {
                var task = Model.CurrencyRates.Initialize.RatesAsync();
                task.ContinueWith(item =>
                {
                    try
                    {
                        var list = item.Result;
                        if (list.Count > 1)
                        {
                            var coll = (from ell in list
                                        where ell.СurrencyNameCode == "USD"
                                        select ell).ToList();

                            if (coll.Count != 0)
                                this.Title = "Официальный курс " + coll[0].СurrencyNameCode + " = " + coll[0].CurrencyCourse.ToString() + "\t" + DateTime.Now.ToString("HH:mm:ss");

                        }
                        else
                            this.Title = list[0].СurrencyNameCode;
                    }
                    catch { }

                }, TaskScheduler.FromCurrentSynchronizationContext());

                oldMinute = minute;
            }
            #endregion

            if (Data == null || Data.SelectCurrencyName == null || Data.FromMonth == "" || Data.ToMonth == "") return;

            if (this.Title.Contains("USD") && isEnabletBtn)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        #endregion

        #endregion
        #region выбор элемента из ComboBox From
        /// <summary>
        /// выбор элемента из ComboBox From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Data.ListMonthsTo.Clear();
            Data.ListMonthsTo = new ObservableCollection<string>((Data.ListMonths.Where(item =>
            Data.ListMonths.IndexOf(item) >= Data.ListMonths.IndexOf(Data.FromMonth))).ToList());
            Data.ToMonth = "";
        } 
        #endregion
    }
}
