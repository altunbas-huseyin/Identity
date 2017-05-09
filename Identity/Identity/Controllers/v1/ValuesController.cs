using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using Identity.Filters;
using Identity.Middleware;

namespace Identity.Controllers
{

    [Route("api/v1/[controller]")]
    [ValidateModel]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public CommonApiResponse Get()
        {
            var r = this.ViewBag.Jwt;
            var t = CommonApiResponse.Create(System.Net.HttpStatusCode.OK, new string[] { "value1", "value2" });
            //  return new string[] { "value1", "value2" };
            return t;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
