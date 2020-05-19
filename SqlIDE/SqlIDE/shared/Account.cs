using System;
using System.Data;

namespace SqlIDE.shared
{
    abstract public class Account: IObserver 
    {
        public readonly User user;
        public readonly IDatabase db;
        public readonly string Action;

        public Account (IDatabase db, User user)
        {
            this.db = db;
            this.user = user;
            db.AddObserver(this);
        }

        public virtual void Connect()
        {
            db.Connect();
        }
        public virtual DbResponse RunScript(string script)
        {
            return db.Run(script);
        }
        public void Update(string state)
        {
            
                Console.WriteLine(state);
        }

        public abstract void Accept(IVisitor visitor);

        public abstract bool canRunScript();
        
    }
}