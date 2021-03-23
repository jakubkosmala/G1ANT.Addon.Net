using G1ANT.Language;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Addon.Net
{
    [Structure(Name = "restresponse", Priority = 10, Default = 0, AutoCreate = false, Tooltip = "Structure contains data sent back from rest command")]
    public class RestResponseStructure : StructureTyped<IRestResponse>
    {
        private static class IndexNames
        {
            public const string ErrorMessage = "error";
            public const string ResponseStatus = "status";
            public const string Headers = "headers";
            public const string RawBytes = "bytes";
            public const string IsSuccessful = "issuccessful";
            public const string StatusCode = "code";
            public const string Content = "content";
            public const string ContentEncoding = "contentencoding";
            public const string ContentLength = "contentlength";
            public const string ContentType = "contenttype";
        }

        public RestResponseStructure(IRestResponse value, string format = "") :
            base(value, format)
        {
            Init();
        }

        public RestResponseStructure(object value, string format = "", AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(IndexNames.ErrorMessage);
            Indexes.Add(IndexNames.ResponseStatus);
            Indexes.Add(IndexNames.Headers);
            Indexes.Add(IndexNames.RawBytes);
            Indexes.Add(IndexNames.IsSuccessful);
            Indexes.Add(IndexNames.StatusCode);
            Indexes.Add(IndexNames.Content);
            Indexes.Add(IndexNames.ContentEncoding);
            Indexes.Add(IndexNames.ContentLength);
            Indexes.Add(IndexNames.ContentType);
        }

        private DictionaryStructure GetHeaders()
        {
            var headers = Value?.Headers.ToDictionary(x => x.Name.Trim().ToLower(), x => x.Value);
            return new DictionaryStructure(headers, null, Scripter);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                return this;
            }

            switch (index.ToLower())
            {
                case IndexNames.ErrorMessage:
                    return new TextStructure(Value?.ErrorMessage, null, Scripter);
                case IndexNames.ResponseStatus:
                    return new TextStructure(Value?.ResponseStatus.ToString(), null, Scripter);
                case IndexNames.Headers:
                    return GetHeaders();
                case IndexNames.RawBytes:
                    return Scripter.Structures.CreateStructure(Value?.RawBytes);
                case IndexNames.IsSuccessful:
                    return new BooleanStructure(Value?.IsSuccessful, null, Scripter);
                case IndexNames.StatusCode:
                    return new TextStructure(Value?.StatusCode.ToString(), null, Scripter);
                case IndexNames.Content:
                    return new TextStructure(Value?.Content, null, Scripter);
                case IndexNames.ContentEncoding:
                    return new TextStructure(Value?.ContentEncoding, null, Scripter);
                case IndexNames.ContentLength:
                    return new IntegerStructure(Value?.ContentLength, null, Scripter);
                case IndexNames.ContentType:
                    return new TextStructure(Value?.ContentType, null, Scripter);
            }
            throw new ArgumentException($"Unknown index '{index}'", nameof(index));
        }

        public override string ToString(string format = "")
        {
            return Value?.ResponseStatus.ToString();
        }

        protected override IRestResponse Parse(string value, string format = null)
        {
            throw new NotImplementedException();
        }
    }
}