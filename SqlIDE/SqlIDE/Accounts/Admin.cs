using SqlIDE.shared;

namespace SqlIDE.Accounts
{
    public class Admin: Account
    {
        public Admin(IDatabase db, User user) : base(db,user) { }
        public override bool canRunScript(string script)
        {
            return true;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.AdminReportTXT(this,this.Action, this.Result);
        }
    }
}