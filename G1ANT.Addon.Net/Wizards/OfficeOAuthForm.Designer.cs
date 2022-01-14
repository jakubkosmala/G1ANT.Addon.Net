namespace G1ANT.Addon.Net.Wizards
{
    partial class OfficeOAuthForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tenantId = new System.Windows.Forms.TextBox();
            this.clientId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.scopeImap = new System.Windows.Forms.CheckBox();
            this.scopeSmtp = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.copyToClipboard = new System.Windows.Forms.Button();
            this.insertToScript = new System.Windows.Forms.Button();
            this.codeTemplate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tenant Id";
            // 
            // tenantId
            // 
            this.tenantId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tenantId.Location = new System.Drawing.Point(15, 35);
            this.tenantId.Name = "tenantId";
            this.tenantId.Size = new System.Drawing.Size(478, 20);
            this.tenantId.TabIndex = 1;
            // 
            // clientId
            // 
            this.clientId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientId.Location = new System.Drawing.Point(15, 77);
            this.clientId.Name = "clientId";
            this.clientId.Size = new System.Drawing.Size(478, 20);
            this.clientId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Application (client) ID";
            // 
            // userName
            // 
            this.userName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userName.Location = new System.Drawing.Point(15, 120);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(478, 20);
            this.userName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username (optional)";
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
            // scopeImap
            // 
            this.scopeImap.AutoSize = true;
            this.scopeImap.Location = new System.Drawing.Point(16, 19);
            this.scopeImap.Name = "scopeImap";
            this.scopeImap.Size = new System.Drawing.Size(49, 17);
            this.scopeImap.TabIndex = 8;
            this.scopeImap.Text = "Imap";
            this.scopeImap.UseVisualStyleBackColor = true;
            // 
            // scopeSmtp
            // 
            this.scopeSmtp.AutoSize = true;
            this.scopeSmtp.Location = new System.Drawing.Point(71, 19);
            this.scopeSmtp.Name = "scopeSmtp";
            this.scopeSmtp.Size = new System.Drawing.Size(50, 17);
            this.scopeSmtp.TabIndex = 9;
            this.scopeSmtp.Text = "Smtp";
            this.scopeSmtp.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.scopeImap);
            this.groupBox1.Controls.Add(this.scopeSmtp);
            this.groupBox1.Location = new System.Drawing.Point(15, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 46);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scope";
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
            // OfficeOAuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 459);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clientId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tenantId);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OfficeOAuthForm";
            this.ShowInTaskbar = false;
            this.Text = "Office OAuth Code Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tenantId;
        private System.Windows.Forms.TextBox clientId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox scopeImap;
        private System.Windows.Forms.CheckBox scopeSmtp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox codeTemplate;
        private System.Windows.Forms.Button copyToClipboard;
        private System.Windows.Forms.Button insertToScript;
    }
}