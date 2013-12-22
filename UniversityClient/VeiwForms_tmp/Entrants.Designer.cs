namespace UniversityClient
{
    partial class Entrants
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMarkSum = new System.Windows.Forms.Label();
            this.textBoxMarkSum = new System.Windows.Forms.TextBox();
            this.labelPrivilege = new System.Windows.Forms.Label();
            this.textBoxPrivilege = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelMarkSum
            // 
            this.labelMarkSum.AutoSize = true;
            this.labelMarkSum.Location = new System.Drawing.Point(10, 270);
            this.labelMarkSum.Name = "labelMarkSum";
            this.labelMarkSum.Size = new System.Drawing.Size(58, 13);
            this.labelMarkSum.TabIndex = 14;
            this.labelMarkSum.Text = "Mark Sum:";
            // 
            // textBoxMarkSum
            // 
            this.textBoxMarkSum.Location = new System.Drawing.Point(100, 270);
            this.textBoxMarkSum.Name = "textBoxMarkSum";
            this.textBoxMarkSum.Size = new System.Drawing.Size(120, 20);
            this.textBoxMarkSum.TabIndex = 15;
            // 
            // labelPrivilege
            // 
            this.labelPrivilege.AutoSize = true;
            this.labelPrivilege.Location = new System.Drawing.Point(10, 310);
            this.labelPrivilege.Name = "labelPrivilege";
            this.labelPrivilege.Size = new System.Drawing.Size(50, 13);
            this.labelPrivilege.TabIndex = 16;
            this.labelPrivilege.Text = "Privilege:";
            // 
            // textBoxPrivilege
            // 
            this.textBoxPrivilege.Location = new System.Drawing.Point(100, 310);
            this.textBoxPrivilege.Name = "textBoxPrivilege";
            this.textBoxPrivilege.Size = new System.Drawing.Size(120, 20);
            this.textBoxPrivilege.TabIndex = 17;
            //
            // OK and Cancel button
            //
            this.buttonOK.Location = new System.Drawing.Point(90, 350);
            this.buttonCancel.Location = new System.Drawing.Point(200, 350);
            // 
            // Entrants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 390);
            this.Controls.Add(this.textBoxPrivilege);
            this.Controls.Add(this.labelPrivilege);
            this.Controls.Add(this.textBoxMarkSum);
            this.Controls.Add(this.labelMarkSum);
            this.Name = "Entrants";
            this.Text = "Entrants";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label labelMarkSum;
        protected System.Windows.Forms.TextBox textBoxMarkSum;
        protected System.Windows.Forms.Label labelPrivilege;
        protected System.Windows.Forms.TextBox textBoxPrivilege;
    }
}