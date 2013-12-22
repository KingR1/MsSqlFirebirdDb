using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    [DataContract]
    public class TUObjectsDTO
    {
        /// <summary>
        /// the text of labels for showing
        /// </summary>
        public List<string> LabelsText { get; protected set; }

        /// <summary>
        /// the info about the fields which are referenced
        /// </summary>
        [DataMember]
        public List<ReferenceInfo> ReferencedField { get; protected set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClassId { get; set; }

        [DataMember]
        public int Major { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string InputDate { get; set; }

        public TUObjectsDTO()
        {
            LabelsText = new List<string>();
            LabelsText.Add("Name");
            LabelsText.Add("InputDate");

            ReferencedField = new List<ReferenceInfo>();
        }

        public TUObjectsDTO(TUObjectsDTO tuObject)
        {
            LabelsText = new List<string>();
            LabelsText.Add("Name");
            LabelsText.Add("InputDate");

            ReferencedField = new List<ReferenceInfo>();

            Id = tuObject.Id;
            ClassId = tuObject.ClassId;
            Major = tuObject.Major;
            Name = tuObject.Name;
            InputDate = tuObject.InputDate;
        }

        /// <summary>
        /// return all field as list of string
        /// </summary>
        public virtual List<string> GetValues()
        {
            List<string> dataValues = new List<string>();
            dataValues.Add(Name);
            dataValues.Add(InputDate);

            return dataValues;
        }

        /// <summary>
        /// set all fields from list of string
        /// </summary>
        public virtual void SetValues(List<string> dataValues)
        {
            Name = dataValues[0];
            InputDate = dataValues[1];
        }
    }
}
