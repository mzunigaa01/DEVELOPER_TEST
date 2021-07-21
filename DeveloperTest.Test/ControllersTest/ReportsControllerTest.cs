using DeveloperTest.Controllers;
using Entities.Administration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeveloperTest.Test.ControllersTest
{
    [TestClass]
    public class ReportsControllerTest : Controller
    {
        [TestClass]
        public class ReportsList : ReportsControllerTest
        {
            [TestMethod]
            public async Task ListReportsIsNotNull()
            {
                //Prepare
                var aver = new HttpClient();

                ReportsController reportsController = new ReportsController();
                //Test
                var result = await reportsController.ListReports() as IAsyncResult;

                //Verification
                Assert.IsNotNull(result);
            }

            [TestMethod]
            public async Task ListReportsByRegionIsNotNull()
            {
                ReportsController reportsController = new ReportsController();
                string regionName = "US";

                var result = await reportsController.ListReportsByRegion(regionName);

                Assert.IsNotNull(result);
            }

            [TestMethod]
            public async Task ListRegionsIsNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();

                //Test
                var result = await reportsController.ListRegions() as IAsyncResult;

                //Verification
                Assert.IsNotNull(result);
            }

        }

        [TestClass]
        public class ReportsExport : ReportsControllerTest
        {
            [TestMethod]
            public async Task ExportToXMLIsNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();
                string id = "US";

                //Test
                var result = await reportsController.ExportToXML(id);

                //Verification
                Assert.IsNotNull(result);
            }

            [TestMethod]
            public async Task ExportToJsonIsNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();
                string id = "US";

                //Test
                var result = await reportsController.ExportToJson(id);

                //Verification
                Assert.IsNotNull(result);
            }

            [TestMethod]
            public async Task ExportToCsvIsNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();
                string id = "US";

                //Test
                var result = await reportsController.ExportToJson(id);

                //Verification
                Assert.IsNotNull(result);
            }

        }


        [TestClass]
        public class ReportsObject : ReportsControllerTest
        {
            [TestMethod]
            public void ProvincesDynamicsNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();

                var region = new List<Region>();
                region.Add(new Region() { Iso = "USA", Name = "US", Province = "NewYork", Lat = 22245, Long = -25452 });
                region.Add(new Region() { Iso = "USA", Name = "US", Province = "Washington", Lat = 42345, Long = -45457 });

                var reports = new List<Report>();
                reports.Add(new Report() { Confirmed = 200, Deaths = 100, Region = region[0]});
                reports.Add(new Report() { Confirmed = 200, Deaths = 100, Region = region[1] });

                //Test
                var result = reportsController.ProvincesDynamics(reports);

                //Verification
                Assert.IsNotNull(result);
            }

 
            [TestMethod]
            public void  RegionsDynamicsisNotNull()
            {
                //Prepare
                ReportsController reportsController = new ReportsController();

                var region = new List<Region>();
                region.Add(new Region() { Iso = "USA", Name = "US", Province = "NewYork", Lat = 22245, Long = -25452 });

                var reports = new List<Report>();
                reports.Add(new Report() { Confirmed = 200, Deaths = 100, Region = region[0]});

                //Test
                var result = reportsController.RegionsDynamics(reports) ;

                //Verification
                Assert.IsNotNull(result);
            }
        }

    }
}
