using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleHttp;

// "Application Manifest File" -> <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
// Nuget Console -> "Install-Package Simple-HTTP -Version 1.0.6"

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 1337; // any port
            Run_Server(PORT);
        }

        public static void Print_Check(string orderId)
        {
            Console.WriteLine($"Printing Check for order {orderId}");
        }

        public static void Run_Server(int port)
        {
            Route.Add("/", (req, res, props) =>
            {
                res.AsText("Hi Sherzod. Server is running.........");
            });

            Route.Add("/order/{orderId}", (req, res, props) =>
            {
                // string anyValue = req.QueryString.Get("anyKey"); // http://url?anyKey=anyValue&anyKey2=anyValue2
                string orderIdProps = props["orderId"];

                Print_Check(orderIdProps);

                res.AsText($"{{\"isPrintedOrder\": true, \"orderId\": {orderIdProps}}}");
            }, "GET");

            HttpServer.ListenAsync(port, CancellationToken.None, Route.OnHttpRequestAsync).Wait();
        }

    }
}