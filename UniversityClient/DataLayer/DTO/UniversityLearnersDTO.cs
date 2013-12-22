using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class UniversityLearnersDTO : LearnersDTO
    {
        [DataMember]
        public int EnteredYear { get; set; }

        public UniversityLearnersDTO()
            : base()
        {
            LabelsText.Add("EnteredYear");
        }

        public UniversityLearnersDTO(UniversityLearnersDTO universityLearner)
            : base(universityLearner)
        {
            LabelsText.Add("EnteredYear");

            EnteredYear = universityLearner.EnteredYear;
        }

        public UniversityLearnersDTO(LearnersDTO universityLearner)
            : base(universityLearner)
        {
            LabelsText.Add("EnteredYear");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(EnteredYear.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            EnteredYear = Convert.ToInt32(dataValues[7]);
        }
    }
}
