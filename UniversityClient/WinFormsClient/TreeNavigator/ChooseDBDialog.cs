using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

namespace UniversityClient
{
    public partial class ChooseDBDialog : Form
    {
        private string connectionStringName;

        public ChooseDBDialog()
        {
            InitializeComponent();
        }

        public ChooseDBDialog(string connectionStringName)
        {
            InitializeComponent();

            this.connectionStringName = connectionStringName;
        }

        public string ConnectionStringName
        {
            get { return connectionStringName; }
        }

        private void ChooseDBDialog_Load(object sender, EventArgs e)
        {
            List<string> listConnectionNames = new List<string>();

            // index o - Local SqlServer - !
            for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                if (ConfigurationManager.ConnectionStrings[i].Name == connectionStringName)
                    continue;
                listConnectionNames.Add(ConfigurationManager.ConnectionStrings[i].Name);
            }
            listConnectionNames.Sort();
            comboBoxSelectBD.DataSource = listConnectionNames;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            connectionStringName = (string)comboBoxSelectBD.SelectedValue;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //cancel button
        }
    }
}
