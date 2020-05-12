namespace SqlIDE.shared
{
    public interface IDatabase: IObservable
    {
        public void Connect();
        public string Run(string script);
    }
}