using CurrencyRates.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CurrencyRates.Model
{
    internal class CurrencyRates
    {
        #region singleton
        private static object objLock = new object();
        private static CurrencyRates initialize;

        internal static CurrencyRates Initialize
        {
            get
            {
                if (initialize == null)
                    lock (objLock)
                        if (initialize == null)
                            initialize = new CurrencyRates();

                return initialize;
            }
        }
        #endregion

        /// <summary>
        /// асинхронно получает список  
        /// котировок актуальных на данный момент времени
        /// </summary>
        /// <returns>Task System.Collections.ObjectModel.ObservableCollection Types.Currency</returns>
        internal async Task<System.Collections.ObjectModel.ObservableCollection<Types.Currency>> RatesAsync()
        {
            return await Task.Factory.StartNew(() => { return Rates(); });
        }

        /// <summary>
        /// получает список 
        /// котировок актуальных на данный момент времени
        /// </summary>
        /// <returns>System.Collections.ObjectModel.ObservableCollection Types.ValueCurs 
        /// </returns>
        internal System.Collections.ObjectModel.ObservableCollection<Types.Currency> Rates()
        {
            System.Collections.ObjectModel.ObservableCollection<Types.Currency> result =
                            new System.Collections.ObjectModel.ObservableCollection<Types.Currency>();
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Types.ValCurs));
                

                XmlReader xr = new XmlTextReader(@"http://www.cbr.ru/scripts/XML_daily.asp");
                
                System.Threading.Thread.Sleep(5000);
                foreach (Types.ValCursValute item in ((Types.ValCurs)xs.Deserialize(xr)).ValuteList)
                {
                    double temp = default(double);
                    if (!double.TryParse(item.ExchangeRate, out temp)) continue;

                    result.Add(new Types.Currency()
                    {
                        Name = item.ValuteName,
                        СurrencyNameCode = item.ValuteStringCode,
                        CurrencyCourse = temp

                    });
                }
            }
            catch (Exception ex)
            {
                result.Add(new Types.Currency()
                {
                    Name = ex.Message,
                    СurrencyNameCode = "Проверьте интернет-подключение или повторите попытку позже.",
                    CurrencyCourse = 0d
                });
                return result;
            }
            
            return result;
        }

        /// <summary>
        /// получает данные с сервиса ЦБ 
        /// по паре доллар рубль
        /// </summary>
        /// <returns></returns>
        internal System.Collections.ObjectModel.ObservableCollection<Types.Currency> RatesService()
        {
            System.Collections.ObjectModel.ObservableCollection<Types.Currency> result =
                            new System.Collections.ObjectModel.ObservableCollection<Types.Currency>();

            using (DelyInfoWebServer.DailyInfoSoapClient client = new DelyInfoWebServer.DailyInfoSoapClient())
            {
                DataSet dataSet = client.GetCursOnDate(DateTime.Now);
                DataTableCollection collectionTables = dataSet.Tables;
                DataTable table = collectionTables[0];


                foreach(DataRowCollection row in table.Rows)
                {
                    var i = row[0].ItemArray;
                }
            }

            return result;
        }

        //----------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// получает данные по котировкам и времени котирования
        /// </summary>
        /// <param name="FromMonth">месяц от</param>
        /// <param name="ToMonth">месяц до</param>
        /// <param name="IdСurrency">id валюты</param>
        internal async Task<CurrencyOptions> GetDataCurrency(string FromMonth, string ToMonth, string IdСurrency)
        {
            CurrencyOptions currencyOptions = null;

            using (DelyInfoWebServer.DailyInfoSoapClient client = new DelyInfoWebServer.DailyInfoSoapClient())
            {
                try
                {
                    int year = DateTime.Now.Year;
                    IntMonth im = new Model.IntMonth();
                    int fromMonth = im.GetIntMonth(FromMonth);
                    int toMonth = im.GetIntMonth(ToMonth);

                    DateTime FromDateTime = new DateTime(year, fromMonth, 01);
                    DateTime ToDateTime = new DateTime(year, toMonth, DateTime.DaysInMonth(year, toMonth));

                    DataSet dataSet = await client.GetCursDynamicAsync(FromDateTime, ToDateTime, IdСurrency);
                    DataTableCollection collectionTables = dataSet.Tables;

                    DataTable table = collectionTables[0];
                    int countRows = table.Rows.Count;

                    currencyOptions = new Types.CurrencyOptions(countRows);

                    for (int i = 0; i < countRows; ++i)
                    {
                        currencyOptions.Prices[i] = Convert.ToDouble(table.Rows[i].ItemArray[3]);
                        currencyOptions.DateTime[i] = Convert.ToDateTime(table.Rows[i].ItemArray[0]).ToString("dd/MM/yyyy");
                    }
                }
                catch { System.Windows.MessageBox.Show("Проверьте интернет-подключение или повторите попытку позже.");}
            }

            return currencyOptions;
        }
    }
}
