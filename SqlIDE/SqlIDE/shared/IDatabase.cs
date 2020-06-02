namespace SqlIDE.shared
{
    public interface IDatabase: IObservable
    {
        public void Connect();
        public DbResponse Run(string script);
        public void Disconnect();
    }
}