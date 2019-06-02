using SIS.HTTP.Common;
using SIS.HTTP.Requests;
using System;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string request =
                "POST url/asd?name=john&id=456#fragment HTTP/1.1" + GlobalConstants.HttpNewLine +
                "Authorization: Basic 123456789" + GlobalConstants.HttpNewLine +
                "Date: " + DateTime.Now + GlobalConstants.HttpNewLine +
                "Host: lockalhost: 5000" + GlobalConstants.HttpNewLine +
                GlobalConstants.HttpNewLine +
                "username=johndoe&password=123";

            HttpRequest httpRequest = new HttpRequest(request);

            int a = 2;
        }
    }
}
