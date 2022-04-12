using G1ANT.Language;
using System;
using G1ANT.Addon.Net.Models;

namespace G1ANT.Addon.Net.Structures
{
    [Structure(Name = "gmailoauth", Priority = 999, Default = 0, AutoCreate = false, Tooltip = "The structure stores OAuth details how to connect to the server")]
    public class GMailOAuthStructure : StructureTyped<GmailOAuthModel>, IOauthWizardModel
    {
        public static class IndexNames
        {
            public const string Username = "username";
            public const string ClientId = "clientid";
            public const string ClientSecret = "clientsecret";
            public const string Token = "token";
            public const string Scope = "scope";
            public const string CacheFolder = "cachefolder";
        }

        public bool IsImapRequested => true;

        public bool IsSmtpRequested => true;

        public string SmtpHost => "smtp.gmail.com";

        public string SmtpPort => "587";

        public string ImapHost => "imap.gmail.com";

        public string ImapPort => "993";
        public string[] RequiredIndexes => new[]
        {
            IndexNames.Username,
            IndexNames.ClientId,
            IndexNames.ClientSecret,
            IndexNames.Token
        };

        public GMailOAuthStructure(object value, string format = null, AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        public GMailOAuthStructure(GmailOAuthModel value, string format = "")
            : base(value, format)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(IndexNames.Username);
            Indexes.Add(IndexNames.ClientId);
            Indexes.Add(IndexNames.ClientSecret);
            Indexes.Add(IndexNames.Token);
            Indexes.Add(IndexNames.Scope);
            Indexes.Add(IndexNames.CacheFolder);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentException($"Index cannot be empty, possible values: {string.Join(", ", index)}", nameof(index));

            switch (index.ToLower())
            {
                case IndexNames.Username:
                    return new TextStructure(Value?.Username);
                case IndexNames.ClientId:
                    return new TextStructure(Value?.ClientId);
                case IndexNames.ClientSecret:
                    return new TextStructure(Value?.ClientSecret);
                case IndexNames.Token:
                    return new TextStructure(Value?.Token);
                case IndexNames.Scope:
                    return new TextStructure(Value?.Scope);
                case IndexNames.CacheFolder:
                    return new TextStructure(Value?.CacheFolder);
            }
            throw new ArgumentException($"Unknown index '{index}', possible values: {string.Join(", ", index)}", nameof(index));
        }

        public override void Set(Structure structure, string index = null)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentException($"Index cannot be empty, possible values: {string.Join(", ", index)}", nameof(index));

            switch (index.ToLower())
            {
                case IndexNames.Username:
                    Value.Username = structure?.ToString();
                    break;
                case IndexNames.ClientId:
                    Value.ClientId = structure?.ToString();
                    break;
                case IndexNames.ClientSecret:
                    Value.ClientSecret = structure?.ToString();
                    break;
                case IndexNames.Token:
                    Value.Token = structure?.ToString();
                    break;
                case IndexNames.Scope:
                    Value.Scope = structure?.ToString();
                    break;
                case IndexNames.CacheFolder:
                    Value.CacheFolder = structure?.ToString();
                    break;
                default:
                    throw new ArgumentException($"Unknown index '{index}', possible values: {string.Join(", ", index)}", nameof(index));
            }
        }

        public override string ToString(string format = "")
        {
            return Value.Username;
        }

        protected override GmailOAuthModel Parse(object value, string format = null)
        {
            return new GmailOAuthModel();
        }

        protected override GmailOAuthModel Parse(string value, string format = null)
        {
            return new GmailOAuthModel();
        }

        public void RequestTokenInteractive()
        {
            Value?.RequestTokenInteractive();
        }
    }
}
