using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using System.Data.Common;
using DataLayer;
using LogicLayer;

namespace UniversityClient
{
    public partial class BaseTreeNavigator : Form
    {
        //fb connections and commands
        protected FbConnection fbConnection;
        protected FbConnection fbConnectionDaughter;
        protected FbCommand fbCommand;
        protected FbCommand fbCommandDaughter;

        //sql server connections and commands
        protected SqlConnection sqlConnection;
        protected SqlConnection sqlConnectionDaughter;
        protected SqlCommand sqlCommand;
        protected SqlCommand sqlCommandDaughter;

        //common connection
        protected DbConnection dbConnection;
        protected DbConnection dbConnectionDaughter;
        protected DbCommand dbCommand;
        protected DbCommand dbCommandDaughter;
        protected DbDataReader dbDataReader;

        //for app.config
        protected string currentConnectionStringName;
        protected string nativeConnectionStringName; //?
        protected string currentProviderName;
        protected string currentConnectionString;

        protected ServerDB dbServer;

        #region Public properties

        public DbConnection MainDbConnection
        {
            get { return dbConnection; }
        }

        public DbConnection DaughterDbConnection
        {
            get { return dbConnectionDaughter; }
        }

        public string CurrentConnectionStringName
        {
            get { return currentConnectionStringName; }
        }

        public string NativeConnectionStringName
        {
            get { return nativeConnectionStringName; }
        }

        public string CurrentProviderName
        {
            get { return currentProviderName; }
        }

        public string CurrentConnectionString
        {
            get { return currentConnectionString; }
        }

        #endregion

        protected void FillTreeNavigation(int major, TreeNode tNode, ContextMenuStrip nodeContextMenuStrip)
        {
            //change the type of data base
            if (tNode != null)
            {
                TreeNodeData dataNode = (TreeNodeData)tNode.Tag;
                currentConnectionStringName = dataNode.ConnectionStringName;
            }

            if (!string.IsNullOrEmpty(currentConnectionStringName))
            {
                currentProviderName = ConfigurationManager.ConnectionStrings[currentConnectionStringName].ProviderName;
                currentConnectionString = ConfigurationManager.ConnectionStrings[currentConnectionStringName].ConnectionString;

                switch (currentProviderName)
                {
                    case "System.Data.SqlClient":
                        {
                            dbConnection = sqlConnection;
                            dbCommand = sqlCommand;

                            dbConnectionDaughter = sqlConnectionDaughter;
                            dbCommandDaughter = sqlCommandDaughter;

                            break;
                        }
                    case "FirebirdSql.Data.FirebirdClient":
                        {
                            dbConnection = fbConnection;
                            dbCommand = fbCommand;

                            dbConnectionDaughter = fbConnectionDaughter;
                            dbCommandDaughter = fbCommandDaughter;
                            break;
                        }
                    default:
                        {
                            throw new Exception("Bad  name of current connection string");
                        }
                }
            }
            else
            {
               throw new Exception("Bad  name of current connection string");
            }

            //logic
            dbConnection.Open();
            dbConnectionDaughter.Open();

            TreeNodeData treeNodeData;
            TreeNode currentNode;
            int i = 0, n = 0;

            dbCommand.CommandText = "Select Id, Name From TUObjects Where Major = " + major + " Order by ClassId, Name";
            dbDataReader = dbCommand.ExecuteReader();

            while (dbDataReader.Read())
            {
                treeNodeData = new TreeNodeData(dbDataReader.GetInt32(0), false, currentConnectionStringName);

                currentNode = new TreeNode(dbDataReader.GetString(1));
                currentNode.Tag = treeNodeData;
                if(nodeContextMenuStrip != null)
                    currentNode.ContextMenuStrip = nodeContextMenuStrip;

                if (tNode != null)
                    tNode.Nodes.Add(currentNode);
                else
                    treeNavigation.Nodes.Add(currentNode);

                dbCommandDaughter.CommandText = "Select Count(Id) From TUObjects Where Major = " + dbDataReader.GetInt32(0);

                n = (int)dbCommandDaughter.ExecuteScalar();
                if (n > 0)
                {
                    if (tNode != null)
                        tNode.Nodes[i].Nodes.Add("Virtual Node");
                    else
                        treeNavigation.Nodes[i].Nodes.Add("Virtual Node");
                }

                i++;
            }
            dbDataReader.Close();
            dbConnection.Close();
            dbConnectionDaughter.Close();
        }

        protected void SetConnections()
        {
            //// get the connection string from app.config and create the connections
            //string sqlConnectionString = ConfigurationManager.ConnectionStrings["sqlServerConnectionString"].ConnectionString;
            //string fbConnectionString = ConfigurationManager.ConnectionStrings["firebirdConnectionString"].ConnectionString;

            ////sql server
            //sqlConnection = new SqlConnection(sqlConnectionString);
            //sqlConnectionDaughter = new SqlConnection(sqlConnectionString);

            //sqlCommand = new SqlCommand();
            //sqlCommand.Connection = sqlConnection;

            //sqlCommandDaughter = new SqlCommand();
            //sqlCommandDaughter.Connection = sqlConnectionDaughter;

            ////firebird
            //fbConnection = new FbConnection(fbConnectionString);
            //fbConnectionDaughter = new FbConnection(fbConnectionString);

            //fbCommand = new FbCommand();
            //fbCommand.Connection = fbConnection;

            //fbCommandDaughter = new FbCommand();
            //fbCommandDaughter.Connection = fbConnectionDaughter;
        }

        public BaseTreeNavigator()
        {
            InitializeComponent();

            //SetConnections();

            // get the connection string from app.config and create the connections
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["sqlServerConnectionString"].ConnectionString;
            string fbConnectionString = ConfigurationManager.ConnectionStrings["firebirdConnectionString"].ConnectionString;

            //sql server
            sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnectionDaughter = new SqlConnection(sqlConnectionString);

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            sqlCommandDaughter = new SqlCommand();
            sqlCommandDaughter.Connection = sqlConnectionDaughter;

            //firebird
            fbConnection = new FbConnection(fbConnectionString);
            fbConnectionDaughter = new FbConnection(fbConnectionString);

            fbCommand = new FbCommand();
            fbCommand.Connection = fbConnection;

            fbCommandDaughter = new FbCommand();
            fbCommandDaughter.Connection = fbConnectionDaughter;
        }

        protected virtual void BaseTreeNavigator_Load(object sender, EventArgs e)
        {

        }

        protected virtual void BaseTreeNavigator_Shown(object sender, EventArgs e)
        {

        }

        protected virtual void BaseTreeNavigator_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        protected virtual void treeNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        protected virtual void BaseTreeNavigator_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
