using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            var serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(
                HttpRequestMethod.GET, 
                "/", 
                request => new HomeController().Index(request));

            Server server = new Server(port: 8000, serverRoutingTable);

            server.Run();
        }
    }
}
