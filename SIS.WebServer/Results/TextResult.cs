using SIS.HTTP.Enums;
using SIS.HTTP.Header;
using SIS.HTTP.Responses;
using System.Text;

namespace SIS.WebServer.Results
{
    public class TextResult : HttpResponse
    {
        public TextResult(string content, 
            HttpResponseStatusCode statusCode, 
            string contentType = "text/plain; charset=utf-8") 
            : base(statusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, 
            HttpResponseStatusCode statusCode, 
            string contentType = "text/plain; charset=utf-8")
            : base(statusCode)
        {
            this.Content = content;
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
        }
    }
}
