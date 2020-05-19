using SqlIDE.Accounts;

namespace SqlIDE.shared
{
    public interface IVisitor
    {
        void ModeratorReportTXT(Account acc,string action, string result);
        
    }
}