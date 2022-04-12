namespace G1ANT.Addon.Net.Wizards
{
    partial class OAuthTokenForm
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
            this.components = new System.ComponentModel.Container();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.copyToClipboard = new System.Windows.Forms.Button();
            this.insertToScript = new System.Windows.Forms.Button();
            this.codeTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.structuresBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectionDetails = new G1ANT.Addon.Net.Wizards.DynamicFormLayoutPanel(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok.Location = new System.Drawing.Point(374, 203);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(119, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Generate code";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(293, 203);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Close";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.copyToClipboard);
            this.groupBox2.Controls.Add(this.insertToScript);
            this.groupBox2.Controls.Add(this.codeTemplate);
            this.groupBox2.Location = new System.Drawing.Point(15, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 195);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Template";
            // 
            // copyToClipboard
            // 
            this.copyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyToClipboard.Enabled = false;
            this.copyToClipboard.Location = new System.Drawing.Point(248, 167);
            this.copyToClipboard.Name = "copyToClipboard";
            this.copyToClipboard.Size = new System.Drawing.Size(105, 23);
            this.copyToClipboard.TabIndex = 2;
            this.copyToClipboard.Text = "Copy to clipboard";
            this.copyToClipboard.UseVisualStyleBackColor = true;
            this.copyToClipboard.Click += new System.EventHandler(this.copyToClipboard_Click);
            // 
            // insertToScript
            // 
            this.insertToScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.insertToScript.Enabled = false;
            this.insertToScript.Location = new System.Drawing.Point(359, 166);
            this.insertToScript.Name = "insertToScript";
            this.insertToScript.Size = new System.Drawing.Size(113, 23);
            this.insertToScript.TabIndex = 1;
            this.insertToScript.Text = "Insert to the script";
            this.insertToScript.UseVisualStyleBackColor = true;
            this.insertToScript.Click += new System.EventHandler(this.insertToScript_Click);
            // 
            // codeTemplate
            // 
            this.codeTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeTemplate.Location = new System.Drawing.Point(6, 19);
            this.codeTemplate.Multiline = true;
            this.codeTemplate.Name = "codeTemplate";
            this.codeTemplate.ReadOnly = true;
            this.codeTemplate.Size = new System.Drawing.Size(466, 142);
            this.codeTemplate.TabIndex = 0;
            this.codeTemplate.TextChanged += new System.EventHandler(this.codeTemplate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Connection type";
            // 
            // structuresBox
            // 
            this.structuresBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.structuresBox.FormattingEnabled = true;
            this.structuresBox.Location = new System.Drawing.Point(102, 12);
            this.structuresBox.Name = "structuresBox";
            this.structuresBox.Size = new System.Drawing.Size(190, 21);
            this.structuresBox.TabIndex = 13;
            this.structuresBox.SelectedIndexChanged += new System.EventHandler(this.structuresBox_SelectedIndexChanged);
            this.structuresBox.Enter += new System.EventHandler(this.structuresBox_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.connectionDetails);
            this.groupBox1.Location = new System.Drawing.Point(15, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 158);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection details";
            // 
            // connectionDetails
            // 
            this.connectionDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionDetails.ColumnCount = 2;
            this.connectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionDetails.Location = new System.Drawing.Point(6, 19);
            this.connectionDetails.Name = "connectionDetails";
            this.connectionDetails.RowCount = 2;
            this.connectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.connectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.connectionDetails.Size = new System.Drawing.Size(466, 133);
            this.connectionDetails.TabIndex = 14;
            // 
            // OAuthTokenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 459);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.structuresBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OAuthTokenForm";
            this.ShowInTaskbar = false;
            this.Text = "OAuth Structure Generator";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox codeTemplate;
        private System.Windows.Forms.Button copyToClipboard;
        private System.Windows.Forms.Button insertToScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox structuresBox;
        private DynamicFormLayoutPanel connectionDetails;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}