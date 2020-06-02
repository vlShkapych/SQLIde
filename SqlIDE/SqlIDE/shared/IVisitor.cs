using SqlIDE.Accounts;

namespace SqlIDE.shared
{
    public interface IVisitor
    {
        void ModeratorReportTXT(Account moderator,string action, string result);
        void AdminReportTXT(Account admin,string action, string result);
    }
}