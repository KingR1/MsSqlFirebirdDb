using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using DataLayer;
using LogicLayer;

namespace UniversityClient
{
    public partial class ViewDataClient : Form
    {
        //data
        private TUObjects tuObject;
        private TUObjectsDTO tuObjectData;
        private DBAction dbAction;
        //private List<ReferenceInfo> listRefInfo;
        private List<string> listEnteredValues;
        private List<int> referencedFieldsIds;

        public int TuObjectId
        {
            get { return tuObjectData.Id; }
        }

        public string TuObjectName
        {
            get { return tuObjectData.Name; }
        }

        //veiw controls
        private List<Label> listLabels;
        private List<TextBox> listTextBoxes;
        private List<Button> listButtons;

        public ViewDataClient()
        {
            tuObject = new TUObjects();
            tuObjectData = new TUObjectsDTO();
            dbAction = DBAction.Edit;

            listEnteredValues = new List<string>();
            referencedFieldsIds = new List<int>();

            listLabels = new List<Label>();
            listTextBoxes = new List<TextBox>();
            listButtons = new List<Button>();

            InitializeComponent();
        }

        public ViewDataClient(TUObjects tuObject, TUObjectsDTO tuObjectData, DBAction dbAction)
        {
            this.tuObject = tuObject;
            this.tuObjectData = tuObjectData;
            this.dbAction = dbAction;

            listEnteredValues = new List<string>();
            referencedFieldsIds = new List<int>();

            listLabels = new List<Label>();
            listTextBoxes = new List<TextBox>();
            listButtons = new List<Button>();

            InitializeComponent();
        }

        private void VeiwDataClient_Load(object sender, EventArgs e)
        {
            if (dbAction == DBAction.Edit)
            {
                int id = tuObjectData.Id;
                tuObjectData = tuObject.Select(id);
            }

            List<string> listNames = tuObjectData.LabelsText;
            List<string> listValues = tuObjectData.GetValues();

            List<ReferenceInfo> listRefInfo = tuObjectData.ReferencedField;

            int yPosition = 10;

            for (int i = 0; i < listNames.Count; i++)
            {
                Label labelTmp = new Label();
                labelTmp.Size = new System.Drawing.Size(80, 20);
                labelTmp.Location = new Point(10, yPosition);
                labelTmp.Text = listNames[i]+":";

                listLabels.Add(labelTmp);
                Controls.Add(labelTmp);


                TextBox textBoxTmp = new TextBox();
                textBoxTmp.Size = new System.Drawing.Size(150, 20);
                textBoxTmp.Location = new Point(90, yPosition);
                textBoxTmp.TabIndex = i;
                if(dbAction == DBAction.Edit)
                    textBoxTmp.Text = listValues[i];
                else
                    textBoxTmp.Text = string.Empty;

                listTextBoxes.Add(textBoxTmp);
                Controls.Add(textBoxTmp);

                yPosition += 40;
            }

            //ref buttons
            //int fieldId;

            //foreach (ReferenceInfo item in listRefInfo)
            //{
            //    referencedFieldsIds.Add(item.FieldNumber);
            //    listEnteredValues

            //    fieldId = Convert.ToInt32(listValues[item.FieldNumber]);
            //    listTextBoxes[item.FieldNumber].Text = tuObject.GetNameById(fieldId);

               

            //    Button buttonReference = new Button();
            //    buttonReference.Text = "Change";
            //    buttonReference.Size = new Size(80, 23);
            //    buttonReference.Location = new Point(250, (listTextBoxes[item.FieldNumber]).Location.Y);

            //    listButtons.Add(buttonReference);
            //    Controls.Add(buttonReference);
            //}

            //ok cancel buttons
            buttonOK.Location = new Point(110, yPosition);
            buttonOK.TabIndex = listNames.Count;

            buttonCancel.Location = new Point(210, yPosition);
            buttonCancel.TabIndex = listNames.Count + 1;

            this.ClientSize = new Size(ClientSize.Width, ClientSize.Height + 10);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
           
                foreach (TextBox item in listTextBoxes)
                {
                    listEnteredValues.Add(item.Text);
                }

                tuObjectData.SetValues(listEnteredValues);

                switch (dbAction)
                {
                    case DBAction.Edit:
                        {
                            tuObject.Update(tuObjectData);
                            break;
                        }

                    case DBAction.Insert:
                        {
                            tuObject.Insert(tuObjectData);
                            break;
                        }

                    case DBAction.Delete:
                        {
                            break;
                        }

                    default:
                        break;
                }

          

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }


    }
}
