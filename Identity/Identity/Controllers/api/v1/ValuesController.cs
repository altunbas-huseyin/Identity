using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using Identity.Filters;
using Identity.Middleware;
using IdentityHelper;
using Microsoft.Extensions.Configuration;

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    
    public class ValuesController : Controller
    {
        IConfiguration configuration;
        public ValuesController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        // GET api/values
        [ValidateModel("")]
        [HttpGet]
        public CommonApiResponse Get()
        {


            var t = CommonApiResponse.Create(System.Net.HttpStatusCode.Accepted,true,"","");
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
