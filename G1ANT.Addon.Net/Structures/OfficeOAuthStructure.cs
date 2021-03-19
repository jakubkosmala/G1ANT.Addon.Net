﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using G1ANT.Addon.Net.Models;

namespace G1ANT.Addon.Net.Structures
{
    [Structure(Name = "officeoauth", Priority = 999, Default = 0, AutoCreate = false, Tooltip = "The attachment structure stores current information about a mail attachment, which was downloaded with the `mail.` commands and stored in the `attachments` field of the mail structure")]
    public class OfficeOAuthStructure : StructureTyped<OfficeOAuthModel>
    {
        public static class IndexNames
        {
            public const string Username = "username";
            public const string ClientId = "clientid";
            public const string TenantId = "tenantid";
            public const string Password = "password";
        }

        public OfficeOAuthStructure(object value, string format = null, AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        public OfficeOAuthStructure(OfficeOAuthModel value, string format = "")
            : base(value, format)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(IndexNames.Username);
            Indexes.Add(IndexNames.ClientId);
            Indexes.Add(IndexNames.TenantId);
            Indexes.Add(IndexNames.Password);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentException($"Index cannot be empty, possible values: {string.Join(", ", index)}", nameof(index));

            switch (index.ToLower())
            {
                case IndexNames.Username:
                    return new TextStructure(Value.Username);
                case IndexNames.ClientId:
                    return new TextStructure(Value.ClientId);
                case IndexNames.TenantId:
                    return new TextStructure(Value.TenantId);
                case IndexNames.Password:
                    return new TextStructure(Value.Password);
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
                    Value.Username = structure.ToString();
                    break;
                case IndexNames.ClientId:
                    Value.ClientId = structure.ToString();
                    break;
                case IndexNames.TenantId:
                    Value.TenantId = structure.ToString();
                    break;
                case IndexNames.Password:
                    Value.Password = structure.ToString();
                    break;
                default:
                    throw new ArgumentException($"Unknown index '{index}', possible values: {string.Join(", ", index)}", nameof(index));
            }
        }

        public override string ToString(string format = "")
        {
            return Value.Username;
        }

        protected override OfficeOAuthModel Parse(object value, string format = null)
        {
            return new OfficeOAuthModel();
        }

        protected override OfficeOAuthModel Parse(string value, string format = null)
        {
            return new OfficeOAuthModel();
        }
    }
}
