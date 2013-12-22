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
    public partial class Entrants : Learners
    {
        public Entrants()
        {
            InitializeComponent();
        }

        public Entrants(TreeNode node, DBAction action, int ClassId, MainTreeNavigator mainTree)
            : base(node, action, ClassId, mainTree)
        {
            InitializeComponent();
        }

        protected override void FillForm()
        {
            base.FillForm();

            dbCommand.CommandText = "Select MarkSum, Privilee From Entrants Where Id = " + ((TreeNodeData)currNode.Tag).Id;
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

            textBoxMarkSum.Text = dataReader.GetDouble(0).ToString();
            textBoxPrivilege.Text = dataReader.GetString(1);

            dataReader.Close();
        }

        protected override void Edit()
        {
            double sum = 0;

            try
            {
                sum = System.Convert.ToInt32(textBoxMarkSum.Text);

                base.Edit();

                dbCommand.CommandText = "Update Entrants Set MarkSum=" + sum + ", Privilee='" + textBoxPrivilege.Text
                    + "' Where Id= " + ((TreeNodeData)currNode.Tag).Id;
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                notClose = true;
                MessageBox.Show(ex.Message);
            }
        }

        protected override void Insert()
        {
            double sum = 0;

            try
            {
                sum = System.Convert.ToInt32(textBoxMarkSum.Text);

                base.Insert();

                dbCommand.CommandText = "Insert Into Entrants(MarkSum, Privilee) Values( " + sum +
                           ", '" + textBoxPrivilege.Text + "' )";

                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                notClose = true;
                MessageBox.Show(ex.Message);
            }
        }

        public override void Delete(int Id)
        {
            base.Delete(Id);

            dbCommand.CommandText = "Delete From Entrants Where Id = " + Id;
            dbCommand.ExecuteNonQuery();
        }
    }
}
