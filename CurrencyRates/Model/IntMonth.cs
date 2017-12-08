using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRates.Model
{
    internal struct IntMonth
    {
        /// <summary>
        /// получает номер 
        /// месяца в году
        /// </summary>
        /// <param name="month">строковое представление имени месяца</param>
        /// <returns>число месяца в году</returns>
        internal int GetIntMonth(string month)
        {
            string[] GetMonth =
            {
                "Январь", "Февраль", "Март", "Апрель",
                "Май", "Июнь", "Июль", "Август", "Сентябрь",
                "Октябрь", "Ноябрь", "Декабрь"
            };

            return Array.IndexOf(GetMonth, month) + 1;
        }
    }
}
