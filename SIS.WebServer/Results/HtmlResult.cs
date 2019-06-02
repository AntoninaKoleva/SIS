using SIS.HTTP.Enums;
using SIS.HTTP.Header;
using SIS.HTTP.Responses;
using System.Text;

namespace SIS.WebServer.Results
{
    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode statusCode) 
            : base(statusCode)
        {
            this.Headers.AddHeader(new HttpHeader(
                HttpHeader.ContentType, "text/html; charset=utf-8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
