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
using System.Data.Common;

using System.Configuration;
using DataLayer;
using LogicLayer;

namespace UniversityClient
{

    public partial class Form1 : Form
    {
        private Configuration config;
        private ConnectionStringSettingsCollection connectionStrings;

        public Form1()
        {
            InitializeComponent();

            //config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //connectionStrings = config.ConnectionStrings.ConnectionStrings;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string currentDBConnectionString = ConfigurationManager.AppSettings["currenDBConnectionStringName"];

            //if (currentDBConnectionString == "firebirdConnectionString")
            //{
            //    string fireBirdConnectionString = ConfigurationManager.ConnectionStrings["firebirdConnectionString"].ConnectionString;

            //    FbConnection fireBirdConnection = new FbConnection(fireBirdConnectionString);

            //    FbCommand fireBirdCommand = new FbCommand();
            //    fireBirdCommand.Connection = fireBirdConnection;
            //    fireBirdCommand.CommandText = "Select ID, NAME, PHONE From PEOPLE";

            //    fireBirdConnection.Open();
            //    FbDataReader firebirdDataReader = fireBirdCommand.ExecuteReader();

            //    string str;
            //    while (firebirdDataReader.Read())
            //    {
            //        str = firebirdDataReader.GetInt32(0).ToString() + "  " + firebirdDataReader.GetString(1) + "  " + firebirdDataReader.GetString(2) + "\r\n";
            //        richTextBox1.Text += str;
            //    }

            //    firebirdDataReader.Close();
            //    fireBirdConnection.Close();
            //}
            //else
            //{

            //    string sqlServerConnectionString = ConfigurationManager.ConnectionStrings["sqlServerConnectionString"].ConnectionString;

            //    SqlConnection fsqlServerConnection = new SqlConnection(sqlServerConnectionString);

            //    SqlCommand sqlServerCommand = new SqlCommand();
            //    sqlServerCommand.Connection = fsqlServerConnection;
            //    sqlServerCommand.CommandText = "Select ID, NAME, PHONE From PEOPLE";

            //    fsqlServerConnection.Open();
            //    SqlDataReader sqlServerdDataReader = sqlServerCommand.ExecuteReader();

            //    string str;
            //    while (sqlServerdDataReader.Read())
            //    {
            //        str = sqlServerdDataReader.GetInt32(0).ToString() + "  " + sqlServerdDataReader.GetString(1) + "  " + sqlServerdDataReader.GetString(2) + "\r\n";
            //        richTextBox1.Text += str;
            //    }

            //    sqlServerdDataReader.Close();
            //    fsqlServerConnection.Close();
            //}

            string fireBirdConnectionString = ConfigurationManager.ConnectionStrings["firebirdConnectionString"].ConnectionString;
            FbConnection fireBirdConnection = new FbConnection(fireBirdConnectionString);

            string sqlServerConnectionString = ConfigurationManager.ConnectionStrings["sqlServerConnectionString"].ConnectionString;
            SqlConnection sqlServerConnection = new SqlConnection(sqlServerConnectionString);

            //set the connection
            DbConnection connection = fireBirdConnection;
            connection.Open();

            //TUObject tuObject = new TUObject(connection, ServerDB.FireBird);
            //TUObjectDTO tuObjectData = new TUObjectDTO()
            //{
            //    Major =0,
            //    Name="NewObject_New",
            //    InputDate="2010-10-25",
            //    ClassId=100,
            //    Id = 208
            //};

            //LogicLayer.Aspirant learner = new LogicLayer.Aspirant(connection, ServerDB.FireBird);

         //   AspirantDTO learnerDTO = (AspirantDTO)learner.Select(11);

            //AuxiliaryWorkerDTO peopleDTO = new AuxiliaryWorkerDTO()
            //{
            //    ClassId = 100,
            //    Major = 0,
            //    Name = "NewObject1",
            //    InputDate = "2010-10-27",
            //    FirstName = "NNameN_",
            //    MiddleName = "NMiddleName",
            //    Address = "Add1",
            //    Phone = "Phone1",

            //    Post = "Wk_",
            //    Salary = 990.23,
            //    Shift = 12,
            //    TypeOfWork = "Worker_1",
            //    Id = 208
            //};

            //learner.Update(peopleDTO);
            //learner.Delete(208);
            
            connection.Close();
            
        }
    }
}
