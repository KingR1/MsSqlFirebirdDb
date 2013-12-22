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
    public class Entrants : Learners
    {
        public Entrants()
            : base()
        {

        }

        public Entrants(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            LearnersDTO lerner = (LearnersDTO)tuObject;
            EntrantsDTO entrant = new EntrantsDTO(lerner);

            dbCommand.CommandText = string.Format("Select MarkSum, Privilee  From Entrants Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            entrant.MarkSum = dataReader.GetInt32(0);
            entrant.Privilege = dataReader.GetString(1);

            dataReader.Close();
            return entrant;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            EntrantsDTO entrant = (EntrantsDTO)tuObject;
            base.Update(entrant);

            dbCommand.CommandText = string.Format("Update Entrants Set MarkSum={0}, Privilee='{1}' Where Id= {2}",
                entrant.MarkSum, entrant.Privilege, entrant.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            EntrantsDTO entrant = (EntrantsDTO)tuObject;
            base.Insert(entrant);

            dbCommand.CommandText = string.Format("Insert Into Entrants(MarkSum, Privilee) Values({0}, '{1}')",
                entrant.MarkSum, entrant.Privilege);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Entrants Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
