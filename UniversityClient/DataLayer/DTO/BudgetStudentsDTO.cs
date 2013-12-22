using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class BudgetStudentsDTO : StudentsDTO
    {
        [DataMember]
        public double Scolarship { get; set; }

        public BudgetStudentsDTO()
            : base()
        {
            LabelsText.Add("Scolarship");
        }

        public BudgetStudentsDTO(BudgetStudentsDTO budgetstudent)
            : base(budgetstudent)
        {
            LabelsText.Add("Scolarship");

            Scolarship = budgetstudent.Scolarship;
        }

        public BudgetStudentsDTO(StudentsDTO budgetstudent)
            : base(budgetstudent)
        {
            LabelsText.Add("Scolarship");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(Scolarship.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            Scolarship = Convert.ToDouble(dataValues[10]);
        }
    }
}
