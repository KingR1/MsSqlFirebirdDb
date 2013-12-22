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

namespace UniversityClient
{
    public partial class TUObjects : Form
    {
        protected DbConnection dbConnection;
        protected DbCommand dbCommand;
        protected DbDataReader dataReader;
        protected DbDataReader dataReader1;

        protected DbConnection dbConnection1;
        protected DbCommand dbCommand1;

        protected int ClassId;
        protected DBAction action;
        protected TreeNode currNode;
        protected TreeNode NodeForOpen;
        protected TreeNode NodeForOpen2;

        protected bool notClose;
        protected int n;
        protected int clsId = 0;
        protected List<int> LId;
        protected List<int> LId2;
        protected int ChooseId;
        protected int ChooseId2;
        protected MainTreeNavigator mainTree;

        public TUObjects()
        {
            notClose = false;
            LId = new List<int>();
            LId2 = new List<int>();

            NodeForOpen = null;
            NodeForOpen2 = null;

            InitializeComponent();
        }

        public TUObjects(TreeNode node, DBAction action, int ClassId, MainTreeNavigator mainTree)
        {
            dbConnection = mainTree.MainDbConnection;
            dbConnection1 = mainTree.DaughterDbConnection;

            string connectionStringName = ((TreeNodeData)node.Tag).ConnectionStringName;

            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

            switch (providerName)
            {
                case "System.Data.SqlClient":
                    {
                        //dbConnection = new SqlConnection(connectionString);
                        //dbConnection1 = new SqlConnection(connectionString);

                        dbCommand = new SqlCommand();
                        dbCommand.Connection = dbConnection;

                        dbCommand1 = new SqlCommand();
                        dbCommand1.Connection = dbConnection1;

                        break;
                    }
                case "FirebirdSql.Data.FirebirdClient":
                    {
                        //dbConnection = new FbConnection(connectionString);
                        //dbConnection1 = new FbConnection(connectionString);

                        dbCommand = new FbCommand();
                        dbCommand.Connection = dbConnection;

                        dbCommand1 = new FbCommand();
                        dbCommand1.Connection = dbConnection1;

                        break;
                    }
                default:
                    {
                        MessageBox.Show("The connection object is invalid", "Connection object error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
            }

            this.currNode = node;
            this.action = action;
            this.ClassId = ClassId;

            notClose = false;
            LId = new List<int>();
            LId2 = new List<int>();
            this.mainTree = mainTree;

            InitializeComponent();
        }

        protected string GetName(int Id)
        {
            string s;
            dbCommand1.CommandText = "Select Name From TUObjects Where Id =" + Id;
            Id = (int)dbCommand1.ExecuteScalar();
            if (Id > 0)
                s = dataReader1.GetString(0);
            else
                s = "";
            return s;
        }

        protected virtual void FillForm()
        {
            dbCommand.CommandText = "Select Name, InputDate From TUObjects Where Id = " + ((TreeNodeData)currNode.Tag).Id;
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            textBoxName.Text = dataReader.GetString(0);
            textBoxCreationDate.Text = dataReader.GetDateTime(1).ToShortDateString();

            dataReader.Close();
        }

        protected virtual void Edit()
        {
            try
            {
                DateTime date = System.Convert.ToDateTime(textBoxCreationDate.Text);
                string dt = date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();

                dbCommand.CommandText = "Update TUObjects Set Name='" + textBoxName.Text + "', InputDate = '" + dt + "' Where Id= " + ((TreeNodeData)currNode.Tag).Id;
                dbCommand.ExecuteNonQuery();

                currNode.Text = textBoxName.Text;
                notClose = false;
            }
            catch (Exception ex)
            {
                notClose = true;
                MessageBox.Show("Incorect date\n"+ex.Message, "Error entering data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void Insert()
        {
            try
            {
                DateTime date = System.Convert.ToDateTime(textBoxCreationDate.Text);
                string dt = date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();

                int major = ((TreeNodeData)currNode.Tag).Id;

                dbCommand.CommandText = "Insert Into TUObjects(Major, InputDate, ClassId, Name) Values( '" + major + "', '" + dt + "', '"
                    + ClassId + "', ' " + textBoxName.Text + "' )";
                dbCommand.ExecuteNonQuery();

                dbCommand.CommandText = "Select Count(Id) From TUObjects Where Major = " + ((TreeNodeData)currNode.Tag).Id;
                int count = (int)dbCommand.ExecuteScalar();

                dbCommand.CommandText = "Select Max(Id) From TUObjects";
                int tmpId = (int)dbCommand.ExecuteScalar();

                //tree modification
                if (((TreeNodeData)currNode.Tag).WasOpened)
                {
                    TreeNode tmp = new TreeNode(textBoxName.Text);
                    TreeNodeData cls = new TreeNodeData(tmpId, false, mainTree.CurrentConnectionStringName);
                    tmp.Tag = cls;
                    tmp.ContextMenuStrip = currNode.ContextMenuStrip;

                    currNode.Nodes.Add(tmp);
                }
                else if (count == 1)
                {
                    currNode.Nodes.Add("Virtual Node");
                }

                notClose = false;
            }
            catch (Exception ex)
            {
                notClose = true;
                MessageBox.Show("Incorect date\n" + ex.Message, "Error entering data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public virtual void Delete(int Id)
        {
            dbCommand.CommandText = "Delete From TUObjects Where Id = " + Id;
            dbCommand.ExecuteNonQuery();

            currNode.Remove();
        }

        private void TUObjects_Load(object sender, EventArgs e)
        {
            //dbConnection.Open();

            switch (action)
            {
                case DBAction.Edit:
                    {
                        FillForm();
                        break;
                    }
                case DBAction.Insert:
                    {
                        break;
                    }
                case DBAction.Delete:
                    {
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Some Error", "Fill Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        break;
                    }
            }

            textBoxCreationDate.Select();
        }

        private void TUObjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (notClose)
                e.Cancel = true;
           // else
                //dbConnection.Close();  - ?
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            switch (action)
            {
                case DBAction.Edit:
                    {
                        Edit();

                        break;
                    }
                case DBAction.Insert:
                    {
                        Insert();

                        break;
                    }
                case DBAction.Delete:
                    {
                        // Delete(Id);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Some Error", "Fill Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        break;
                    }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            notClose = false;
        }

    }
}
