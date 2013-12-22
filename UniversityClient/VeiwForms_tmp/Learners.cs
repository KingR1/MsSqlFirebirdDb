using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;

namespace UniversityClient
{
    public partial class Learners : People
    {
        public Learners()
            :base()
        {
            InitializeComponent();
        }

        public Learners(TreeNode node, DBAction action, int ClassId, MainTreeNavigator mainTree)
            : base(node, action, ClassId, mainTree)
        {
            InitializeComponent();
        }

        protected override void FillForm()
        {
            base.FillForm();

            dbCommand.CommandText = "Select LearnNumber From Learners Where Id = " + ((TreeNodeData)currNode.Tag).Id;
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            textBoxLearnNumber.Text = dataReader.GetString(0);

            dataReader.Close();
        }

        protected override void Edit()
        {
                base.Edit();

                dbCommand.CommandText = "Update Learners Set LearnNumber='" + textBoxLearnNumber.Text + "' Where Id= " + ((TreeNodeData)currNode.Tag).Id;
                dbCommand.ExecuteNonQuery();
        }

        protected override void Insert()
        {

            base.Insert();

            dbCommand.CommandText = "Insert Into Learners(LearnNumber) Values('" + textBoxLearnNumber.Text + "')";
            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int Id)
        {
            base.Delete(Id);

            dbCommand.CommandText = "Delete From Learners Where Id = " + Id;
            dbCommand.ExecuteNonQuery();
        }
    }
}
