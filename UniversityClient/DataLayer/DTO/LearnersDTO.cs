using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class LearnersDTO: PeopleDTO
    {
        [DataMember]
        public string LearnNumber { get; set; }

        public LearnersDTO() 
            : base()
        {
            LabelsText.Add("LearnNumber");
        }

        public LearnersDTO(LearnersDTO learner)
            : base(learner)
        {
            LabelsText.Add("LearnNumber");

            LearnNumber = learner.LearnNumber;
        }

        public LearnersDTO(PeopleDTO learner)
            : base(learner)
        {
            LabelsText.Add("LearnNumber");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(LearnNumber);

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            LearnNumber = dataValues[6];
        }
    }
}
