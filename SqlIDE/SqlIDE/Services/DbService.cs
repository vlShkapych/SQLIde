using System;
using System.Collections.Generic;
using SqlIDE.shared;
using SqlIDE.Accounts;
using SqlIDE.Databases;

namespace SqlIDE.Services
{
    public class DbService: IObserver
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
            db.AddObserver(this);



            _accounts.Add("moderator", new Moderator(db, script.User));

            _account = _accounts.GetValueOrDefault(script.User.AccType);
        }

        public string RunScript()
        {
            if (_account.canRunScript())
            {
                _account.Connect();
                return _account.RunScript(_scriptText);
            }

            return "";
        }


        public void Update(string state)
        {

            Console.WriteLine(state);

        }

    }
}