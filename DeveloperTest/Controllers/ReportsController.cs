using BusinessLayer.Administration;
using Entities.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace DeveloperTest.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Reports";
            return View();
        }

        public async Task<ActionResult> ListReports()
        {
            var datos = RegionsDynamics(await new blReports().ListAsync());
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ListReportsByRegion(string regionName)
        {
            var datos = ProvincesDynamics(await new blReports().ListAsyncByRegion(regionName));
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ListRegions()
        {
            var datos = RegionsDynamics(await new blReports().ListAsync());
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<ActionResult> ExportToXML(string id)
        {
            try
            {
                object reports;
                string node = string.Empty;
                if (string.IsNullOrEmpty(id))
                {
                    reports = RegionsDynamics(await new blReports().ListAsync());
                    node = "Region";
                }
                else
                {
                    reports = ProvincesDynamics(await new blReports().ListAsyncByRegion(id));
                    node = "Province";
                }

                MemoryStream ms = new MemoryStream();
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;
                xws.Indent = true;
                XmlDocument xml = new XmlDocument();
                XmlElement root = xml.CreateElement(node);
                xml.AppendChild(root);

                foreach (PropertyInfo propertyInfo in reports.GetType().GetProperties())
                {
                    var Key = propertyInfo.Name;
                    var data = (IEnumerable<dynamic>)propertyInfo.GetValue(reports, null);
                    foreach (var item in data)
                    {
                        XmlElement child = xml.CreateElement("Row");

                        if (!string.IsNullOrEmpty(id))
                            child.SetAttribute("province", item.province);
                        else
                            child.SetAttribute("region", item.region);
                        child.SetAttribute("cases", Convert.ToString(item.cases));
                        child.SetAttribute("deaths", Convert.ToString(item.deaths));
                        root.AppendChild(child);
                    }
                }

                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/xml";
                Response.AddHeader("Content-Disposition", "attachment; filename=TOP 10 COVID-19 CASES.xml;");
                Response.Output.Write(xml.OuterXml.ToString());
                Response.Flush();
                Response.End();
                return View();
            }
            catch (Exception)
            {
                //Register exception in database log
                throw;
            }
        }

        public async Task<ActionResult> ExportToJson(string id)
        {
            try
            {
                object reports;
                string node = string.Empty;
                if (string.IsNullOrEmpty(id))
                {
                    reports = RegionsDynamics(await new blReports().ListAsync());
                    node = "Region";
                }
                else
                {
                    reports = ProvincesDynamics(await new blReports().ListAsyncByRegion(id));
                    node = "Province";
                }
                string jsonProductList = new JavaScriptSerializer().Serialize(reports);

                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/json";
                Response.AddHeader("Content-Length", jsonProductList.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=TOP 10 COVID-19 CASES.json;");
                Response.Output.Write(jsonProductList);
                Response.Flush();
                Response.End();

                return View("Index");
            }
            catch (Exception)
            {
                //Register exception in database log
                throw;
            }
        }

        public async Task<ActionResult> ExportToCsv(string id)
        {
            try
            {
                object reports;
                string node = string.Empty;
                if (string.IsNullOrEmpty(id))
                {
                    reports = RegionsDynamics(await new blReports().ListAsync());
                    node = "Region";
                }
                else
                {
                    reports = ProvincesDynamics(await new blReports().ListAsyncByRegion(id));
                    node = "Province";
                }

                var sw = new StringWriter();
                sw.WriteLine(String.Format("{0},{1},{2}", node, "cases", "deaths"));
                foreach (PropertyInfo propertyInfo in reports.GetType().GetProperties())
                {
                    var Key = propertyInfo.Name;
                    var data = (IEnumerable<dynamic>)propertyInfo.GetValue(reports, null);
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(id))
                            sw.WriteLine(String.Format("{0},{1},{2}", item.province, item.cases, item.deaths));
                        else
                            sw.WriteLine(String.Format("{0},{1},{2}", item.region, item.cases, item.deaths));
                    }
                }

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=TOP 10 COVID-19 CASES.csv");
                Response.ContentType = "text/csv";
                Response.Write(sw);
                Response.End();

                return View("Index");
            }
            catch (Exception)
            {
                //Register exception in database log
                throw;
            }
        }

        public object ProvincesDynamics(IEnumerable<Report> reports)
        {
            var data =
            from item in reports
            group item by item.Region.Province into myGroup
            select new
            {
                province = myGroup.Key,
                cases = myGroup.Sum(x => x.Confirmed),
                deaths = myGroup.Sum(x => x.Deaths)
            };

            var list = new
            {
                data = data.Select(x => new
                {
                    province = string.IsNullOrEmpty(x.province) ? "NA" : x.province,
                    cases = x.cases,
                    deaths = x.deaths
                })
                .OrderByDescending(x => x.cases)
                .Take(10)
            };

            return list;
        }

        public object RegionsDynamics(IEnumerable<Report> reports)
        {
            var data =
            from item in reports
            group item by item.Region.Name into myGroup
            select new
            {
                region = myGroup.Key,
                cases = myGroup.Sum(x => x.Confirmed),
                deaths = myGroup.Sum(x => x.Deaths)
            };

            var list = new
            {
                data = data.Select(x => new
                {
                    region = x.region,
                    cases = x.cases,
                    deaths = x.deaths
                })
                .OrderByDescending(x => x.cases)
                .Take(10)
            };
            return list;
        }
    }
}