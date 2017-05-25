using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers.api.v1
{
    [Route("api/[controller]")]
    public class UserRoleController : Controller
    {
     

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string UserId, String RoleId)
        {
        }

        
        // DELETE api/values/5
        [HttpDelete]
        public void Delete(string RoleId)
        {
        }
    }
}
