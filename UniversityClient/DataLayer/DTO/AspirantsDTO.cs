using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class AspirantsDTO : UniversityLearnersDTO
    {
        [DataMember]
        public int Teacher { get; set; }

        [DataMember]
        public string DesertationTopic { get; set; }

        [DataMember]
        public double Scolarship { get; set; }

        public AspirantsDTO()
            : base()
        {
            LabelsText.Add("Teacher");
            LabelsText.Add("DesertationTopic");
            LabelsText.Add("Scolarship");

            ReferenceInfo refInfo = new ReferenceInfo()
            {
                Major = Id,
                FieldNumber = 10,
                FilteredIds = new List<int>() { 11 }
            };

            ReferencedField.Add(refInfo);
        }

        public AspirantsDTO(AspirantsDTO aspirant)
            : base(aspirant)
        {
            LabelsText.Add("Teacher");
            LabelsText.Add("DesertationTopic");
            LabelsText.Add("Scolarship");

            Teacher = aspirant.Teacher;
            DesertationTopic = aspirant.DesertationTopic;
            Scolarship = aspirant.Scolarship;

            ReferenceInfo refInfo = new ReferenceInfo()
            {
                Major = Id,
                FieldNumber = 10,
                FilteredIds = new List<int>() { 11 }
            };

            ReferencedField.Add(refInfo);
        }

        public AspirantsDTO(UniversityLearnersDTO aspirant)
            : base(aspirant)
        {
            LabelsText.Add("Teacher");
            LabelsText.Add("DesertationTopic");
            LabelsText.Add("Scolarship");

            ReferenceInfo refInfo = new ReferenceInfo()
            {
                Major = Id,
                FieldNumber = 10,
                FilteredIds = new List<int>() { 8 }
            };

            ReferencedField.Add(refInfo);
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(Teacher.ToString());
            dataValues.Add(DesertationTopic);
            dataValues.Add(Scolarship.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            Teacher = Convert.ToInt32(dataValues[8]);
            DesertationTopic = dataValues[9];
            Scolarship = Convert.ToDouble(dataValues[10]);
        }
    }
}
