using Entities.Administration;
using LogicLayer.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Administration
{
    public class blRegions
    {
        public async Task<IEnumerable<Region>> ListRegionsAsync() => await new llRegions().ListRegionsAsync();
    }
}
