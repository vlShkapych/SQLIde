using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SqlIDE.Accounts;
using SqlIDE.shared;
using ReportsSaverApi.Model;


namespace SqlIDE.reports
{
    public class ReportTXT: IVisitor
    {

        public async void ModeratorReportTXT(Account moderator,string action, string result)
        {
            
            var name = "ModeratorLogs.txt";
            var path = @"D:\Users\Владислав\Desktop\SqlIDE\SqlIDE\reportsFiles\"+name;
            var text = $"[{DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}] ({moderator.user.Id}:{moderator.user.Name})|"+ 
                       $"{action}=>{result}";
            Console.WriteLine(text);
            await Save(new Report()
            {
                Path = path,
                Text = text
            });
        }

        public async void AdminReportTXT(Account admin, string action, string result)
        {
            var name = "AdminLogs.txt";
            var path = @"D:\Users\Владислав\Desktop\SqlIDE\SqlIDE\reportsFiles\"+name;
            var text = $"[{DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}] {admin.user.AccType}({admin.user.Id}:{admin.user.Name})|"+ 
                       $"{action}=>{result}";
            Console.WriteLine(text);
            await Save(new Report()
            {
                Path = path,
                Text = text
            });
        }

        private  async Task  Save( Report report)
        {
           
            Console.WriteLine("working");
            var json = JsonConvert.SerializeObject(report);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:6000/save";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }
    }
}