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
    public partial class People : TUObjects
    {
        public People()
            :base()
        {
            InitializeComponent();
        }

        public People(TreeNode node, DBAction action, int ClassId, MainTreeNavigator mainTree)
            :base(node, action, ClassId, mainTree)
        {
            InitializeComponent();
        }

        protected override void FillForm()
        {
            base.FillForm();

            dbCommand.CommandText = "Select FirstName, MiddleName, Address, Phone From People Where Id = " + ((TreeNodeData)currNode.Tag).Id;
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            textBoxFirstName.Text = dataReader.GetString(0);
            textBoxMiddleName.Text = dataReader.GetString(1);
            textBoxAddress.Text = dataReader.GetString(2);
            textBoxPhone.Text = dataReader.GetString(3);

            dataReader.Close();
        }

        protected override void Edit()
        {
            base.Edit();

            dbCommand.CommandText = "Update People Set FirstName='" + textBoxFirstName.Text + "', MiddleName='" + textBoxMiddleName.Text
                + "', Address='" + textBoxAddress.Text + "', Phone='" + textBoxPhone.Text + "' Where Id= " + ((TreeNodeData)currNode.Tag).Id;

            dbCommand.ExecuteNonQuery();
        }

        protected override void Insert()
        {
            base.Insert();

            dbCommand.CommandText = "Insert Into People(FirstName, MiddleName, Address, Phone) Values('" + textBoxFirstName.Text + "', '"
                + textBoxMiddleName.Text + "', '" + textBoxAddress.Text + "', '" + textBoxPhone.Text + "')";

            dbCommand.ExecuteNonQuery();
        }

        public override void Delete(int Id)
        {
            base.Delete(Id);

            dbCommand.CommandText = "Delete From People Where Id = " + Id;
            dbCommand.ExecuteNonQuery();
        }
    }
}
