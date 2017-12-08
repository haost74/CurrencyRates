using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyRates.Types
{
    public class ValCursValute
    {
        [XmlElementAttribute("CharCode")]
        public string ValuteStringCode;

        [XmlElementAttribute("Name")]
        public string ValuteName;

        [XmlElementAttribute("Value")]
        public string ExchangeRate;

        //[XmlAnyElement("Nominal")]
        //public string Nominal;

        //[XmlElementAttribute("NumCode")]
        //public string NumCode;
    }
}
