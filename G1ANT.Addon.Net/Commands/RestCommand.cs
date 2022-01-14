using G1ANT.Language;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/**
*    Copyright (c) G1ANT Robot Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
namespace G1ANT.Addon.Net
{
    [Command(Name = "rest", Tooltip = "This command prepares a request to a desired URL with a selected method")]
    public class RestCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "HTTP method of the `rest` request: `post` or `get`")]
            public TextStructure Method { get; set; }

            [Argument(Required = true, Tooltip = "URL of API method")]
            public TextStructure Url { get; set; }

            [Argument(DefaultVariable = "timeoutrest", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "Headers attached to the request (list or dictionary structure is expected).\n" +
                "- when list is passed each item should contains key and value separated by colon (:) eg. 'param1:value1❚param2:value2'\n" +
                "- when dictionary is passed as a value its type should be explicit declared eg. '⟦dictionary⟧param1❚value1❚param2❚value2'")]
            public Structure Headers { get; set; }

            [Argument(Tooltip = "Parameters attached to the request (list or dictionary structure is expected).\n" +
                "- when list is passed each item should contains key and value separated by colon (:) eg. 'param1:value1❚param2:value2'\n" +
                "- when dictionary is passed as a value its type should be explicit declared eg. '⟦dictionary⟧param1❚value1❚param2❚value2'")]
            public Structure Parameters { get; set; }

            [Argument(Tooltip = "Files attached to the request. Separate files from path using ❚ character (Ctrl+\\); their keys, values and content type should be separated with asterisk (*). Use key name RequestBody to send file as request body")]
            public ListStructure Files { get; set; }

            [Argument(Tooltip = "File path to be sent as request body. Shorthand for sending file as body using \"Files\" argument with key name \"RequestBody\". Content-type can be set after asterisk (*)")]
            public TextStructure BodyFile { get; set; }

            [Argument(Tooltip = "Text to be sent as request body. Shorthand for sending text as body using \"Parameters\" argument with key name \"RequestBody\", but accepts colons as a part of content. Can't set both BodyText and BodyFile")]
            public TextStructure BodyText { get; set; }

            [Argument(Tooltip = "Set to True if you want to return \"RestResult\" structure in the result which describes detailed result of rest request.")]
            public BooleanStructure ResultAsRestResponse { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "Name of a variable which will store the data returned by the API (usually json or xml)")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Name of a variable which will store the request delivery status")]
            public VariableStructure Status { get; set; } = new VariableStructure("status");

            [Argument(Tooltip = "Name of a variable which will store the response status code")]
            public VariableStructure StatusCode { get; set; } = new VariableStructure("statuscode");
        }

        private class PostFileModel
        {
            public string FormFieldName { get; }
            public string FilePath { get; }
            public string ContentType { get; }

            public PostFileModel(string data)
            {
                var separatedData = data.Split(FileKeyValueSeparator);

                if (separatedData.Length < 2)
                    throw new FormatException($"Missing separator in [{data}], use [{FileKeyValueSeparator}] instead");

                FormFieldName = separatedData[0];
                FilePath = separatedData[1];
                ContentType = separatedData.Length > 2 ? separatedData[2] : null;
            }

            public PostFileModel(string formFieldName, string filePath, string contentType)
            {
                FormFieldName = formFieldName;

                if (filePath.Contains(FileKeyValueSeparator))
                {
                    var splittedPath = filePath.Split(FileKeyValueSeparator);
                    filePath = splittedPath[0];
                    contentType = splittedPath[1];
                }

                FilePath = filePath;
                ContentType = contentType;
            }
        }


        private const char KeyValueSeparator = ':';
        private const char FileKeyValueSeparator = '*';

        public RestCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            var client = new RestClient(arguments.Url.Value)
            {
                Timeout = (int)arguments.Timeout.Value.TotalMilliseconds
            };

            var method = arguments.Method.Value;
            var currentMethod = ParseRestMethod(method);

            if (arguments.BodyFile != null && arguments.BodyText != null)
                throw new ArgumentException("Can't set BodyFile and BodyText at the same time");

            var request = new RestRequest(string.Empty, currentMethod);

            ParameterType paramType = currentMethod == Method.PUT ? ParameterType.QueryString : ParameterType.GetOrPost;

            AddRequestData(request, arguments.Headers, ParameterType.HttpHeader);
            AddRequestData(request, arguments.Parameters, paramType);
            AddRequestBody(request, arguments.BodyText);
            AddRequestFiles(request, arguments.Files, arguments.BodyFile);

            var response = client.Execute(request);

            var content = response.Content;
            if (response.ResponseStatus == ResponseStatus.TimedOut)
            {
                throw new TimeoutException("Request timed out");
            }

            if (arguments.ResultAsRestResponse.Value)
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new RestResponseStructure(response, null, Scripter));
            else
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(content));
            Scripter.Variables.SetVariableValue(arguments.Status.Value, new TextStructure(response.ResponseStatus.ToString()));
            Scripter.Variables.SetVariableValue(arguments.StatusCode.Value, new IntegerStructure((int)response.StatusCode));
        }

        private void AddRequestBody(RestRequest request, TextStructure bodyText)
        {
            if (bodyText?.Value != null)
                request.AddParameter(ParameterType.RequestBody.ToString(), bodyText.Value, ParameterType.RequestBody);
        }

        private void AddRequestFiles(RestRequest request, ListStructure files, TextStructure bodyFile)
        {
            var allFiles = new List<PostFileModel>();
            if (bodyFile?.Value != null)
                allFiles.Add(new PostFileModel(ParameterType.RequestBody.ToString(), bodyFile.Value.ToString(), null));
            if (files != null)
                allFiles.AddRange(files.Value.Select(v => new PostFileModel(v.ToString())));

            foreach (var file in allFiles)
            {
                if (file.FormFieldName == ParameterType.RequestBody.ToString())
                    AddBodyFileName(request, file);
                else
                    request.AddFile(file.FormFieldName, file.FilePath, file.ContentType);
            }
        }

        private static void AddBodyFileName(RestRequest request, PostFileModel file)
        {
            if (file.ContentType != null)
                request.AddHeader("Content-Type", file.ContentType);

            request.AddHeader("Content-Disposition", string.Format("file; filename=\"{0}\";", Path.GetFileName(file.FilePath)));
            request.AddParameter("", File.ReadAllBytes(file.FilePath), ParameterType.RequestBody);
        }

        private Method ParseRestMethod(string method)
        {
            try
            {
                return (Method)Enum.Parse(typeof(Method), method, true);
            }
            catch
            {
                throw new NotSupportedException($"Given method [{method}] is not supported in rest");
            }
        }

        private void AddRequestData(RestRequest request, Structure data, ParameterType? parameterType)
        {
            if (data == null)
                return;

            if (data is DictionaryStructure dict)
                AddRequestData(request, dict, parameterType);
            else if (data is ListStructure list)
                AddRequestData(request, list, parameterType);
            else
            {
                // handle old behaviour when all entries have been converted into ListStructure due to type of argument
                var newList = Scripter.Structures.CreateStructure(data, "", typeof(ListStructure)) as ListStructure;
                AddRequestData(request, newList, parameterType);
            }
        }

        private void AddRequestData(RestRequest request, DictionaryStructure dict, ParameterType? parameterType)
        {
            if (dict != null)
            {
                foreach (var listData in dict.Value)
                {
                    var name = listData.Key;
                    var value = listData.Value;
                    if (parameterType.HasValue)
                        request.AddParameter(name, value, parameterType.Value);
                    else
                        request.AddParameter(name, value);
                }
            }
        }

        private void AddRequestData(RestRequest request, ListStructure list, ParameterType? parameterType)
        {
            if (list != null)
            {
                foreach (var listData in list.Value)
                {
                    string data = listData.ToString();
                    var separatedData = data.Split(KeyValueSeparator);
                    if (separatedData.Length != 2)
                    {
                        throw new FormatException($"Missing separator in [{data}], use [{KeyValueSeparator}] instead");
                    }
                    var name = separatedData[0];
                    var value = separatedData[1];

                    if (parameterType.HasValue)
                        request.AddParameter(name, value, parameterType.Value);
                    else
                        request.AddParameter(name, value);
                }
            }
        }
    }
}
