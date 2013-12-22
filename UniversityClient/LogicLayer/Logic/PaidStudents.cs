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
    public class PaidStudents : Students
    {
        public PaidStudents()
            : base()
        {

        }

        public PaidStudents(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            StudentsDTO student = (StudentsDTO)tuObject;
            PaidStudentsDTO paidStudent = new PaidStudentsDTO(student);

            dbCommand.CommandText = string.Format("Select Price From PaidStudents Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            paidStudent.Price = dataReader.GetDouble(0);

            dataReader.Close();
            return paidStudent;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            PaidStudentsDTO paidStudent = (PaidStudentsDTO)tuObject;
            base.Update(paidStudent);

            dbCommand.CommandText = string.Format("Update PaidStudents Set Price={0} Where Id= {1}",
                paidStudent.Price.ToString(NumberFormatInfo.InvariantInfo), paidStudent.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            PaidStudentsDTO paidStudent = (PaidStudentsDTO)tuObject;
            base.Insert(paidStudent);

            dbCommand.CommandText = string.Format("Insert Into PaidStudents(Price) Values({0})",
               paidStudent.Price.ToString(NumberFormatInfo.InvariantInfo));

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From PaidStudents Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
