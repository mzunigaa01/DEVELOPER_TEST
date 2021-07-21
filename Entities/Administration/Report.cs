using System;
using System.Collections.Generic;

namespace Entities.Administration
{
    public class Report
    {
        public DateTime Date { get; set; }

        public int Confirmed { get; set; }

        public int Deaths { get; set; }

        public int Recovered { get; set; }

        public int Confirmed_diff { get; set; }

        public int Deaths_diff { get; set; }

        public int Recovered_diff { get; set; }

        public DateTime Last_update { get; set; }

        public int Active { get; set; }

        public int Active_diff { get; set; }

        public Nullable<decimal> Fatality_rate { get; set; }

        public Region Region { get; set; }  
    }
}
