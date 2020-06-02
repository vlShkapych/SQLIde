using System;
using System.Text.RegularExpressions;
using SqlIDE.shared;

namespace SqlIDE.Accounts
{
    public class Moderator : Account
    {
        private string[] _constrains = { "create", "drop" };

    public Moderator(IDatabase db, User user) : base(db,user) { }
        public override bool canRunScript(string script)
        {
            foreach (var command in _constrains)
            {
                if (script.IndexOf(command) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.ModeratorReportTXT(this,this.Action, this.Result);
        }
    }
}