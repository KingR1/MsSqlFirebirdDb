using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer;
using System.Data.Common;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace LogicLayer
{
    public class TUObjects
    {
        protected DbConnection dbConnection;
        protected DbCommand dbCommand;
        protected DbDataReader dataReader;
        protected ServerDB dbServer;

        public TUObjects()
        {

        }

        public TUObjects(DbConnection dbConnection, ServerDB server)
        {
            this.dbConnection = dbConnection;
            this.dbServer = server;
            switch (dbServer)
            {
                case ServerDB.MSSqlServer:
                    {
                        dbCommand = new SqlCommand();
                        dbCommand.Connection = dbConnection;

                        break;
                    }
                case ServerDB.FireBird:
                    {
                        dbCommand = new FbCommand();
                        dbCommand.Connection = dbConnection;

                        break;
                    }
                default:
                    break;
            }
        }

        public virtual TUObjectsDTO Select(int id)
        {
            dbCommand.CommandText = string.Format("Select Id, ClassId, Major, Name, InputDate From TUObjects Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            TUObjectsDTO tuObject = new TUObjectsDTO()
            {
                Id = dataReader.GetInt32(0),
                ClassId = dataReader.GetInt32(1),
                Major = dataReader.GetInt32(2),
                Name = dataReader.GetString(3),
                InputDate = DataConverter.DateTimeToString(dataReader.GetDateTime(4))
            };

            dataReader.Close();
            return tuObject;
        }

        public virtual void Update(TUObjectsDTO tuObject)
        {
            dbCommand.CommandText = string.Format("Update TUObjects Set ClassId = {0}, Major = {1}, Name='{2}', InputDate = '{3}' Where Id = {4}",
                tuObject.ClassId, tuObject.Major, tuObject.Name, tuObject.InputDate, tuObject.Id);
            dbCommand.ExecuteNonQuery();
        }

        public virtual void Insert(TUObjectsDTO tuObject)
        {
            dbCommand.CommandText = string.Format("Insert Into TUObjects(ClassId, Major, Name, InputDate) Values({0}, {1}, '{2}', '{3}')",
                tuObject.ClassId, tuObject.Major, tuObject.Name, tuObject.InputDate);
            dbCommand.ExecuteNonQuery();
        }

        public virtual void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From TUObjects Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();
        }

        public string GetNameById(int id)
        {
            string name;

            dbCommand.CommandText = string.Format("Select Name From TUObjects Where Id = {0}", id);
            name = (string)dbCommand.ExecuteScalar();

            return name;
        }

        public DbConnection Connection
        {
            get { return dbConnection; }
        }

        public ServerDB DbServer
        {
            get { return dbServer; }
        }

        //more methods will be here if need
    }
}
