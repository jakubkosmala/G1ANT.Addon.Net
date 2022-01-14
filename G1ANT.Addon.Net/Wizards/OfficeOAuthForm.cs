using G1ANT.Addon.Net.Models;
using G1ANT.Addon.Net.Structures;
using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace G1ANT.Addon.Net.Wizards
{
    public partial class OfficeOAuthForm : Form
    {
        private const string c_scopeImap = "imap";
        private const string c_scopeSmtp = "smtp";
        private IMainForm mainForm;

        public OfficeOAuthForm(IMainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            scopeImap.Checked = true;
            scopeSmtp.Checked = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            try
            {
                codeTemplate.Text = GetOfficeOAuthTemplate(tenantId.Text, clientId.Text, userName.Text, GetSelectedScopes());
            }
            catch (Exception ex)
            {
                var messageBuilder = new StringBuilder();
                if (ex is AggregateException aggregateException)
                {
                    messageBuilder.AppendLine(aggregateException.Message);
                    foreach (var innerException in aggregateException.InnerExceptions)
                        messageBuilder.AppendLine($" {innerException.Message}");
                }
                else
                {
                    messageBuilder.AppendLine(ex.Message);
                    if (ex.InnerException != null)
                        messageBuilder.AppendLine($" {ex.InnerException.Message}");
                }
                RobotMessageBox.Show(messageBuilder.ToString());
            }
        }

        private string[] GetSelectedScopes()
        {
            var scopes = new List<string>();
            if (scopeImap.Checked)
                scopes.Add(c_scopeImap);
            if (scopeSmtp.Checked)
                scopes.Add(c_scopeSmtp);
            return scopes.ToArray();
        }

        private void AppendImapOpenExTemplate(StringBuilder template, string authenticationVariableName)
        {
            template.AppendLine($"imap.openex host {SpecialChars.Text}outlook.office365.com{SpecialChars.Text} port 993 usessl true ignorecertificateerrors true authentication {authenticationVariableName}");
            template.AppendLine("- your code here");
            template.AppendLine($"imap.close");
        }

        private void AppendSmtpOpenExTemplate(StringBuilder template, string authenticationVariableName)
        {
            template.AppendLine($"smtp.openex host {SpecialChars.Text}smtp.office365.com{SpecialChars.Text} port 587 options tls ignorecertificateerrors true authentication {authenticationVariableName}");
            template.AppendLine("- your code here");
            template.AppendLine($"smtp.close");
        }

        private string GetOfficeOAuthTemplate(string tenantId, string clientId, string username, string[] scopes)
        {
            var model = CreateModelWithToken(tenantId, clientId, username, string.Join(",", scopes));
            var varName = $"{SpecialChars.Variable}authorization";
            var template = new StringBuilder();
            template.AppendLine($"{varName} = {SpecialChars.IndexBegin}officeoauth{SpecialChars.IndexEnd}");
            template.AppendLine($"{varName}{SpecialChars.IndexBegin}{OfficeOAuthStructure.IndexNames.TenantId}{SpecialChars.IndexEnd} = {SpecialChars.Text}{model.TenantId}{SpecialChars.Text}");
            template.AppendLine($"{varName}{SpecialChars.IndexBegin}{OfficeOAuthStructure.IndexNames.ClientId}{SpecialChars.IndexEnd} = {SpecialChars.Text}{model.ClientId}{SpecialChars.Text}");
            template.AppendLine($"{varName}{SpecialChars.IndexBegin}{OfficeOAuthStructure.IndexNames.Username}{SpecialChars.IndexEnd} = {SpecialChars.Text}{model.Username}{SpecialChars.Text}");
            template.AppendLine($"{varName}{SpecialChars.IndexBegin}{OfficeOAuthStructure.IndexNames.Scope}{SpecialChars.IndexEnd} = {SpecialChars.Text}{model.Scope}{SpecialChars.Text}");
            if (scopes.Contains(c_scopeImap))
                AppendImapOpenExTemplate(template, varName);
            if (scopes.Contains(c_scopeSmtp))
                AppendSmtpOpenExTemplate(template, varName);
            return template.ToString();
        }

        private OfficeOAuthModel CreateModelWithToken(string tenantId, string clientId, string username, string scope)
        {
            var model = new OfficeOAuthModel()
            {
                TenantId = tenantId,
                ClientId = clientId,
                Username = username,
                Scope = scope
            };
            model.RequestTokenInteractive();
            return model;
        }

        private void copyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(codeTemplate.Text);
        }

        private void insertToScript_Click(object sender, EventArgs e)
        {
            mainForm.InsertTextIntoCurrentEditor(codeTemplate.Text);
        }

        private void codeTemplate_TextChanged(object sender, EventArgs e)
        {
            copyToClipboard.Enabled = !string.IsNullOrEmpty(codeTemplate.Text);
            insertToScript.Enabled = !string.IsNullOrEmpty(codeTemplate.Text);
        }
    }
}
