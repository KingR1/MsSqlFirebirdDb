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
    public class BudgetStudents : Students
    {
        public BudgetStudents()
            : base()
        {

        }

        public BudgetStudents(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            StudentsDTO student = (StudentsDTO)tuObject;
            BudgetStudentsDTO budgetStudent = new BudgetStudentsDTO(student);

            dbCommand.CommandText = string.Format("Select Scolarship From BudgetStudents Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            budgetStudent.Scolarship = dataReader.GetDouble(0);

            dataReader.Close();
            return budgetStudent;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            BudgetStudentsDTO budgetStudent = (BudgetStudentsDTO)tuObject;
            base.Update(budgetStudent);

            dbCommand.CommandText = string.Format("Update BudgetStudents Set Scolarship={0} Where Id= {1}",
                budgetStudent.Scolarship.ToString(NumberFormatInfo.InvariantInfo), budgetStudent.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            BudgetStudentsDTO budgetStudent = (BudgetStudentsDTO)tuObject;
            base.Insert(budgetStudent);

            dbCommand.CommandText = string.Format("Insert Into BudgetStudents(Scolarship) Values({0})",
               budgetStudent.Scolarship.ToString(NumberFormatInfo.InvariantInfo));

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From BudgetStudents Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
