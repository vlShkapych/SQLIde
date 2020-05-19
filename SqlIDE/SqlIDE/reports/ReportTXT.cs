using System;
using System.IO;
using SqlIDE.Accounts;
using SqlIDE.shared;

namespace SqlIDE.reports
{
    public class ReportTXT: IVisitor
    {
        public void ModeratorReportTXT(Account acc,string action, string result)
        {
            var name = "ModeratorLogs.txt";
            var path = @"C:\Users\Владислав\Desktop\SqlIDE\SqlIDE\reportsFiles\"+name;
            var text = $"[{DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}] ({acc.user.Id}:{acc.user.Name})|"+ 
                       $"{action}=>{result}";
            ReportsCreator.createTxt(path, text);
        }

    }
}