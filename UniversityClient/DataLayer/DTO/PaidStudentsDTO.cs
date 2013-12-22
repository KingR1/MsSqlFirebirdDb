using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class PaidStudentsDTO : StudentsDTO
    {
        [DataMember]
        public double Price { get; set; }

        public PaidStudentsDTO()
            : base()
        {
            LabelsText.Add("Price");
        }

        public PaidStudentsDTO(PaidStudentsDTO paidStusent)
            : base(paidStusent)
        {
            LabelsText.Add("Price");

            Price = paidStusent.Price;
        }

        public PaidStudentsDTO(StudentsDTO paidStusent)
            : base(paidStusent)
        {
            LabelsText.Add("Price");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(Price.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            Price = Convert.ToDouble(dataValues[10]);
        }
    }
}
