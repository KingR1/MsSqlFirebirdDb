using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class EntrantsDTO : LearnersDTO
    {
        [DataMember]
        public int MarkSum { get; set; }

        [DataMember]
        public string Privilege { get; set; }

        public EntrantsDTO() 
            : base()
        {
            LabelsText.Add("MarkSum");
            LabelsText.Add("Privilege");
        }

        public EntrantsDTO(EntrantsDTO entrant)
            : base(entrant)
        {
            LabelsText.Add("MarkSum");
            LabelsText.Add("Privilege");

            MarkSum = entrant.MarkSum;
            Privilege = entrant.Privilege;
        }

        public EntrantsDTO(LearnersDTO entrant)
            : base(entrant)
        {
            LabelsText.Add("MarkSum");
            LabelsText.Add("Privilege");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(MarkSum.ToString());
            dataValues.Add(Privilege);

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            MarkSum = Convert.ToInt32(dataValues[7]);
            Privilege = dataValues[8];
        }
    }
}
