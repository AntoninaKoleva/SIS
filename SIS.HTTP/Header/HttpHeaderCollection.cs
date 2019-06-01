using SIS.HTTP.Common;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Header
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> httpHeaders;

        public HttpHeaderCollection()
        {
            this.httpHeaders = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.httpHeaders.Add(header.Key, header);
        }

        public bool CotainsHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            return this.httpHeaders.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            return this.httpHeaders[key];
        }

        public override string ToString()
        {
            return string.Join(GlobalConstants.HttpNewLine, 
                this.httpHeaders.Values.Select(h => h.ToString()))  ;
        }

    }
}
