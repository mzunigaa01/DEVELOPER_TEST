using DataLayer;
using Entities.Administration;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace LogicLayer.Administration
{
    public class llRegions
    {
        private Core core;

        public async Task<IEnumerable<Region>> ListRegionsAsync()
        {
            core = new Core();
            var result = await core.Get<IEnumerable<Region>>(request =>
            {
                request.Path = $"/regions";
            });

            return result.Data;
        }
    }
}
