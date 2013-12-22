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
    public class UniversityLearners : Learners
    {
         public UniversityLearners()
            : base()
        {

        }

         public UniversityLearners(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            LearnersDTO learner = (LearnersDTO)tuObject;
            UniversityLearnersDTO universityLearner = new UniversityLearnersDTO(learner);

            dbCommand.CommandText = string.Format("Select EnteredYear From UniversityLearners Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            universityLearner.EnteredYear = dataReader.GetInt32(0);

            dataReader.Close();
            return universityLearner;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            UniversityLearnersDTO universityLearner = (UniversityLearnersDTO)tuObject;
            base.Update(universityLearner);

            dbCommand.CommandText = string.Format("Update UniversityLearners Set EnteredYear={0} Where Id= {1}",
                universityLearner.EnteredYear, universityLearner.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            UniversityLearnersDTO universityLearner = (UniversityLearnersDTO)tuObject;
            base.Insert(universityLearner);

            dbCommand.CommandText = string.Format("Insert Into UniversityLearners(EnteredYear) Values({0})",
               universityLearner.EnteredYear);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From UniversityLearners Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
