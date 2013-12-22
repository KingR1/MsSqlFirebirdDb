using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer;
using System.Data.Common;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;

namespace LogicLayer
{
    public class Workers : People
    {
         public Workers()
            : base()
        {

        }

         public Workers(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

         public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            PeopleDTO people = (PeopleDTO)tuObject;
            WorkersDTO worker = new WorkersDTO(people);

            dbCommand.CommandText = string.Format("Select Post, Salary From Workers Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            worker.Post = dataReader.GetString(0);
            worker.Salary = dataReader.GetDouble(1);

            dataReader.Close();
            return worker;
        }

         public override void Update(TUObjectsDTO tuObject)
        {
            WorkersDTO worker = (WorkersDTO)tuObject;
            base.Update(worker);

            dbCommand.CommandText = string.Format("Update Workers Set Post='{0}', Salary={1} Where Id= {2}",
                worker.Post, worker.Salary.ToString(NumberFormatInfo.InvariantInfo), worker.Id);

            dbCommand.ExecuteNonQuery();
        }

         public override void Insert(TUObjectsDTO tuObject)
        {
            WorkersDTO worker = (WorkersDTO)tuObject;
            base.Insert(worker);

            dbCommand.CommandText = string.Format("Insert Into Workers(Post, Salary) Values('{0}', {1})",
                worker.Post, worker.Salary.ToString(NumberFormatInfo.InvariantInfo));

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Workers Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
