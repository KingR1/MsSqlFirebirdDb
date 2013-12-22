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
    public partial class MainTreeNavigator : BaseTreeNavigator
    {
        private TreeNode linkNode;
        private TreeNode clickNode;
        private bool wasLoaded_linkNode;

        public MainTreeNavigator()
        {
            InitializeComponent();
        }

        public TreeNode LinkNode
        {
            get { return linkNode; }
        }

        public TreeNode ClickNode
        {
            get { return clickNode; }
        }

        private void OpenLinkNode()
        {
            ChooseDBDialog chooseDB = new ChooseDBDialog(nativeConnectionStringName);

            if (chooseDB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                linkNode.Nodes.Clear();

                TreeNodeData nodeData = (TreeNodeData)linkNode.Tag;
                nodeData.ConnectionStringName = chooseDB.ConnectionStringName;

                FillTreeNavigation(0, linkNode, nodeContextMenuStrip);

                wasLoaded_linkNode = true;
            }
            else
            {
                wasLoaded_linkNode = false;
            }
        }

        protected override void BaseTreeNavigator_Load(object sender, EventArgs e)
        {
            base.BaseTreeNavigator_Load(sender, e);
        }

        protected override void BaseTreeNavigator_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.BaseTreeNavigator_FormClosing(sender, e);
        }

        protected override void BaseTreeNavigator_Shown(object sender, EventArgs e)
        {
            //currentConnectionStringName = ConfigurationManager.AppSettings["currentConnectionStringName"];
            ChooseDBDialog chooseDB = new ChooseDBDialog();

            if (chooseDB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                treeNavigation.BeginUpdate();
                treeNavigation.UseWaitCursor = true;

                nativeConnectionStringName = chooseDB.ConnectionStringName;
                currentConnectionStringName = chooseDB.ConnectionStringName;

                FillTreeNavigation(0, null, nodeContextMenuStrip);

                TreeNodeData linkNodeData = new TreeNodeData(0, false, string.Empty);

                linkNode = new TreeNode("Link");
                linkNode.Tag = linkNodeData;
                linkNode.ContextMenuStrip = linkContextMenuStrip;
                linkNode.Nodes.Add("Virtual Node");

                wasLoaded_linkNode = false;
                treeNavigation.Nodes.Add(linkNode);

                treeNavigation.UseWaitCursor = false;
                treeNavigation.EndUpdate();
            }
            else
            {
                Application.Exit();
            }
        }

        protected override void BaseTreeNavigator_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeData nodeData = (TreeNodeData)e.Node.Tag;
            if (!nodeData.WasOpened)
            {
                nodeData.WasOpened = true;

                if (e.Node == linkNode && !wasLoaded_linkNode)
                {
                    OpenLinkNode();
                    if (!wasLoaded_linkNode)
                    {
                        e.Cancel = true;
                         nodeData.WasOpened = false;
                    }

                }
                else
                {
                    try
                    {
                        treeNavigation.BeginUpdate();
                        treeNavigation.UseWaitCursor = true;

                        e.Node.Nodes.Clear();
                        FillTreeNavigation(nodeData.Id, e.Node, nodeContextMenuStrip);

                        treeNavigation.UseWaitCursor = false;
                        treeNavigation.EndUpdate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        protected override void treeNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            clickNode = e.Node;
        }

        #region linkNode  context menu

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkNode.Collapse();
            OpenLinkNode();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkNode.Nodes.Clear();
            linkNode.Nodes.Add("Virtual Node");
            linkNode.Collapse();

            TreeNodeData nodeData = (TreeNodeData)linkNode.Tag;
            nodeData.WasOpened = false;
            wasLoaded_linkNode = false;
        }

        #endregion

        #region nodeComtextMenu implementation

        private void nodeContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            string connectionStringName = ((TreeNodeData)(clickNode.Tag)).ConnectionStringName;
            string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

            switch (providerName)
            {
                case "System.Data.SqlClient":
                    {
                        dbConnection = sqlConnection;
                        dbCommand = sqlCommand;

                        dbConnectionDaughter = sqlConnectionDaughter;
                        dbCommandDaughter = sqlCommandDaughter;

                        dbServer = ServerDB.MSSqlServer;

                        break;
                    }
                case "FirebirdSql.Data.FirebirdClient":
                    {
                        dbConnection = fbConnection;
                        dbCommand = fbCommand;

                        dbConnectionDaughter = fbConnectionDaughter;
                        dbCommandDaughter = fbCommandDaughter;

                        dbServer = ServerDB.FireBird;

                        break;
                    }
                default:
                    {
                        throw new Exception("Bad  name of current connection string");
                    }
            }

            dbConnection.Open();

            insertToolStripMenuItem.DropDownItems.Clear();

            dbCommand.CommandText = "Select FormName, InsertClassId "
            + "From InsertInTables_ inner join Classes_ on InsertInTables_.InsertClassId = Classes_.ClassId "
            + "Where InsertInTables_.ClassId = (Select ClassId From TUObjects Where Id =" + ((TreeNodeData)clickNode.Tag).Id + ")"
            + "Order by FormName";

            dbDataReader = dbCommand.ExecuteReader();

            while (dbDataReader.Read())
            {
                ToolStripMenuItem tsMenuItem = new ToolStripMenuItem(dbDataReader.GetString(0));
                tsMenuItem.Tag = dbDataReader.GetInt32(1).ToString();
                tsMenuItem.Click += new EventHandler(insertItem_Click);
                insertToolStripMenuItem.DropDownItems.Add(tsMenuItem);
            }
            dbDataReader.Close();

            if (insertToolStripMenuItem.DropDownItems.Count == 0)
                insertToolStripMenuItem.Enabled = false;
            else
                insertToolStripMenuItem.Enabled = true;

            dbConnection.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string connectionStringName = ((TreeNodeData)(clickNode.Tag)).ConnectionStringName;
            //string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

            //switch (providerName)
            //{
            //    case "System.Data.SqlClient":
            //        {
            //            dbConnection = sqlConnection;
            //            dbCommand = sqlCommand;

            //            dbConnectionDaughter = sqlConnectionDaughter;
            //            dbCommandDaughter = sqlCommandDaughter;

            //            break;
            //        }
            //    case "FirebirdSql.Data.FirebirdClient":
            //        {
            //            dbConnection = fbConnection;
            //            dbCommand = fbCommand;

            //            dbConnectionDaughter = fbConnectionDaughter;
            //            dbCommandDaughter = fbCommandDaughter;

            //            break;
            //        }
            //    default:
            //        {
            //            throw new Exception("Bad  name of current connection string");
            //        }
            //}

            dbConnection.Open();

            TUObjectsDTO tuObjectData = new TUObjectsDTO()
            {
                Id = ((TreeNodeData)(clickNode.Tag)).Id

            };
            
            dbCommand.CommandText = "Select ClassId From TUObjects Where Id = " + tuObjectData.Id;
            int classId = (int)dbCommand.ExecuteScalar();

            dbCommand.CommandText = "Select FormName From Classes_ Where ClassId = " + classId;
            string className = (string)dbCommand.ExecuteScalar();

            TUObjects tuObject;
            Type t = Type.GetType(DefinedConstants.logicLeyerNameSpace+ "." + className+", "+DefinedConstants.logicLeyerNameSpace);

            tuObject = (TUObjects)Activator.CreateInstance(t, dbConnection, dbServer);

            //showing dialog

            ViewDataClient editDialog = new ViewDataClient(tuObject, tuObjectData, DBAction.Edit);
            if (editDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                clickNode.Text = editDialog.TuObjectName;
            }

            dbConnection.Close();
        }

        private void insertItem_Click(object sender, EventArgs e)
        {
            dbConnection.Open();

            //TUObjectDTO tuObjectData = new TUObjectDTO();
            int classId = System.Convert.ToInt32(((ToolStripMenuItem)sender).Tag);

            dbCommand.CommandText = "Select FormName From Classes_ Where ClassId = " + classId;
            string className = (string)dbCommand.ExecuteScalar();

            TUObjects tuObject;
            Type t = Type.GetType(DefinedConstants.logicLeyerNameSpace + "." + className + ", " + DefinedConstants.logicLeyerNameSpace);

            tuObject = (TUObjects)Activator.CreateInstance(t, dbConnection, dbServer);

            TUObjectsDTO tuObjectData;
            Type tDTO = Type.GetType(DefinedConstants.dataLeyerNameSpace + "." + className + "DTO, " + DefinedConstants.dataLeyerNameSpace);

            tuObjectData = (TUObjectsDTO)Activator.CreateInstance(tDTO);

            tuObjectData.ClassId = classId;
            tuObjectData.Major = ((TreeNodeData)clickNode.Tag).Id;

            //showing dialog

            ViewDataClient editDialog = new ViewDataClient(tuObject, tuObjectData, DBAction.Insert);
            if (editDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dbCommand.CommandText = "Select Count(Id) From TUObjects Where Major = " + ((TreeNodeData)clickNode.Tag).Id;
                int count = (int)dbCommand.ExecuteScalar();

                dbCommand.CommandText = "Select Max(Id) From TUObjects";
                int tmpId = (int)dbCommand.ExecuteScalar();

                //tree modification
                if (((TreeNodeData)clickNode.Tag).WasOpened)
                {
                    string connectionStr = ((TreeNodeData)clickNode.Tag).ConnectionStringName;
                    TreeNode tmp = new TreeNode(tuObjectData.Name);
                    TreeNodeData cls = new TreeNodeData(tmpId, false, connectionStr);
                    tmp.Tag = cls;
                    tmp.ContextMenuStrip = clickNode.ContextMenuStrip;

                    clickNode.Nodes.Add(tmp);
                }
                else if (count == 1)
                {
                    clickNode.Nodes.Add("Virtual Node");
                }
            }

            dbConnection.Close();
        }

        //private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        //recursive function for deleting nodes from tree - delete subtree
        private void DeleteTreeNodes(TUObjects fuObject, int Id, bool IsFirst) //TUObjects
        {
            dbConnection.Open();

            List<int> LId = new List<int>();
            bool IsDel = false;
            string className;
            dbCommand.CommandText = "Select Count(Id) From TUObjects Where Major =" + Id;
            int count = (int)dbCommand.ExecuteScalar();

            if (count > 0)
            {
                if (IsFirst)
                {
                    DialogResult d = MessageBox.Show("This node has douter nodes. Do you want to delete them?", "Deleting nodes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                        IsDel = true;
                }
                else
                    IsDel = true;

                if (IsDel)
                {

                    dbCommand.CommandText = "Select Id From TUObjects Where Major = " + Id;
                    dbDataReader = dbCommand.ExecuteReader();

                    while (dbDataReader.Read())
                    {
                        LId.Add(dbDataReader.GetInt32(0));
                    }
                    dbDataReader.Close();

                    dbConnection.Close();
                    foreach (int i in LId)
                    {
                        dbConnection.Open();

                        dbCommand.CommandText = "Select FormName From Classes_ Where ClassId = "
                        + "(Select ClassId From TUObjects Where Id = " + i + ")";
                        className = (string)dbCommand.ExecuteScalar();

                        TUObjects tuObject;
                        Type t = Type.GetType(DefinedConstants.logicLeyerNameSpace + "." + className + ", " + DefinedConstants.logicLeyerNameSpace);

                        tuObject = (TUObjects)Activator.CreateInstance(t, dbConnection, dbServer);

                        dbConnection.Close();

                        //
                        DeleteTreeNodes(tuObject, i, false);
                    }

                    dbConnection.Open();
                    fuObject.Delete(Id);
                    clickNode.Remove();
                }
            }
            else
            {
                fuObject.Delete(Id);
                clickNode.Remove();
            }


            dbConnection.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbConnection.Open();

            int Id = ((TreeNodeData)(clickNode.Tag)).Id;

            dbCommand.CommandText = "Select FormName From Classes_ Where ClassId = "
                + "(Select ClassId From TUObjects Where Id = " + Id + ")";
            string className = (string)dbCommand.ExecuteScalar();

            TUObjects tuObject;
            Type t = Type.GetType(DefinedConstants.logicLeyerNameSpace + "." + className + ", " + DefinedConstants.logicLeyerNameSpace);

            tuObject = (TUObjects)Activator.CreateInstance(t, dbConnection, dbServer);

            dbConnection.Close();

            DeleteTreeNodes(tuObject, Id, true);
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
