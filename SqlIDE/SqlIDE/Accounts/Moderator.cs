using SqlIDE.shared;

namespace SqlIDE.Accounts
{
    public class Moderator: Account
    {

        public Moderator(IDatabase db, User user) : base(db,user) { }
        public override bool canRunScript()
        {
            return true;
        }


    }
}