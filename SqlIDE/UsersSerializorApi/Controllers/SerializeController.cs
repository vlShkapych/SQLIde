using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UsersSerializorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerializeController : ControllerBase
    {
        public SerializeController()
        {

        }
    
        [HttpPost("writeReport/txt")]
        public string WriteReport([FromBody]Report report)
        {
            Console.WriteLine(report.Path);
            Console.WriteLine(report);
            return "ok";
            // DirectoryInfo dirInfo = new DirectoryInfo(report.Path);
            // try
            // {
            //     if (!dirInfo.Exists)
            //     {
            //         dirInfo.Create();
            //     }
            //     using (var stream = new FileStream(
            //         report.Path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
            //     {
            //         var bytes = Encoding.UTF8.GetBytes(report.Text);
            //         stream.Write(bytes, 0, bytes.Length);
            //     }
            //
            //     return "Message saved in dir:"+report.Path;
            // }
            // catch (Exception e)
            // {
            //     return e.Message;
            // }


        }
    }
}