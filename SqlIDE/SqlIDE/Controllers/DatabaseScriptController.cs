using System;
using Microsoft.AspNetCore.Http;
using SqlIDE.Services;
using Microsoft.AspNetCore.Mvc;

using SqlIDE.shared;
namespace SqlIDE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DatabaseScriptController : Controller
    {


        [HttpPost("run")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Run([FromBody]Script scr)
        {
            string res = "";
            scr.User = new User()
            {
                Id = 1,
                AccType = "moderator",
                Name = "Vlad"
            };
           
            var db = new DbService(scr);
            res = db.RunScript();
            Console.WriteLine(res);
            return res;
        }
    }
}
