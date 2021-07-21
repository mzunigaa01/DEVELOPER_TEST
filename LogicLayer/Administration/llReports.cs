using DataLayer;
using Entities.Administration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicLayer.Administration
{
    public class llReports
    {
        private Core core;

        public async Task<IEnumerable<Report>> ListAsync()
        {
            core = new Core();
            var result = await core.Get<IEnumerable<Report>>(request =>
            {
                request.Path = $"/reports";
            });
            if (string.IsNullOrEmpty(result.Message))
                return result.Data;
            else
                return Enumerable.Empty<Report>();
        }

        public async Task<IEnumerable<Report>> ListAsyncByRegion(string regionName)
        {
            core = new Core();
            var result = await core.Get<IEnumerable<Report>>(request =>
            {
                request.Path = $"/reports?region_name={regionName}";
            });

            if (string.IsNullOrEmpty(result.Message))
                return result.Data;
            else
                return Enumerable.Empty<Report>();
        }
    }
}
