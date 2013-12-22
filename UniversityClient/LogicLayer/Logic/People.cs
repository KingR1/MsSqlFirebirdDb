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
    public class People : TUObjects
    {
        public People()
            : base()
        {

        }

        public People(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            PeopleDTO people = new PeopleDTO(tuObject);

            dbCommand.CommandText = string.Format("Select FirstName, MiddleName, Address, Phone From People Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            people.FirstName = dataReader.GetString(0);
            people.MiddleName = dataReader.GetString(1);
            people.Address = dataReader.GetString(2);
            people.Phone = dataReader.GetString(3);

            dataReader.Close();
            return people;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            PeopleDTO people = (PeopleDTO) tuObject;
            base.Update(people);

            dbCommand.CommandText = string.Format("Update People Set FirstName='{0}', MiddleName='{1}', Address='{2}', Phone='{3}' Where Id= {4}",
                people.FirstName, people.MiddleName, people.Address, people.Phone, people.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            PeopleDTO people = (PeopleDTO)tuObject;
            base.Insert(tuObject);

            dbCommand.CommandText = string.Format("Insert Into People(FirstName, MiddleName, Address, Phone) Values('{0}', '{1}', '{2}', '{3}')",
                people.FirstName, people.MiddleName, people.Address, people.Phone);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From People Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
