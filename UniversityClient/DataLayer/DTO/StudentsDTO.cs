using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class StudentsDTO : UniversityLearnersDTO
    {
        [DataMember]
        public string LearningForm { get; set; }

        [DataMember]
        public double AverageSessionMark { get; set; }

        public StudentsDTO()
            : base()
        {
            LabelsText.Add("LearningForm");
            LabelsText.Add("AverageSessionMark");
        }

        public StudentsDTO(StudentsDTO student)
            : base(student)
        {
            LabelsText.Add("LearningForm");
            LabelsText.Add("AverageSessionMark");

            LearningForm = student.LearningForm;
            AverageSessionMark = student.AverageSessionMark;
        }

        public StudentsDTO(UniversityLearnersDTO student)
            : base(student)
        {
            LabelsText.Add("LearningForm");
            LabelsText.Add("AverageSessionMark");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(LearningForm);
            dataValues.Add(AverageSessionMark.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            LearningForm = dataValues[8];
            AverageSessionMark = Convert.ToDouble(dataValues[9]);
        }
    }
}
