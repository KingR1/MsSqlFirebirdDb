using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityClient
{
    class TreeNodeData
    {
        private int id;
        private bool wasOpened;
        private string connectionStringName;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TreeNodeData()
        {
            id = 0;
            wasOpened = false;
            connectionStringName = string.Empty;
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">Object Id from table TUObject</param>
        /// <param name="wasOpened">Determine if appropriate tree node was opened</param>
        /// <param name="connectionStringName">Name of connection strig to Data Base</param>
        /// <param name="connectionTypeName">Name of connection ty to Data Base</param>
        public TreeNodeData(int id, bool wasOpened, string connectionStringName)
        {
            this.id = id;
            this.wasOpened =wasOpened;
            this.connectionStringName = connectionStringName;
        }

        /// <summary>
        /// Gets or sets the Id of the appropriate TUObject
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the value which determine if appropriate tree node was opened
        /// </summary>
        public bool WasOpened
        {
            get { return wasOpened; }
            set { wasOpened = value; }
        }

        /// <summary>
        /// Gets or sets the connection string name wich uses to fill data for current node
        /// </summary>
        public string ConnectionStringName
        {
            get { return connectionStringName; }
            set { connectionStringName = value; }
        }
    }

}
