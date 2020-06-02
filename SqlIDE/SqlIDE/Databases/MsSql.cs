using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Npgsql;
using SqlIDE.shared;

namespace SqlIDE.Databases
{
    public class MsSql: IDatabase
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private string _state;
        private string _response;
        private readonly List<IObserver> _observers;
        private string _message;
        public MsSql(string conStr)
        {
            _connectionString = conStr;
            _observers = new List<IObserver>();
        }
        public DbResponse Run(string script)
        {
            try
            {

               
                var command = new SqlCommand(script, _connection);
                var reader = command.ExecuteReader();
                

                
                while (reader.Read())
                {
                    _response += "<tr>";
                    for (int i = 0; i < reader.FieldCount; i++)
                        _response += "<td>"+reader[i]+ "</td>";
                    _response += "</tr>";
                }
                reader.Close();
                
                _state = command.ExecuteScalar().ToString();
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
                NotifyObservers();
            }
            return new DbResponse()
            {
                Table = _response,
                Message = _response == null?_message = _message: "Success"
            };
        }
        
        public void Connect()
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                _state = "Connection is opened";
            }
            catch (Exception e)
            {
                _state = e.Message;
                throw new Exception(e.Message);
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