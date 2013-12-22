using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class WorkersDTO : PeopleDTO
    {
        [DataMember]
        public string Post { get; set; }

        [DataMember]
        public double Salary { get; set; }

        public WorkersDTO()
            : base()
        {
            LabelsText.Add("Post");
            LabelsText.Add("Salary");
        }

        public WorkersDTO(WorkersDTO worker)
            : base(worker)
        {
            LabelsText.Add("Post");
            LabelsText.Add("Salary");

            Post = worker.Post;
            Salary = worker.Salary;
        }

        public WorkersDTO(PeopleDTO worker)
            : base(worker)
        {
            LabelsText.Add("Post");
            LabelsText.Add("Salary");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(Post);
            dataValues.Add(Salary.ToString());

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            Post = dataValues[6];
            Salary = Convert.ToDouble(dataValues[7]);
        }
    }
}
