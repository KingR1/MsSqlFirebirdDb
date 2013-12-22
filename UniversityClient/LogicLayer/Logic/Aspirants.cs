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
    public class Aspirants : UniversityLearners
    {
        public Aspirants()
            : base()
        {

        }

        public Aspirants(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            UniversityLearnersDTO universityLearner = (UniversityLearnersDTO)tuObject;
            AspirantsDTO aspirant = new AspirantsDTO(universityLearner);

            dbCommand.CommandText = string.Format("Select Teacher, DesertationTopic, Scolarship From Aspirants Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            aspirant.Teacher = dataReader.GetInt32(0);
            aspirant.DesertationTopic = dataReader.GetString(1);
            aspirant.Scolarship = dataReader.GetDouble(2);

            dataReader.Close();
            return aspirant;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            AspirantsDTO aspirant = (AspirantsDTO)tuObject;
            base.Update(aspirant);

            dbCommand.CommandText = string.Format("Update Aspirants Set Teacher={0}, DesertationTopic='{1}', Scolarship={2} Where Id= {3}",
                aspirant.Teacher, aspirant.DesertationTopic, aspirant.Scolarship.ToString(NumberFormatInfo.InvariantInfo), aspirant.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            AspirantsDTO aspirant = (AspirantsDTO)tuObject;
            base.Insert(aspirant);

            dbCommand.CommandText = string.Format("Insert Into Aspirants(Teacher, DesertationTopic, Scolarship) Values({0}, '{1}', {2})",
               aspirant.Teacher, aspirant.DesertationTopic, aspirant.Scolarship.ToString(NumberFormatInfo.InvariantInfo));

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From Aspirants Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
