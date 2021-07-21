using Entities.Administration;
using LogicLayer.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Administration
{
    public class blReports
    {
        public async Task<IEnumerable<Report>> ListAsync() => await new llReports().ListAsync();

        public async Task<IEnumerable<Report>> ListAsyncByRegion(string regionName) => await new llReports().ListAsyncByRegion(regionName);
    }
}
