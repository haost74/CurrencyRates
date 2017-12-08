using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRates.Types
{
    public class Currency
    {
        /// <summary>
        /// имя валюты 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// абравиатура вылюты
        /// </summary>
        public string СurrencyNameCode { get; set; }
        /// <summary>
        /// валютный кур на данный момент времени
        /// </summary>
        public double CurrencyCourse { get; set; }
    }
}
