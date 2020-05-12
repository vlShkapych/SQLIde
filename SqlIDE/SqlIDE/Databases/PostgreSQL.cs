using System;
using System.Collections.Generic;
using Npgsql;
using SqlIDE.shared;

namespace SqlIDE.Databases
{
 public class PostgreSQl: IDatabase, IObservable
    {
        private string _connectionString;
        private NpgsqlConnection _connection;
        private string _state;
        private string _response;
        private List<IObserver> _observers;

        public PostgreSQl(string conStr)
        {
            _connectionString = conStr;
            _observers = new List<IObserver>();
        }
        public string Run(string script)
        {
            try
            {
                 var cmd = new NpgsqlCommand(script, _connection);

               
                NpgsqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    _response += "<td>";
                    for (int i = 0; i < res.FieldCount; i++)
                        _response += "<tr>"+res[i]+ "</tr>";
                    _response += "</td>";
                }
                _state = cmd.ExecuteScalar().ToString();
                NotifyObservers();


            }
            catch (Exception e)
            {
                _state = e.Message;
                NotifyObservers();
            }
            finally
            {
                _connection.Close();
                _state = "Close Connection";
                NotifyObservers();
            }
            return _response;
        }
        
        public void Connect()
        {
            try
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
                _state = "Connection is opened";
            }
            catch (Exception e)
            {
                _state = e.Message;
            }
            finally
            {
                NotifyObservers();

            }
        }
        //IObservable
        public void AddObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
                observer.Update(_state);
        }
    }
}