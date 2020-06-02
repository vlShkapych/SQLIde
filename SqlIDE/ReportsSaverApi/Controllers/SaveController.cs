using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsSaverApi.Model;

namespace ReportsSaverApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaveController: ControllerBase
    {
        [HttpPost]
        public  string Post(Report rep)
        {
            Console.WriteLine(rep.Path);
            return "hello";
        }
        
    }
}