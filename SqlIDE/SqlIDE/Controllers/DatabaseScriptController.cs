using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SqlIDE.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlIDE.shared;
namespace SqlIDE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DatabaseScriptController : Controller
    {


        [HttpPost("run")]
        public async Task<ActionResult<string>> Run([FromBody]Script scr)
        {
            DbResponse res;
            scr.User = new User()
            {
                Id = 1,
                AccType = "moderator",
                Name = "Vlad"
            };
           
            var db = new DbService(scr);
            res = await db.RunScriptAsync();
            
            string jsonRes = JsonConvert.SerializeObject(res);
            return Ok(jsonRes);
        }
    }
}
