using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Administration
{
    public class Region
    {
        public string Iso { get; set; }

        public string Name { get; set; }

        public string Province { get; set; }

        public Nullable<decimal> Lat { get; set; }

        public Nullable<decimal> Long { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
