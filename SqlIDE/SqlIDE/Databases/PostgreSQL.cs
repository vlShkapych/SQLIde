using System;
using System.Collections.Generic;
using Npgsql;
using SqlIDE.shared;

namespace SqlIDE.Databases
{
 public class PostgreSQl: IDatabase
    {
        private string _connectionString;
        private NpgsqlConnection _connection;
        private string _state;
        private string _response;
        private List<IObserver> _observers;
        private string _message;
        public PostgreSQl(string conStr)
        {
            _connectionString = conStr;
            _observers = new List<IObserver>();
        }
        public DbResponse Run(string script)
        {            
            
            try
            {
                 using var cmd = new NpgsqlCommand(script, _connection);

               
                using NpgsqlDataReader res = cmd.ExecuteReader();
                _response = "";
                while (res.Read())
                {
                    _response += "<tr>";
                    for (int i = 0; i < res.FieldCount; i++)
                        _response += "<td>"+res[i]+ "</td>";
                    _response += "</tr>";
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
                _message =  _state;   
                _connection.Close();
                _state = "Close Connection";
                NotifyObservers();
            }
            return new DbResponse()
            {
                Table = _response,
                Message = _response ==null?_message = _message: "Success"
            };
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
        public void Disconnect()
        {
            try
            {
                _connection.Close();
                _state = "Connection is Closed";
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