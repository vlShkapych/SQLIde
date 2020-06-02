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
        [HttpPost("checkConnection")]
        public string CheckConnection([FromBody]Script scr)
        {
            var dbRes = new DbResponse();

            
            var db =  DbService.GetDbService();
            

            db.OpenSession(scr);
            dbRes =  db.CheckConnection();
            db.CloseSession();
            string jsonRes = JsonConvert.SerializeObject(dbRes);
            return jsonRes;
        }
        
        [HttpPost("run")]
        public async Task<ActionResult<string>> Run([FromBody]Script scr)
        {
            DbResponse res;
         //   Console.WriteLine(scr.User.AccType);
           
            var db =  DbService.GetDbService();
            db.OpenSession(scr);
            res = await db.RunScriptAsync(scr.DbScript);
            db.CloseSession();
            string jsonRes = JsonConvert.SerializeObject(res);
            
            return Ok(jsonRes);
        }
    }
}
