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
    public class Students : UniversityLearners
    {
        public Students()
            : base()
        {

        }

        public Students(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            UniversityLearnersDTO universityLearner = (UniversityLearnersDTO)tuObject;
            StudentsDTO student = new StudentsDTO(universityLearner);

            dbCommand.CommandText = string.Format("Select LearningForm, AverageSessionMark From Students Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            student.LearningForm = dataReader.GetString(0);
            student.AverageSessionMark = dataReader.GetDouble(1);

            dataReader.Close();
            return student;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            StudentsDTO student = (StudentsDTO)tuObject;
            base.Update(student);

            dbCommand.CommandText = string.Format("Update Students Set LearningForm='{0}', AverageSessionMark={1} Where Id= {2}",
                student.LearningForm, student.AverageSessionMark.ToString(NumberFormatInfo.InvariantInfo), student.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            StudentsDTO student = (StudentsDTO)tuObject;
            base.Insert(student);

            dbCommand.CommandText = string.Format("Insert Into Students(LearningForm, AverageSessionMark) Values('{0}', {1})",
               student.LearningForm, student.AverageSessionMark.ToString(NumberFormatInfo.InvariantInfo));

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Students Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
