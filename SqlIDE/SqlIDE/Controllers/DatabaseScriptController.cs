using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using SqlIDE.shared;
namespace SqlIDE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DatabaseScriptController : Controller
    {


        [HttpPost("run")]
        public void Run([FromBody]Script scr)
        {
            Console.WriteLine(scr.script);
        }
    }
}
