using System;

namespace Entities.Administration
{
    public class City
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Nullable<int> Fips { get; set; }

        public Nullable<decimal> Lat { get; set; }

        public Nullable<decimal> Long { get; set; }

        public Nullable<int> Confirmed { get; set; }

        public Nullable<int> Deaths { get; set; }

        public Nullable<int> Confirmed_diff { get; set; }

        public Nullable<int> Deaths_diff { get; set; }

        public Nullable<DateTime> Last_update { get; set; }
    }
}
