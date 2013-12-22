namespace UniversityClient
{
    partial class Learners
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
            this.labelLearnNumber = new System.Windows.Forms.Label();
            this.textBoxLearnNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelLearnNumber
            // 
            this.labelLearnNumber.AutoSize = true;
            this.labelLearnNumber.Location = new System.Drawing.Point(10, 230);
            this.labelLearnNumber.Name = "labelLearnNumber";
            this.labelLearnNumber.Size = new System.Drawing.Size(74, 13);
            this.labelLearnNumber.TabIndex = 12;
            this.labelLearnNumber.Text = "LearnNumber:";
            // 
            // textBoxLearnNumber
            // 
            this.textBoxLearnNumber.Location = new System.Drawing.Point(100, 230);
            this.textBoxLearnNumber.Name = "textBoxLearnNumber";
            this.textBoxLearnNumber.Size = new System.Drawing.Size(120, 20);
            this.textBoxLearnNumber.TabIndex = 13;
            //
            // OK and Cancel button
            //
            this.buttonOK.Location = new System.Drawing.Point(90, 270);
            this.buttonCancel.Location = new System.Drawing.Point(200, 270);
            // 
            // Learners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 310);
            this.Controls.Add(this.textBoxLearnNumber);
            this.Controls.Add(this.labelLearnNumber);
            this.Name = "Learners";
            this.Text = "Learners";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label labelLearnNumber;
        protected System.Windows.Forms.TextBox textBoxLearnNumber;
    }
}