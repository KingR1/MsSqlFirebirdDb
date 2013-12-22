using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    /// <summary>
    /// Contains informaition for references (save id in db - show name)
    /// </summary>
    [DataContract]
    public class ReferenceInfo
    {
        /// <summary>
        /// number in list of fields
        /// </summary>
        [DataMember]
        public int FieldNumber { get; set; }

        /// <summary>
        /// major for doughter objects (for tree navigator)
        /// </summary>
        [DataMember]
        public int Major { get; set; }

        /// <summary>
        /// them list of ids of doughter objects (for tree navigator)
        /// </summary>
        [DataMember]
        public List<int> FilteredIds { get; set; }
    }

    public static class DataConverter
    {
        public static string DateTimeToString(DateTime date)
        {
            return date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();
        }
    }

    //type of server
    public enum ServerDB
    {
        MSSqlServer,

        FireBird
    }

    //actions
    public enum DBAction
    {
        Edit = 0,
        Insert = 1,
        Delete = 2
    }

    //constants
    public static class DefinedConstants
    {
        public const string dataLeyerNameSpace = "DataLayer";
        public const string logicLeyerNameSpace = "LogicLayer";

        public const string currentConnectionStringName = "currentConnectionStringName";

        public const string currentNamespace = "UniversityClient";
    }
}
