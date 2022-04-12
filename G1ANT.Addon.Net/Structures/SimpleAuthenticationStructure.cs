/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Addon.Net.Models;
using G1ANT.Language;
using System;

namespace G1ANT.Addon.Net.Structures
{
    [Structure(Name = "simpleauthentication", Priority = 999, Default = 0, AutoCreate = false, Tooltip = "The attachment structure stores current information about a mail attachment, which was downloaded with the `mail.` commands and stored in the `attachments` field of the mail structure")]
    public class SimpleAuthenticationStructure : StructureTyped<SimpleAuthenticationModel>
    {
        public static class IndexNames
        {
            public const string Username = "username";
            public const string Password = "password";
        }

        public SimpleAuthenticationStructure(object value, string format = null, AbstractScripter scripter = null) 
            : base(value, format, scripter)
        {
            Init();
        }

        public SimpleAuthenticationStructure(SimpleAuthenticationModel value, string format = "") 
            : base(value, format)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(IndexNames.Username);
            Indexes.Add(IndexNames.Password);
        }
        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentException($"Index cannot be empty, possible values: {string.Join(", ", index)}", nameof(index));

            switch (index.ToLower())
            {
                case IndexNames.Username:
                    return new TextStructure(Value?.Username);
                case IndexNames.Password:
                    return new TextStructure(Value?.Password);
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
                case IndexNames.Password:
                    Value.Password = structure?.ToString();
                    break;
                default:
                    throw new ArgumentException($"Unknown index '{index}', possible values: {string.Join(", ", index)}", nameof(index));
            }
        }

        public override string ToString(string format = "")
        {
            return Value.Username;
        }

        protected override SimpleAuthenticationModel Parse(object value, string format = null)
        {
            return new SimpleAuthenticationModel();
        }

        protected override SimpleAuthenticationModel Parse(string value, string format = null)
        {
            return new SimpleAuthenticationModel();
        }
    }
}
