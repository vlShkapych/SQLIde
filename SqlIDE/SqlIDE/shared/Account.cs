using System;
using System.Data;


namespace SqlIDE.shared
{
    abstract public class Account
    {
        public  User user;
        public  IDatabase db;
        public  string Action;
        public string Result;

        public IDbCommand commands;
        public void SetCommand(IDbCommand com)
        {
            commands = com;
        }
        public Account (IDatabase db, User user)
        {
            this.db = db;
            this.user = user;
            SetCommand(new DbCommands(db));
        }

        public virtual DbResponse Connect()
        {
            try
            {
                commands.Connect();
                return new DbResponse(){ Message = "Connection Success!", Table = ""};
            }
            catch (Exception e)
            {
                return new DbResponse(){ Message = e.Message, Table = ""};
            }
            
        }
        public virtual DbResponse Disconnect()
        {
            try
            {
                commands.Disconnect();
                return new DbResponse(){ Message = "Connection Closed!", Table = ""};
            }
            catch (Exception e)
            {
                return new DbResponse(){ Message = e.Message, Table = ""};
            }
            
        }
        
        public virtual DbResponse RunScript(string script)
        {
            Connect();
            var scr = commands.Run(script);
            Action = script;
            Result = scr.Message;
 
            return scr;
        }


        public abstract void Accept(IVisitor visitor);

        public abstract bool canRunScript(string script);
        
    }
}