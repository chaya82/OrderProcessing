using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderProcessing.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Default/5
        public string Get(int? ProductID, int? Year)
        {
            string City = "";
            var City_Param = new SqlParameter("@city", City);
            var Year_Param = new SqlParameter("@year", Year);
            var ProductID_Param = new SqlParameter("@product", ProductID);

            //var courseList = db.Database.SqlQuery<sales_dtl>("exec SP_GetTotalSales @city='" + City_Param.Value + "', @year=" + Year_Param.Value + ", @product=" + ProductID_Param.Value + "").ToList<sales_dtl>();
            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new StringContent(JArray.FromObject(courseList).ToString(), Encoding.UTF8, "application/json")
            //};
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
