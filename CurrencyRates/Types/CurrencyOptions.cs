using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRates.Types
{
    /// <summary>
    /// параметры курсы валюты
    /// </summary>
    internal class CurrencyOptions
    {
        /// <summary>
        /// содержит список цен
        /// </summary>
        public double[] Prices = null;
        /// <summary>
        /// содержит список времени 
        /// исполнения котирования
        /// </summary>
        public string[] DateTime = null;


        /// <summary>
        /// инициализируем массивы и определяем их размер
        /// </summary>
        /// <param name="size"></param>
        public CurrencyOptions(int size)
        {
            Prices = new double[size];
            DateTime = new string[size];
        }
    }
}
