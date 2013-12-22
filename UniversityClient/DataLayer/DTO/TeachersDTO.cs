using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class TeachersDTO : WorkersDTO
    {
        [DataMember]
        public string SientistLevel { get; set; }

        [DataMember]
        public int AmountOfHours { get; set; }

        public TeachersDTO()
            : base()
        {
            LabelsText.Add("SientistLevel");
            LabelsText.Add("AmountOfHours");
        }

        public TeachersDTO(TeachersDTO teacher)
            : base(teacher)
        {
            LabelsText.Add("SientistLevel");
            LabelsText.Add("AmountOfHours");

            SientistLevel = teacher.SientistLevel;
            AmountOfHours = teacher.AmountOfHours;
        }

        public TeachersDTO(WorkersDTO teacher)
            : base(teacher)
        {
            LabelsText.Add("SientistLevel");
            LabelsText.Add("AmountOfHours");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(SientistLevel);
            dataValues.Add(AmountOfHours.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            SientistLevel = dataValues[8];
            AmountOfHours = Convert.ToInt32(dataValues[9]);
        }
    }
}
