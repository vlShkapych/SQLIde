using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlIDE.shared;
using SqlIDE.Accounts;
using SqlIDE.Databases;
using SqlIDE.reports;

namespace SqlIDE.Services
{
    public class DbService: IObserver
    {
        private Dictionary<string, IDatabase> _databases;
        private Dictionary<string, Account> _accounts;
        private Account _account;
        IObservable dbObservable;
        private string _state;
        private static DbService _instance;
        
        public static DbService GetDbService()
        {
            if (_instance == null)
                _instance = new DbService();
            return _instance;
        }
        
        private DbService() { }

        public void OpenSession(Script script)
        {
            _databases = new Dictionary<string, IDatabase>();
            _accounts = new Dictionary<string, Account>();
        
            _databases.Add("pgDb", new PostgreSQl(script.ConStr));
            _databases.Add("msSql", new MsSql(script.ConStr));
    

            IDatabase db = _databases.GetValueOrDefault(script.DbType);
            
            db.AddObserver(this);
            
            _accounts.Add("moderator", new Moderator(db, script.User));
            _accounts.Add("admin",new Admin(db,script.User));

            _account = _accounts.GetValueOrDefault(script.User.AccType);
        }

        public void CloseSession()
        {
            _databases.Clear();
            _accounts.Clear();
            _account = null;
            dbObservable = null;

        }
        public async Task<DbResponse> RunScriptAsync(string scriptText)
        {
            if (_account.canRunScript(scriptText))
            {

                var res = _account.RunScript(scriptText);
                _account.Accept(new ReportTXT());
                return res;
            }
            return new DbResponse()
            {
                Message = "Access denied",
                Table = ""
            };
        }

        public  DbResponse CheckConnection()
        {
            var dbRes = new DbResponse();
            try
            {
                _account.Connect();
                dbRes.Message = _state;
            }
            catch (Exception e)
            {
                dbRes.Message = e.Message;
            }
            finally
            {
                _account.Disconnect();
            }
            return dbRes;
        }

        public void Update(string state)
        {
            _state = state;
            var text = $"[{DateTime.Now.ToString("MM/dd/yyyy h:mm tt")}]({_account.user.Id}):"+state;
            Console.WriteLine(text);
        }
        
    }
}