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
    public class Teachers : Workers
    {
        public Teachers()
            : base()
        {

        }

        public Teachers(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            WorkersDTO worker = (WorkersDTO)tuObject;
            TeachersDTO teacher = new TeachersDTO(worker);

            dbCommand.CommandText = string.Format("Select SientistLevel, AmountOfHours From Teachers Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            teacher.SientistLevel = dataReader.GetString(0);
            teacher.AmountOfHours = dataReader.GetInt32(1);

            dataReader.Close();
            return teacher;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            TeachersDTO teacher = (TeachersDTO)tuObject;
            base.Update(teacher);

            dbCommand.CommandText = string.Format("Update Teachers Set SientistLevel='{0}', AmountOfHours={1} Where Id= {2}",
               teacher.SientistLevel, teacher.AmountOfHours, teacher.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            TeachersDTO teacher = (TeachersDTO)tuObject;
            base.Insert(teacher);

            dbCommand.CommandText = string.Format("Insert Into Teachers(SientistLevel, AmountOfHours) Values('{0}', {1})",
               teacher.SientistLevel, teacher.AmountOfHours);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Teachers Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
