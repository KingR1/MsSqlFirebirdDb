using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class PeopleDTO : TUObjectsDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Phone { get; set; }

        public PeopleDTO() 
            : base()
        {
            LabelsText.Add("FirstName");
            LabelsText.Add("MiddleName");
            LabelsText.Add("Address");
            LabelsText.Add("Phone");
        }

        public PeopleDTO(PeopleDTO people)
            : base(people)
        {
            LabelsText.Add("FirstName");
            LabelsText.Add("MiddleName");
            LabelsText.Add("Address");
            LabelsText.Add("Phone");

            FirstName = people.FirstName;
            MiddleName = people.MiddleName;
            Address = people.Address;
            Phone = people.Phone;
        }

        public PeopleDTO(TUObjectsDTO people)
            : base(people)
        {
            LabelsText.Add("FirstName");
            LabelsText.Add("MiddleName");
            LabelsText.Add("Address");
            LabelsText.Add("Phone");
        }

        public override List<string> GetValues()
        {
            List<string> dataValues = base.GetValues();

            dataValues.Add(FirstName);
            dataValues.Add(MiddleName);
            dataValues.Add(Address);
            dataValues.Add(Phone);

            return dataValues;
        }

        public override void SetValues(List<string> dataValues)
        {
            base.SetValues(dataValues);

            FirstName = dataValues[2];
            MiddleName = dataValues[3];
            Address = dataValues[4];
            Phone = dataValues[5];
        }
    }
}
