using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using OrderProcessing.Models;

namespace OrderProcessing.Controllers
{
    [RoutePrefix("api/Sales")]
    public class SalesController : ApiController
    {
     
        private DbModels db = new DbModels();
        // GET: api/Sales
        [HttpGet]
        public HttpResponseMessage TotalSales(int? ProductID, int ? Year)
        {
            
               // var result = db.SP_GetTotalSales("", 0, 0);

            string City = "";
            var City_Param = new SqlParameter("@city", City);
            var Year_Param = new SqlParameter("@year", Year);
            var ProductID_Param = new SqlParameter("@product", ProductID);

            var courseList = db.Database.SqlQuery<sales_dtl>("exec SP_GetTotalSales @city='" + City_Param.Value + "', @year=" + Year_Param.Value + ", @product=" + ProductID_Param.Value + "").ToList<sales_dtl>();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JArray.FromObject(courseList).ToString(), Encoding.UTF8, "application/json")
            };

            
        }
    }
}
