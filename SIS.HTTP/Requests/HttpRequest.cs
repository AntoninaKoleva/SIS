using System;
using System.Collections.Generic;
using System.Linq;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Header;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3
                || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private bool IsValidQueryString(string queryString, string[] queryParameters)
        {
            throw new NotImplementedException();
        }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            var parced = HttpRequestMethod.TryParse(requestLine[0], out HttpRequestMethod method);

            if (!parced)
            {
                throw new BadRequestException(string.Format(
                    GlobalConstants.UnsupportedHttpMethodException, requestLine[0]));
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            foreach (var header in plainHeaders)
            {
                var headersKeyValuePair = header.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                this.Headers.AddHeader(new HttpHeader(headersKeyValuePair[0], headersKeyValuePair[1]));
            }

            //plainHeaders
            //    .Select(ph => ph.Split(new [] { ": "}, StringSplitOptions.RemoveEmptyEntries))
            //    .ToList()
            //    .ForEach(kvp => this.Headers.AddHeader(new HttpHeader(kvp[0], kvp[1])));
        }

        //private void ParseCookies()
        //{

        //}

        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                this.Url.Split(new char[] { '?', '#' })[1]
                .Split(new char[] { '&' })
                .Select(query => query.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(kvp => this.QueryData.Add(kvp[0], kvp[1]));
            }   
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            //Potential BUG here :)
            requestBody
                .Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(formData => formData.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(kvp => this.FormData.Add(kvp[0], kvp[1]));
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestString = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);
            string[] requestLineParams = splitRequestString[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestString).ToArray());
            //this.ParseCookies();

            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }

    }
}
