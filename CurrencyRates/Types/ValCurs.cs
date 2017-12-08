using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyRates.Types
{
    public class ValCurs
    {
        [XmlElementAttribute("Valute")]
        public ValCursValute[] ValuteList;
    }
}
