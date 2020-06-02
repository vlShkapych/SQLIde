namespace SqlIDE.shared
{
    public class DbCommands: IDbCommand
    {
        private IDatabase db;

        public DbCommands(IDatabase db)
        {
            this.db = db;
        }

        public void Connect()
        {
            db.Connect();
        }

        public DbResponse Run(string script)
        {
            return db.Run(script);
        }
        
        public void Disconnect()
        {
            db.Disconnect();
        }
    }
}