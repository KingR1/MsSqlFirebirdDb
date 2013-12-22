using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class AuxiliaryWorkersDTO : WorkersDTO
    {
        [DataMember]
        public int Shift { get; set; }

        [DataMember]
        public string TypeOfWork { get; set; }

        public AuxiliaryWorkersDTO()
            : base()
        {
            LabelsText.Add("Shift");
            LabelsText.Add("TypeOfWork");
        }

        public AuxiliaryWorkersDTO(AuxiliaryWorkersDTO auxiliaryWorker)
            : base(auxiliaryWorker)
        {
            LabelsText.Add("Shift");
            LabelsText.Add("TypeOfWork");

            Shift = auxiliaryWorker.Shift;
            TypeOfWork = auxiliaryWorker.TypeOfWork;
        }

        public AuxiliaryWorkersDTO(WorkersDTO auxiliaryWorker)
            : base(auxiliaryWorker)
        {
            LabelsText.Add("Shift");
            LabelsText.Add("TypeOfWork");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(Shift.ToString());
            dataValues.Add(TypeOfWork);

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            Shift = Convert.ToInt32(dataValues[8]);
            TypeOfWork = dataValues[9];
        }
    }
}
