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
    public class AuxiliaryWorkers : Workers
    {
        public AuxiliaryWorkers()
            : base()
        {

        }

        public AuxiliaryWorkers(DbConnection dbConnection, ServerDB server)
            : base(dbConnection, server)
        {

        }

        public override TUObjectsDTO Select(int id)
        {
            TUObjectsDTO tuObject = base.Select(id);
            WorkersDTO worker = (WorkersDTO)tuObject;
            AuxiliaryWorkersDTO auxiliaryWorker = new AuxiliaryWorkersDTO(worker);

            dbCommand.CommandText = string.Format("Select Shift, TypeOfWork From AuxiliaryWorkers Where Id = {0}", id);
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            auxiliaryWorker.Shift = dataReader.GetInt32(0);
            auxiliaryWorker.TypeOfWork = dataReader.GetString(1);

            dataReader.Close();
            return auxiliaryWorker;
        }

        public override void Update(TUObjectsDTO tuObject)
        {
            AuxiliaryWorkersDTO auxiliaryWorker = (AuxiliaryWorkersDTO)tuObject;
            base.Update(auxiliaryWorker);

            dbCommand.CommandText = string.Format("Update AuxiliaryWorkers Set Shift={0}, TypeOfWork='{1}' Where Id= {2}",
               auxiliaryWorker.Shift, auxiliaryWorker.TypeOfWork, auxiliaryWorker.Id);

            dbCommand.ExecuteNonQuery();
        }

        public override void Insert(TUObjectsDTO tuObject)
        {
            AuxiliaryWorkersDTO auxiliaryWorker = (AuxiliaryWorkersDTO)tuObject;
            base.Insert(auxiliaryWorker);

            dbCommand.CommandText = string.Format("Insert Into AuxiliaryWorkers(Shift, TypeOfWork) Values({0}, '{1}')",
               auxiliaryWorker.Shift, auxiliaryWorker.TypeOfWork);

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int id)
        {
            dbCommand.CommandText = string.Format("Delete From AuxiliaryWorkers Where Id = {0}", id);
            dbCommand.ExecuteNonQuery();

            base.Delete(id);
        }
    }
}
