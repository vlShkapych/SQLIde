namespace SqlIDE.shared
{
    public interface IDbCommand
    {
        public void Connect();
        public DbResponse Run(string script);
        public void Disconnect();
    }
}