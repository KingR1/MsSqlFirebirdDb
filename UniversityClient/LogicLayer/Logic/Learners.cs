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
    public class Learners : People
    {
         public Learners()
            : base()
        {

        }

         public Learners(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            PeopleDTO people = (PeopleDTO)tuObject;
            LearnersDTO learner = new LearnersDTO(people);

            dbCommand.CommandText = string.Format("Select LearnNumber From Learners Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            learner.LearnNumber = dataReader.GetString(0);

            dataReader.Close();
            return learner;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            LearnersDTO learner = (LearnersDTO)tuObject;
            base.Update(learner);

            dbCommand.CommandText = string.Format("Update Learners Set LearnNumber='{0}' Where Id= {1}",
                learner.LearnNumber, learner.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            LearnersDTO learner = (LearnersDTO)tuObject;
            base.Insert(learner);

            dbCommand.CommandText = string.Format("Insert Into Learners(LearnNumber) Values('{0}')",
                learner.LearnNumber);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Learners Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
