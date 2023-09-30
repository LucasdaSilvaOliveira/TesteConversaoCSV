using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteConversaoCSV
{
    public class PGFNMap : ClassMap<PGFN>
    {
        public PGFNMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
        }
    }
}
