namespace UniversityClient
{
    partial class BaseTreeNavigator
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
            this.treeNavigation = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeNavigation
            // 
            this.treeNavigation.Location = new System.Drawing.Point(12, 12);
            this.treeNavigation.Name = "treeNavigation";
            this.treeNavigation.Size = new System.Drawing.Size(280, 312);
            this.treeNavigation.TabIndex = 0;
            this.treeNavigation.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.BaseTreeNavigator_BeforeExpand);
            this.treeNavigation.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeNavigation_NodeMouseClick);
            // 
            // BaseTreeNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 338);
            this.Controls.Add(this.treeNavigation);
            this.Name = "BaseTreeNavigator";
            this.Text = "TreeNavigator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseTreeNavigator_FormClosing);
            this.Load += new System.EventHandler(this.BaseTreeNavigator_Load);
            this.Shown += new System.EventHandler(this.BaseTreeNavigator_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TreeView treeNavigation;
    }
}