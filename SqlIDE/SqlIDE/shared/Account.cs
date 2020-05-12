namespace SqlIDE.shared
{
    abstract public class Account
    {
        protected User user;
        protected IDatabase db;


        public Account (IDatabase db, User user)
        {
            this.db = db;
            this.user = user;
       
        }

        public virtual void Connect()
        {
            db.Connect();
        }
        public virtual string RunScript(string script)
        {
            return db.Run(script);
        }
        
        public abstract bool canRunScript();
    }
}