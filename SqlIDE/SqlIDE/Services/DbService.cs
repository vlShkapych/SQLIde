using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlIDE.shared;
using SqlIDE.Accounts;
using SqlIDE.Databases;
using SqlIDE.reports;

namespace SqlIDE.Services
{
    public class DbService
    {
        private Dictionary<string, IDatabase> _databases;
        private Dictionary<string, Account> _accounts;
        private Account _account;
        private string _scriptText;
        IObservable dbObservable;

        public DbService(Script script)
        {
            _scriptText = script.DbScript;
            _databases = new Dictionary<string, IDatabase>();
            _accounts = new Dictionary<string, Account>();

            _databases.Add("pgDb", new PostgreSQl(script.ConStr));


            IDatabase db = _databases.GetValueOrDefault(script.DbType);
            



            _accounts.Add("moderator", new Moderator(db, script.User));

            _account = _accounts.GetValueOrDefault(script.User.AccType);
        }

        public async Task<DbResponse> RunScriptAsync()
        {
            if (_account.canRunScript())
            {
                _account.Connect();
                var res = _account.RunScript(_scriptText);
                _account.Accept(new ReportTXT());
            }

            return new DbResponse()
            {
                Message = "Access denied",
                Table = ""
            };
        }
        
    }
}